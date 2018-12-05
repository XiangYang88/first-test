using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
/// <summary>
/// Bs_ProDao 的摘要说明
/// </summary>
public class Bs_ProDao
{

    /// <summary>
    /// 获取商品
    /// </summary>
    /// <param name="proId"></param>
    /// <returns></returns>
    public DataTable getProById(int proId)
    {
        string strSql = "select * from bs_products  where id = " + proId;
        return CSA.DAL.DBAccess.getRS(strSql);
    }
    /// <summary>
    /// 获取礼品内容
    /// </summary>
    /// <param name="code">编号</param>
    /// <returns></returns>
    public DataTable getGiftById(string code)
    {
        string strSql = string.Format("select * from Bs_Gift where code='{0}'", code);
        return CSA.DAL.DBAccess.getRS(strSql);
    }

    /// <summary>
    /// 获取商品
    /// </summary>
    /// <param name="pro_name">商品Code</param>
    /// <returns></returns>
    public DataTable getProByCode(string pro_code)
    {
        string strSql = "";
        if (pro_code.Length > 0)
        {
            strSql = string.Format("select * from bs_products  where code = '{0}'", pro_code);
        }
        else
        {
            strSql = string.Format("select * from bs_products order by sortno ", pro_code);
        }
        return CSA.DAL.DBAccess.getRS(strSql);
    }

    /// <summary>
    /// 获取商品属性
    /// </summary>
    /// <param name="prop_type">属性名</param>
    /// <param name="pro_id"></param>
    /// <returns></returns>
    public DataTable getPropByProId(string prop_type, int pro_id)
    {
        string strSql = string.Format("select prop.* from bs_products pro " +
                            " inner join Bs_Pro_m2m_Bs_Pro{0} pp on pp.pro_id = pro.id " +
                            " inner join Bs_ProProperty prop on prop.id = pp.prop_{0}_id " +
                            " where pro.id = {1} ", prop_type, pro_id);
        return CSA.DAL.DBAccess.getRS(strSql);
    }


    /// <summary>
    /// 添加商品点击
    /// </summary>
    /// <param name="proid"></param>
    public void addProHits(int proid)
    {
        string strSql = string.Format("update  bs_products  set hits = hits +1  where id = {0} ", proid);
        CSA.DAL.DBAccess.ExecuteNonQuery(strSql);
    }

    /// <summary>
    /// 添加商品评论
    /// </summary>
    /// <param name="userid"></param>
    /// <param name="proid"></param>
    /// <param name="name"></param>
    /// <param name="content"></param>
    public void addProComment(int userid, int proid,int grade, string name, string content)
    {
        CSA.DAL.SQLBuilder builder = new CSA.DAL.SQLBuilder();
        builder.TblName = "Bs_ProComments";
        builder.AddData("Bs_UserID", userid);
        builder.AddData("Bs_ProductsID", proid);
        builder.AddData("score", grade);
        builder.AddData("title", name);
        builder.AddData("Content", content);
        builder.AddData("ip", CSA.HC.Common.getIP());
        builder.AddData("AddTime", DateTime.Now.ToString("s"));
        builder.AutoInsert();
    }
    /// <summary>
    /// 添加商品咨询
    /// </summary>
    /// <param name="userid">用户ID</param>
    /// <param name="proid">商品ID</param>
    /// <param name="name">标题</param>
    /// <param name="content">内容</param>
    public void addProquest(int userid, int proid, string name, string content)
    {
        CSA.DAL.SQLBuilder builder = new CSA.DAL.SQLBuilder();
        builder.TblName = "Bs_Proquest";
        builder.AddData("Bs_UserID", userid);
        builder.AddData("Bs_ProductsID", proid);
        builder.AddData("title", name);
        builder.AddData("content", content);
        builder.AddData("Addtime", DateTime.Now.ToString("s"));
        builder.AutoInsert();
    }

