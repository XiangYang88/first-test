using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Reflection;
using System.Diagnostics;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
/// <summary>
/// Util 的摘要说明
/// </summary>
namespace Com
{
    public class Util
    {


        #region 转换操作

        public static string getStringByObject(Object obj)
        {
            if (obj == null)
                return "";
            if (!IsSafeSqlString(obj.ToString()))
                return "";
            obj = Filter(obj.ToString());
            return obj.ToString().Trim();
        }
        /// <summary>
        /// 检测是否有Sql危险字符
        /// </summary>
        /// <param name="str">要判断字符串</param>
        /// <returns>判断结果</returns>
        public static bool IsSafeSqlString(string str)
        {
            return !Regex.IsMatch(str, @"[-|;|,|\/|\(|\)|\[|\]|\}|\{|%|@|\*|!|\']");
        }

        /// <summary>
        /// 检查危险字符
        /// </summary>
        /// <param name="Input"></param>
        /// <returns></returns>
        public static string Filter(string sInput)
        {
            if (sInput == null || sInput == "")
                return null;
            string sInput1 = sInput.ToLower();
            string output = sInput;
            string pattern = @"*|and|exec|insert|select|delete|update|count|master|truncate|declare|char(|mid(|chr(|'";
            if (Regex.Match(sInput1, Regex.Escape(pattern), RegexOptions.Compiled | RegexOptions.IgnoreCase).Success)
            {
                //throw new Exception("字符串中含有非法字符!");
                return null;
            }
            else
            {
                output = output.Replace("'", "''");
            }
            return output;
        }
        public static int getIntByObject(Object obj)
        {
            return ObjToInt(obj, 0);
        }
        /// <summary>
        /// 将对象转换为Int32类型
        /// </summary>
        /// <param name="expression">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的int类型结果</returns>
        public static int ObjToInt(object expression, int defValue)
        {
            if (expression != null)
                return StrToInt(expression.ToString(), defValue);

            return defValue;
        }
        public static int StrToInt(string expression, int defValue)
        {
            if (string.IsNullOrEmpty(expression) || expression.Trim().Length >= 11 || !Regex.IsMatch(expression.Trim(), @"^([-]|[0-9])[0-9]*(\.\w*)?$"))
                return defValue;

            int rv;
            if (Int32.TryParse(expression, out rv))
                return rv;

            return Convert.ToInt32(StrToFloat(expression, defValue));
        }
        /// <summary>
        /// string型转换为float型
        /// </summary>
        /// <param name="strValue">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的int类型结果</returns>
        public static float StrToFloat(string expression, float defValue)
        {
            if ((expression == null) || (expression.Length > 10))
                return defValue;

            float intValue = defValue;
            if (expression != null)
            {
                bool IsFloat = Regex.IsMatch(expression, @"^([-]|[0-9])[0-9]*(\.\w*)?$");
                if (IsFloat)
                    float.TryParse(expression, out intValue);
            }
            return intValue;
        }

        /// <summary>
        /// 获取DataRow
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static DataRow getDrByTable(DataTable dt, int index)
        {
            DataRow dr = null;
            if (dt.Rows.Count > 0 && dt.Rows.Count - 1 >= index)
            {
                dr = dt.Rows[index];
            }
            else
            {
                dr = dt.NewRow();
            }
            return dr;
        }

        public static DataRow getDrByTable(DataTable dt)
        {
            return getDrByTable(dt, 0);
        }


        #endregion

        #region 将DataRow转换成指定类型

