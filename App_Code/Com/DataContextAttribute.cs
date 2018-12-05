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
    /// 自定义特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class DataContextAttribute : Attribute
    {
        /// <summary>
        /// 自定义特性
        /// </summary>
        /// <param name="fieldName">数据表字段名称</param>
        public DataContextAttribute(string property) { this.Property = property; }

        string _property;
        public string Property
        {
            get
            {
                return _property;
            }
            set
            {
                _property = value;
            }
        }
        ///// <summary>
        ///// 数据表字段属性(实体属性)
        ///// </summary>
        //public string Property { get; set; }
    }

