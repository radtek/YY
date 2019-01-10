using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Xr.RtScreen.RtUserContronl
{
    /// <summary>
    /// 自定义控件（控件中的数据从右向左滚动）
    /// Summary description for ScrollingTextControl.
    /// </summary>
    [
    ToolboxBitmapAttribute(typeof(DevScrollingText), "ScrollingText.bmp"),
    DefaultEvent("TextClicked")
    ]
    public class DevScrollingText : DevExpress.XtraEditors.XtraUserControl  
    {

    }
}
