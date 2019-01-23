﻿using DevExpress.XtraCharts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xr.RtManager.Utils
{
   public static class ChartToolSocre
    {
        /// <summary>
        /// 创建Series
        /// </summary>
        /// <param name="chat">ChartControl</param>
        /// <param name="seriesName">Series名字『诸如：理论电量』</param>
        /// <param name="seriesType">seriesType『枚举』</param>
        /// <param name="dataSource">数据源</param>
        /// <param name="xBindName">ChartControl的X轴绑定</param>
        /// <param name="yBindName">ChartControl的Y轴绑定</param>
        public static void CreateSeries(this ChartControl chat, string seriesName, ViewType seriesType, object dataSource, string xBindName, string yBindName)
        {
            CreateSeries(chat, seriesName, seriesType, dataSource, xBindName, yBindName, null);
        }
        /// <summary>
        /// 创建Series
        /// </summary>
        /// <param name="chat">ChartControl</param>
        /// <param name="seriesName">Series名字『诸如：理论电量』</param>
        /// <param name="seriesType">seriesType『枚举』</param>
        /// <param name="dataSource">数据源</param>
        /// <param name="xBindName">ChartControl的X轴绑定</param>
        /// <param name="yBindName">ChartControl的Y轴绑定</param>
        /// <param name="createSeriesRule">Series自定义『委托』</param>
        public static void CreateSeries(this ChartControl chat, string seriesName, ViewType seriesType, object dataSource, string xBindName, string yBindName, Action<Series> createSeriesRule)
        {
            if (chat == null)
                throw new ArgumentNullException("chat");
            if (string.IsNullOrEmpty(seriesName))
                throw new ArgumentNullException("seriesType");
            if (string.IsNullOrEmpty(xBindName))
                throw new ArgumentNullException("xBindName");
            if (string.IsNullOrEmpty(yBindName))
                throw new ArgumentNullException("yBindName");

            Series _series = new Series(seriesName, seriesType);
            _series.ArgumentScaleType = ScaleType.Qualitative;
            _series.ArgumentDataMember = xBindName;
            _series.ValueDataMembers[0] = yBindName;

            _series.DataSource = dataSource;
            if (createSeriesRule != null)
                createSeriesRule(_series);
            chat.Series.Clear();
            chat.Series.Add(_series);
        }
        #region Dictionary转换到DataTable(显示统计图所用到的)
        public static DataTable DicToTable(Dictionary<string, string> dicDep)
        {
            DataTable dt = new DataTable();
            dt.Clear();
            dt.Columns.Add("ID", typeof(string));
            dt.Columns.Add("DID", typeof(decimal));
            var target = dicDep.ToList();
            for (int i = 0; i < target.Count; i++)
            {
                DataRow _drNew = dt.NewRow();
                _drNew["ID"] = target[i].Key;
                _drNew["DID"] = target[i].Value;
                dt.Rows.Add(_drNew);
            }
            return dt;
        }
        #endregion
    }
}
