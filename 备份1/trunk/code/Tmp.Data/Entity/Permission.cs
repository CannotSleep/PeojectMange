// <auto-generated>
// ReSharper disable RedundantUsingDirective
// ReSharper disable DoNotCallOverridableMethodsInConstructor
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable PartialMethodWithSinglePart
// ReSharper disable RedundantNameQualifier
// TargetFrameworkVersion = 4.0
#pragma warning disable 1591    //  Ignore "Missing XML Comment" warning

using System;
using Tmp.Core.Data;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Data.Entity.Infrastructure;
using System.Linq.Expressions;
using System.Data.Entity;
using System.Data;
using System.Data.Entity.Core.Objects;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Data.Entity.ModelConfiguration;
using DatabaseGeneratedOption = System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption;

namespace Tmp.Data.Entity
{
    // tbLOG_Permission
    public partial class Permission: BaseEntity
    {

        ///<summary>
        /// 全局唯一ID
        ///</summary>
        public Guid ID { get; set; } // ID (Primary key)

        ///<summary>
        /// 角色ID
        ///</summary>
        public int RolesID { get; set; } // RolesID

        ///<summary>
        /// 菜单ID
        ///</summary>
        public int MenuID { get; set; } // MenuID
        
        public Permission()
        {
            ID = System.Guid.NewGuid();
            RolesID = 0;
            MenuID = 0;
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
