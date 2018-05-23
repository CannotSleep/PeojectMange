using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XcBIMwebSys.Service.Dto
{
     public class FileMangeDto
    {
        public FileMangeDto()
        { }
        #region Model

        public Guid FileID { get; set; } // FileID (Primary key)
        public string NameBefore { get; set; } // NameBefore
        public string NameNow { get; set; } // NameNow
        public string Date { get; set; } // Date
        public decimal? Size { get; set; } // Size
        public Guid? UserId { get; set; } // UserId
        public string FileType { get; set; } // FileType
        public string FilePath { get; set; } // FilePath
        public Guid? ProjectId { get; set; } // ProjectId
        public string FileCategory { get; set; } // FileCategory
        public string FileExplain { get; set; } // FileExplain
        public string UserName { get; set; } // UserName
        public string ProjectName { get; set; } // ProjectName

        #endregion Model
    }
}
