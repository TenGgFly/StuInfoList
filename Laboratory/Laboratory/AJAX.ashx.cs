using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YX.DEMO.BLL;

namespace YX.DEMO.Laboratory
{
    /// <summary>
    /// AJAX 的摘要说明
    /// </summary>
    public class AJAX : IHttpHandler
    {
        public string Laboratory_ID
        {
            get
            {
                return YX.SSO.OATools.QueryString("LaboratoryID");
            }
        }
        public string Laboratory_Code
        {
            get
            {
                return YX.SSO.OATools.QueryString("Laboratory_Code");
            }
        }
        public void ProcessRequest(HttpContext context)
        {
            LijmBLL bll = new LijmBLL();
            bool s = bll.IsExistCode(Laboratory_Code, Laboratory_ID);
            context.Response.Write(s);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}