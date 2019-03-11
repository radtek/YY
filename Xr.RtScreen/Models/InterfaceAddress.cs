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
        /// 医院接口地址
       /// </summary>
       public const String hostal = "itf/intro/hospitalByCode";
       /// <summary>
       /// 科室接口地址
       /// </summary>
       public const String dept = "itf/intro/deptByhospitalCode";
       /// <summary>
       /// 诊室接口地址
       /// </summary>
       public const String clin = "itf/triage/findClinicIdByCode";
       /// <summary>
       /// 公共大屏接口地址
       /// </summary>
       public const String findPublicScreenData = "itf/screen/findPublicScreenData";
       /// <summary>
       /// 诊室小屏（方案一）接口地址
       /// </summary>
       public const String findRoomScreenDataOne = "itf/screen/findRoomScreenDataOne";
       /// <summary>
       /// 诊室小屏（方案二）接口地址
       /// </summary>
       public const String findRoomScreenDataTwo = "itf/screen/findRoomScreenDataTwo";
       /// <summary>
       /// 语音呼号记录接口地址
       /// </summary>
       public const String findCallList = "itf/screen/findCallList";
       /// <summary>
       /// 科室候诊说明（大屏）接口地址
       /// </summary>
       public const String findWaitingDesc = "itf/screen/findWaitingDesc";
    }
}
