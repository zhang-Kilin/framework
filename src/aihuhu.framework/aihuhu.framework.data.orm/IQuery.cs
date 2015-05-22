using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aihuhu.framework.data.orm
{
    public interface IQuery
    {
        /// <summary>
        /// 查询所有的数据集
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Collection<T> QueryAll<T>() where T : ModelBase;

        /// <summary>
        /// 根据条件查询数据集
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fn"></param>
        /// <returns></returns>
        Collection<T> Query<T>(Func<T, bool> fn) where T : ModelBase;

        /// <summary>
        /// 根据条件查询一条数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fn"></param>
        /// <returns></returns>
        T Single<T>(Func<T, bool> fn) where T : ModelBase;
    }
}
