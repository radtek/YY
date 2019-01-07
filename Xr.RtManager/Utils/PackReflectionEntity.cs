﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xr.RtManager
{

    /// <summary>
    /// 实体类、字符串互相转换
    /// </summary>
    public class PackReflectionEntity<T>
    {
        /// <summary>
        /// 将实体类通过反射组装成字符串
        /// </summary>
        /// <param name="t">实体类</param>
        /// <returns>组装的字符串</returns>
        public static string GetEntityToString(T t)
        {
            System.Text.StringBuilder sb = new StringBuilder();
            Type type = t.GetType();
            System.Reflection.PropertyInfo[] propertyInfos = type.GetProperties();
            for (int i = 0; i < propertyInfos.Length; i++)
            {
                sb.Append(propertyInfos[i].Name + ":" + propertyInfos[i].GetValue(t, null) + ",");
            }
            return sb.ToString().TrimEnd(new char[] { ',' });
        }

        /// <summary>
        /// 将实体类通过反射组装成请求参数字符串
        /// </summary>
        /// <param name="t">实体类</param>
        /// <returns>组装的字符串</returns>
        public static string GetEntityToRequestParameters(T t)
        {
            System.Text.StringBuilder sb = new StringBuilder();
            Type type = t.GetType();
            System.Reflection.PropertyInfo[] propertyInfos = type.GetProperties();
            for (int i = 0; i < propertyInfos.Length; i++)
            {
                if (!propertyInfos[i].Name.Equals("userList")){
                    sb.Append("&" + propertyInfos[i].Name + "=" + propertyInfos[i].GetValue(t, null));
                }
                
                //var ss = propertyInfos[i]
                
               
            }
            return sb.ToString().TrimStart(new char[] { '&' });
        }

        /// <summary>
        /// 将实体类通过反射组装成请求参数字符串
        /// </summary>
        /// <param name="t">实体类</param>
        /// <param name="t">是否使用注解进行转换或过滤</param>
        /// <returns>组装的字符串</returns>
        public static string GetEntityToRequestParameters(T t, bool flag)
        {
            System.Text.StringBuilder sb = new StringBuilder();
            Type type = t.GetType();
            System.Reflection.PropertyInfo[] propertyInfos = type.GetProperties();
            for (int i = 0; i < propertyInfos.Length; i++)
            {
                if (flag)
                {
                    object[] opObj = propertyInfos[i].GetCustomAttributes(typeof(ObjectPoint), true);
                    object[] ipObj = propertyInfos[i].GetCustomAttributes(typeof(IgnoreParam), true);
                    if (opObj.Count() > 0)
                    {
                        ObjectPoint objectPoint = opObj[0] as ObjectPoint;
                        sb.Append("&" + objectPoint.ColumnName + "=" + propertyInfos[i].GetValue(t, null));
                    }else if(ipObj.Count()>0){

                    }
                    else
                    {
                        sb.Append("&" + propertyInfos[i].Name + "=" + propertyInfos[i].GetValue(t, null));
                    }
                }
                else
                {
                    sb.Append("&" + propertyInfos[i].Name + "=" + propertyInfos[i].GetValue(t, null));
                }
            }
            return sb.ToString().TrimStart(new char[] { '&' });
        }

        /// <summary>
        /// 将反射得到字符串转换为对象
        /// </summary>
        /// <param name="str">反射得到的字符串</param>
        /// <returns>实体类</returns>
        public static T GetEntityStringToEntity(string str)
        {
            string[] array = str.Split(',');
            string[] temp = null;
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            foreach (string s in array)
            {
                temp = s.Split(':');
                dictionary.Add(temp[0], temp[1]);
            }
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetAssembly(typeof(T));
            T entry = (T)assembly.CreateInstance(typeof(T).FullName);
            System.Text.StringBuilder sb = new StringBuilder();
            Type type = entry.GetType();
            System.Reflection.PropertyInfo[] propertyInfos = type.GetProperties();
            for (int i = 0; i < propertyInfos.Length; i++)
            {
                foreach (string key in dictionary.Keys)
                {
                    if (propertyInfos[i].Name == key.ToString())
                    {
                        propertyInfos[i].SetValue(entry, GetObject(propertyInfos[i], dictionary[key]), null);
                        break;
                    }
                }
            }
            return entry;
        }
        /// <summary>
        /// 转换值的类型
        /// </summary>
        /// <param name="p"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        static object GetObject(System.Reflection.PropertyInfo p, string value)
        {
            switch (p.PropertyType.Name.ToString().ToLower())
            {
                case "int16":
                    return Convert.ToInt16(value);
                case "int32":
                    return Convert.ToInt32(value);
                case "int64":
                    return Convert.ToInt64(value);
                case "string":
                    return Convert.ToString(value);
                case "datetime":
                    return Convert.ToDateTime(value);
                case "boolean":
                    return Convert.ToBoolean(value);
                case "char":
                    return Convert.ToChar(value);
                case "double":
                    return Convert.ToDouble(value);
                default:
                    return value;
            }
        }
    }

}
