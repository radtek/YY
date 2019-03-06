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
        /// 医院
        /// </summary>
        [IgnoreParam]
        public HospitalInfoEntity hospital { get; set; }

        /// <summary>
        /// 医院id
        /// </summary>
        [Required]
        [ObjectPoint("hospital.id")]
        public String hospitalId { get; set; }

        /// <summary>
        /// 上级科室
        /// </summary>
        [IgnoreParam]
        public DeptInfoEntity parent { get; set; }

        /// <summary>
        /// 上级科室id
        /// </summary>
        [Required]
        [ObjectPoint("parent.id")]
        public String parentId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Required]
        public String name { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        [Required]
        public String code { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        [Required]
        public String telPhoneNo { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        [Required]
        public String showSort { get; set; }

        /// <summary>
        /// 打印顺序
        /// </summary>
        [Required]
        public String printSort { get; set; }

        /// <summary>
        /// 关键字
        /// </summary>
        [Required]
        public String kayword { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        [Required]
        public String address { get; set; }

        /// <summary>
        /// 简介
        /// </summary>
        [Required]
        public String synopsis { get; set; }

        /// <summary>
        /// logoUrl
        /// </summary>
        [Required]
        public String logoUrl { get; set; }

        /// <summary>
        /// 宣传图Url
        /// </summary>
        [Required]
        public String pictureUrl { get; set; }

        /// <summary>
        /// 是否显示编码
        /// </summary>
        [Required]
        public String isShow { get; set; }

        /// <summary>
        /// 是否启用编码
        /// </summary>
        [Required]
        public String isUse { get; set; }

        /// <summary>
        /// 是否启用名称
        /// </summary>
        [IgnoreParam]
        public String isUseLabel { get; set; }

        /// <summary>
        /// 候诊说明
        /// </summary>
        public String waitingDesc { get; set; }
        
        /// <summary>
        /// 创建时间
        /// </summary>
        [IgnoreParam]
        public String createDate { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        [IgnoreParam]
        public String updateDate { get; set; }

        /// <summary>
        /// 上级(父级)科室id(提供给科室列表使用)
        /// </summary>
        [IgnoreParam]
        public String superiorId { 
            get 
            { 
                if(parent!=null){
                    return parent.id; 
                }
                return null;
            } 
        }
    }
}
