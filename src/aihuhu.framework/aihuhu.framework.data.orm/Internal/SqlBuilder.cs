using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace aihuhu.framework.data.orm.Internal
{
    internal class SqlBuilder<T> where T : ModelBase
    {
        private IDataCommand m_Command = null;
        private OrmMapping m_OrmMapping = null;

        internal SqlBuilder(IDataCommand command)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }
            this.m_Command = command;
            Type modelType = typeof(T);
            OrmMapping mapping = this.m_OrmMapping = PropertyCacheManager.GetOrmMapping(modelType);
            if (mapping.TableAttribute == null)
            {
                throw new InvalidOperationException(string.Format("can not found TableAttribute in type {0}.pls check it.", modelType.FullName));
            }
            if (mapping.PropertyMapping == null)
            {
                throw new InvalidOperationException(string.Format("can not found ColumnNameAttribute in type {0}.pls check it.", modelType.FullName));
            }
            KeyValuePair<PropertyInfo, ColumnNameAttribute> primaryKey = mapping.PrimaryKey;
            if (primaryKey.Key == null)
            {
                throw new InvalidOperationException(string.Format("can not found PrimaryKey in type {0}. pls check it.", modelType.FullName));
            }
            if (!primaryKey.Key.CanRead)
            {
                throw new InvalidOperationException(string.Format("the PrimaryKey can not read in type {0}. pls check it.", modelType.FullName));
            }
        }

        //internal List<IDataCommand> CreateSqlCommand<T>(Collection<T> collection)
        //    where T : ModelBase
        //{
        //    List<IDataCommand> addList = new List<IDataCommand>(collection.Count);
        //    List<IDataCommand> updateList = new List<IDataCommand>(collection.Count);
        //    List<IDataCommand> deletedList = new List<IDataCommand>(collection.DeletedCollection.Count);
        //    IDataCommand command = null;
        //    foreach (T item in collection)
        //    {
        //        if (item.ModelStatus == ModelStatus.NewRow)
        //        {
        //            command = CreateAddCommand<T>(item);
        //            addList.Add(command);
        //        }
        //        else if (item.ModelStatus == ModelStatus.ModifyRow)
        //        {
        //            command = CreateUpdateCommand<T>(item);
        //            addList.Add(command);
        //        }
        //    }
        //    foreach (T item in collection.DeletedCollection)
        //    {
        //        command = CreateDeleteCommand<T>(item);
        //        deletedList.Add(command);
        //    }

        //    addList.AddRange(updateList);
        //    addList.AddRange(deletedList);

        //    return addList;
        //}

        internal IDataCommand CreateSqlCommand(T model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("model");
            }
            switch (model.ModelStatus)
            {
                case ModelStatus.NewRow:
                    return CreateAddCommand(model);
                case ModelStatus.ModifyRow:
                    return CreateUpdateCommand(model);
                case ModelStatus.DeleteRow:
                    return CreateDeleteCommand(model);
            }
            return null;
        }

        private IDataCommand CreateAddCommand(T model)
        {
            OrmMapping mapping = this.m_OrmMapping;
            TableAttribute table = mapping.TableAttribute;
            KeyValuePair<PropertyInfo, ColumnNameAttribute> primaryKey = mapping.PrimaryKey;
            KeyValuePair<PropertyInfo, ColumnNameAttribute> item;
            StringBuilder sb = new StringBuilder();
            StringBuilder sbColumns = new StringBuilder();
            StringBuilder sbParams = new StringBuilder();

            IDataCommand command = this.m_Command.Clone();

            sb.AppendFormat("INSERT INTO [{0}].[{1}](", table.Schema, table.TableName);
            sb.AppendLine();
            int columnIndex = 0;
            string paramName;
            for (int i = 0; i < mapping.PropertyMapping.Length; i++)
            {
                item = mapping.PropertyMapping[i];
                if (!item.Value.IsIdentity && item.Key.CanRead)
                {
                    if (columnIndex > 0)
                    {
                        sbColumns.Append(",");
                        sbParams.Append(",");
                    }
                    sbColumns.AppendLine(item.Value.ColumnName);
                    paramName = DataCommandManager.FormatParameterName(item.Value.ColumnName);
                    sbParams.AppendLine(paramName);
                    command.AddInParameter(paramName, item.Key.GetValue(model, null));
                    columnIndex++;
                }
            }
            sb.AppendLine(sbColumns.ToString());
            sb.AppendLine(") VALUES (");
            sb.AppendLine(sbParams.ToString());
            sb.AppendLine(")");
            sb.AppendLine();

            for (int i = 0; i < mapping.PropertyMapping.Length; i++)
            {
                item = mapping.PropertyMapping[i];
                if (item.Value.IsIdentity)
                {
                    sbColumns.AppendFormat(",{0}", item.Value.ColumnName);
                    sbColumns.AppendLine();
                }
            }

            sb.AppendLine("SELECT TOP(1)");
            sb.AppendLine(sbColumns.ToString());
            sb.AppendFormat("FROM [{0}].[{1}] WITH(NOLOCK)", table.Schema, table.TableName);
            sb.AppendLine();
            sb.AppendFormat("WHERE {0} = {1}", primaryKey.Value.ColumnName, primaryKey.Value.IsIdentity ? "SCOPE_IDENTITY()" : DataCommandManager.FormatParameterName(primaryKey.Value.ColumnName));
            
            command.CommandText = sb.ToString();
            
            return command;
        }

        private IDataCommand CreateUpdateCommand(T model)
        {
            OrmMapping mapping = this.m_OrmMapping;
            TableAttribute table = mapping.TableAttribute;
            KeyValuePair<PropertyInfo, ColumnNameAttribute> primaryKey = mapping.PrimaryKey;
            KeyValuePair<PropertyInfo, ColumnNameAttribute> item;
            StringBuilder sb = new StringBuilder();
            StringBuilder sbColumns = new StringBuilder();
            //StringBuilder sbParams = new StringBuilder();

            IDataCommand command = this.m_Command.Clone();
            sb.AppendFormat("UPDATE TOP(1) [{0}].[{1}] SET", table.Schema, table.TableName);
            sb.AppendLine();
            int columnIndex = 0;
            string paramName;
            for (int i = 0; i < mapping.PropertyMapping.Length; i++)
            {
                item = mapping.PropertyMapping[i];
                if (!item.Value.IsIdentity && item.Key.CanRead && !item.Value.IsPrimaryKey)
                {
                    if (columnIndex > 0)
                    {
                        sbColumns.Append(",");
                    }
                    paramName = DataCommandManager.FormatParameterName(item.Value.ColumnName);
                    sbColumns.AppendFormat("{0}={1}", item.Value.ColumnName, paramName);
                    sbColumns.AppendLine();
                    command.AddInParameter(paramName, item.Key.GetValue(model, null));
                    columnIndex++;
                }
            }
            sb.AppendLine(sbColumns.ToString());
            sb.AppendFormat("WHERE {0} = {1}", primaryKey.Value.ColumnName, DataCommandManager.FormatParameterName(primaryKey.Value.ColumnName));
            sb.AppendLine();
            sb.AppendLine("SELECT TOP(1)");
            sb.AppendLine(sbColumns.ToString());
            sb.AppendFormat("FROM [{0}].[{1}] WITH(NOLOCK)", table.Schema, table.TableName);
            sb.AppendLine();
            sb.AppendFormat("WHERE {0} = {1}", primaryKey.Value.ColumnName, DataCommandManager.FormatParameterName(primaryKey.Value.ColumnName));
            command.AddInParameter(primaryKey.Value.ColumnName, primaryKey.Key.GetValue(model, null));

            command.CommandText = sb.ToString();
            
            return command;
        }

        private IDataCommand CreateDeleteCommand(T model)
        {
            OrmMapping mapping = this.m_OrmMapping;
            KeyValuePair<PropertyInfo, ColumnNameAttribute> primaryKey = mapping.PrimaryKey;
            TableAttribute table = mapping.TableAttribute;
            StringBuilder sb = new StringBuilder();
            IDataCommand command = this.m_Command.Clone();
            sb.AppendFormat("DELETE TOP(1) FROM [{0}].[{1}] ", table.Schema, table.TableName);
            sb.AppendLine();
            sb.AppendFormat("WHERE {0} = {1}", primaryKey.Value.ColumnName, DataCommandManager.FormatParameterName(primaryKey.Value.ColumnName));
            command.AddInParameter(primaryKey.Value.ColumnName, primaryKey.Key.GetValue(model, null));

            command.CommandText = sb.ToString();
            
            return command;
        }
    }
}