    /// <summary>
    /// 获取商品评论
    /// </summary>
    /// <param name="proid"></param>
    /// <param name="size"></param>
    /// <returns></returns>
    public DataTable getProComment(int proid, int size)
    {
        string strTop = "";
        if (size > 0)
        {
            strTop = " top " + size;
        }
        string strSql = string.Format("select {0} a.*,b.name as name2 ,b.Code from Bs_ProComments a inner join Bs_user b on a.Bs_userid=b.id where a.Bs_ProductsID={1} and a.status=0 order by a.addtime desc ", strTop, proid);
        return CSA.DAL.DBAccess.getRS(strSql);
    }
    /// <summary>
    /// 根据用户获取商品评论
    /// </summary>
    /// <param name="userid">用户id</param>
    /// <returns></returns>
    public DataTable getProCommnet(int userid)
    {
        string strSql = string.Format("select * from Bs_ProComments where Bs_UserID={0}  order by addtime desc ", userid);
        return CSA.DAL.DBAccess.getRS(strSql);
    }

    /// <summary>
    /// 获取商品咨询
    /// </summary>
    /// <param name="proid">商品ID</param>
    /// <param name="size">条数</param>
    /// <returns></returns>
    public DataTable getProquest(int proid, int size)
    {
        string strTop = "";
        if (size > 0)
        {
            strTop = "top " + size;
        }
        string strsql = string.Format("select {0} a.*,b.name,b.Code from Bs_proquest a inner join Bs_user b on a.Bs_userid=b.id where a.Bs_ProductsID={1} order by a.addtime desc ", strTop, proid);
        return CSA.DAL.DBAccess.getRS(strsql);
    }

    /// <summary>
    /// 根据用户获取商品咨询
    /// </summary>
    /// <param name="userid">用户id</param>
    /// <returns></returns>
    public DataTable getProquest(int userid)
    {
        string strSql = string.Format("select * from  Bs_proquest where Bs_UserID={0}  order by addtime desc ", userid);
        return CSA.DAL.DBAccess.getRS(strSql);
    }

    /// <summary>
    /// 获取商品列表
    /// </summary>
    /// <param name="filter">过滤</param>
    /// <param name="pageIndex">当前页</param>
    /// <param name="pageSize">显示数</param>
    /// <param name="recordCound">记录数</param>
    /// <returns></returns>
    public DataTable getProList(Bs_Products filter, int pageIndex, int pageSize, out int recordCound)
    {

        string strSql = "select * from bs_products pro where 1= 1 " + getSqlWhere(filter);


        DataTable dt = CSA.DAL.DBAccess.getRS(strSql);
        recordCound = dt.Rows.Count;
        return CSA.DAL.PageHelper.getPage(dt, pageSize, pageIndex);
    }

   /// <summary>
   /// 获取当前商品品牌
   /// </summary>
   /// <param name="filter"></param>
   /// <returns></returns>
    public string getBandlist(Bs_Products filter)
    {
        string strsql = "select  distinct bra.code,bra.name  from bs_Products pro inner join Bs_brand bra on pro.Bs_BrandCode=bra.code where 1=1 " + getSqltbWhere(filter, "pro"); ;
        return  strsql;
    }

