using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xr.RtCall.Model
{
    /// <summary>
    /// 接口地址
    /// </summary>
   public static class InterfaceAddress
    {
       /// <summary>
       /// 医院列表接口地址
       /// </summary>
        public const String HostalInfo = "itf/intro/hospital/findAll";//itf/intro/hospital/findAll
       /// <summary>
        /// 医院接口地址
       /// </summary>
       public const String hostal = "itf/intro/hospitalByCode";
       /// <summary>
       /// 科室接口地址
       /// </summary>
       public const String dept = "itf/intro/deptByhospitalCode";
       /// <summary>
       /// 医生接口地址
       /// </summary>
       public const String doctor = "itf/triage/findDoctorIdByClinicId";
       /// <summary>
       /// 诊室接口地址
       /// </summary>
       public const String clinc = "itf/triage/findClinicIdByCode";
       /// <summary>
       /// 诊室列表接口
       /// </summary>
       public const String ClincInfo = "itf/intro/findCliniList";
       /// <summary>
       /// 下一位接口地址
       /// </summary>
       public const String callNextPerson = "itf/call/callNextPerson";
       /// <summary>
       /// 呼号到诊接口地址
       /// </summary>
       public const String inPlace = "itf/call/inPlace";
       /// <summary>
       /// 过号重排接口地址
       /// </summary>
       public const String passNum = "itf/triage/passNum";
       /// <summary>
       /// 临时停诊/继续开诊接口地址
       /// </summary>
       public const String openStop = "itf/call/openStop";
       /// <summary>
       /// 完成就诊接口地址
       /// </summary>
       public const String completeTriage = "itf/call/completeTriage";
       /// <summary>
       /// 患者列表接口地址
       /// </summary>
       public const String findPatientListByDoctor = "itf/triage/findTriageListByDoctor";
       /// <summary>
       /// 获取卡类型接口地址
       /// </summary>
       public const String card_type = "sys/sysDict/findByType";
       /// <summary>
       /// 医生排班日期接口地址
       /// </summary>
       public const String findByDeptAndDoctor = "itf/booking/findByDeptAndDoctor";
       /// <summary>
       /// 日期排班号源接口地址
       /// </summary>
       public const String findTimeNum = "itf/booking/findTimeNum";
       /// <summary>
       /// 科室下面的医生接口地址
       /// </summary>
       public const String DoctorName = "itf/intro/doctorByDeptId";
       /// <summary>
       /// 确认预约接口地址
       /// </summary>
       public const String confirmBooking = "itf/booking/confirmBooking";
       /// <summary>
       /// 确认是否启动
       /// </summary>
       public const String IsStop = "itf/call/transferRoom";
    }
}
