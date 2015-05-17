using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aihuhu.framework.data
{
    public interface IDataCommand:IDisposable
    {
        /// <summary>
        /// 指定连接超时时间
        /// </summary>
        int CommandTimeout { get; set; }

        /// <summary>
        /// 要执行的sql语句
        /// </summary>
        string CommandText { get; set; }

        /// <summary>
        /// 
        /// </summary>
        CommandType CommandType { get; set; }

        /// <summary>
        /// 返回受影响的行数
        /// </summary>
        /// <returns></returns>
        int ExecuteNonQuery();

        /// <summary>
        /// 执行sql并返回IDataReader对象
        /// </summary>
        /// <returns></returns>
        IDataReader ExecuteReader();

        /// <summary>
        /// 返回首行首列
        /// </summary>
        /// <returns></returns>
        object ExecuteScalar();

        /// <summary>
        /// 返回Dataset集合
        /// </summary>
        /// <returns></returns>
        DataSet ExecuteDataset();


        void AddInParameter(string name, DbType dbType);
        void AddInParameter(string name, DbType dbType, int size);
        void AddInParameter(string name, DbType dbType, object value);
        void AddInParameter(string name, DbType dbType, int size, object value);
        void AddOutParameter(string name, DbType dbType);
        void AddOutParameter(string name, DbType dbType, int size);
        void AddInOutParameter(string name, DbType dbType);
        void AddInOutParameter(string name, DbType dbType, int size);
        void AddInOutParameter(string name, DbType dbType, int size, object value);
        void AddInOutParameter(string name, DbType dbType, object value);
        void AddReturnParameter(string name, DbType dbType);
        void AddReturnParameter(string name, DbType dbType, int size);

        void SetParameterValue(string name, object value);

        object GetParameterValue(string name);

    }
}
