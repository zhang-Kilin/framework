using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace aihuhu.framework.data.orm
{
    [Serializable]
    public class ModelBase
    {
        public ModelBase()
        {
            this.m_ModelStatus = orm.ModelStatus.Normal;
        }

        private readonly Hashtable m_PropertyCache = new Hashtable();

        /// <summary>
        /// 记录当前model的数据状态
        /// </summary>
        private ModelStatus m_ModelStatus;
        internal ModelStatus ModelStatus
        {
            get
            {
                return this.m_ModelStatus;
            }
        }
        /// <summary>
        /// 标记当前是否处于系统写数据状态，开启该状态，对属性的修改，将不会导致ModelStatus的更改
        /// </summary>
        private bool m_Writting = false;

        protected void Set(string propertyName, object value)
        {
            if (string.IsNullOrWhiteSpace(propertyName))
            {
                throw new ArgumentNullException("propertyName");
            }

            if (this.m_Writting)
            {
                m_PropertyCache[propertyName] = value;
                return;
            }

            Type currentType = this.GetType();
            object oldValue = null;
            PropertyInfo property = currentType.GetProperty(propertyName);
            if (property == null)
            {
                throw new InvalidOperationException(string.Format("the property name '{0}' is not exists in {1}.", propertyName, currentType.FullName));
            }
            if (m_PropertyCache.ContainsKey(propertyName))
            {
                oldValue = m_PropertyCache[propertyName];
            }
            //如果本次修改的值与原始值相同，则直接忽略本次修改
            if (object.Equals(value, oldValue))
            {
                return;
            }
            //记录本次修改的value值
            m_PropertyCache[propertyName] = value;

            this.ChangeStatus(ModelStatus.ModifyRow);

            if (OnPropertyChange != null)
            {
                OnPropertyChange.Invoke(this, new PropertyChangeEventArgs
                {
                    PropertyName = propertyName,
                    Value = value
                });
            }
        }

        /// <summary>
        /// 开启写数据状态
        /// </summary>
        internal void BeginWrite()
        {
            this.m_Writting = true;
        }
        /// <summary>
        /// 结束写数据状态
        /// </summary>
        internal void EndWrite()
        {
            this.m_Writting = false;
        }

        internal bool ChangeStatus(ModelStatus status)
        {
            if (this.m_ModelStatus == status)
            {
                return true;
            }
            //已经删除的行，不能再修改状态 
            if (this.m_ModelStatus == ModelStatus.DeleteRow)
            {
                return false;
            }
            //新增行，不允许改为modify或normal
            if (this.m_ModelStatus == ModelStatus.NewRow
                && (status == ModelStatus.ModifyRow || status == ModelStatus.Normal))
            {
                return false;
            }
            //修改行，不允许修改为new 或 normal
            if (this.m_ModelStatus == ModelStatus.ModifyRow
                && (status == ModelStatus.NewRow || status == ModelStatus.Normal))
            {
                return false;
            }

            this.m_ModelStatus = status;
            //如果当前处于系统写数据中，则允许直接修改状态
            //并且不会触发状态更改事件
            if (this.m_Writting)
            {
                return true;
            }
            //触发状态更改事件
            if (this.OnStatusChange != null)
            {
                this.OnStatusChange.Invoke(this, new StatusChangeEventArgs { Status = this.m_ModelStatus });
            }
            return true;
        }

        internal void Complete()
        {
            this.m_ModelStatus = orm.ModelStatus.Normal;
        }

        internal event PropertyChange OnPropertyChange;

        internal event StatusChange OnStatusChange;
    }

    /// <summary>
    /// 模型的状态
    /// </summary>
    internal enum ModelStatus
    {
        /// <summary>
        /// 正常行
        /// </summary>
        Normal,

        /// <summary>
        /// 新数据行
        /// </summary>
        NewRow,

        /// <summary>
        /// 被修改过的数据行
        /// </summary>
        ModifyRow,

        /// <summary>
        /// 被删除的行
        /// </summary>
        DeleteRow
    }

    internal class PropertyChangeEventArgs : EventArgs
    {
        /// <summary>
        /// 属性名称
        /// </summary>
        public string PropertyName { get; set; }
        /// <summary>
        /// 变化值
        /// </summary>
        public object Value { get; set; }
    }

    internal class StatusChangeEventArgs : EventArgs
    {
        public ModelStatus Status { get; set; }
    }

    /// <summary>
    /// 属性发生变化
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    internal delegate void PropertyChange(object sender, PropertyChangeEventArgs e);

    internal delegate void StatusChange(object sender, StatusChangeEventArgs e);
}
