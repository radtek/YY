using DevExpress.XtraWaitForm;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Xr.Common.Utils;

namespace Xr.Common.Controls
{
    public class OpaqueCommand
    {
        Control Control;
        private bool _IsWaitingBoxCreated = false;
        private float _alpha = 0f;
        private String _text = "���Ժ�...";
        public bool status = false;
        public OpaqueCommand(Control control)
        {
            Control = control;
        }
        /// <summary>
        /// �Ƿ���ʾ͸������
        /// </summary>
        bool IsShowtransparencyBG;
        /// <summary>
        /// �Ƿ���ʾȡ����ť
        /// </summary>
        public bool IsShowCancelBtn=false;
        Xr.Common.Controls.ButtonControl buttonControl;
        /// <summary>
        /// ��ʾ�ĵȴ���
        /// </summary>
        public System.Windows.Forms.Panel waitingBox;
        /// <summary>
        /// ������ʾ����
        /// </summary>
        public Rectangle rectDisplay;

        public ProgressPanel _Loading; 

        PanelEx pe;
        /// <summary>
        /// ��ʾ���ֲ�
        /// </summary>
        /// <param name="alpha">͸����</param>
        /// <param name="isShowLoadingImage">�Ƿ���ʾͼ��</param>
        public void ShowOpaqueLayer(int alpha, bool isShowtransparencyBG)
        {
            
            IsShowtransparencyBG = isShowtransparencyBG;
            _alpha = 0.56f;
            CreateWaitingBox();
        }

        /// <summary>
        /// ��ʾ���ֲ�
        /// </summary>
        public void ShowOpaqueLayer()
        {
            _alpha = 0.56f;
            CreateWaitingBox();
        }

        /// <summary>
        /// ��ʾ���ֲ�
        /// </summary>
        /// <param name="alpha">͸����</param>
        public void ShowOpaqueLayer(float alpha)
        {

            _alpha = alpha;
            CreateWaitingBox();
        }

        /// <summary>
        /// ��ʾ���ֲ�
        /// </summary>
        /// <param name="alpha">͸����</param>
        /// <param name="text">��ʾ����</param>
        public void ShowOpaqueLayer(float alpha, String text)
        {

            _alpha = alpha;
            if(text!=null)
                _text = text;
            CreateWaitingBox();
        }
        /// <summary>
        /// �ı�ȴ����С
        /// </summary>
        /// <param name="control"></param>
        public void ChangeSize(Control control)
        {
            Control = control;
            Rectangle rect = Control.DisplayRectangle;
            if (rectDisplay == new Rectangle())
            {
                rect = Control.DisplayRectangle;
            }

            else
            {
                rect = rectDisplay;
                pe.Location = new Point((rect.Width - _Loading.Width) / 2, (rect.Height - _Loading.Height) / 2);//����
                buttonControl.Location = new Point((pe.Width - buttonControl.Width) / 2 + 10, _Loading.Height);//����
            }
            if (waitingBox!=null&& waitingBox.Visible)
            {
                waitingBox.Width = rect.Width;
                waitingBox.Height = rect.Height;
                waitingBox.Location = new Point(rect.X, rect.Y);
                if (IsShowtransparencyBG)
                {

                    waitingBox.BackgroundImage = this.CreateBacgroundImage();
                    //waitingBox.BackgroundImage = Properties.Resources.logo_mini;
                    waitingBox.BackgroundImageLayout = ImageLayout.Stretch;
                }
                if (_alpha != 0f)
                {
                    waitingBox.BackgroundImage = this.CreateBacgroundImage();
                    //waitingBox.BackgroundImage = Properties.Resources.logo_mini;
                    waitingBox.BackgroundImageLayout = ImageLayout.Stretch;
                }
            }
        }
        /// <summary>
        /// Creates the waiting box.
        /// </summary>
        private void CreateWaitingBox()
        {
            if (!this._IsWaitingBoxCreated)
            {
                #region CreateWaitingBox

                this.waitingBox = new System.Windows.Forms.Panel();
                //ControlHelper.BindMouseMoveEvent(this.waitingBox);
                waitingBox.BackColor = Color.WhiteSmoke;

                if (!Control.Contains(waitingBox))
                {

                    _Loading = new ProgressPanel();
                    //_Loading.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
                    _Loading.Appearance.BackColor = System.Drawing.Color.FromArgb(60, Color.WhiteSmoke);
                    _Loading.Appearance.Font = new System.Drawing.Font("Tahoma", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    _Loading.Appearance.ForeColor = System.Drawing.Color.Black;
                    _Loading.Appearance.Options.UseBackColor = true;
                    _Loading.Appearance.Options.UseFont = true;
                    _Loading.Appearance.Options.UseForeColor = true;
                    _Loading.AppearanceCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
                    _Loading.AppearanceCaption.Options.UseFont = true;
                    _Loading.AppearanceDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
                    _Loading.AppearanceDescription.Options.UseFont = true;
                    _Loading.Size = new System.Drawing.Size(223, 53);
                    _Loading.Caption = _text;
                    _Loading.Description = "";
                    //_Loading.Dock = System.Windows.Forms.DockStyle.Fill;
                    _Loading.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
                    _Loading.Name = "progressPanel1";
                    _Loading.Padding = new System.Windows.Forms.Padding(38, 0, 0, 0);
                    //_Loading.Size = new System.Drawing.Size(223, 53);
                    //_Loading.Location = new Point((Control.Width - _Loading.Width) / 2,  (Control.Height - _Loading.Height) / 2);//����
                    _Loading.Dock = DockStyle.Top;
                    _Loading.LookAndFeel.SkinName = "Visual Studio 2013 Light";
                    _Loading.LookAndFeel.UseDefaultLookAndFeel = false;

                    _Loading.TabIndex = 0;
                    _Loading.Text = "progressPanel1";

                    pe = new PanelEx();
                    pe.Size = new System.Drawing.Size(223, 75);
                    pe.Location = new Point((Control.Width - _Loading.Width) / 2, (Control.Height - _Loading.Height) / 2);//����
                    pe.BorderColor = Color.LightGray;
                    pe.Controls.Add(_Loading);
                    waitingBox.Controls.Add(pe);
                    Control.Controls.Add(waitingBox);
                    //ȡ����ť
                    buttonControl = new Xr.Common.Controls.ButtonControl();

                    buttonControl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
                    buttonControl.Dock = DockStyle.None;
                    buttonControl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(131)))), ((int)(((byte)(113)))));
                    buttonControl.HoverBackColor = System.Drawing.Color.Empty;
                    buttonControl.Location = new Point((pe.Width - buttonControl.Width) / 2 + 10, _Loading.Height);//����
                    buttonControl.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
                    buttonControl.Size = new System.Drawing.Size(50, 20);
                    buttonControl.Style = Xr.Common.Controls.ButtonStyle.Return;
                    buttonControl.TabIndex = 90;
                    buttonControl.TabStop = false;
                    buttonControl.Text = "ȡ��";
                    buttonControl.Click += new System.EventHandler(this.button_Click);
                    pe.Controls.Add(buttonControl);

                }
                
