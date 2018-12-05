using System;
using System.Collections.Generic;
using System.Web;
using System.Data;

namespace MyEx
{
    /// <summary>
    /// ModelHelper 的摘要说明
    /// </summary>
    public class ModelHelper
    {
        public ModelHelper()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        public static string GetProPertyName(string code)
        {
            if (!string.IsNullOrEmpty(code))
            {
                return CSA.DAL.DBAccess.getRS("select * from Bs_ProProperty where id = " + code).Rows[0]["name"].ToString();
            }
            return "";
        }

        public static DataTable GetProductByProperty(int id, string code, string[] codes)
        {
            if (codes.Length > 0)
            {
                string sql = "select top 6 * from bs_Products where status = 0 and id != " + id + " and bs_prokindcode = '" + code + "' and (";
                foreach (string item in codes)
                {
                    sql += " ProProperty like '%" + item + "%' or ";
                }
                sql = sql.Substring(0, sql.Length - 3);
                sql += ") order by sortno ";

                return CSA.DAL.DBAccess.getRS(sql);
            }
            else
            {
                return null;
            }
        }

        public static DataTable GetPhotos(string pkid)
        {
            return CSA.DAL.DBAccess.getRS("select * from Bs_NewsAlbums where new_pkid = '" + pkid + "' and type = 'product' and big_img != (select pic from bs_products where pkid = '" + pkid + "')");
        }
    }
}