    private string getSqltbWhere(Bs_Products filter, string tbl)
    {
        StringBuilder strb = new StringBuilder();
        if (!string.IsNullOrEmpty(filter.Bs_BrandCode))
        {
            strb.Append(string.Format(" and bra.code like '{1}%' ", tbl,filter.Bs_BrandCode));
        }
        if (!string.IsNullOrEmpty(filter.BS_ProKindCode))
        {
            strb.Append(string.Format(" and {0}.BS_ProKindCode like '{1}%' ",tbl, filter.BS_ProKindCode));
         
        }
        if (filter.Status != null)
        {
            strb.Append(string.Format(" and {0}.Status = {1} ", tbl, filter.Status));
        }
        return strb.ToString();
    }
    private  string getSqlWhere(Bs_Products filter)
    {
        string strSubSql = "";
        StringBuilder strb = new StringBuilder();
        if (!string.IsNullOrEmpty(filter.Bs_BrandCode))
        {
            strb.Append(string.Format(" and Bs_BrandCode like '{0}%' ",filter.Bs_BrandCode));
        }
        if (!string.IsNullOrEmpty(filter.BS_ProKindCode))
        {
            strb.Append(string.Format(" and BS_ProKindCode like '{0}%' ", filter.BS_ProKindCode));
        }
        if (!string.IsNullOrEmpty(filter.Name))
        {
            strb.Append(string.Format(" and Name like '%{0}%' ", filter.Name));
        }
        if (filter.keywords != null)
        { 
            strb.Append(string.Format("and (Name like '%{0}%' or code like '%{0}%'  )",filter.keywords));
        }

        if (filter.Status != null)
        {
            strb.Append(string.Format(" and Status = {0} ", filter.Status));
        }


        if (!string.IsNullOrEmpty(filter.Color))
        {
            strSubSql = string.Format("select p.id from bs_products p " +
                        "inner join Bs_Pro_m2m_Bs_ProColor pc on pc.pro_id = p.id " +
                        "where pc.prop_color_id in ({0}) " +
                        "group by p.id ",filter.Color);
            strb.Append(string.Format(" and pro.id in ({0}) ", strSubSql));
        }
        if (filter.isHotSales == 1)
        {
            strb.Append(string.Format(" and pro.isHotSales  = {0} ", filter.isHotSales));
        }

        if (filter.isNew == 1)
        {
            strb.Append(string.Format(" and pro.isNew = {0} ", filter.isNew));
        }

        if (filter.isCommend == 1)
        {
            strb.Append(string.Format(" and pro.isCommend = {0} ", filter.isCommend));
        }

        if (filter.isIndex == 1)
        {
            strb.Append(string.Format(" and pro.isIndex = {0} ", filter.isIndex));
        }

        if (filter.isPromote == 1)
        {
            strb.Append(string.Format(" and pro.isPromote = {0} ", filter.isPromote));
        }


        if (filter.isSoldOut == 1)
        {
            strb.Append(string.Format(" and pro.isSoldOut = {0} ", filter.isSoldOut));
        }
        if (!string.IsNullOrEmpty(filter.orderby))
        {
            if (filter.orderby == "price_up")
            {
                strb.Append(" order by Price");
            }

            if (filter.orderby == "price_down")
            {
                strb.Append("order by Price desc");
            }
            if (filter.orderby == "time_down")
            {
                strb.Append(" order by Addtime desc");
            }
        }
        else
        {
            strb.Append(" order by sale desc");
        }

        return strb.ToString();

    }


    /// <summary>
    /// 获取商品分类导航
    /// </summary>
    /// <param name="level">分类编码</param>
    /// <returns></returns>
    public DataTable getProKindNavInCode(string code)
    {
        //string strSql = string.Format("select * from bs_prokind where rtrim(code) like '{0}%' and (length(rtrim(code)) < {1} or rtrim(code) = '{2}') order by code", code.Substring(0, 2), code.Length, code);
        string strSql = string.Format("select * from bs_prokind where code = '{0}'  {1}  order by code",  code ,getKindNavSql(code));
        return CSA.DAL.DBAccess.getRS(strSql);
    }

    private string getKindNavSql(string code)
    {
        string strWhere = " ";
        for (int i = 0; i < code.Length / 2; i++)
        {
                strWhere += string.Format(" or code ='{0}'  ", code.Substring(0, code.Length - i * 2));
        }
        
        return strWhere;

    }



    /// <summary>
    /// 获取商品分类列表
    /// </summary>
    /// <param name="level">获取深度 0:无限制</param>
    /// <returns></returns>
    public DataTable getProKindList(int level)
    {
        int length = level * 2;
        if (level == 0)
        {
            length = 10;    //最大长度10
        }
        string strSql = string.Format("select * from bs_prokind where len(code) <= {0}  order by code", length );
        return CSA.DAL.DBAccess.getRS(strSql);


    }

    /// <summary>
    /// 获取商品分类列表
    /// </summary>
    /// <returns></returns>
    public DataTable getProKindListInCode(string code, int level)
    {
        int length = level * 2;
        if (level == 0)
        {
            length = 10;    //最大长度10
        }
        string strSql = string.Format("select * from bs_prokind where code like '{0}%' and trim(code) <> '{0}' and len(trim(code)) = {1} order by sortno", code, length);
        return CSA.DAL.DBAccess.getRS(strSql);
    }


