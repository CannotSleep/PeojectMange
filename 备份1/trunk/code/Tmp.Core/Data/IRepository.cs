﻿using System;
using System.Collections.Generic;
using System.Linq;
using Tmp.Core.Dependency;

namespace Tmp.Core.Data
{

    public delegate void SwitchDbExcuteFun();
    public enum EDbType
    {
        QueryDb = 0,
        UpdateDb = 1
    };
    
    /// <summary>
    /// Repository
    /// </summary>
    public partial interface IRepository<T> : IDependency where T : BaseEntity
    {
        /// <summary>
        /// Get entity by identifier
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <returns>Entity</returns>
        T GetById(object id);

        /// <summary>
        /// Insert entity
        /// </summary>
        /// <param name="entity">Entity</param>
        void Insert(T entity);

        /// <summary>
        /// Insert entities
        /// </summary>
        /// <param name="entities">Entities</param>
        void Insert(IEnumerable<T> entities);

        /// <summary>
        /// Update entity
        /// </summary>
        /// <param name="entity">Entity</param>
        void Update(T entity);

        /// <summary>
        /// Update entities
        /// </summary>
        /// <param name="entities">Entities</param>
        void Update(IEnumerable<T> entities);

        /// <summary>
        /// Delete entity
        /// </summary>
        /// <param name="entity">Entity</param>
        void Delete(T entity);

        /// <summary>
        /// Delete entities
        /// </summary>
        /// <param name="entities">Entities</param>
        void Delete(IEnumerable<T> entities);

        /// <summary>
        /// Gets a table
        /// </summary>
        IQueryable<T> Table { get; }

        /// <summary>
        /// Gets a table with "no tracking" enabled (EF feature) Use it only when you load record(s) only for read-only operations
        /// </summary>
        IQueryable<T> TableNoTracking { get; }


        T Find(params object[] keyValues);

        /// <summary>
        /// 采用原生sql获取对象，sql select的字段必须与T1属性对应
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        List<T1> SqlQuery<T1>(string sql, params object[] parameters);


        string GetTableName();


        bool SwitchDb(SwitchDbExcuteFun fun, EDbType e);
    }
}
