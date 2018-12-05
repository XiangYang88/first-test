using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
	/// <summary>
	/// ���ݷ�����Bs_UserDao��
	/// </summary>
	public class Bs_UserDao
	{
		public Bs_UserDao()
		{}


        /// <summary>
        /// ע���û�
        /// </summary>
        /// <param name="user">�û�ʵ��</param>
        public void reg(Bs_User user)
        {
            CSA.DAL.SQLBuilder builder = new CSA.DAL.SQLBuilder();
            builder.TblName = "Bs_User";
            builder.AddData("name", user.Name);
            builder.AddData("realname", user.Name);
            builder.AddData("password",user.Password);
            builder.AddData("email", user.EMail);
            builder.AddData("addtime", DateTime.Now.ToString("s"));
            builder.AddData("Code", "01");
            builder.AutoInsert();

        }

        /// <summary>
        /// ����ID��ȡ�û�ʵ��
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public DataTable getDtUserByID(int userId)
        {
            string strSql = string.Format("select * from Bs_user where id = '{0}' ", userId);
            return CSA.DAL.DBAccess.getRS(strSql);
        }

        /// <summary>
        /// ��ȡ�ȼ�Ȩ��
        /// </summary>
        /// <param name="grade">�ȼ�����</param>
        /// <returns></returns>
        public string getgradeinfo(string  grade)
        {
            string contet = "";
            DataTable dt = CSA.DAL.DBAccess.getRS("select * from Bs_UserGrade where code='"+grade+"'");
            if (dt.Rows.Count > 0)
            {
               contet=dt.Rows[0]["content"].ToString();
            }
            return contet;
        }



        /// <summary>
        /// �����û�ID��ȡ�û�
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Bs_User getUserByID(int userId)
        {
            string strSql = string.Format("select * from Bs_user where id = '{0}' ", userId);
            DataTable dt = CSA.DAL.DBAccess.getRS(strSql);
            Bs_User user = new Bs_User();
            if (dt.Rows.Count > 0)
            {
                Com.Util.ConvertToEntity(user, dt.Rows[0]);
            }
            return user;
        }


        /// <summary>
        /// �����û�����ȡ�û�
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public Bs_User getUserByName(string userName)
        {
            string strSql = string.Format("select * from Bs_user where name = '{0}' ", userName);
            DataTable dt = CSA.DAL.DBAccess.getRS(strSql);
            Bs_User user = new Bs_User();
            if (dt.Rows.Count > 0)
            {
                Com.Util.ConvertToEntity(user, dt.Rows[0]);
            }
            return user;
        }


        /// <summary>
        /// �ж��û����Ƿ����
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public bool existsUserName(string userName)
        {
            string strSql = string.Format("select id,name,password,email,addtime from Bs_user where name = '{0}' ", userName);
            DataTable dt = CSA.DAL.DBAccess.getRS(strSql);
            if (dt.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// �ж��û����Ƿ����
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public bool checkUserAndEmail(string userName,string email)
        {
            string strSql = string.Format("select id,name,password,email,addtime from Bs_user where name= '{0}' and  email = '{1}' ", userName, email);
            DataTable dt = CSA.DAL.DBAccess.getRS(strSql);
            if (dt.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// �޸��û�����
        /// </summary>
        /// <param name="newPwd"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public void EditPwd(string newPwd, int userId)
        {
            string strSql = string.Format("update bs_user set password = '{0}' where id = {1}", newPwd, userId);
            CSA.DAL.DBAccess.ExecuteNonQuery(strSql);
        }

        /// <summary>
        /// �޸��û�
        /// </summary>
        /// <param name="user"></param>
        public void updateUser(Bs_User user)
        {
            CSA.DAL.SQLBuilder builder = new CSA.DAL.SQLBuilder();
            builder.TblName = "bs_user";
            builder.AddData("realname",user.RealName);
            builder.AddData("Sex",user.Sex);

            builder.AddData("QQ",user.QQ);
            builder.AddData("MSN",user.MSN);
            builder.AddData("WanWan",user.WanWan);
            builder.AddData("Pic",user.Pic);
            builder.AddData("Mobile",user.Mobile);
            builder.AddData("Phone",user.Phone);
            builder.AddData("Fax",user.Fax);
            builder.AddData("EMail",user.EMail);
            builder.AddData("Company",user.Company);
            builder.AddData("Address",user.Address);
            builder.AddData("PostCode",user.PostCode);
            builder.AddData("County", user.County);
            builder.AddData("Country", user.Country);
            builder.AddData("City",user.City);
            builder.AddData("Province",user.Province);

            builder.AddData("csgAddress",user.csgAddress);
            builder.AddData("csgCity",user.csgCity);
            builder.AddData("csgCountry", user.csgCountry);
            builder.AddData("csgCounty", user.csgCounty);
            builder.AddData("csgEmail",user.csgEmail);
            builder.AddData("csgMobile",user.csgMobile);
            builder.AddData("csgName",user.csgName);
            builder.AddData("csgPhone",user.csgPhone);
            builder.AddData("csgPostCode",user.csgPostCode);
            builder.AddData("csgProvince",user.csgProvince);

            builder.Where = " and id= " + user.ID;
            builder.AutoUpdate();
            
        }



        /// <summary>
        /// �����ղ���Ʒ
        /// </summary>
        /// <param name="userId">�û�id</param>
        /// <param name="proId">��Ʒid</param>
        public void saveProductFav(int userId, int proId)
        {
            string fsql;
            fsql=string.Format("update Bs_Products set fhit=fhit+1 where id={0}",proId);
            CSA.DAL.DBAccess.ExecuteNonQuery(fsql);
            string strSql;
            strSql = string.Format("select * from bs_profav where bs_userid = {0} and bs_productsid = {1}", userId, proId);
            DataTable dt = CSA.DAL.DBAccess.getRS(strSql);
            if (dt.Rows.Count > 0)
            {
                strSql = string.Format("update bs_profav set addtime = '{0}'  where bs_userid = {1} and bs_productsid = {2}", DateTime.Now.ToString("s"), userId, proId);
            }
            else
            {
                strSql = string.Format("insert into bs_profav ([bs_userid],[bs_productsid],[addtime]) values ({0},{1},'{2}')", userId, proId, DateTime.Now.ToString("s"));
            }
            CSA.DAL.DBAccess.ExecuteNonQuery(strSql);
        }
        /// <summary>
        /// �ղ���Ʒ
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="gifId"></param>
        public void saveGiftFav(int userId, int gifId)
        {
            string fsql;
            fsql = string.Format("update Bs_Gift set hits=hits+1 where id={0}", userId);
            CSA.DAL.DBAccess.ExecuteNonQuery(fsql);
            string strSql;
            strSql = string.Format("select * from bs_profav where bs_userid = {0} and Bs_GiftID = {1}", userId, gifId);
            DataTable dt = CSA.DAL.DBAccess.getRS(strSql);
            if (dt.Rows.Count > 0)
            {
                strSql = string.Format("update bs_profav set addtime = '{0}'  where bs_userid = {1} and Bs_GiftID = {2}", DateTime.Now.ToString("s"), userId, gifId);
            }
            else
            {
                strSql = string.Format("insert into bs_profav ([bs_userid],[Bs_GiftID],[addtime]) values ({0},{1},'{2}')", userId, gifId, DateTime.Now.ToString("s"));
            }
            CSA.DAL.DBAccess.ExecuteNonQuery(strSql);

        }

        /// <summary>
        /// ��ȡ�û��ղ���Ʒ�б�
        /// </summary>
        /// <param name="userId">�û�ID</param>
        /// <returns></returns>
        public DataTable getProByUserFav(int userId)
        {
            string strSql = string.Format(
                    "select p.*,f.addtime  from bs_products p inner join bs_profav f on  p.id = f.Bs_ProductsID inner join bs_user u on u.id = f.Bs_UserID where u.id = {0}",
                    userId);
            DataTable dt = CSA.DAL.DBAccess.getRS(strSql.ToString());
            return dt;
        }

        /// <summary>
        /// ��ȡ�û��ղ���Ʒ�б�
        /// </summary>
        /// <param name="userId">�û�ID</param>
        /// <returns></returns>

        public DataTable getGiftByUserFav(int userId)
        {
            string strSql = string.Format(
                   "select p.*,f.addtime  from Bs_Gift p inner join bs_profav f on  p.id = f.Bs_GiftID inner join bs_user u on u.id = f.Bs_UserID where u.id = {0}",
                   userId);
            DataTable dt = CSA.DAL.DBAccess.getRS(strSql.ToString());
            return dt;
            
        }

        /// <summary>
        /// ɾ���������Ʒ
        /// </summary>
        /// <param name="userId">�û�ID</param>
        /// <param name="proId">��Ʒid</param>
        /// <returns></returns>
        public void delProFav(int userId, int proId)
        {
            string strSql = string.Format("delete from bs_profav  where Bs_UserID = {0} and  Bs_ProductsID= {1}",userId, proId);
            CSA.DAL.DBAccess.ExecuteNonQuery(strSql);
        }
        /// <summary>
        /// ɾ���������Ʒ
        /// </summary>
        /// <param name="userId">�û�ID</param>
        /// <param name="giftId">��ƷID</param>
        public void delGiftFav(int userId, int giftId)
        {
            string strSql = string.Format("delete from bs_profav  where Bs_UserID = {0} and  Bs_GiftID= {1}", userId, giftId);
            CSA.DAL.DBAccess.ExecuteNonQuery(strSql);
        }
	}

