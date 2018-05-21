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
    // MaterialInfo
    public partial class MaterialInfo: BaseEntity
    {
        public Guid tbid { get; set; } // tb_int (Primary key)
        public string tbnamebefore { get; set; } // tb_name_before
        public string tbnamenow { get; set; } // tb_name_now
        public string tbdate { get; set; } // tb_date
        public decimal? tbsize { get; set; } // tb_size
        public string tbusername { get; set; } // tb_username
        public int? tbuserid { get; set; } // tb_userid
        public int? tbtype { get; set; } // tb_type
        public string tbaddress { get; set; } // tb_address
        public int? tbprojectid { get; set; } // tb_projectid
        public string tbproject { get; set; } // tb_project
        public string tbfileType { get; set; } // tb_project
        public string tbfileExplain { get; set; } // tb_project

        public MaterialInfo()
        {
            InitializePartial();
        }

        partial void InitializePartial();

       
    }

}
// </auto-generated>
