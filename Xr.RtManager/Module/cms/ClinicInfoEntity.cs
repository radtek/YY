﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xr.Common.Validation;

namespace Xr.RtManager
{
    /// <summary>
    /// 诊室信息
    /// </summary>
    public class ClinicInfoEntity
    {
        public String id { get; set; }
        [Required]
        [ObjectPoint("hospital.id")]
        public String hospitalId
        {
            get
            {
                return AppContext.Session.hospitalId;
            }
        }
        [Required]
        [ObjectPoint("dept.id")]
        public String deptId
        {
            get;
            set;
            //get
            //{
            //    return AppContext.Session.deptId;
            //}
        }
        [IgnoreParam]
        public String deptname { get; set; }
        public String name { get; set; }
        public String code { get; set; }
        public String prefix { get; set; }
        public String isUse { get; set; }
        [IgnoreParam]
        public String createDate { get; set; }
        [IgnoreParam]
        public String updateDate { get; set; }
    }
    public class Dept
    {
        public String deptname { get; set; }
        public String id { get; set; }
        public String code { get; set; }
        public String isUse { get; set; }
        public String prefix { get; set; }
        public String name { get; set; }

    }
}
