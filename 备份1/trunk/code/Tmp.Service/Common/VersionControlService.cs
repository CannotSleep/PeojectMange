using Tmp.Core.Caching;
using Tmp.Core.Data;
using Tmp.Core.Dependency;
using Tmp.Data;
using Tmp.Data.Entity;
using Tmp.Service;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Tmp.Service
{
    public partial class VersionControlService : IVersionControlService
    {
        private IRepository<VersionControl> _versionRepository;

        public VersionControlService(IRepository<VersionControl> versionRepository)
        {
            _versionRepository = versionRepository;

        }

        public VersionControlService(): this(RepositoryFactory.Create<VersionControl>())
        {
            // TODO: Complete member initialization
        }

      

        public IRepository<VersionControl> UseRepository
        {
            get
            {
                return _versionRepository;
            }
        }

        public static List<VersionControl> versionList;

        public  bool GetVersionFlag(string key)
        {
            ICacheManager cacheManager = IocObjectManager.GetInstance().Resolve<IHttpContextCacheManager>();
            var result = cacheManager.Get("VersionList", 1, () =>
            {
                return UseRepository.Table.ToList();
            });
            if (versionList == null)
            {
                versionList = result;
                return false;
            }

            var cacheItem = result.Find(t => t.Key == key);
            var item = versionList.Find(t => t.Key == key);
            if (cacheItem != null)
            {
                if (cacheItem.Version == -1)
                {
                    return true;
                }
                if (item == null || item.Version != cacheItem.Version)
                {
                    versionList = result;
                    return true;
                }
            }
            else
            {
                //UseRepository.Insert(new VersionControl()
                //{
                //    Key = key,
                //    Version = 1
                //});
                throw new ArgumentNullException(key + "未插入版本控制表(Version_Control)中");
            }
          
            return false;
        }

      
    }
}
