using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Bs_ProBLL 的摘要说明
/// </summary>
public class Bs_ProBLL
{

    private readonly Bs_ProDao dao = new Bs_ProDao();

    #region 商品操作

    /// <summary>
    /// 获取商品
    /// </summary>
    /// <param name="proId"></param>
    /// <returns></returns>
    public Bs_Products getProById(int proId){
       DataTable dt = dao.getProById(proId);
       Bs_Products pro = new Bs_Products();
       if (dt.Rows.Count > 0)
       {
           Com.Util.ConvertToEntity(pro, dt.Rows[0]);
       }
       return pro;
    }
    /// <summary>
    /// 根据产品id，获取限时或自定义或参考价
    /// </summary>
    /// <param name="proid"> 产品id</param>
    /// <returns></returns>
    public  string  timeprice(int proid)
    {
        return dao.timeprice(proid);
    }
    /// <summary>
    /// 获取礼品内容
    /// </summary>
    /// <param name="code">编号</param>
    /// <returns></returns>
    public DataTable getGiftById(string code)
    {
        return dao.getGiftById(code);
    }
 

    /// <summary>
    /// 获取商品
    /// </summary>
    /// <param name="pro_name">商品Code</param>
    /// <returns></returns>
    public Bs_Products getProByCode(string pro_code)
    {
        DataTable dt = dao.getProByCode(pro_code);
        Bs_Products pro = new Bs_Products();
        if (dt.Rows.Count > 0)
        {
            Com.Util.ConvertToEntity(pro, dt.Rows[0]);
        }
        return pro;
    }

    /// <summary>
    /// 获取商品属性
    /// </summary>
    /// <param name="prop_type">属性名</param>
    /// <param name="pro_id"></param>
    /// <returns></returns>
    public DataTable getPropByProId(string prop_type,int pro_id)
    {
        return dao.getPropByProId(prop_type, pro_id);
    }

    /// <summary>
    /// 获取商品评论
    /// </summary>
    /// <param name="proid"></param>
    /// <param name="size"></param>
    /// <returns></returns>
    public DataTable getProComment(int proid, int size)
    {
        return dao.getProComment(proid, size);
    }
    public DataTable getProCommnet(int userid)
    {
        return dao.getProCommnet(userid);
    }
    /// <summary>
    /// 获取商品咨询
    /// </summary>
    /// <param name="proid">商品id</param>
    /// <param name="size">条数</param>
    /// <returns></returns>
    public DataTable getproquest(int proid, int size)
    {
        return dao.getProquest(proid, size);
    }
    /// <summary>
    /// 根据用户获取商品咨询
    /// </summary>
    /// <param name="userid">用户id</param>
    /// <returns></returns>
    public DataTable getproquest(int userid)
    {
        return dao.getProquest(userid);
    }
    /// <summary>
    /// 添加商品评论
    /// </summary>
    /// <param name="userid"></param>
    /// <param name="proid"></param>
    /// <param name="name"></param>
    /// <param name="content"></param>
    public void addProComment(int userid, int proid, int grade,string name, string content)
    {
        dao.addProComment(userid,proid,grade,name,content);
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
        dao.addProquest(userid, proid, name, content);
       
    }

    /// <summary>
    /// 添加商品点击
    /// </summary>
    /// <param name="proid"></param>
    public void addProHits(int proid)
    {
        dao.addProHits(proid);
    }
    



    #endregion


    #region 商品属性分类

    /// <summary>
    /// 获取商品分类导航
    /// </summary>
    /// <param name="level">分类编码</param>
    /// <returns></returns>
    public DataTable getProKindNavInCode(string code)
    {
        if (code != "")
        {
            return dao.getProKindNavInCode(code);
        }
        else
        {
            return new DataTable();
        }
    }


    /// <summary>
    /// 获取商品分类列表
    /// </summary>
    /// <param name="level">获取深度 0:无限制</param>
    /// <returns></returns>
    public DataTable getProKindList(int level)
    {
        return dao.getProKindList(level);
    }

    /// <summary>
    /// 获取商品分类列表
    /// </summary>
    /// <returns></returns>
    public DataTable getProKindListInCode(string code,int level)
    {
        return dao.getProKindListInCode(code, level);
    }



    /// <summary>
    /// 获取商品品牌列表
    /// </summary>
    /// <param name="level">获取深度 0:无限制</param>
    /// <returns></returns>
    public DataTable getProBrandList(int level)
    {
        return dao.getProBrandList(level);
    }

    /// <summary>
    /// 获取商品品牌
    /// </summary>
    /// <param name="code">编码</param>
    /// <returns></returns>
    public DataTable getProBrandByCode(string code)
    {
        return dao.getProBrandByCode(code);
    }
    /// <summary>
    /// 获取商品品牌
    /// </summary>
    /// <param name="code">编码</param>
    /// <returns></returns>
    public DataTable getProBrandListInCode(string code, int level)
    {
        return dao.getProBrandListInCode(code, level);
    }


    /// <summary>
    /// 获取商品属性
    /// </summary>
    /// <param name="code">属性编码</param>
    /// <returns></returns>
    public DataTable getProProperty(string code)
    {
        return dao.getProProperty(code);
    }

    /// <summary>
    /// 获取商品属性
    /// </summary>
    /// <param name="code">属性编码</param>
    /// <returns></returns>
    public DataTable getProPropertyById(int propid)
    {
        return dao.getProPropertyById(propid);
    }

