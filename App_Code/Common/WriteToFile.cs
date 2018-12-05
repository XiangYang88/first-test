using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.IO;
using System.Text;
namespace Common
{
    /// <summary>
    ///WriteToFile 的摘要说明
    /// </summary>
    public class WriteToFile
    {
        /// <summary>
        /// 写数据到文件
        /// </summary>
        /// <param name="strDataList">行数据列表</param>
        /// <param name="filename">文件名</param>
        /// <returns>是否成功</returns>
        public static bool WriteToTxt(string str, string filePath)
        {
            string fileName = HttpContext.Current.Server.MapPath(filePath);
            StreamWriter sw = null;
            FileStream oFileStream = null;
            string[] vtype = str.Split('.');
            if (vtype.Length < 2)
                return false;
            try
            {
                if (!System.IO.File.Exists(fileName))
                {
                    oFileStream = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
                }
                else
                {
                    oFileStream = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite);
                }

                sw = new StreamWriter(oFileStream, Encoding.Default);
                sw.Write(str);
                sw.WriteLine();
                return true;
            }
            catch (IOException ee)
            {
                HttpContext.Current.Response.Write("<script>alert(" + ee.Message + ")</script>");
                return false;
            }
            finally
            {
                if (sw != null)
                {
                    sw.Close();
                    oFileStream.Close();
                }
            }
        }

    }
}
