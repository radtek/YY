using DevExpress.XtraTab;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Drawing.Drawing2D;
using Newtonsoft.Json.Linq;
using System.Threading;
using System.Runtime.Remoting;
using System.Configuration;
using Xr.Common;
using DevExpress.XtraNavBar;
using Xr.Common.Controls;
using Xr.Http;

namespace Xr.RtManager
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            cmd = new Xr.Common.Controls.OpaqueCommand(this);
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            panMenuBar.BorderColor = borderColor;
        }
        Xr.Common.Controls.OpaqueCommand cmd;
        Xr.Common.Controls.OpaqueCommand cmd2;
        private Color borderColor = Color.FromArgb(157, 160, 170);

        private void MainForm_Load(object sender, EventArgs e)
        {
            AppContext.Session.openStatus = false;
            cmd.ShowOpaqueLayer(0f);
            labBottomLeft.Text = AppContext.Session.deptName + " | " + AppContext.Session.name + " | " + System.DateTime.Now.ToString();
            this.timer1.Start();

            tmHeartbeat.Enabled = true;

            //获取父级为1的菜单
            List<MenuEntity> menuList = new List<MenuEntity>();
            for (int i = 0; i < AppContext.Session.menuList.Count(); i++)
            {
                MenuEntity menu = AppContext.Session.menuList[i];
                if (menu.parentId.Equals("1"))
                {
                    menuList.Add(menu);
                }
            }
            //排序菜单
            menuList = menuList.OrderBy(x => x.sort).ToList();
            //循环添加菜单
            panMenuBar.Controls.Clear();
            foreach (MenuEntity menu in menuList)
            {
                AddContextMenu(menu.id, menu.name, menu.href, panMenuBar);
            }

            this.DoWorkAsync(500,(o) => //耗时逻辑处理(此处不能操作UI控件，因为是在异步中)
            {
                Thread.Sleep(1000);
                return null;

            }, null, (data) => //显示结果（此处用于对上面结果的处理，比如显示到界面上）
            {
                //显示欢迎页
                WelcomeForm form = new WelcomeForm();
                AaddUserControl(form, "Welcome", "欢迎页");
                cmd.HideOpaqueLayer();
            });
            //AppContext.Session.waitControl = xtraTabControl1;
        }

        #region 菜单相关事件
        //添加菜单
        private void AddContextMenu(String menuId, String Caption, String tag, Panel parentPanel)
        {
            Font font = new Font("微软雅黑", 11);//菜单字体样式
            int MenuItemHeight = 34; //菜单选项高度

            //选项
            PanelEx panel21 = new PanelEx();
            panel21.BorderColor = borderColor;
            panel21.BorderStyleTop = ButtonBorderStyle.None;
            panel21.BorderStyleBottom = ButtonBorderStyle.None;
            panel21.BorderStyleRight = ButtonBorderStyle.None;
            panel21.BorderStyleLeft = ButtonBorderStyle.None;
            panel21.Name = "panel" + menuId;
            panel21.BackColor = Color.Transparent;
            panel21.AutoSize = true;
            panel21.Dock = DockStyle.Top;
            //当前选项头
            PanelEx panel22 = new PanelEx();
            panel22.BorderColor = borderColor;
            panel22.BorderStyleTop = ButtonBorderStyle.None;
            panel22.BorderStyleLeft = ButtonBorderStyle.None;
            panel22.BorderStyleRight = ButtonBorderStyle.None;
            panel22.Margin = new Padding(0, 0, 0, 0);
            panel22.Padding = new Padding(0,6,0,0);
            panel22.Height = MenuItemHeight;
            panel22.Dock = DockStyle.Top;
            //当前选项文本
            Label label21 = new Label();
            label21.Name = "lab" + menuId;
            label21.Text = Caption;
            label21.Tag = tag;
            label21.Font = font;
            label21.Dock = DockStyle.Fill;
            panel22.Controls.Add(label21);
            panel21.Controls.Add(panel22);
            
            //获取某菜单的下一级所有菜单
            List<MenuEntity> menuList = new List<MenuEntity>();
            foreach (MenuEntity menu in AppContext.Session.menuList)
            {
                if (menu.parentId.Equals(menuId))
                {
                    menuList.Add(menu);
                }
            }
            if (menuList.Count > 0)
            {
                //子菜单面板
                PanelEx panel23 = new PanelEx();
                panel23.BorderStyleLeft = ButtonBorderStyle.None;
                panel23.BorderStyleRight = ButtonBorderStyle.None;
                panel23.BorderColor = Color.FromArgb(157, 160, 170);
                panel23.Margin = new Padding(0, 0, 0, 0);
                panel23.Padding = new Padding(0, 1, 0, 0);
                panel23.Visible = false;
                panel23.AutoSize = true;
                panel23.Dock = DockStyle.Top;
                //Panel panel24 = new Panel();
                //panel24.AutoSize = true;
                //排序菜单
                menuList = menuList.OrderBy(x => x.sort).ToList();
                //循环添加菜单
                foreach (MenuEntity menu in menuList)
                {
                    //2级菜单
                    PanelEx panel25 = new PanelEx();
                    panel25.BorderColor = borderColor;
                    panel25.BorderStyleTop = ButtonBorderStyle.None;
                    panel25.BorderStyleBottom = ButtonBorderStyle.None;
                    panel25.BorderStyleLeft = ButtonBorderStyle.None;
                    panel25.BorderStyleRight = ButtonBorderStyle.None;
                    panel25.BackColor = Color.Transparent;
                    panel25.Height = MenuItemHeight;
                    panel25.Dock = DockStyle.Top;
                    panel25.Padding = new Padding(20, 6, 1, 1);
                    Label label22 = new Label();
                    label22.BackColor = Color.Transparent;
                    label22.Font = font;
                    label22.Name = "lab" + menu.id;
                    label22.Text = menu.name;
                    label22.Tag = menu.href;
                    label22.Dock = DockStyle.Fill;
                    label22.Click += new EventHandler(TwoLevelMenuClicked);
                    label22.MouseEnter += new EventHandler(TwoLevelMouseEnter);
                    label22.MouseLeave += new EventHandler(TwoLevelMouseLeave);
                    panel25.Controls.Add(label22);
                    panel23.Controls.Add(panel25);
                    panel25.BringToFront();
                    //AddContextMenu(menu.id, menu.name, panel24);
                }
                //panel23.Controls.Add(panel25);
                panel21.Controls.Add(panel23);
                panel23.BringToFront();
            }
            label21.Click += new EventHandler(OneLevelMenuClicked);
            label21.MouseEnter += new EventHandler(OneLevelMouseEnter);
            label21.MouseLeave += new EventHandler(OneLevelMouseLeave);
            parentPanel.Controls.Add(panel21);
            panel21.BringToFront();
        }

        //记录当前一级菜单name，点击当前菜单，不做任何处理
        String currentMenuName = ""; 

        /// <summary>
        /// 一级菜单点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OneLevelMenuClicked(object sender, EventArgs e)
        {
            //子菜单的显示隐藏
            Label item = (Label)sender;
            if (currentMenuName.Equals(item.Name))
            {
                return;
            }
            currentMenuName = item.Name;
            PanelEx selectionPanelTop = (PanelEx)item.Parent; //选中的一级菜单(头部)
            PanelEx selectionPanel = (PanelEx)selectionPanelTop.Parent; //选中的一级菜单（包含二级菜单panel）
            //隐藏所有二级菜单
            for (int i = panMenuBar.Controls.Count - 1; i >= 0; i--)
            {
                System.Windows.Forms.Control control = panMenuBar.Controls[i]; //一级菜单（包含二级菜单panel）
                PanelEx panelTop = new PanelEx(); //一级菜单（有二级菜单的时候取[1]，没有取[]）
                if (control.Controls.Count == 2)
                {
                    panelTop = (PanelEx)control.Controls[1];
                    //隐藏二级菜单
                    PanelEx panel = (PanelEx)control.Controls[0]; //二级菜单  
                    panel.Visible = false;
                }
                else
                {
                    panelTop = (PanelEx)control.Controls[0];
                }
                panelTop.BackColor = Color.Transparent;
                //所有文字改为黑色
                Label label = (Label)panelTop.Controls[0];
                label.ForeColor = Color.Black;
            }
            //显示所选一级菜单下的二级菜单，并且二级菜单背景色都改为透明
            if (selectionPanel.Controls.Count == 2)
            {
                PanelEx panel = (PanelEx)selectionPanel.Controls[0];
                foreach (System.Windows.Forms.Control control in panel.Controls)
                {

                    PanelEx itemPanel = (PanelEx)control;
                    Label itemLabel = (Label)itemPanel.Controls[0];
                    control.BackColor = Color.Transparent;
                    itemLabel.ForeColor = Color.Black;
                }
                panel.Visible = true;
            }

            //背景色的变化
            if (selectionPanelTop.BackColor == Color.Transparent)
            {
                selectionPanelTop.BackColor = Color.FromArgb(57, 61, 73);
                item.ForeColor = Color.White;
            }
            else
            {
                selectionPanelTop.BackColor = Color.Transparent;
                item.ForeColor = Color.Black;
            }
        }

        //记录所选菜单的name、标题、链接
        String[] param = new String[] {null, null, null };
        Color OneLevelMouseOriginally = Color.Transparent;//一级菜单原色
        Color TwoLevelMouseOriginally = Color.Transparent;//二级菜单原色

        /// <summary>
        /// 二级菜单点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void TwoLevelMenuClicked(object sender, EventArgs e)
        {
            //if (sender.ToString().IndexOf("System.Windows.Forms.Label") != -1)
            Label selectionLabel = (Label)sender;
            PanelEx selectionPanel = (PanelEx)selectionLabel.Parent;
            PanelEx allPanel = (PanelEx)selectionPanel.Parent;
            //当前面板下所有二级菜单背景改为透明,文字颜色改为黑色
            foreach(System.Windows.Forms.Control control in allPanel.Controls){
                PanelEx panel = (PanelEx)control;
                Label label = (Label)panel.Controls[0];
                control.BackColor = Color.Transparent;
                label.ForeColor = Color.Black;
            }
            //修改选择的二级菜单背景色
            selectionPanel.BackColor = Color.FromArgb(24, 166, 137);
            selectionLabel.ForeColor = Color.White;
            //这里用异步延迟0.1秒再加载模块，不然菜单的样式反应不过来
            //（改到一半的时候因为加载模块而卡顿，导致菜单界面不好看）
            //这里不能使用定时器，使用定时器的时候，一直点同一个选项会导致定时器无限循环(不知道为什么没有关闭)
            this.DoWorkAsync(200, (o) => //耗时逻辑处理(此处不能操作UI控件，因为是在异步中)
            {
                return null;

            }, null, (data) => //显示结果（此处用于对上面结果的处理，比如显示到界面上）
            {
                if (selectionLabel.Tag != null && selectionLabel.Tag.ToString().Length > 0)
                {
                    param[0] = selectionLabel.Tag.ToString();
                    param[1] = selectionLabel.Name;
                    param[2] = selectionLabel.Text;
                }
                else
                {
                    param[0] = null;
                    param[1] = null;
                    param[2] = null;
                }

                String href = param[0];
                String id = param[1];
                String name = param[2];
                if (href != "" && href != null)
                {
                    int i = GetTabName(id);
                    if (i == -1)
                    {
                        System.Type tab = System.Type.GetType("Xr.RtManager." + href);
                        //UserControl uc ;//= (UserControl)Activator.CreateInstance(tab);
                        AaddUserControl(tab, id, name);
                    }
                    else
                    {
                        xtraTabControl1.SelectedTabPageIndex = i;
                    }
                }
            });
        }

        /// <summary>
        /// 一级菜单鼠标悬停事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        
        void OneLevelMouseEnter(object sender, EventArgs e)
        {
            Label label = (Label)sender;
            PanelEx selectionPanel = (PanelEx)label.Parent;
            OneLevelMouseOriginally = selectionPanel.BackColor;
            selectionPanel.BackColor = Color.FromArgb(47, 64, 86);
            label.ForeColor = Color.White;
            toolTip1.SetToolTip(label, label.Text);
        }

        /// <summary>
        /// 一级菜单鼠标离开事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OneLevelMouseLeave(object sender, EventArgs e)
        {
            Label label = (Label)sender;
            PanelEx selectionPanel = (PanelEx)label.Parent;
            if (selectionPanel.BackColor != Color.FromArgb(57, 61, 73))
            {
                selectionPanel.BackColor = OneLevelMouseOriginally;
            }
            if (selectionPanel.BackColor != Color.FromArgb(47, 64, 86)
                && selectionPanel.BackColor != Color.FromArgb(57, 61, 73))
            {
                label.ForeColor = Color.Black;
            }
        }

        /// <summary>
        /// 二级菜单鼠标悬停事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void TwoLevelMouseEnter(object sender, EventArgs e)
        {
            Label label = (Label)sender;
            PanelEx selectionPanel = (PanelEx)label.Parent;
            OneLevelMouseOriginally = selectionPanel.BackColor;
            selectionPanel.BackColor = Color.FromArgb(26, 179, 148);
            label.ForeColor = Color.White;
            toolTip1.SetToolTip(label, label.Text);
        }

        /// <summary>
        /// 二级菜单鼠标离开事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void TwoLevelMouseLeave(object sender, EventArgs e)
        {
            Label label = (Label)sender;
            PanelEx selectionPanel = (PanelEx)label.Parent;
            if (selectionPanel.BackColor != Color.FromArgb(24, 166, 137))
            {
                selectionPanel.BackColor = OneLevelMouseOriginally;
            }
            if (selectionPanel.BackColor != Color.FromArgb(24, 166, 137)
                && selectionPanel.BackColor != Color.FromArgb(26, 179, 148))
            {
                label.ForeColor = Color.Black;
            }
        }

        #endregion 

        /// <summary>  
        /// 遍历打开的窗口  
        /// </summary>  
        /// <param name="value">name值</param>  
        /// <returns></returns>  
        public int GetTabName(string value)
        {
            int count = -1;
            for (int i = 0; i < xtraTabControl1.TabPages.Count; i++)
            {
                if (xtraTabControl1.TabPages[i].Name == value)
                {
                    return i;
                }
            }
            return count;
        }

        public delegate void ShowDatatableDelegate(XtraTabPage page, UserControl Xuser);
        private void showPage(XtraTabPage page, UserControl Xuser)
        {
            Xuser.BackColor = Color.Transparent;
            //使用一个有双缓存的panel做背景，避免背景图片闪烁
            PanelEnhanced panelE = new PanelEnhanced();
            panelE.BackgroundImage = Properties.Resources.bg2;
            panelE.Dock = DockStyle.Fill;
            panelE.Controls.Add(Xuser);
            page.Controls.Add(panelE);
            xtraTabControl1.TabPages.Add(page);
            xtraTabControl1.SelectedTabPage.ResetBackColor();
            xtraTabControl1.SelectedTabPage.BackColor = Color.Transparent;
            xtraTabControl1.SelectedTabPage = page;  //首页显示  
        }

        public delegate void ShowDatatableTypeDelegate(XtraTabPage page, System.Type type );
        private void showPage(XtraTabPage page, System.Type type)
        {
            //使用一个有双缓存的panel做背景，避免背景图片闪烁
            PanelEnhanced panelE = new PanelEnhanced();
            panelE.BackgroundImage = Properties.Resources.bg2;
            panelE.Dock = DockStyle.Fill;
            page.Controls.Add(panelE);
            cmd2 = new Xr.Common.Controls.OpaqueCommand(page);
            cmd2.ShowOpaqueLayer(0f);
            this.DoWorkAsync(100, (o) => //耗时逻辑处理(此处不能操作UI控件，因为是在异步中)
            {
                return null;

            }, null, (data) => //显示结果（此处用于对上面结果的处理，比如显示到界面上）
            {
                UserControl uc = (UserControl)Activator.CreateInstance(type);
                uc.BackColor = Color.Transparent;
                setUcUI(null, uc);
                panelE.Controls.Add(uc);
                cmd2.HideOpaqueLayer();
            });
        }
        private void setUcUI(XtraTabPage page, UserControl Xuser)
        {
            Xuser.Parent = this;
            Xuser.Dock = DockStyle.Fill;  //dock属性 全屏撑大  
        }
        /// <summary>  
        /// 添加到Tab控件里  
        /// </summary>  
        /// <param name="Xuser">要添加的用户控件实例</param>  
        /// <param name="name"> 控件唯一的 name 属性</param>  
        /// <param name="caption">显示标题 caption</param>  
        private void AaddUserControl(UserControl Xuser, string name, string caption)
        {
            try
            {
                this.Invoke(new ShowDatatableDelegate(setUcUI), new object[] { null, Xuser});
                XtraTabPage page = new XtraTabPage();
                page.BackColor = Color.Transparent;
                page.Name = name;   //控件标示  
                page.Text = caption;  //显示标题  
                this.Invoke(new ShowDatatableDelegate(showPage), new object[] { page, Xuser});
            }
            catch (Exception ex)
            {
                MessageBoxUtils.Show(ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }
        /// <summary>  
        /// 添加到Tab控件里  
        /// </summary>  
        /// <param name="Xuser">要添加的用户控件实例</param>  
        /// <param name="name"> 控件唯一的 name 属性</param>  
        /// <param name="caption">显示标题 caption</param>  
        private void AaddUserControl(System.Type type, string name, string caption)
        {
            try
            {
                //this.Invoke(new ShowDatatableDelegate(setUcUI), new object[] { null, Xuser });
                XtraTabPage page = new XtraTabPage();
                page.BackColor = Color.Transparent;
                page.Name = name;   //控件标示  
                page.Text = caption;  //显示标题  

                xtraTabControl1.TabPages.Add(page);
                xtraTabControl1.SelectedTabPage.ResetBackColor();
                xtraTabControl1.SelectedTabPage.BackColor = Color.Transparent;
                xtraTabControl1.SelectedTabPage = page;  //首页显示  

                AppContext.Session.waitControl = page;
                this.Invoke(new ShowDatatableTypeDelegate(showPage), new object[] { page, type });
                //cmd2 = new Xr.Common.Controls.OpaqueCommand(this);
                //AppContext.Session.cmd = cmd2;
                //cmd2.ShowOpaqueLayer(1f);
                //this.DoWorkAsync(0, (o) => //耗时逻辑处理(此处不能操作UI控件，因为是在异步中)
                //{
                //    this.Invoke(new ShowDatatableTypeDelegate(showPage), new object[] { page, type });
                //    return null;

                //}, null, (data) => //显示结果（此处用于对上面结果的处理，比如显示到界面上）
                //{
                //    cmd2.HideOpaqueLayer();
                //});
                
            }
            catch (Exception ex)
            {
                MessageBoxUtils.Show(ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }
        /// <summary>
        /// 子界面添加新的tab界面的方法（给子界面用的）
        /// </summary>
        /// <param name="name">控件唯一的 name 属性（类名）</param>
        /// <param name="caption">显示标题 caption</param>
        /// <param name="navigationData">参数</param>
        public void JumpInterface(string name, string caption, object navigationData)
        {
            //int i = GetTabName(name);
            //if (i == -1)
            //{
                Dictionary<string, object> data = new Dictionary<string, object>();
                data = (Dictionary<String, Object>)navigationData;
                //this._pkPv = data["pkPv"].ToString();

                if (name.Equals("RoleDistributionForm"))
                {
                    RoleDistributionForm tab = new RoleDistributionForm();
                    tab.id = data["id"].ToString();
                    AaddUserControl(tab, name, caption);
                }
            //}
            //else
            //{
            //    xtraTabControl1.SelectedTabPageIndex = i;
            //}
        }

        /// <summary>
        /// 关闭当前页，给子页面用的
        /// </summary>
        public void CloseTab()
        {
            string name = this.xtraTabControl1.SelectedTabPage.Text;//得到关闭的选项卡的text
            foreach (XtraTabPage page in xtraTabControl1.TabPages)//遍历得到和关闭的选项卡一样的Text
            {
                if (page.Text == name)
                {
                    xtraTabControl1.TabPages.Remove(page);
                    page.Dispose();
                    return;
                }
            }
        }

        public void ExitProgram()
        {
            this.Close();
        }

        /// <summary>
        /// 调用已打开的的方法(提供给子界面用的)
        /// </summary>
        /// <param name="name"></param>
        public void refreshInterface(String name)
        {
            //foreach (XtraTabPage page in xtraTabControl1.TabPages)//遍历得到和关闭的选项卡一样的Text
            //{
            //    if (page.Text == name)
            //    {
            //        if (name.Equals("入院登记"))
            //        {
            //            DaiRuYuanLieBiao tab = (DaiRuYuanLieBiao)page.Controls[0];
            //            tab.skinButton1_Click(null, null);
            //        }
            //    }
            //}
        }

        //模块关闭事件
        private void xtraTabControl1_CloseButtonClick(object sender, EventArgs e)
        {
            DevExpress.XtraTab.ViewInfo.ClosePageButtonEventArgs c = (DevExpress.XtraTab.ViewInfo.ClosePageButtonEventArgs)e;
            DevExpress.XtraTab.XtraTabPage page = (DevExpress.XtraTab.XtraTabPage)c.PrevPage;
            this.xtraTabControl1.TabPages.Remove(page);
        }

        /// <summary>
        /// 背景图片随窗体的大小而改变大小
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panelEnhanced1_SizeChanged(object sender, EventArgs e)
        {
            loadBackImage();
        }

        private void loadBackImage()
        {
            Bitmap bit = new Bitmap(this.Width, this.Height);
            Graphics g = Graphics.FromImage(bit);
            g.DrawImage(Properties.Resources.bg, new Rectangle(0, 0, bit.Width, bit.Height), 0, 0, Properties.Resources.bg1.Width, Properties.Resources.bg1.Height, GraphicsUnit.Pixel);
            panelEnhanced1.BackgroundImage = bit;
            g.Dispose();
        }
        #region 标签右键关闭操作
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBoxUtils.Show("你确定退出系统吗?", MessageBoxButtons.OKCancel,
                 MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.OK)
             {
                String url = AppContext.AppConfig.serverUrl + "logout";
                String data = HttpClass.httpPost(url);
                JObject objT = JObject.Parse(data);
                if (string.Compare(objT["state"].ToString(), "true", true) == 0)
                {
                    AppContext.Unload();
                    //Application.Exit();
                    e.Cancel = false;  //点击OK   
                }
                else
                {
                    MessageBox.Show("退出系统失败:"+objT["message"].ToString());
                }
            }
            else
            {
                e.Cancel = true;
            }    
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.labBottomRight.Text = System.DateTime.Now.ToString();
        }
        private void xtraTabControl_MouseDown(object sender, MouseEventArgs e)
        {
            DevExpress.XtraTab.ViewInfo.XtraTabHitInfo hinfo = xtraTabControl1.CalcHitInfo(new Point(e.X, e.Y));
            //判断点击在标签上才打开选项卡菜单
            if (e.Button == MouseButtons.Right && hinfo.Page!=null)
            {
                contextMenuStrip1.Show(xtraTabControl1, new Point(e.X, e.Y));
                
               // xtraTabControl1.ContextMenuStrip = null;

                //TreeListHitInfo hInfo = treeList1.CalcHitInfo(new Point(e.X, e.Y));
                //TreeListNode node = hInfo.Node;
                //treeList1.FocusedNode = node;
                
                //xtraTabControl1.ContextMenuStrip = contextMenuStrip1;

            }
        }

        private void xtraTabControl_MouseUp(object sender, MouseEventArgs e)
        {
        }
        /// <summary>
        /// 关闭当前页签
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmCloseCurrent_Click(object sender, EventArgs e)
        {

            string name = xtraTabControl1.SelectedTabPage.Text;//得到关闭的选项卡的text
            foreach (XtraTabPage page in xtraTabControl1.TabPages)//遍历得到和关闭的选项卡一样的Text
            {
                if (page.Text == name)
                {
                    xtraTabControl1.TabPages.Remove(page);
                    page.Dispose();
                    return;
                }
            }
        }
        /// <summary>
        /// 关闭其他页签
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmCloseOther_Click(object sender, EventArgs e)
        {
            int index = xtraTabControl1.SelectedTabPageIndex;//得到关闭的选项卡的索引
            for (int i = xtraTabControl1.TabPages.Count - 1; i >= 0; i--)
            {
                if (i != index)
                {
                    xtraTabControl1.TabPages.RemoveAt(i);

                }
            }

        }
        /// <summary>
        /// 关闭全部页签
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmCloseAll_Click(object sender, EventArgs e)
        {
            //for (int i = xtraTabControl1.TabPages.Count - 1; i > 0; i--)
            //{
              //  xtraTabControl1.TabPages.RemoveAt(i);
                // xtraTabControl.TabPages[i].Dispose();
           // }
            xtraTabControl1.TabPages.Clear();
        }
        /*
        /// <summary>
        /// 双击关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void xtraTabControl_DoubleClick(object sender, EventArgs e)
        {
            DevExpress.XtraTab.ViewInfo.XtraTabHitInfo tabHitInfo = ((XtraTabControl)sender).CalcHitInfo(((XtraTabControl)sender).PointToClient(Control.MousePosition));
            ((XtraTabControl)sender).TabPages.Remove(tabHitInfo.Page);
        }
         */
        #endregion

        /// <summary>
        /// 检查会话 12个小时一次(43200000秒)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tmHeartbeat_Tick(object sender, EventArgs e)
        {
            tmHeartbeat.Enabled = false;
            String url = AppContext.AppConfig.serverUrl + "sys/sysUser/getCurrentDate";
            this.DoWorkAsync(0, (o) => //耗时逻辑处理(此处不能操作UI控件，因为是在异步中)
            {
                String data = HttpClass.httpPost(url);
                return data;

            }, null, (data) => //显示结果（此处用于对上面结果的处理，比如显示到界面上）
            {
                JObject objT = JObject.Parse(data.ToString());
                if (string.Compare(objT["state"].ToString(), "true", true) == 0)
                {
                    tmHeartbeat.Enabled = true;
                }
                else
                {
                    if (MessageBoxUtils.Show(objT["message"].ToString(), MessageBoxButtons.OKCancel, new[] { "重新登录", "退出系统" }) == DialogResult.OK)
                    {
                        Application.Restart();
                    }
                    else
                    {
                        Close();
                    }
                }
            });
        }

        /// <summary>
        /// 多线程异步后台处理某些耗时的数据，不会卡死界面
        /// </summary>
        /// <param name="time">线程延迟多少</param>
        /// <param name="workFunc">Func委托，包装耗时处理（不含UI界面处理），示例：(o)=>{ 具体耗时逻辑; return 处理的结果数据 }</param>
        /// <param name="funcArg">Func委托参数，用于跨线程传递给耗时处理逻辑所需要的对象，示例：String对象、JObject对象或DataTable等任何一个值</param>
        /// <param name="workCompleted">Action委托，包装耗时处理完成后，下步操作（一般是更新界面的数据或UI控件），示列：(r)=>{ datagirdview1.DataSource=r; }</param>
        protected void DoWorkAsync(int time, Func<object, object> workFunc, object funcArg = null, Action<object> workCompleted = null)
        {
            var bgWorkder = new BackgroundWorker();


            //Form loadingForm = null;
            //System.Windows.Forms.Control loadingPan = null;
            bgWorkder.WorkerReportsProgress = true;
            bgWorkder.ProgressChanged += (s, arg) =>
            {
                if (arg.ProgressPercentage > 1) return;

            };

            bgWorkder.RunWorkerCompleted += (s, arg) =>
            {

                try
                {
                    bgWorkder.Dispose();

                    if (workCompleted != null)
                    {
                        workCompleted(arg.Result);
                    }
                }
                catch (Exception ex)
                {
                    cmd.HideOpaqueLayer();
                    throw new Exception(ex.InnerException.Message);
                }
            };

            bgWorkder.DoWork += (s, arg) =>
            {
                bgWorkder.ReportProgress(1);
                var result = workFunc(arg.Argument);
                arg.Result = result;
                bgWorkder.ReportProgress(100);
                Thread.Sleep(time);
            };

            bgWorkder.RunWorkerAsync(funcArg);
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            cmd.rectDisplay = this.DisplayRectangle;
        }
    }
}
