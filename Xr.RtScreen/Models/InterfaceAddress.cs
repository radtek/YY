using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xr.RtScreen.Models
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
       /// 诊室
       /// </summary>
       public static String clin = "api/itf/triage/findClinicIdByCode";
       /// <summary>
       /// 公共大屏
       /// </summary>
       public static String findPublicScreenData = "api/itf/screen/findPublicScreenData";
       /// <summary>
       /// 诊室小屏（方案一）
       /// </summary>
       public static String findRoomScreenDataOne = "api/itf/screen/findRoomScreenDataOne";
       /// <summary>
       /// 诊室小屏（方案二）
       /// </summary>
       public static String findRoomScreenDataTwo = "api/itf/screen/findRoomScreenDataTwo";
       /// <summary>
       /// 语音呼号记录
       /// </summary>
       public static String findCallList = "api/itf/screen/findCallList";
       /// <summary>
       /// 科室候诊说明（大屏）
       /// </summary>
       public static String findWaitingDesc = "api/itf/screen/findWaitingDesc";
    }
}