        /// 将DataRow转换成指定类型  
        /// 实体类  
        /// <summary>
        /// 将DataRow转换成实体
        /// </summary>
        /// <param name="obj">实体</param>
        /// <param name="row">数据表一行数据</param>
        public static void ConvertToEntity(object obj, DataRow row)
        {
            ///得到obj的类型
            Type type = obj.GetType();
            ///返回这个类型的所有公共属性
            PropertyInfo[] infos = type.GetProperties();
            ///循环公共属性数组
            foreach (PropertyInfo info in infos)
            {
                ///返回自定义属性数组
                object[] attributes = info.GetCustomAttributes(typeof(DataContextAttribute), false);
                ///将自定义属性数组循环
                foreach (DataContextAttribute attribute in attributes)
                {
                    ///如果DataRow里也包括此列
                    if (row.Table.Columns.Contains(attribute.Property))
                    {
                        ///将DataRow指定列的值赋给value
                        object value = row[attribute.Property];
                        ///如果value为null则返回
                        ///
                        Debug.Print(info.PropertyType.ToString());
                        if (value == DBNull.Value) continue;
                        ///将值做转换
                        if (info.PropertyType.Equals(typeof(string)))
                        {
                            value = row[attribute.Property].ToString();
                        }
                        else if (info.PropertyType.Equals(typeof(Int32)))
                        {
                            value = Convert.ToInt32(row[attribute.Property]);
                        }
                        else if (info.PropertyType.Equals(typeof(System.Nullable<Int32>)))
                        {
                            value = Convert.ToInt32(row[attribute.Property]);
                        }
                        else if (info.PropertyType.Equals(typeof(Int64)))
                        {
                            value = Convert.ToInt64(row[attribute.Property]);
                        }
                        else if (info.PropertyType.Equals(typeof(System.Nullable<Int64>)))
                        {
                            value = Convert.ToInt64(row[attribute.Property]);
                        }
                        else if (info.PropertyType.Equals(typeof(decimal)))
                        {
                            value = Convert.ToDecimal(row[attribute.Property]);
                        }
                        else if (info.PropertyType.Equals(typeof(DateTime)))
                        {
                            value = Convert.ToDateTime(row[attribute.Property]);
                        }
                        else if (info.PropertyType.Equals(typeof(double)))
                        {
                            value = Convert.ToDouble(row[attribute.Property]);
                        }
                        else if (info.PropertyType.Equals(typeof(bool)))
                        {
                            value = Convert.ToBoolean(row[attribute.Property]);
                        }
                        ///利用反射自动将value赋值给obj的相应公共属性
                        info.SetValue(obj, value, null);
                    }
                }
            }
        }


        public static void ConvertToEntity(object obj, DataTable dt, int index)
        {
            if (dt.Rows.Count > 0)
            {
                ConvertToEntity(obj, dt.Rows[index]);
            }
        }

        public static void ConvertToEntity(object obj, DataTable dt)
        {
            ConvertToEntity(obj, dt, 0);
        }

        #endregion

        #region 字符操作


        /// <summary>
        /// 分割字符串,并返回指定值
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="index"></param>
        /// <param name="itemSp"></param>
        /// <returns></returns>
        public static string getSplitStr(Object obj, int index, char itemSp)
        {
            return obj.ToString().Split(itemSp)[index];
        }

        public static string getSplitStr(Object obj, int index)
        {
            return obj.ToString().Split(Const.ItemSp)[index];
        }

        public static string getSplitStr(Object obj)
        {
            return getSplitStr(obj, 0);
        }


        //public static bool IsNumeric(Object obj)
        //{
        //    if (obj == null)
        //    {
        //        return false;
        //    }

        //    System.Text.RegularExpressions.Regex reg1
        //        = new System.Text.RegularExpressions.Regex(@"^[-]?d+[.]?d*$");
        //    return reg1.IsMatch(obj.ToString());

        //}



        //读取txt文件的内容
        public static string GetFileContent(string strfile)
        {
            string strout;
            strout = "";
            if (!File.Exists(System.Web.HttpContext.Current.Server.MapPath(strfile)))
            {
            }
            else
            {
                StreamReader sr = new StreamReader(System.Web.HttpContext.Current.Server.MapPath(strfile), System.Text.Encoding.Default);
                String input = sr.ReadToEnd();
                sr.Close();
                strout = input;
            }
            return strout;
        }




        /// <summary>
        /// 截取字符串
        /// </summary>
        /// <param name="str"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static string getSubStr(string str, int size)
        {
            if (str.Length > size)
            {
                return str.Substring(0, size) + "...";
            }
            else
            {
                return str;
            }
        }

        /// <summary>
        /// 改变当前UrlParam值
        /// </summary>
        /// <param name="param"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string getUrlUpdateParam(string param, string value)
        {
            string result = "";
            string strUrl = CurInfo.CurUrlPath;
            strUrl = strUrl.ToLower();
            param = param.ToLower();
            if (strUrl.IndexOf('?') > 0)
            {
                strUrl = strUrl.Substring(strUrl.IndexOf('?') + 1);
                if (strUrl.Contains(param)) //更新参数
                {
                    int start = 0;
                    int end = 0;
                    start = strUrl.IndexOf(param);
                    end = strUrl.IndexOf('&', start);
                    if (value != "")
                    {
                        result = strUrl.Substring(0, start) + param + "=" + value;
                    }
                    else
                    {
                        result = strUrl.Substring(0, start);
                        int i = result.LastIndexOf('&');
                        if (result.LastIndexOf('&') > 0)
                        {
                            result = result.Remove(result.LastIndexOf('&'));
                        }
                    }
                    if (end > 0)
                    {
                        result += strUrl.Substring(end);
                    }
                }
                else
                {   //添加参数
                    if (value != "")
                    {
                        if (strUrl.Length > 0)
                        {
                            result = strUrl + "&";
                        }
                        result += param + "=" + value;
                    }
                }
            }
            else
            {
                //不存在任何参数
                result = param + "=" + value;
            }
            return result;
        }

