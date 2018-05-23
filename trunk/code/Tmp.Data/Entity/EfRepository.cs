using Tmp.Core.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;

namespace Tmp.Data.Entity
{
    /// <summary>
    /// Entity Framework repository
    /// </summary>
    public partial class EfRepository<T> : IRepository<T> where T : BaseEntity
    {
        #region Fields

        private readonly DbContext _context;
        private IDbSet<T> _entities;

        #endregion

        #region Properties

       


        public  IDbSet<T> Entities
        {
            get
            {
                if (_entities == null)
                    _entities = _context.Set<T>();
                return _entities;
            }
        }

        public virtual IQueryable<T> Table
        {
            get
            {
                return this.Entities;
            }
        }

        /// <summary>
        /// Gets a table with "no tracking" enabled (EF feature) Use it only when you load record(s) only for read-only operations
        /// </summary>
        public virtual IQueryable<T> TableNoTracking
        {
            get
            {
                return this.Entities.AsNoTracking();
            }
        }

        #endregion


        #region Ctor
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="context">Object context</param>
        //public EfRepository()
        //{
        //    //todo完善
        //    //this._context = new CouDbContext();
        //    this._context = PerHttpObjectFactory.Create<DbContext>();
        //}

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="context">Object context</param>
        public EfRepository(DbContext context)
        {
            this._context = context;
            _context.Configuration.AutoDetectChangesEnabled = false;
        }
        #endregion

        #region PrivateMethods

        private void AttachIfNoAttached(T entity, EntityState e)
        {
            try
            {
                this.Entities.Attach(entity);
                this._context.Entry(entity).State = e;
            }
            catch (Exception ex)
            {
                var objContext = ((IObjectContextAdapter)_context).ObjectContext;
                var objSet = objContext.CreateObjectSet<T>();
                var entityKey = objContext.CreateEntityKey(objSet.EntitySet.Name, entity);

                Object foundEntity;
                var exists = objContext.TryGetObjectByKey(entityKey, out foundEntity);

                if (exists)
                {
                    objContext.Detach(foundEntity);
                }

                this.Entities.Attach(entity);
                this._context.Entry(entity).State = e;
            }
            

            return;
        }



        public bool SwitchDb(SwitchDbExcuteFun fun, EDbType e)
        {
            try
            {
                this._context.Database.Connection.ConnectionString =
                    e == EDbType.UpdateDb ? DbHelperSql.DefaultUpdateConn : DbHelperSql.DefaultQueryConn;
                fun();
            }
            catch (Exception dbEx)
            {
                var fail = new Exception("SwitchDb:" + dbEx.Message, dbEx);
                throw fail;
            }
            finally
            {
                this._context.Database.Connection.ConnectionString =
                    e == EDbType.UpdateDb ? DbHelperSql.DefaultQueryConn : DbHelperSql.DefaultUpdateConn;
            }

            return true;
        }


        private bool SaveChanges()
        {
            try
            {
                this._context.Database.Connection.ConnectionString = DbHelperSql.DefaultUpdateConn;
                this._context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = string.Empty;

                foreach (var validationErrors in dbEx.EntityValidationErrors)
                    foreach (var validationError in validationErrors.ValidationErrors)
                        msg += string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage) + Environment.NewLine;

                var fail = new Exception(msg, dbEx);
                throw fail;
            }
            finally
            {
                this._context.Database.Connection.ConnectionString = DbHelperSql.DefaultQueryConn;
            }

            return true;
        }

        #endregion


        #region PublicMethods

        /// <summary>
        /// Get entity by identifier
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <returns>Entity</returns>
        public virtual T GetById(object id)
        {
            //see some suggested performance optimization (not tested)
            //http://stackoverflow.com/questions/11686225/dbset-find-method-ridiculously-slow-compared-to-singleordefault-on-id/11688189#comment34876113_11688189
            return this.Entities.Find(id);
        }


        /// <summary>
        /// Find根据主键进行查找
        /// </summary>
        /// <param name="keyValues"></param>
        /// <returns></returns>
        public T Find(params object[] keyValues)
        {
            return this.Entities.Find(keyValues);
        }

        /// <summary>
        /// Insert entity
        /// </summary>
        /// <param name="entity">Entity</param>
        public virtual void Insert(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");
            this.Entities.Add(entity);
            this.SaveChanges();
        }

