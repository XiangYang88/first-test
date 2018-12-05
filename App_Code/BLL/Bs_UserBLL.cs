using System;
using System.Data;
using System.Collections.Generic;
	/// <summary>
	/// 业务逻辑类Bs_UserBLL 的摘要说明。
	/// </summary>
	public class Bs_UserBLL
	{
		private readonly Bs_UserDao dao=new Bs_UserDao();
		public Bs_UserBLL()
		{}


        /// <summary>
        /// 根据用户账号获取用户
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public Bs_User getUserByName(string username)
        {
            return dao.getUserByName(username);
        }

        /// <summary>
        /// 注册用户
        /// </summary>
        /// <param name="user">用户实体</param>
        public void reg(Bs_User user)
        {
            dao.reg(user);
        }

        /// <summary>
        /// 根据ID获取用户实体
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public DataRow getDrUserByID(int userId)
        {
            DataTable dtUser = dao.getDtUserByID(userId);
            return Com.Util.getDrByTable(dtUser);
        }
        /// <summary>
        /// 获取等级权利
        /// </summary>
        /// <param name="grade">等级代码</param>
        /// <returns></returns>
        public string getgradeinfo(string grade)
        {
            return dao.getgradeinfo(grade);
        }


        /// <summary>
        /// 登录用户
        /// </summary>
        /// <param name="model"></param>
        public Bs_User login(Bs_User model)
        {
            Bs_User user = dao.getUserByName(model.Name);
            if (user != null && CSA.Security.Encrypt.verifyMD5(model.Password, user.Password))
            {
                CurInfo.CurUser = user;
                BLL.BsUser.User.LoginLog(user.ID.ToString());
                return user;
            }
            else
            {
                CurInfo.CurUser = null;
                return null;
            }
        }


        /// <summary>
        /// 修改登录用户密码
        /// </summary>
        /// <param name="newPwd"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Bs_User EditPwd(string newPwd, int userId)
        {
            dao.EditPwd(newPwd, userId);
            Bs_User user = dao.getUserByID(userId);
            return user;
        }

        /// <summary>
        /// 修改用户
        /// </summary>
        /// <param name="user"></param>
        public void updateUser(Bs_User user)
        {
            dao.updateUser(user);
        }

        /// <summary>
        /// 验证用户账号及邮箱
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        public bool checkUserAndEmail(string userName,string email)
        {
            return dao.checkUserAndEmail(userName, email);
        }






        /// <summary>
        /// 判断用户名是否存在
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public bool existsUserName(string userName)
        {
            return dao.existsUserName(userName);
        }



        /// <summary>
        /// 退出登录
        /// </summary>
        /// <returns></returns>
        public bool logout()
        {
            CurInfo.CurUser = null;
            CSA.HC.SessionHelper.clear(Com.Const.sessionNames[0]);
            CSA.HC.CookiesHelper.clear(Com.Const.sessionNames[0]);
            return true;
        }

        /// <summary>
        /// 保存商品
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <param name="proId">商品id</param>
        public void saveProductFav(int userId, int proId)
        {
            dao.saveProductFav(userId, proId);
        }

        /// <summary>
        /// 保存礼品
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="gifId">礼品ID</param>
        public void savegiftFav(int userId, int gifId)
        {
            dao.saveGiftFav(userId, gifId);
        }

        /// <summary>
        /// 获取用户收藏商品列表
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        public DataTable getProByUserFav(int userId)
        {
            return dao.getProByUserFav(userId);
        }
        /// <summary>
        /// 获取用户收藏礼品列表
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        public DataTable getGiftByUserFav(int userId)
        {
            return dao.getGiftByUserFav(userId);
        }

        /// <summary>
        /// 删除保存的商品
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="proId">商品id</param>
        /// <returns></returns>
        public void delProFav(int userId, int proId)
        {
            dao.delProFav(userId, proId);
        }
        /// <summary>
        /// 删除保存的礼品
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="giftId">礼品ID</param>
        public void delGiftFav(int userId, int giftId)
        {
            dao.delGiftFav(userId, giftId);
        }


	}