                waitingBox.Show();
                this._IsWaitingBoxCreated = true;
                #endregion
            }
            _Loading.Caption = _text;
            if (IsShowCancelBtn)
            {
                if (buttonControl != null)
                {
                    buttonControl.Visible = true;
                    pe.Size = new System.Drawing.Size(223, 75);
                }
            }
            else
            {
                if (buttonControl != null)
                {
                    buttonControl.Visible = false;
                    pe.Size = new System.Drawing.Size(223, 52);
                }
            }
            if (status == false)
            {
                Rectangle rect = Control.DisplayRectangle;
                if (rectDisplay == new Rectangle())
                {
                    rect = Control.DisplayRectangle;
                }

                else
                {
                    rect = rectDisplay;
                    pe.Location = new Point((rect.Width - _Loading.Width) / 2, (rect.Height - _Loading.Height) / 2);//����
                    buttonControl.Location = new Point((pe.Width - buttonControl.Width) / 2 + 10, _Loading.Height);//����
                }
                waitingBox.Width = rect.Width;
                waitingBox.Height = rect.Height;
                waitingBox.Location = new Point(rect.X, rect.Y);

                if (IsShowtransparencyBG)
                {

                    waitingBox.BackgroundImage = this.CreateBacgroundImage();
                    //waitingBox.BackgroundImage = Properties.Resources.logo_mini;
                    waitingBox.BackgroundImageLayout = ImageLayout.Stretch;
                }
                if (_alpha != 0f)
                {
                    waitingBox.BackgroundImage = this.CreateBacgroundImage();
                    //waitingBox.BackgroundImage = Properties.Resources.logo_mini;
                    waitingBox.BackgroundImageLayout = ImageLayout.Stretch;
                }
            }

            waitingBox.Visible = true;
            waitingBox.BringToFront();
            waitingBox.Focus();
            status = true;
        }
        /// <summary>
        /// ������ʱ����ͼƬ
        /// </summary>
        /// <returns>Return a data(or instance) of Bitmap.</returns>
        private Bitmap CreateBacgroundImage()
        {
            Rectangle rect = Control.ClientRectangle;
            //100%��ʱ��DPI��96��������������ʱ��ȡ�Ŵ����
            int w = (int)(rect.Width * PrimaryScreen.ScaleX);
            int h = (int)(rect.Height * PrimaryScreen.ScaleY);

            Point p1 = Control.PointToScreen(new Point(0, 0));
            Point p = new Point((int)(p1.X * PrimaryScreen.ScaleX), (int)(p1.Y * PrimaryScreen.ScaleY));

            Bitmap TempImg = new Bitmap(w, h);
            try
            {

                Bitmap img = new Bitmap(w, h);
                using (Graphics g = Graphics.FromImage(TempImg))
                {
                    g.CopyFromScreen(p, new Point(0, 0), new Size(w, h));
                }

                using (Graphics g = Graphics.FromImage(img))
                {
                    GDIHelper.DrawImage(g, new Rectangle(0, 0, w, h), TempImg, _alpha);
                }

                return img;
            }
            catch
            {
                return null;
            }
            finally
            {
                TempImg.Dispose();
            }
        }
        /// <summary>
        /// �������ֲ�
        /// </summary>
       public void HideOpaqueLayer()
        {
            if (this.waitingBox==null)
            {
                Control.Enabled = true;
                return;
            }
            else
            {
                //Control.Enabled = true;
                waitingBox.Visible = false;
            }
            status = false;
        }
       private void button_Click(object sender, EventArgs e)
       {
           HideOpaqueLayer();
       }
    }
    public class PrimaryScreen
    {
        #region Win32 API
        [DllImport("user32.dll")]
        static extern IntPtr GetDC(IntPtr ptr);
        [DllImport("gdi32.dll")]
        static extern int GetDeviceCaps(
       IntPtr hdc, // handle to DC
       int nIndex // index of capability
       );
        [DllImport("user32.dll", EntryPoint = "ReleaseDC")]
        static extern IntPtr ReleaseDC(IntPtr hWnd, IntPtr hDc);
        #endregion
        #region DeviceCaps����
        const int HORZRES = 8;
        const int VERTRES = 10;
        const int LOGPIXELSX = 88;
        const int LOGPIXELSY = 90;
        const int DESKTOPVERTRES = 117;
        const int DESKTOPHORZRES = 118;
        #endregion

        #region ����
        /// <summary>
        /// ��ȡ��Ļ�ֱ��ʵ�ǰ�����С
        /// </summary>
        public static Size WorkingArea
        {
            get
            {
                IntPtr hdc = GetDC(IntPtr.Zero);
                Size size = new Size();
                size.Width = GetDeviceCaps(hdc, HORZRES);
                size.Height = GetDeviceCaps(hdc, VERTRES);
                ReleaseDC(IntPtr.Zero, hdc);
                return size;
            }
        }
        /// <summary>
        /// ��ǰϵͳDPI_X ��С һ��Ϊ96
        /// </summary>
        public static int DpiX
        {
            get
            {
                IntPtr hdc = GetDC(IntPtr.Zero);
                int DpiX = GetDeviceCaps(hdc, LOGPIXELSX);
                ReleaseDC(IntPtr.Zero, hdc);
                return DpiX;
            }
        }
        /// <summary>
        /// ��ǰϵͳDPI_Y ��С һ��Ϊ96
        /// </summary>
        public static int DpiY
        {
            get
            {
                IntPtr hdc = GetDC(IntPtr.Zero);
                int DpiX = GetDeviceCaps(hdc, LOGPIXELSY);
                ReleaseDC(IntPtr.Zero, hdc);
                return DpiX;
            }
        }
        /// <summary>
        /// ��ȡ��ʵ���õ�����ֱ��ʴ�С
        /// </summary>
        public static Size DESKTOP
        {
            get
            {
                IntPtr hdc = GetDC(IntPtr.Zero);
                Size size = new Size();
                size.Width = GetDeviceCaps(hdc, DESKTOPHORZRES);
                size.Height = GetDeviceCaps(hdc, DESKTOPVERTRES);
                ReleaseDC(IntPtr.Zero, hdc);
                return size;
            }
        }

        /// <summary>
        /// ��ȡ������Űٷֱ�
        /// </summary>
        public static float ScaleX
        {
            get
            {
                IntPtr hdc = GetDC(IntPtr.Zero);
                int t = GetDeviceCaps(hdc, DESKTOPHORZRES);
                int d = GetDeviceCaps(hdc, HORZRES);
                float ScaleX = (float)GetDeviceCaps(hdc, DESKTOPHORZRES) / (float)GetDeviceCaps(hdc, HORZRES);
                ReleaseDC(IntPtr.Zero, hdc);
                return ScaleX;
            }
        }
        /// <summary>
        /// ��ȡ�߶����Űٷֱ�
        /// </summary>
        public static float ScaleY
        {
            get
            {
                IntPtr hdc = GetDC(IntPtr.Zero);
                float ScaleY = (float)(float)GetDeviceCaps(hdc, DESKTOPVERTRES) / (float)GetDeviceCaps(hdc, VERTRES);
                ReleaseDC(IntPtr.Zero, hdc);
                return ScaleY;
            }
        }
        #endregion
    }
}
