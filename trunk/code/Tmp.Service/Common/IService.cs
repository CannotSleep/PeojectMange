using Tmp.Core.Data;
using Tmp.Core.Dependency;

namespace Tmp.Service
{
   
    
    
    public interface IService<T> : IDependency where T : BaseEntity
    {

        IRepository<T> UseRepository
        {
            get;
        }



    }
}