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
        /// 医院
       /// </summary>
       public static String hostal = "api/itf/intro/hospitalByCode";
       /// <summary>
       /// 科室
       /// </summary>
       public static String dept = "api/itf/intro/deptByhospitalCode";
       /// <summary>
       /// 医生
       /// </summary>
       public static String doctor = "api/itf/triage/findDoctorIdByClinicId";
       /// <summary>
       /// 诊室
       /// </summary>
       public static String clinc = "api/itf/triage/findClinicIdByCode";
       /// <summary>
       /// 下一位
       /// </summary>
       public static String callNextPerson = "api/itf/call/callNextPerson";
       /// <summary>
       /// 呼号到诊
       /// </summary>
       public static String inPlace = "api/itf/call/inPlace";
       /// <summary>
       /// 过号重排
       /// </summary>
       public static String passNum = "api/itf/triage/passNum";
       /// <summary>
       /// 临时停诊/继续开诊
       /// </summary>
       public static String openStop = "api/itf/call/openStop";
       /// <summary>
       /// 完成就诊
       /// </summary>
       public static String completeTriage = "api/itf/call/completeTriage";
       /// <summary>
       /// 患者列表
       /// </summary>
       public static String findPatientListByDoctor = "api/itf/triage/findTriageListByDoctor";
       /// <summary>
       /// 获取卡类型
       /// </summary>
       public static String card_type = "api/sys/sysDict/findByType";
       /// <summary>
       /// 医生排班日期
       /// </summary>
       public static String findByDeptAndDoctor = "api/itf/booking/findByDeptAndDoctor";
       /// <summary>
       /// 日期排班号源
       /// </summary>
       public static String findTimeNum = "api/itf/booking/findTimeNum";
       /// <summary>
       /// 科室下面的医生
       /// </summary>
       public static String DoctorName = "api/itf/intro/doctorByDeptId";
       /// <summary>
       /// 确认预约
       /// </summary>
       public static String confirmBooking = "api/itf/booking/confirmBooking";
    }
}
