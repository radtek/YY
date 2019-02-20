using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Xr.RtManager
{
    public partial class WelcomeForm : UserControl
    {
        public WelcomeForm()
        {
            InitializeComponent();
            Bitmap bm = new Bitmap(Properties.Resources.welcom); //fbImage图片路径
            this.BackgroundImage = bm;//设置背景图片
            this.BackgroundImageLayout = ImageLayout.Stretch;//设置背景图片自动适应
        }
    }
}
