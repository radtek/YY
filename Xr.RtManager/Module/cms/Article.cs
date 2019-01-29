using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xr.Common.Validation;

namespace Xr.RtManager.Module.cms
{
    /// <summary>
    /// 编辑文章保存类
    /// </summary>
    public class Article
    {

        [Required]
        public String id { get; set; }
        [Required]
        public String hospitalId { get { return AppContext.Session.hospitalId; } }
        [Required]
        public String deptId { get; set; }
        [Required]
        public String doctorId { get; set; }
        [Required]
        public String type { get; set; }
        [Required]
        public String categoryId { get; set; }
        [Required]
        public String title { get; set; }
        [Required]
        public String content { get; set; }
        [Required]
        public String isUse { get; set; }
        [IgnoreParam]
        public String createById { get; set; }
        [IgnoreParam]
        public String createByName { get; set; }
        [IgnoreParam]
        public String createDate { get; set; }
        [IgnoreParam]
        public String readNum { get; set; }
        [IgnoreParam]
        public String updateById { get; set; }
        [IgnoreParam]
        public String updateByName { get; set; }
        [IgnoreParam]
        public String updateDate { get; set; }
    }
}
