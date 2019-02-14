using DevExpress.XtraWaitForm;
using System;
using System.Drawing;
using System.Windows.Forms;
using Xr.Common.Utils;

namespace Xr.Common.Controls
{
    public class OpaqueCommand
    {
        Control Control;
        private bool _IsWaitingBoxCreated = false;
        private float _alpha = 0f;
        private String _text = "请稍候...";
        public OpaqueCommand(Control control)
        {
            Control = control;
        }
        /// <summary>
        /// 是否显示透明背景
        /// </summary>
        bool IsShowtransparencyBG;
        /// <summary>
        /// 是否显示取消按钮
        /// </summary>
        public bool IsShowCancelBtn=false;
        Xr.Common.Controls.ButtonControl buttonControl;
        /// <summary>
        /// 显示的等待框
        /// </summary>
        public System.Windows.Forms.Panel waitingBox;

        public ProgressPanel _Loading; 

        PanelEx pe;
        /// <summary>
        /// 显示遮罩层
        /// </summary>
        /// <param name="control">控件</param>
        /// <param name="alpha">透明度</param>
        /// <param name="isShowLoadingImage">是否显示图标</param>
        public void ShowOpaqueLayer(int alpha, bool isShowtransparencyBG)
        {
            IsShowtransparencyBG = isShowtransparencyBG;
            _alpha = 0.56f;
            CreateWaitingBox();
        }

        /// <summary>
        /// 显示遮罩层
        /// </summary>
        /// <param name="control">控件</param>
        public void ShowOpaqueLayer()
        {
            _alpha = 0.56f;
            CreateWaitingBox();
        }

        /// <summary>
        /// 显示遮罩层
        /// </summary>
        /// <param name="control">控件</param>
        /// <param name="alpha">透明度</param>
        public void ShowOpaqueLayer(float alpha)
        {

            _alpha = alpha;
            CreateWaitingBox();
        }

        /// <summary>
        /// 显示遮罩层
        /// </summary>
        /// <param name="control">控件</param>
        /// <param name="alpha">透明度</param>
        /// <param name="text">显示内容</param>
        public void ShowOpaqueLayer(float alpha, String text)
        {

            _alpha = alpha;
            if(text!=null)
                _text = text;
            CreateWaitingBox();
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
                    //_Loading.Location = new Point((Control.Width - _Loading.Width) / 2,  (Control.Height - _Loading.Height) / 2);//居中
                    _Loading.Dock = DockStyle.Top;
                    _Loading.LookAndFeel.SkinName = "Visual Studio 2013 Light";
                    _Loading.LookAndFeel.UseDefaultLookAndFeel = false;

                    _Loading.TabIndex = 0;
                    _Loading.Text = "progressPanel1";

                     pe = new PanelEx();
                    pe.Size = new System.Drawing.Size(223, 75);
                    pe.Location = new Point((Control.Width - _Loading.Width) / 2, (Control.Height - _Loading.Height) / 2);//居中
                    pe.BorderColor = Color.LightGray;
                    pe.Controls.Add(_Loading);
                    waitingBox.Controls.Add(pe);
                    Control.Controls.Add(waitingBox);
                    if (IsShowCancelBtn)
                    {
                        //取消按钮
                        buttonControl = new Xr.Common.Controls.ButtonControl();

                        buttonControl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
                        buttonControl.Dock = DockStyle.None;
                        buttonControl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(131)))), ((int)(((byte)(113)))));
                        buttonControl.HoverBackColor = System.Drawing.Color.Empty;
                        buttonControl.Location = new Point((pe.Width - buttonControl.Width) / 2 + 10, _Loading.Height);//居中
                        buttonControl.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
                        buttonControl.Size = new System.Drawing.Size(50, 20);
                        buttonControl.Style = Xr.Common.Controls.ButtonStyle.Return;
                        buttonControl.TabIndex = 90;
                        buttonControl.TabStop = false;
                        buttonControl.Text = "取消";
                        buttonControl.Click += new System.EventHandler(this.button_Click);
                        pe.Controls.Add(buttonControl);
                    }
                    else
                    {
                        pe.Size = new System.Drawing.Size(223, 52); 
                    }
                    
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
            Rectangle rect = Control.DisplayRectangle;
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


            waitingBox.Visible = true;
            waitingBox.BringToFront();
            waitingBox.Focus();
        }
        /// <summary>
        /// 创建临时背景图片
        /// </summary>
        /// <returns>Return a data(or instance) of Bitmap.</returns>
        private Bitmap CreateBacgroundImage()
        {
            Rectangle rect = Control.ClientRectangle;
            int w = rect.Width;
            int h = rect.Height;

            Point p = Control.PointToScreen(new Point(0, 0));
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
        /// 隐藏遮罩层
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
        }
       private void button_Click(object sender, EventArgs e)
       {
           HideOpaqueLayer();
       }
    }
}
