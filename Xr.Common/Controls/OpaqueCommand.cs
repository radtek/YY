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
        public OpaqueCommand(Control control)
        {
            Control = control;
        }

        /// <summary>
        /// œ‘ æµƒµ»¥˝øÚ
        /// </summary>
        private System.Windows.Forms.Panel waitingBox;
        /// <summary>
        /// œ‘ æ’⁄’÷≤„
        /// </summary>
        /// <param name="control">øÿº˛</param>
        /// <param name="alpha">Õ∏√˜∂»</param>
        /// <param name="isShowLoadingImage"> «∑Òœ‘ æÕº±Í</param>
        public void ShowOpaqueLayer(int alpha, bool isShowLoadingImage)
        {
            CreateWaitingBox();

        }
        /// <summary>
        /// Creates the waiting box.
        /// </summary>
        /// User:Ryan  CreateTime:2012-8-5 16:22.
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
                    
                    ProgressPanel _Loading = new ProgressPanel();
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
                    _Loading.Caption = "«Î…‘∫Ú...";
                    _Loading.Description = "";
                    //_Loading.Dock = System.Windows.Forms.DockStyle.Fill;
                    _Loading.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
                    _Loading.Name = "progressPanel1";
                    _Loading.Padding = new System.Windows.Forms.Padding(38, 0, 0, 0);
                    //_Loading.Size = new System.Drawing.Size(223, 53);
                    //_Loading.Location = new Point((Control.Width - _Loading.Width) / 2,  (Control.Height - _Loading.Height) / 2);//æ”÷–
                    _Loading.Dock = DockStyle.Fill;
                    _Loading.LookAndFeel.SkinName = "Visual Studio 2013 Light";
                    _Loading.LookAndFeel.UseDefaultLookAndFeel = false;

                    _Loading.TabIndex = 0;
                    _Loading.Text = "progressPanel1";

                    PanelEx pe = new PanelEx();
                    pe.Size = new System.Drawing.Size(223, 53);
                    pe.Location = new Point((Control.Width - _Loading.Width) / 2, (Control.Height - _Loading.Height) / 2);//æ”÷–
                    pe.BorderColor = Color.LightGray;
                    pe.Controls.Add(_Loading);
                    waitingBox.Controls.Add(pe);
                    Control.Controls.Add(waitingBox);
                    
                }
                waitingBox.Show();
                this._IsWaitingBoxCreated = true;
                #endregion
            }
            Rectangle rect = Control.DisplayRectangle;
            waitingBox.Width = rect.Width;
            waitingBox.Height = rect.Height;
            waitingBox.Location = new Point(rect.X, rect.Y);
            waitingBox.BackgroundImage = this.CreateBacgroundImage();
            //waitingBox.BackgroundImage = Properties.Resources.logo_mini;
            waitingBox.BackgroundImageLayout = ImageLayout.Stretch;
            waitingBox.Visible = true;
            waitingBox.BringToFront();
            waitingBox.Focus();
        }
        /// <summary>
        /// ¥¥Ω®¡Ÿ ±±≥æ∞Õº∆¨
        /// </summary>
        /// <returns>Return a data(or instance) of Bitmap.</returns>
        /// User:Ryan  CreateTime:2012-8-5 16:21.
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
                    GDIHelper.DrawImage(g, new Rectangle(0, 0, w, h), TempImg, 0.56F);
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
        /// “˛≤ÿ’⁄’÷≤„
        /// </summary>
       public void HideOpaqueLayer()
        {
            if (this.waitingBox==null)
            {
                return;
            }
            else
            {
                //Control.Enabled = true;
                waitingBox.Visible = false;
            }
        }

    }
}
