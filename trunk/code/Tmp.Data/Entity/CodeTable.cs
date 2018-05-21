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
    // tbCOM_CodeTable
    public partial class CodeTable: BaseEntity
    {
        public int ID { get; set; } // ID (Primary key)

        ///<summary>
        /// 表名
        ///</summary>
        public string Name { get; set; } // Name

        ///<summary>
        /// 表描述
        ///</summary>
        public string Desc { get; set; } // Desc

        ///<summary>
        /// 查询语句
        ///</summary>
        public string SelectSql { get; set; } // SelectSql

        ///<summary>
        /// 是否启用
        ///</summary>
        public bool IsUse { get; set; } // IsUse
        
        public CodeTable()
        {
            Name = "";
            Desc = "";
            SelectSql = "";
            IsUse = false;
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
