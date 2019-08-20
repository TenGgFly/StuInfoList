using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YX.Security;
using System.Data;
using YX.DEMO.BLL;
using YX.SSO.BLL;

namespace YX.DEMO.DEMO.Laboratory
{
    public partial class View : BasePage
    {
        public string LaboratoryID
        {
            get
            {
                return YX.SSO.OATools.QueryString("LaboratoryID");
            }
        }
        LijmBLL bll = new LijmBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
            }
        }
        public void LoadData()
        {
            if (!string.IsNullOrWhiteSpace(LaboratoryID))
            {
                DataTable dt = bll.GetLaboratoryByID(LaboratoryID).Tables[0];
                txtLaboratory_Code.InnerText = dt.Rows[0]["Laboratory_Code"].ToString();
                txtLaboratory_Name.InnerText = dt.Rows[0]["Laboratory_Name"].ToString();
                chkObj.InnerText = dt.Rows[0]["OpenTo"].ToString();
                txtLaboratory_Time.InnerText = string.Format("{0:yyyy-MM-dd}", dt.Rows[0]["BulidTime"]);
                txtLaboratory_Cost.InnerText = "￥" + dt.Rows[0]["BulidPay"].ToString();
                txtLaboratory_Admin.InnerText = dt.Rows[0]["Manager_Name"].ToString();
                txtLaboratory_Tel.InnerText = dt.Rows[0]["Manager_Tel"].ToString();
                txtLaboratory_Mail.InnerText = dt.Rows[0]["Manager_Mail"].ToString();
                txtLaboratory_Note.InnerText = dt.Rows[0]["Remark"].ToString();
                txtLaboratory_System.InnerText = dt.Rows[0]["Use_Rule"].ToString();

                if (dt.Rows[0]["Type"].ToString() == "0")
                {
                    rdType.InnerText = "生物";
                }
                else if (dt.Rows[0]["Type"].ToString() == "1")
                {
                    rdType.InnerText = "水产";
                }
                else if (dt.Rows[0]["Type"].ToString() == "2")
                {
                    rdType.InnerText = "电子";
                }

                if (dt.Rows[0]["Fiinancial_Source"].ToString() == "0")
                {
                    txtLaboratory_Income.InnerText = "市财政";
                }
                else if (dt.Rows[0]["Fiinancial_Source"].ToString() == "1")
                {
                    txtLaboratory_Income.InnerText = "省财政";
                }
                else if (dt.Rows[0]["Fiinancial_Source"].ToString() == "2")
                {
                    txtLaboratory_Income.InnerText = "中央财政";
                }
                AttachmentShow1.PrimaryID = LaboratoryID;
            }
        }
        protected void btn_cancel_ServerClick(object sender, EventArgs e)
        {
            Response.Redirect("List.aspx");
        }
    }
}