        /// <summary>
        /// Insert entities
        /// </summary>
        /// <param name="entities">Entities</param>
        public virtual void Insert(IEnumerable<T> entities)
        {
            if (entities == null)
                throw new ArgumentNullException("entities");

            foreach (var entity in entities)
                this.Entities.Add(entity);

            this.SaveChanges();
        }

        /// <summary>
        /// Update entity
        /// </summary>
        /// <param name="entity">Entity</param>
        public virtual void Update(T entity)
        {
            if (entity == null)
                    throw new ArgumentNullException("entity");
            AttachIfNoAttached(entity, EntityState.Modified);
            this.SaveChanges();
        }

        /// <summary>
        /// Update entities
        /// </summary>
        /// <param name="entities">Entities</param>
        public virtual void Update(IEnumerable<T> entities)
        {
            if (entities == null)
                    throw new ArgumentNullException("entities");
            
            foreach (var entity in entities)
            {
                AttachIfNoAttached(entity, EntityState.Modified);
            }

            this.SaveChanges();
        }

        /// <summary>
        /// Delete entity
        /// </summary>
        /// <param name="entity">Entity</param>
        public virtual void Delete(T entity)
        {
            AttachIfNoAttached(entity, EntityState.Deleted);
            this.Entities.Remove(entity);
            this.SaveChanges();
        }

        /// <summary>
        /// Delete entities
        /// </summary>
        /// <param name="entities">Entities</param>
        public virtual void Delete(IEnumerable<T> entities)
        {
            if (entities == null)
                throw new ArgumentNullException("entities");
            
            foreach (var entity in entities)
            {
                this.AttachIfNoAttached(entity, EntityState.Deleted);
            }

            foreach (var entity in entities)
                this.Entities.Remove(entity);
             
            this.SaveChanges();
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="">Entities</param>
        public List<T> GetFileList(Expression<Func<T, bool>> where, Expression<Func<T, dynamic>> order,int pageIndex,int pageSize)
        {
            var list = _context.Set<T>().Where(where).OrderByDescending(order).Skip((pageIndex-1)*pageSize).Take(pageSize).ToList();
            
            return list;
        }

        /// <summary>
        /// 表数据总数查询
        /// </summary>
        /// <param name="">Entities</param>
        public int GetDataTotal(Expression<Func<T, bool>> where)
        {
            var total = _context.Set<T>().Where(where).Count();

            return total;
        }
        #endregion


        #region ExtendMethods

        public string GetTableName()
        {
            var entitySet = GetEntitySet();
            if (entitySet == null)
                throw new Exception("Unable to find entity set '{0}' in edm metadata");
            var tableName = GetStringProperty(entitySet, "Schema") + "." + GetStringProperty(entitySet, "Table");
            return tableName;
        }

        private EntitySet GetEntitySet()
        {
            var type = typeof(T);
            var entityName = type.Name;
            var metadata = ((IObjectContextAdapter)_context).ObjectContext.MetadataWorkspace;
 
            IEnumerable<EntitySet> entitySets;
            entitySets = metadata.GetItemCollection(DataSpace.SSpace)
                             .GetItems<EntityContainer>()
                             .Single()
                             .BaseEntitySets
                             .OfType<EntitySet>()
                             .Where(s => !s.MetadataProperties.Contains("Type")
                                         || s.MetadataProperties["Type"].ToString() == "Tables");
            var entitySet = entitySets.FirstOrDefault(t => t.Name == entityName);
            return entitySet;
        }

        private string GetStringProperty(MetadataItem entitySet, string propertyName)
        {
            MetadataProperty property;
            if (entitySet == null)
                throw new ArgumentNullException("entitySet");
            if (entitySet.MetadataProperties.TryGetValue(propertyName, false, out property))
            {
                string str = null;
                if (((property != null) &&
                    (property.Value != null)) &&
                    (((str = property.Value as string) != null) &&
                    !string.IsNullOrEmpty(str)))
                {
                    return str;
                }
            }
            return string.Empty;
        }

        public DbContext GetDbContext()
        {
            return _context;
        }

        public List<T1> SqlQuery<T1>(string sql, params object[] parameters)
        {
            return _context.Database.SqlQuery<T1>(sql, parameters).ToList();
        }

        #endregion


    }
}