    /// <summary>
    /// 获取商品热门搜索
    /// </summary>
    /// <param name="code"></param>
    /// <returns></returns>
    public DataTable getProHotSearch(string code, int size)
    {
        string strTop = "";
        if (size > 0)
        {
            strTop = " top " + size;
        }
        string strSql = string.Format("select {0} * from sy_code where code like '{1}%'  order by sortno",strTop, code);
        return CSA.DAL.DBAccess.getRS(strSql);
    }

    /// <summary>
    /// 获取商品品牌列表
    /// </summary>
    /// <param name="level">获取深度 0:无限制</param>
    /// <returns></returns>
    public DataTable getProBrandList(int level)
    {
        int length = level * 2;
        if (level == 0)
        {
            length = 10;    //最大长度10
        }
        string strSql = string.Format("select * from bs_brand where len(code) <= {0}  order by code", length);
        return CSA.DAL.DBAccess.getRS(strSql);
    }

    /// <summary>
    /// 获取商品品牌列表
    /// </summary>
    /// <returns></returns>
    public DataTable getProBrandListInCode(string code, int level)
    {
        int length = level * 2;
        if (level == 0)
        {
            length = 10;    //最大长度10
        }
        string strSql = string.Format("select * from bs_brand where code like '{0}%'  and len(code) <= {1} order by sortno", code, length);
        return CSA.DAL.DBAccess.getRS(strSql);
    }


    /// <summary>
    /// 获取商品品牌
    /// </summary>
    /// <param name="code">编码</param>
    /// <returns></returns>
    public DataTable getProBrandByCode(string code)
    {
        string strSql = string.Format("select * from bs_brand where rtrim(code) = '{0}' ", code);
        return CSA.DAL.DBAccess.getRS(strSql);
    }

    /// <summary>
    /// 获取商品属性
    /// </summary>
    /// <param name="code">属性编码</param>
    /// <returns></returns>
    public DataTable getProProperty(string code)
    {
        string strSql = string.Format("select * from bs_proproperty where code like '{0}%' and rtrim(code) <>  '{0}'  order by sortno ", code);
        return CSA.DAL.DBAccess.getRS(strSql);
    }
    /// <summary>
    /// 获取商品属性
    /// </summary>
    /// <param name="code">属性编码</param>
    /// <returns></returns>
    public DataTable getProPropertyById(int propid)
    {
        string strSql = string.Format("select * from bs_proproperty where id = {0}", propid);
        return CSA.DAL.DBAccess.getRS(strSql);
    }

    /// <summary>
    /// 获取商品颜色
    /// </summary>
    /// <param name="code">颜色类别</param>
    /// <param name="propid">颜色ID</param>
    /// <returns></returns>
    public DataTable getProcolorBycodeandByid(string code, int propid)
    {
        string strSql = string.Format("select * from bs_proproperty where code like'{0}%' and  id = {1}",code, propid);
        return CSA.DAL.DBAccess.getRS(strSql);
    }
    /// <summary>
    /// 获取相同商品
    /// </summary>
    /// <param name="name">商品名称</param>
    /// <param name="propid">当前商品的id(不包括相同的)</param>
    /// <returns></returns>
    public DataTable getallprod(string name,int propid)
    {
        string strsql = string.Format("select * from Bs_products where name='{0}'and id<>{1} order by sortno desc,id desc",name,propid);
        return CSA.DAL.DBAccess.getRS(strsql);
    }
    /// <summary>
    /// 获取商品列表
    /// </summary>
    /// <param name="strWhere">条件</param>
    /// <param name="size">显示数目</param>
    /// <returns></returns>
    public DataTable getProList(string strWhere , int size)
    {
        string strTop = "";
        if (size > 0)
        {
            strTop = " top " + size;
        }
        if (strWhere.Trim() != "")
        {
            strWhere = " and " + strWhere;
        }
        string strSql = string.Format("select {0} * from bs_products pro where pro.status = 0  {1} order by addtime desc ", strTop, strWhere);
        return CSA.DAL.DBAccess.getRS(strSql);  
    }



