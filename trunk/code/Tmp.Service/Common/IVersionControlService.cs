using Tmp.Data.Entity;

namespace Tmp.Service
{
   public  interface IVersionControlService:IService<VersionControl>
    {
       bool GetVersionFlag(string key);
    }
}
