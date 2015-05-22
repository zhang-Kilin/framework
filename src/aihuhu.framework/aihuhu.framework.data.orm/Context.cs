using aihuhu.framework.data.orm.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace aihuhu.framework.data.orm
{
    /// <summary>
    /// ORM执行上下文
    /// </summary>
    public class Context : IQuery, ISave
    {
        private IDataCommand m_Command = null;

        public Context(string database)
        {
            IDataCommand command = DataCommand.CreateCustomCommand(database);
            if (command == null)
            {
                throw new InvalidOperationException(string.Format("the database of '{0}' is not exists.", database));
            }
            this.m_Command = command;
        }

        public Collection<T> QueryAll<T>() where T : ModelBase
        {
            throw new NotImplementedException();
        }

        public Collection<T> Query<T>(Func<T, bool> fn) where T : ModelBase
        {
            throw new NotImplementedException();
        }

        public T Single<T>(Func<T, bool> fn) where T : ModelBase
        {
            throw new NotImplementedException();
        }

        public void Save<T>(Collection<T> collection) where T : ModelBase
        {
            if (collection == null)
            {
                throw new ArgumentNullException("collection");
            }
            SqlBuilder<T> builder = new SqlBuilder<T>(this.m_Command);
            Dictionary<T, IDataCommand> dic = new Dictionary<T, IDataCommand>();
            foreach (T model in collection)
            {
                dic[model] = builder.CreateSqlCommand(model);
            }
            foreach (T model in collection.DeletedCollection)
            {
                dic[model] = builder.CreateSqlCommand(model);
            }

            TransactionScopeOption option = TransactionScopeOption.Required;
            IDataCommand command = null;
            T result = null;
            using (TransactionScope scope = new TransactionScope(option))
            {
                foreach (T model in dic.Keys)
                {
                    command = dic[model];
                    if (command != null)
                    {
                        result = command.ExecuteEntity<T>();
                        if (result != null)
                        {
                            PropertyWriter.Refresh<T>(model, result);
                            model.Complete();
                        }
                    }
                }
                scope.Complete();
            }
            //清理掉已被删除的记录
            collection.DeletedCollection.Clear();
        }

    }
}
