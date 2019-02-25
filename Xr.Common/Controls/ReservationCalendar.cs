using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Xr.Common.Controls;

namespace Xr.Common.Controls
{
    public partial class ReservationCalendar : UserControl
    {
        private DateTime currentYearMonth ;
        public ReservationCalendar()
        {
            currentYearMonth = new DateTime(System.DateTime.Now.Year, System.DateTime.Now.Month, 1);
            InitializeComponent();
            lab_YM.Text = String.Format("{0}年{1}月", currentYearMonth.Year, currentYearMonth.Month);
            SetGridClanderValue();
            
        }
        //事件处理函数形式，用delegate定义
        public delegate void SelectDateDelegate(DateTime SelectedDate);
        /// <summary>
        /// 选中日期事件
        /// </summary>
        public event SelectDateDelegate SelectDate;

        //事件处理函数形式，用delegate定义
        public delegate void ChangeMonthDelegate(DateTime SelectedMonth);
        /// <summary>
        /// 选中月份事件
        /// </summary>
        public event ChangeMonthDelegate ChangeMonth;

        //事件处理函数形式，用delegate定义
        public delegate void SelectDateTestDelegate(DateTime SelectedDate);
        /// <summary>
        /// 选中日期测试事件
        /// </summary>
        public event SelectDateTestDelegate SelectDateTest;

        private DateTime selectedDate = System.DateTime.Now;

        /// <summary>
        /// 当前选择日期
        /// </summary>
        public DateTime SelectedDate
        {
            get { return selectedDate; }
            set { selectedDate = value; }
        }
        private List<Dictionary<int,DateTime>> ValidDateLists=new List<Dictionary<int,DateTime>>();
        private int monthIndex = 0;
        public void ChangeValidDate(List<Dictionary<int, DateTime>> validDateLists)
        {
            currentYearMonth = new DateTime(System.DateTime.Now.Year, System.DateTime.Now.Month, 1);
            lab_YM.Text = String.Format("{0}年{1}月", currentYearMonth.Year, currentYearMonth.Month);
            ValidDateLists.Clear();
            ValidDateLists = validDateLists;
            monthIndex = 0;
            SetGridClanderValue();
        }
        private Dictionary<int, DateTime> CurrentValidDateList = new Dictionary<int, DateTime>();
        public  void SetGridClanderValue()
        {
            //grid_clanderPanel.Controls.Clear();
           

            List<DateTime> dtList = GetTimeList();
            int i = 0;
            for (int r = 0; r < 6; r++)
            {
                for (int c = 0; c < 7; c++)
                {
                    //ButtonControl b = new ButtonControl();
                    ButtonControl b = grid_clanderPanel.GetControlFromPosition(c, r) as ButtonControl;
                    b.Text = dtList[i].Day.ToString();// +"\r\n" + "666";
                    //b.Tag = dtList[i].Day;
                    b.Font = new Font("黑体", 13f);
                    b.Dock = DockStyle.Fill;
                    b.Margin = new System.Windows.Forms.Padding(0);
                    b.Padding = new System.Windows.Forms.Padding(0);
                    if (c == 0 || c == 6) 
                    {
                        b.Style = ButtonStyle.Calendar_weekend;
                        b.Tag = "weekend";
                    }
                    else
                    {
                        b.Style = ButtonStyle.Calendar_day;
                        b.Tag = "weekday";
                    }
                    //b.MouseDown += new System.Windows.Forms.MouseEventHandler(this._MouseDown);
                    /*Grid.SetRow(b, r);
                    Grid.SetColumn(b, c);
                     */
                    //grid_clanderPanel.Controls.Add(b,c,r);
                    if (dtList[i].Month != currentYearMonth.Month || dtList[i] < System.DateTime.Now.Date)
                    {
                        b.Enabled = false;
                    }
                    else//当月
                    {
                        if (ValidDateLists.Count != 0 && ValidDateLists.Count > monthIndex)
                        {
                            if (!ValidDateLists[monthIndex].ContainsKey(dtList[i].Day))
                            {
                                b.Enabled = false;
                            }
                            else
                            {
                                b.Enabled = true;
                            }
                        }
                        else
                        {
                            b.Enabled = false;
                        }
                    }
                    if (dtList[i].Year == System.DateTime.Now.Year && dtList[i].Month == System.DateTime.Now.Month && dtList[i].Day == System.DateTime.Now.Day)
                    {
                        b.Style = ButtonStyle.Calendar_today;
                    }
                    //b.Click += new RoutedEventHandler(OK);
                    i++;


                }
            }
        }
        private List<DateTime> GetTimeList()
        {
            DateTime dtm = new DateTime(currentYearMonth.Year, currentYearMonth.Month, 1);//当前时间自己设定

            int i = (int)((DayOfWeek)dtm.DayOfWeek);
            //dtm.DayOfWeek.Enum.();//输出结果为Wednesday
            DateTime dtmStart = dtm.AddDays(-i);
            DateTime dtmMonthEnd = dtm.AddMonths(1).AddDays(-1 * (dtm.Day));
            int days = new TimeSpan(dtmMonthEnd.Ticks - dtmStart.Ticks).Days;
            DateTime dtmEnd = dtmMonthEnd.AddDays(42 - days);
            List<DateTime> dtList = new List<DateTime>();
            for (DateTime dt = dtmStart; dt < dtmEnd; dt = dt.AddDays(1))
            {
                dtList.Add(dt);
            }
            return dtList;
        }

