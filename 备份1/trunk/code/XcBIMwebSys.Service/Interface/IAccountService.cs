using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tmp.Data.Entity;
using Tmp.Service;
using XcBIMwebSys.Service.Dto;

namespace XcBIMwebSys.Service.Interface
{
    public interface IAccountService : IService<Account>
    {
        Account GetAccount();

        bool BatchAdd(DataTable dt);

        List<AccountDto> GteAccountList(AccountDto model);
    }
}
