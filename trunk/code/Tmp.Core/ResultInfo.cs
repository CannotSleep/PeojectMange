﻿using System.Data;
namespace Tmp.Core
{
    public class ResultInfo
    {
        /// <summary>
        /// 错误码
        /// </summary>
        /// <remarks></remarks>

        public int ErrorNo;
        /// <summary>
        /// 错误信息
        /// </summary>
        /// <remarks></remarks>

        public string ErrorMsg;
        /// <summary>
        /// 错误信息反馈表
        /// </summary>
        /// <remarks></remarks>

        public DataTable ErrorTable;
    }
}