    ///<summary>
    ///获取商品列表
    ///</summary>
    ///<param name="code">属性编码</param>
    ///<returns></returns>
    public DataTable getProByCodex(string code)
    {
        return dao.getProByCode(code);
    }


    /// <summary>
    /// 获取相同商品
    /// </summary>
    /// <param name="name">商品名称</param>
    /// <param name="propid">当前产品Id</param>
    /// <returns></returns>
    public DataTable getallpro(string name,int propid)
    {
        return dao.getallprod(name, propid);
    }

    /// <summary>
    /// 获取商品ID颜色
    /// </summary>
    /// <param name="code">属性编码</param>
    /// <returns></returns>
    public DataTable getcolorbycodeid(string code, int propid)
    {
        return dao.getProcolorBycodeandByid(code, propid);
    }


    #endregion


    #region 商品列表
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
        return dao.getProList(filter, pageIndex, pageSize, out recordCound);
    }

  

    ///<summary>
    ///获取当前商品品牌
    ///</summary>
    ///<param name="filter">过滤</param>
    ///<returns></returns>
    public DataTable getBandlist(Bs_Products filter)
    {
        string sql = dao.getBandlist(filter);
        return CSA.DAL.DBAccess.getRS(sql);
 
    }

    /// <summary>
    /// 获取商品热门搜索
    /// </summary>
    /// <param name="code"></param>
    /// <returns></returns>
    public DataTable getProHotSearch(string code,int size)
    {
        return dao.getProHotSearch(code, size);

    }


    /// <summary>
    /// 获取首页显示商品
    /// </summary>
    /// <param name="size">显示数</param>
    /// <returns></returns>
    public DataTable getProlistIsIndex(string kindcode ,int size)
    {
        Bs_Products filter = new Bs_Products();
        filter.BS_ProKindCode = kindcode;
        filter.isIndex = 1;
        return dao.getProList(filter, size);
    }

    /// <summary>
    /// 获取热卖商品
    /// </summary>
    /// <param name="size">显示数</param>
    /// <param name="kindcode">分类Code</param>
    /// <returns></returns>
    public DataTable getProlistIsHotsale(string kindcode, int size)
    {
        Bs_Products filter = new Bs_Products();
        filter.BS_ProKindCode = kindcode;
        filter.isHotSales = 1;
        return dao.getProList(filter, size);
    }
    /// <summary>
    /// 获取推荐商品
    /// </summary>
    /// <param name="size">显示数</param>
    /// <returns></returns>
    public DataTable getProlistIsCommend(string kindcode, int size)
    {
        Bs_Products filter = new Bs_Products();
        filter.BS_ProKindCode = kindcode;
        filter.isCommend = 1;
        return dao.getProList(filter, size);
    }
    /// <summary>
    /// 获取最新商品
    /// </summary>
    /// <param name="size">显示数</param>
    /// <returns></returns>
    public DataTable getProlistIsNew(string kindcode, int size)
    {
        Bs_Products filter = new Bs_Products();
        filter.BS_ProKindCode = kindcode;
        filter.isNew = 1;
        return dao.getProList(filter, size);
    }
    /// <summary>
    /// 获取特卖商品
    /// </summary>
    /// <param name="size">显示数</param>
    /// <returns></returns>
    public DataTable getProlistIsPromote(string kindcode, int size)
    {
        Bs_Products filter = new Bs_Products();
        filter.BS_ProKindCode = kindcode;
        filter.isPromote = 1;
        return dao.getProList(filter, size);
    }
    /// <summary>
    /// 获取断码商品
    /// </summary>
    /// <param name="size">显示数</param>
    /// <returns></returns>
    public DataTable getProlistIsShortsale(string kindcode, int size)
    {
        string strWhere = string.Format("id in ( select p.id from bs_products p " +
                            "inner join Bs_Pro_m2m_Bs_ProSize ps on ps.pro_id = p.id " +
                            "group by  p.id having count(ps.prop_size_id)  = 1 ) and BS_ProKindCode like '{0}%'",kindcode);

        return dao.getProList(strWhere, size);
    }
    /// <summary>
    /// 获取人气商品
    /// </summary>
    /// <param name="size">显示数</param>
    /// <returns></returns> 
    public DataTable getProlistIsHitsale(string kindcode, int size)
    {
        return dao.getProList(string.Format(" BS_ProKindCode like '{0}%'",kindcode), size, " Hits desc ");
    }


    /// <summary>
    /// 获取id组内的商品
    /// </summary>
    /// <param name="proIds"></param>
    /// <param name="size"></param>
    /// <returns></returns>
    public DataTable getProlistInProId(string proIds, int size)
    {
        return dao.getProList(string.Format(" id in ({0})", proIds), size, "");
    }

    /// <summary>
    /// 获取礼品列表
    /// </summary>
    /// <param name="size">条数</param>
    /// <param name="pageIndex">当前页</param>
    /// <param name="pageSize">显示数</param>
    /// <param name="recordcound">记录数</param>
    /// <returns></returns>

    public DataTable getGifts(int size, int pageIndex, int pageSize, out int recordcound)
    {
        return dao.getgifts(size, pageIndex, pageSize, out recordcound);
    }
    /// <summary>
    /// 获取礼品
    /// </summary>
    /// <returns></returns>
    public DataTable getGifts()
    {
        return dao.getgifts(0);
    }

    /// <summary>
    /// 根据条数返回社品列表
    /// </summary>
    /// <param name="size">条数</param>
    /// <returns></returns>
    public DataTable getgiftsbysize(int size)
    {
        return dao.getgiftsbysize(size);
    }

    #endregion





}
