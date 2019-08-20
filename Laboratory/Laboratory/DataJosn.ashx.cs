using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using YX.DEMO.BLL;
using System.Web.SessionState;
using YX.Security;
using YX.SSO.BLL;

namespace YX.DEMO.Laboratory
{
    /// <summary>
    /// DataJosn 的摘要说明
    /// </summary>
    public class DataJosn : IHttpHandler, IReadOnlySessionState
    {


        public void ProcessRequest(HttpContext context)
        {
            UserInfo CurrentUserInfo = BasePage.GetUserInfo();
            if (CurrentUserInfo == null)
            {
                context.Response.Write(YX.SSO.OATools.SecurityErr);
                return;
            }

            string UserID = CurrentUserInfo.UserID;//获取当前登录人的ID
            string query = "Deletemark=0";
            string txtAutoSearch = context.Server.UrlDecode(context.Request["txtAutoSearch"]);//接收查询条件
            string sortflag = context.Request["sortName"];//排序的字段名
            string orderflag = context.Request["sortOrder"];//排序的顺序
            string Laboratory_Code = context.Request["Laboratory_Code"];
            string Laboratory_Name = context.Server.UrlDecode(context.Request["Laboratory_Name"]);
            string Laboratory_Type = context.Request["Laboratory_Type"];
            string txtLaboratory_Income = context.Request["txtLaboratory_Income"];
            string txtLaboratory_Admin = context.Request["txtLaboratory_Admin"];
            string txtLaboratory_Obj = context.Request["txtLaboratory_Obj"];
            string txtLaboratory_Tel = context.Request["txtLaboratory_Tel"];
            string txtLaboratory_Mail = context.Request["txtLaboratory_Mail"];
            string txtLaboratory_Time = context.Request["txtLaboratory_Time"];
            string txtLaboratory_Cost = context.Request["txtLaboratory_Cost"];

            if (!string.IsNullOrWhiteSpace(txtAutoSearch))//判断当前传下来的这个值是不是为空
            {
                query += " and (Laboratory_Code like '%" + txtAutoSearch + "%' or Laboratory_Code like '%" + txtAutoSearch + "%')";
            }

            if (!string.IsNullOrWhiteSpace(Laboratory_Code))
            {
                query += " and Laboratory_Code like '%" + Laboratory_Code + "%'";
            }

            if (!string.IsNullOrWhiteSpace(Laboratory_Name))
            {
                query += " and Laboratory_Name like '%" + Laboratory_Name + "%'";
            }

            if (!string.IsNullOrWhiteSpace(Laboratory_Type))
            {
                query += " and Type like '%" + Laboratory_Type + "%'";
            }

            if (!string.IsNullOrWhiteSpace(txtLaboratory_Income))
            {
                query += " and Fiinancial_Source='" + txtLaboratory_Income + "'";
            }

            if (!string.IsNullOrWhiteSpace(txtLaboratory_Admin))
            {
                query += " and Manager_Name like '%" + txtLaboratory_Admin + "%'";
            }

            if (!string.IsNullOrWhiteSpace(txtLaboratory_Obj))
            {
                query += " and OpenTo='" + txtLaboratory_Obj + "'";
            }

            if (!string.IsNullOrEmpty(txtLaboratory_Tel))
            {
                query += " and Manager_Tel='" + txtLaboratory_Tel + "'";
            }

            if (!string.IsNullOrEmpty(txtLaboratory_Mail))
            {
                query += " and Manager_Mail='" + txtLaboratory_Mail + "'";
            }
            if (!string.IsNullOrEmpty(txtLaboratory_Time))
            {
                query += " and BulidTime='" + txtLaboratory_Time + "'";
            }

            if (!string.IsNullOrEmpty(txtLaboratory_Cost))
            {
                query += " and BulidPay='" + txtLaboratory_Cost + "'";
            }

            int f = 0;
            string orderstr = "Laboratory_Code Desc";
            if (!string.IsNullOrWhiteSpace(sortflag))
            {
                orderstr = sortflag + " " + orderflag;//排序条件
            }
            DataSet ds = CommonBLL.Pager("DEMO_Laboratory", orderstr, Convert.ToInt32(context.Request["rows"]), Convert.ToInt32(context.Request["page"]), query, "*", out f); //取数据源其中f为总条数
            string json = YX.SSO.JEasyUIJsonHelper.ToGridJson(ds.Tables[0], "*", f);//拼接josn返回回去的字符串
            if (string.IsNullOrEmpty(sortflag))//非表头排序时保存查询条件
            {
                CommonBLL.SaveQueryCondition(UserID, "DEMO_Laboratory", "txtAutoSearch", txtAutoSearch);
                CommonBLL.SaveQueryCondition(UserID, "DEMO_Laboratory", "txtLaboratory_Code", Laboratory_Code);
                CommonBLL.SaveQueryCondition(UserID, "DEMO_Laboratory", "txtLaboratory_Name", Laboratory_Name);
                CommonBLL.SaveQueryCondition(UserID, "DEMO_Laboratory", "txtLaboratory_Type", Laboratory_Type);
                CommonBLL.SaveQueryCondition(UserID, "DEMO_Laboratory", "txtLaboratory_Income", txtLaboratory_Income);
                CommonBLL.SaveQueryCondition(UserID, "DEMO_Laboratory", "txtLaboratory_Admin",txtLaboratory_Admin);
                CommonBLL.SaveQueryCondition(UserID, "DEMO_Laboratory", "txtLaboratory_Obj", txtLaboratory_Obj);
                CommonBLL.SaveQueryCondition(UserID, "DEMO_Laboratory", "txtLaboratory_Tel", txtLaboratory_Tel);
                CommonBLL.SaveQueryCondition(UserID, "DEMO_Laboratory", "txtLaboratory_Mail", txtLaboratory_Mail);
                CommonBLL.SaveQueryCondition(UserID, "DEMO_Laboratory", "txtLaboratory_Time", txtLaboratory_Time);
                CommonBLL.SaveQueryCondition(UserID, "DEMO_Laboratory", "txtLaboratory_Cost", txtLaboratory_Cost);

                CommonBLL.SaveQueryCondition(UserID, "DEMO_Laboratory", "PageSize", context.Request["rows"]);
                CommonBLL.SaveQueryCondition(UserID, "DEMO_Laboratory", "PageIndex", context.Request["page"]);

            }
            context.Response.Write(json);//向前台输出字符串 
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