        ButtonControl preBtn;
        private void _MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Middle)//屏蔽鼠标中键
            {
                ButtonControl bc = sender as ButtonControl;
                selectedDate = new DateTime(currentYearMonth.Year, currentYearMonth.Month, Int32.Parse(bc.Text));
                if (preBtn != null && preBtn.Style != ButtonStyle.Calendar_today)
                {
                    if (preBtn.Tag.ToString() == "weekday")
                    {
                        preBtn.Style = ButtonStyle.Calendar_day;
                    }
                    else
                    {
                        preBtn.Style = ButtonStyle.Calendar_weekend;
                    }
                }
                preBtn = bc;
                if (bc.Style != ButtonStyle.Calendar_today)
                    bc.Style = ButtonStyle.Save;

                if (e.Button == MouseButtons.Right)
                {
                    contextMenuStrip1.Show(MousePosition.X, MousePosition.Y);
                    //MessageBox.Show(bc.Text);
                }
                SelectDate(selectedDate);
            }
        }

        private void buttonControl1_Click(object sender, EventArgs e)
        {
            SetGridClanderValue();
        }
        private static DateTime NowYearMonth = new DateTime(System.DateTime.Now.Year, System.DateTime.Now.Month, 1);
        private void btn_forward_Click(object sender, EventArgs e)
        {
            if (currentYearMonth > NowYearMonth)
            {
                currentYearMonth = currentYearMonth.AddMonths(-1);
                monthIndex--;
                ChangeMonth(currentYearMonth);
                SetGridClanderValue();
                lab_YM.Text = String.Format("{0}年{1}月", currentYearMonth.Year, currentYearMonth.Month);
                if (currentYearMonth == NowYearMonth)
                {
                    btn_forward.Enabled = false;
                }
                btn_backward.Enabled = true;
            }

        }

        private void btn_backward_Click(object sender, EventArgs e)
        {
            if (currentYearMonth < NowYearMonth.AddMonths(3))
            {
                currentYearMonth = currentYearMonth.AddMonths(1);
                monthIndex++;
                ChangeMonth(currentYearMonth);
                SetGridClanderValue();
                lab_YM.Text = String.Format("{0}年{1}月", currentYearMonth.Year, currentYearMonth.Month);
                if (currentYearMonth == NowYearMonth.AddMonths(3))
                {
                    btn_backward.Enabled = false;
                }
                btn_forward.Enabled = true;
            }
        }

        private void 测试ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectDateTest(selectedDate);
        }
         
    }
}