        /// <summary>
        /// 删除值在字符串数组中
        /// </summary>
        /// <param name="strArr">字符串数组</param>
        /// <param name="value">删除值</param>
        /// <param name="itemSp">字符串分割</param>
        /// <returns></returns>
        public static string removeValInArrToStr(Array array, string value, char itemSp)
        {
            string strResult = "";
            if (array.Length > 0)
            {
                foreach (string str in array)
                {
                    if (str != value)
                    {
                        strResult += str + itemSp;
                    }
                }
                if (strResult.Length > 0)
                {
                    strResult = strResult.Substring(0, strResult.Length - 1);
                }
            }
            return strResult;
        }


        /// <summary>
        /// 新增值在字符串数组中
        /// </summary>
        /// <param name="strArr">字符串数组</param>
        /// <param name="value">新增值</param>
        /// <param name="itemSp">字符串分割</param>
        /// <returns></returns>
        public static string addValInArrToStr(Array array, string value, char itemSp)
        {
            string strResult = "";
            bool hasValue = false;

            if (array.Length > 0)
            {
                foreach (string str in array)
                {
                    if (str == value)
                    {
                        hasValue = true;
                    }
                    strResult += str + itemSp;
                }
                if (strResult.Length > 0)
                {
                    strResult = strResult.Substring(0, strResult.Length - 1);
                }
                if (hasValue == false)
                {
                    if (strResult.Trim().Length > 0)
                    {
                        strResult += itemSp + value;
                    }
                    else
                    {
                        strResult = value;
                    }

                }
            }
            else
            {
                strResult = value;
            }
            return strResult;
        }


        /// <summary>
        /// 获取并删除Url指定值
        /// </summary>
        /// <param name="param"></param>
        /// <param name="org_value"></param>
        /// <returns></returns>
        public static string getProListFilterDelUrlParam(string param, string org_value, string remove_value)
        {
            string value = removeValInArrToStr(org_value.Split(Com.Const.ItemSp), remove_value, Com.Const.ItemSp);
            return getUrlUpdateParam(param, value);
        }

        /// <summary>
        /// 获取并增加Url指定值
        /// </summary>
        /// <param name="param"></param>
        /// <param name="org_value"></param>
        /// <returns></returns>
        public static string getProListFilterAddUrlParam(string param, string org_value, string add_value)
        {
            string value = addValInArrToStr(org_value.Split(Com.Const.ItemSp), add_value, Com.Const.ItemSp);
            return getUrlUpdateParam(param, value);
        }



        #endregion

        /// <summary>
        /// 判断Object是否数值
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsNumeric(Object obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (obj.ToString() == null || obj.ToString().Length == 0)
                return false;
            foreach (char c in obj.ToString())
            {
                if (!Char.IsNumber(c))
                {
                    return false;
                }
            }
            return true;
        }
        public static string UnSafeHTMLFilter(string Htmlstring) //去除HTML标记   
        {
            //删除脚本   
            Htmlstring = Regex.Replace(Htmlstring, @"<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);
            //删除HTML   
            Htmlstring = Regex.Replace(Htmlstring, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"([/r/n])[/s]+", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"-->", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"<!--.*", "", RegexOptions.IgnoreCase);

            Htmlstring = Regex.Replace(Htmlstring, @"&(quot|#34);", "/", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(lt|#60);", "<", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(gt|#62);", ">", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(nbsp|#160);", " ", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(iexcl|#161);", "/xa1", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(cent|#162);", "/xa2", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(pound|#163);", "/xa3", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(copy|#169);", "/xa9", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&#(/d+);", "", RegexOptions.IgnoreCase);

            Htmlstring.Replace("<", "");
            Htmlstring.Replace(">", "");
            Htmlstring.Replace("/r/n", "");
            Htmlstring = HttpContext.Current.Server.HtmlEncode(Htmlstring).Trim();

            return Htmlstring;
        }
    }



}