using Tmp.Core.Dependency;
using Tmp.Core.Data;

namespace Tmp.Data
{
    /// <summary>
    /// Repository
    /// </summary>
    public  class RepositoryFactory
    {
        static public IRepository<T> Create<T>() where T : BaseEntity
        {
            return IocObjectManager.GetInstance().Resolve<IRepository<T>>();
        }
    }
}
