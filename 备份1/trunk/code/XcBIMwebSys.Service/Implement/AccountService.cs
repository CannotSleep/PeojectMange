using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XcBIMwebSys.Service.Interface;
using Tmp.Data.Entity;
using Tmp.Core.Data;
using Tmp.Data;
using System.Data.SqlClient;
using System.Data;
using XcBIMwebSys.Service.Dto;

namespace XcBIMwebSys.Service.Implement
{
    public partial class AccountService : IAccountService
    {
        IRepository<Account> _accountRepository;
        IRepository<Departments> _departmentsRepository;
        IRepository<Roles> _rolesRepository;
        IRepository<AccountOfRoles> _accountOfRolesRepository;

        public IRepository<Account> UseRepository
        {
            get;
            set;
        }

        public AccountService()
            : this(RepositoryFactory.Create<Account>(), 
                  RepositoryFactory.Create<Departments>(),
                  RepositoryFactory.Create<Roles>(),
                  RepositoryFactory.Create<AccountOfRoles>())
        {
        }


        public AccountService(IRepository<Account> accountRepository, 
            IRepository<Departments> departmentsRepository,
            IRepository<Roles> rolesRepository,
        IRepository<AccountOfRoles> accountOfRolesRepository)
        {
            _accountRepository = accountRepository;
            _departmentsRepository = departmentsRepository;
            _rolesRepository = rolesRepository;
            _accountOfRolesRepository = accountOfRolesRepository;
            UseRepository = accountRepository;
        }


        public List<AccountDto> GteAccountList(AccountDto model)
        {
            var query = from a in _accountRepository.Table
                        join d in _departmentsRepository.Table on a.DepartmentsID equals d.ID
                        join d2 in _departmentsRepository.Table on d.ParentID equals d2.ID into temp1
                        from depttemp in temp1.DefaultIfEmpty()
                        join ar in _accountOfRolesRepository.Table on a.ID equals ar.AccountID
                        join r in _rolesRepository.Table on ar.RolesID equals r.ID
                        select new AccountDto
                        {
                            ID = a.ID,
                            AccountID = a.AccountID,
                            Name = a.Name,
                            Departments = (depttemp.Name == null ? "" : depttemp.Name + "-") + d.Name,
                            DepartmentsID = a.DepartmentsID,
                            Roles = r.Name,
                            RolesID = ar.RolesID
                        };
            if (model.ID != null && model.ID != Guid.Empty)
                query = query.Where(t => t.ID == model.ID);
            if (!string.IsNullOrWhiteSpace(model.AccountID))
                query = query.Where(t => t.AccountID == model.AccountID);
            if (model.DepartmentsID != 0)
                query = query.Where(t => t.DepartmentsID == model.DepartmentsID);
            if (model.RolesID != 0)
                query = query.Where(t => t.RolesID == model.RolesID);
            return query.ToList();
        }

        public Account GetAccount()
        {
            //方法一：通过主键获得
            var acconnt = _accountRepository.GetById(new Guid("2EA2D746-C57B-47F7-BA74-9E8D73D8D7C1"));
            //方法二：通过参数ID查询
            //var acconnt2 = _accountRepository.Find("ID=@ID", new SqlParameter[] {
            //    new SqlParameter("@ID",new Guid("2EA2D746-C57B-47F7-BA74-9E8D73D8D7C1"))
            //});
            //方法三：通过linq查询
            var query = from u in _accountRepository.Table
                        select u;
            //query = query.Where(t => t.AccountID == "admin");
            return query.First();
        }

        public bool BatchAdd(DataTable dt)
        {
            SqlParameter[] parameters = {
                new SqlParameter("@ErrorNo", SqlDbType.Int, 4),
                new SqlParameter("@ErrorMsg", SqlDbType.NVarChar, 500),
                new SqlParameter("@tpImportAccount", dt)
            };
            parameters[0].Direction = ParameterDirection.Output;
            parameters[1].Direction = ParameterDirection.Output;

            DbHelperSql.RunProcedure(DbHelperSql.DefaultUpdateConn, "sp_BatchImportAccount", parameters, "ds");
            if ((int)parameters[0].Value == 1)
            {
                return true;
            }
            else
                return false;
        }


    }
}
