using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XcBIMwebSys.Service.Dto
{
     public class MaterialDto
    {
        public MaterialDto()
        { }
        #region Model
        private Guid _tb_id;
        private string _tb_name_before;
        private string _tb_name_now;
        private string _tb_date;
        private decimal? _tb_size;
        private string _tb_username;
        private int? _tb_userid;
        private int? _tb_type;
        private string _tb_address;
        private int? _tb_projectid;
        private string _tb_project;
        private string _tb_fileType;
        private string _tb_fileExplain;
        /// <summary>
        /// 全局唯一标识
        /// </summary>
        public Guid tb_id
        {
            set { _tb_id = value; }
            get { return _tb_id; }
        }
        /// <summary>
        /// 文件原名称
        /// </summary>
        public string tb_name_before
        {
            set { _tb_name_before = value; }
            get { return _tb_name_before; }
        }
        /// <summary>
        /// 文件修改后名称
        /// </summary>
        public string tb_name_now
        {
            set { _tb_name_now = value; }
            get { return _tb_name_now; }
        }
        /// <summary>
        /// 上传日期
        /// </summary>
        public string tb_date
        {
            set { _tb_date = value; }
            get { return _tb_date; }
        }
        /// <summary>
        /// 文件大小
        /// </summary>
        public decimal? tb_size
        {
            set { _tb_size = value; }
            get { return _tb_size; }
        }
        /// <summary>
        /// 上传文件用户
        /// </summary>
        public string tb_username
        {
            set { _tb_username = value; }
            get { return _tb_username; }
        }
        /// <summary>
        /// 上传文件用户id
        /// </summary>
        public int? tb_userid
        {
            set { _tb_userid = value; }
            get { return _tb_userid; }
        }
        /// <summary>
        /// 文件类型
        /// </summary>
        public int? tb_type
        {
            set { _tb_type = value; }
            get { return _tb_type; }
        }
        /// <summary>
        /// 文件存放地址
        /// </summary>
        public string tb_address
        {
            set { _tb_address = value; }
            get { return _tb_address; }
        }
        /// <summary>
		/// 文件所属项目id
		/// </summary>
		public int? tb_projectid
        {
            set { _tb_projectid = value; }
            get { return _tb_projectid; }
        }
        /// <summary>
        /// 文件所属项目
        /// </summary>
        public string tb_project
        {
            set { _tb_project = value; }
            get { return _tb_project; }
        }

        /// <summary>
        /// 文件分类
        /// </summary>
        public string tb_fileType
        {
            set { _tb_fileType = value; }
            get { return _tb_fileType; }
        }

        /// <summary>
        /// 文件说明
        /// </summary>
        public string tb_fileExplain
        {
            set { _tb_fileExplain = value; }
            get { return _tb_fileExplain; }
        }
        #endregion Model
    }
}