    /// <summary>
    /// 获取商品列表
    /// </summary>
    /// <param name="strWhere">条件</param>
    /// <param name="size">显示数目</param>
    /// <returns></returns>
    public DataTable getProList(Bs_Products filter, int size)
    {
        string strTop = "";
        if (size > 0)
        {
            strTop = " top " + size;
        }
        string strSql = string.Format("select {0} * from bs_products pro where pro.status = 0  {1} order by addtime desc ", strTop, getSqlWhere(filter));
        return CSA.DAL.DBAccess.getRS(strSql);
    }



    /// <summary>
    /// 获取商品列表
    /// </summary>
    /// <param name="strWhere">条件</param>
    /// <param name="size">显示数目</param>
    /// <returns></returns>
    public DataTable getProList(string strWhere, int size, string order)
    {
        string strTop = "";
        if (size > 0)
        {
            strTop = " top " + size;
        }
        if (strWhere.Trim() != "")
        {
            strWhere = " and " + strWhere;
        }

        if (order.Trim() == "")
        {
            order = " addtime desc ";
        }
        string strSql = string.Format("select {0} * from bs_products pro where pro.status = 0  {1} order by {2} ", strTop, strWhere, order);
        return CSA.DAL.DBAccess.getRS(strSql);
    }
    /// <summary>
    /// 获取礼品列表
    /// </summary>
    /// <param name="size">条数</param>
    /// <param name="pageIndex">当前页</param>
    /// <param name="pageSize">显示数</param>
    /// <param name="recordcound">记录数</param>
    /// <returns></returns>
    public DataTable getgifts(int size,int pageIndex, int pageSize, out int recordCound)
    {
        string strsql = "";
        string strTop = "";
        if (size > 0)
        {
            strTop = " top " + size;
        }
        strsql = string.Format("select {0} * from Bs_Gift order by  sortno ",strTop);

        DataTable dt = CSA.DAL.DBAccess.getRS(strsql);
        recordCound = dt.Rows.Count;
        return CSA.DAL.PageHelper.getPage(dt, pageSize, pageIndex);
    }
    /// <summary>
    /// 获取礼品
    /// </summary>
    /// <returns></returns>
    public DataTable getgifts(int size)
    {
        string strsql = "";
        string strTop = "";
        if (size > 0)
        {
            strTop = " top " + size;
        }

        strsql = string.Format("select {0} * from Bs_Gift where status=0  order by sortno ", strTop);
        return CSA.DAL.DBAccess.getRS(strsql);
        
    }
    /// <summary>
    /// 根据条数返回社品列表
    /// </summary>
    /// <param name="size">条数</param>
    /// <returns></returns>
    public DataTable getgiftsbysize(int size)
    {
        string strsql = "";
        string strTop = "";
        if (size > 0)
        {
            strTop = " top " + size;
        }
        strsql = string.Format("select {0} * from Bs_Gift order by  sortno ", strTop);

        return CSA.DAL.DBAccess.getRS(strsql);
    }

    /// <summary>
    /// 根据产品id，获取限时或自定义或参考价和打折
    /// </summary>
    /// <param name="proid"> 产品id</param>
    /// <returns></returns>
    public string  timeprice(int proid)
    {
        string sql = "select a.timePrice from bs_timeProd a  left join bs_timekind b on a.Bs_ProtimeCode=b.id where b.etime>'{0}' and a.Proid={1}";
        sql = string.Format(sql, DateTime.Now.ToString("s"), proid);
        DataTable dt = CSA.DAL.DBAccess.getRS(sql);
        if (dt.Rows.Count > 0)
        {
            return dt.Rows[0]["timeprice"] + "/" + "0";
        }
        else
        {
            DataTable  pro =getProById(proid);
            if (pro.Rows[0]["Pstatus"].ToString() == "1")
            {

                return pro.Rows[0]["Pricein"].ToString() + "/" + "0"; 

            }
            else
            {
                return pro.Rows[0]["price"].ToString() + "/" + "1"; ;
            }

        }

    }
}
