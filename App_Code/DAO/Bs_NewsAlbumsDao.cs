using System;
using System.Collections.Generic;
using System.Web;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.IO;


    public partial class Bs_NewsAlbumsDao
    {
        public Bs_NewsAlbumsDao()
        { }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Bs_NewsAlbums> GetList(string new_pkid, string type)
        {
            List<Bs_NewsAlbums> modelList = new List<Bs_NewsAlbums>();

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,new_pkid,big_img,small_img,remark ");
            strSql.Append(" FROM Bs_NewsAlbums ");
            strSql.Append(" where new_pkid='" + new_pkid + "' and type='" + type + "'");
            DataTable dt = CSA.DAL.DBAccess.getRS(strSql.ToString());

            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Bs_NewsAlbums model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new Bs_NewsAlbums();
                    if (dt.Rows[n]["id"] != null && dt.Rows[n]["id"].ToString() != "")
                    {
                        model.id = int.Parse(dt.Rows[n]["id"].ToString());
                    }
                    if (dt.Rows[n]["new_pkid"] != null && dt.Rows[n]["new_pkid"].ToString() != "")
                    {
                        model.new_pkid = dt.Rows[n]["new_pkid"].ToString();
                    }
                    if (dt.Rows[n]["big_img"] != null && dt.Rows[n]["big_img"].ToString() != "")
                    {
                        model.big_img = dt.Rows[n]["big_img"].ToString();
                    }
                    if (dt.Rows[n]["small_img"] != null && dt.Rows[n]["small_img"].ToString() != "")
                    {
                        model.small_img = dt.Rows[n]["small_img"].ToString();
                    }
                    if (dt.Rows[n]["remark"] != null && dt.Rows[n]["remark"].ToString() != "")
                    {
                        model.remark = dt.Rows[n]["remark"].ToString();
                    }
                    modelList.Add(model);
                }
            }
            return modelList;
        }

        public void AddAlbums(List<Bs_NewsAlbums> albums)
        {
            foreach (Bs_NewsAlbums models in albums)
            {
                string sql = string.Format("insert into Bs_NewsAlbums(new_pkid,big_img,small_img,remark,type) values ('{0}','{1}','{2}','{3}','new')", models.new_pkid, models.big_img, models.small_img, models.remark);
                CSA.DAL.DBAccess.ExecuteNonQuery(sql);
            }
        }

        /// <summary>
        /// 查找不存在的图片并删除已删除的图片及数据
        /// </summary>
        public void DeleteList(string idList, string new_pkid)
        {
            string id_list = DelLastChar(idList.ToString(), ",");
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,big_img,small_img from Bs_NewsAlbums where new_pkid='" + new_pkid + "'");
            if (!string.IsNullOrEmpty(id_list))
            {
                strSql.Append(" and id not in(" + id_list + ")");
            }
            DataTable dt = CSA.DAL.DBAccess.getRS(strSql.ToString());
            foreach (DataRow dr in dt.Rows)
            {
                int rows = CSA.DAL.DBAccess.ExecuteNonQuery("delete from Bs_NewsAlbums where id=" + dr["id"].ToString()); //删除数据库
                if (rows > 0)
                {
                    DeleteFile(dr["big_img"].ToString()); //删除原图
                    DeleteFile(dr["small_img"].ToString()); //删除缩略图
                }
            }
        }

        /// <summary>
        /// 删除相册图片
        /// </summary>
        public void DeleteFile(List<Bs_NewsAlbums> models)
        {
            if (models != null)
            {
                foreach (Bs_NewsAlbums modelt in models)
                {
                    DeleteFile(modelt.big_img);
                    DeleteFile(modelt.small_img);
                }
            }
        }

        #region 删除最后结尾的指定字符后的字符
        /// <summary>
        /// 删除最后结尾的指定字符后的字符
        /// </summary>
        public static string DelLastChar(string str, string strchar)
        {
            if (string.IsNullOrEmpty(str))
                return "";
            if (str.LastIndexOf(strchar) >= 0 && str.LastIndexOf(strchar) == str.Length - 1)
            {
                return str.Substring(0, str.LastIndexOf(strchar));
            }
            return str;
        }
        #endregion

        /// <summary>
        /// 删除单个文件
        /// </summary>
        /// <param name="_filepath">文件相对路径</param>
        public static bool DeleteFile(string _filepath)
        {
            if (string.IsNullOrEmpty(_filepath))
            {
                return false;
            }
            string fullpath = GetMapPath(_filepath);
            if (File.Exists(fullpath))
            {
                File.Delete(fullpath);
                return true;
            }
            return false;
        }

        #region 获得当前绝对路径
        /// <summary>
        /// 获得当前绝对路径
        /// </summary>
        /// <param name="strPath">指定的路径</param>
        /// <returns>绝对路径</returns>
        public static string GetMapPath(string strPath)
        {
            if (strPath.ToLower().StartsWith("http://"))
            {
                return strPath;
            }
            if (HttpContext.Current != null)
            {
                return HttpContext.Current.Server.MapPath(strPath);
            }
            else //非web程序引用
            {
                strPath = strPath.Replace("/", "\\");
                if (strPath.StartsWith("\\"))
                {
                    strPath = strPath.Substring(strPath.IndexOf('\\', 1)).TrimStart('\\');
                }
                return System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, strPath);
            }
        }
        #endregion
    }
