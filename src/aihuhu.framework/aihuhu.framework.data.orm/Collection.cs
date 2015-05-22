using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aihuhu.framework.data.orm
{
    public class Collection<T> : System.Collections.ObjectModel.Collection<T>, ICollection<T>
        where T : ModelBase
    {
        private List<T> m_DeletedCollection = new List<T>(50);

        public new T this[int index]
        {
            get
            {
                return base[index];
            }
        }

        public new void Add(T item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            item.OnStatusChange += this.OnStatusChange;

            if (item.ModelStatus == ModelStatus.DeleteRow)
            {
                if (!m_DeletedCollection.Contains(item))
                {
                    m_DeletedCollection.Add(item);
                }
            }
            else
            {
                if (!this.Contains(item))
                {
                    base.Add(item);
                }
            }
        }

        internal IList<T> DeletedCollection
        {
            get
            {
                return this.m_DeletedCollection;
            }
        }

        /// <summary>
        /// 造一个新实体
        /// </summary>
        /// <returns></returns>
        public T New()
        {
            T model = Activator.CreateInstance<T>();
            model.BeginWrite();
            model.ChangeStatus(ModelStatus.NewRow);
            model.EndWrite();
            this.Add(model);
            return model;
        }

        private void OnStatusChange(object sender, StatusChangeEventArgs e)
        {
            if (e.Status == ModelStatus.DeleteRow)
            {
                T model = (T)sender;
                this.Remove(model);
                if (!this.m_DeletedCollection.Contains(model))
                {
                    this.m_DeletedCollection.Add(model);
                }
            }
        }
    }
}
