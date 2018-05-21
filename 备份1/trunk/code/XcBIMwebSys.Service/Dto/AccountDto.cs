using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XcBIMwebSys.Service.Dto
{
    public class AccountDto
    {
        //<summary>
        /// 全局唯一标识
        ///</summary>
        public Guid ID { get; set; } // ID (Primary key)

        ///<summary>
        /// 用户ID
        ///</summary>
        public string AccountID { get; set; } // AccountID

        ///<summary>
        /// 用户名称
        ///</summary>
        public string Name { get; set; } // Name

        ///<summary>
        /// 部门ID
        ///</summary>
        public short DepartmentsID { get; set; } // DepartmentsID

        ///<summary>
        /// 部门名称
        ///</summary>
        public string Departments { get; set; } // Departments

        ///<summary>
        /// 角色ID
        ///</summary>
        public int RolesID { get; set; } // RolesID

        ///<summary>
        /// 角色名称
        ///</summary>
        public string Roles { get; set; } // Roles

    }
}
