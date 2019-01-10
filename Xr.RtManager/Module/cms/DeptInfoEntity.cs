using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xr.Common.Validation;

namespace Xr.RtManager
{
    /// <summary>
    /// 科室信息实体类
    /// </summary>
    public class DeptInfoEntity
    {
        /// <summary>
        /// ID
        /// </summary>
        public String id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public String name { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        public String code { get; set; }

        /// <summary>
        /// 上级科室
        /// </summary>
        public HospitalInfoEntity parent { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public String showSort { get; set; }

        /// <summary>
        /// 打印顺序
        /// </summary>
        public String printSort { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public String isUse { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public String createDate { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public String updateDate { get; set; }

        ///// <summary>
        ///// logoUrl
        ///// </summary>
        //[Required]
        //public String logoUrl { get; set; }

        ///// <summary>
        ///// 宣传图Url
        ///// </summary>
        //[Required]
        //public String pictureUrl { get; set; }

        ///// <summary>
        ///// 地址
        ///// </summary>
        //[Required]
        //public String address { get; set; }

        ///// <summary>
        ///// 简介
        ///// </summary>
        //[Required]
        //public String information { get; set; }
    }
}
