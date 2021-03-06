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
    // tbLOG_Departments
    public partial class Departments: BaseEntity
    {

        ///<summary>
        /// 部门ID
        ///</summary>
        public int ID { get; set; } // ID (Primary key)

        ///<summary>
        /// 父级ID
        ///</summary>
        public int ParentID { get; set; } // ParentID

        ///<summary>
        /// 部门名称
        ///</summary>
        public string Name { get; set; } // Name

        ///<summary>
        /// 是否启用
        ///</summary>
        public bool IsUse { get; set; } // IsUse

        ///<summary>
        /// 排序
        ///</summary>
        public int Priority { get; set; } // Priority
        
        public Departments()
        {
            ParentID = 0;
            Name = "";
            IsUse = false;
            Priority = 0;
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
