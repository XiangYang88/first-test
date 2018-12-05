using System;
using System.Data;
using System.Collections.Generic;
	/// <summary>
	/// ҵ���߼���Bs_UserBLL ��ժҪ˵����
	/// </summary>
	public class Bs_UserBLL
	{
		private readonly Bs_UserDao dao=new Bs_UserDao();
		public Bs_UserBLL()
		{}


        /// <summary>
        /// �����û��˺Ż�ȡ�û�
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public Bs_User getUserByName(string username)
        {
            return dao.getUserByName(username);
        }

        /// <summary>
        /// ע���û�
        /// </summary>
        /// <param name="user">�û�ʵ��</param>
        public void reg(Bs_User user)
        {
            dao.reg(user);
        }

        /// <summary>
        /// ����ID��ȡ�û�ʵ��
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public DataRow getDrUserByID(int userId)
        {
            DataTable dtUser = dao.getDtUserByID(userId);
            return Com.Util.getDrByTable(dtUser);
        }
        /// <summary>
        /// ��ȡ�ȼ�Ȩ��
        /// </summary>
        /// <param name="grade">�ȼ�����</param>
        /// <returns></returns>
        public string getgradeinfo(string grade)
        {
            return dao.getgradeinfo(grade);
        }


        /// <summary>
        /// ��¼�û�
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
        /// �޸ĵ�¼�û�����
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
        /// �޸��û�
        /// </summary>
        /// <param name="user"></param>
        public void updateUser(Bs_User user)
        {
            dao.updateUser(user);
        }

        /// <summary>
        /// ��֤�û��˺ż�����
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        public bool checkUserAndEmail(string userName,string email)
        {
            return dao.checkUserAndEmail(userName, email);
        }






        /// <summary>
        /// �ж��û����Ƿ����
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public bool existsUserName(string userName)
        {
            return dao.existsUserName(userName);
        }



        /// <summary>
        /// �˳���¼
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
        /// ������Ʒ
        /// </summary>
        /// <param name="userId">�û�id</param>
        /// <param name="proId">��Ʒid</param>
        public void saveProductFav(int userId, int proId)
        {
            dao.saveProductFav(userId, proId);
        }

        /// <summary>
        /// ������Ʒ
        /// </summary>
        /// <param name="userId">�û�ID</param>
        /// <param name="gifId">��ƷID</param>
        public void savegiftFav(int userId, int gifId)
        {
            dao.saveGiftFav(userId, gifId);
        }

        /// <summary>
        /// ��ȡ�û��ղ���Ʒ�б�
        /// </summary>
        /// <param name="userId">�û�ID</param>
        /// <returns></returns>
        public DataTable getProByUserFav(int userId)
        {
            return dao.getProByUserFav(userId);
        }
        /// <summary>
        /// ��ȡ�û��ղ���Ʒ�б�
        /// </summary>
        /// <param name="userId">�û�ID</param>
        /// <returns></returns>
        public DataTable getGiftByUserFav(int userId)
        {
            return dao.getGiftByUserFav(userId);
        }

        /// <summary>
        /// ɾ���������Ʒ
        /// </summary>
        /// <param name="userId">�û�ID</param>
        /// <param name="proId">��Ʒid</param>
        /// <returns></returns>
        public void delProFav(int userId, int proId)
        {
            dao.delProFav(userId, proId);
        }
        /// <summary>
        /// ɾ���������Ʒ
        /// </summary>
        /// <param name="userId">�û�ID</param>
        /// <param name="giftId">��ƷID</param>
        public void delGiftFav(int userId, int giftId)
        {
            dao.delGiftFav(userId, giftId);
        }


	}

