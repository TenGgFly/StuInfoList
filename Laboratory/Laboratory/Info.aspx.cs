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
using System.Text.RegularExpressions;

namespace LaboratoryInfo
{
    public partial class DEMO_Laboratory_showInfo : BasePage
    {
        public string Laboratory_ID
        {
            get
            {
                return YX.SSO.OATools.QueryString("Laboratory_ID");
            }
        }
        //EmployeeBLL bll = new EmployeeBLL();
        LijmBLL bll = new LijmBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();

                AttachmentMore1.IsFromKnowledge = false;
            }
        }
        public void LoadData()
        {
            if (!string.IsNullOrWhiteSpace(Laboratory_ID))//ID不为空表示是修改进来将值赋给相关控件
            {
                AttachmentMore1.PrimaryID = Laboratory_ID;
                title.InnerText = "修改实验室信息";
                DataTable dt = bll.GetLaboratoryByID(Laboratory_ID).Tables[0];//根据ID取出相关的值
                txtLaboratory_Code.Value = dt.Rows[0]["Laboratory_Code"].ToString();
                txtLaboratory_Name.Value = dt.Rows[0]["Laboratory_Name"].ToString();
                if (dt.Rows[0]["Fiinancial_Source"].ToString() == "0")
                {
                    txtLaboratory_Income.Value = "市政府";
                }
                if (dt.Rows[0]["Fiinancial_Source"].ToString() == "2")
                {
                    txtLaboratory_Income.Value = "省政府";
                }
                if (dt.Rows[0]["Fiinancial_Source"].ToString() == "3")
                {
                    txtLaboratory_Income.Value = "中央政府";
                }

                if (dt.Rows[0]["Type"].ToString() == "0")
                {
                    rdType1.Checked = true;
                }
                if (dt.Rows[0]["Type"].ToString() == "1")
                {
                    rdType2.Checked = true;
                }
                if (dt.Rows[0]["Fiinancial_Source"].ToString() == "3")
                {
                    rdType3.Checked = true;
                }
                txtLaboratory_Time.Value = dt.Rows[0]["BulidTime"].ToString();
                txtLaboratory_Cost.Value = dt.Rows[0]["BulidPay"].ToString();
                if (dt.Rows[0]["OpenTo"].ToString().IndexOf("研究所") >= 0)
                {
                    chkObj1.Checked = true;
                }
                if (dt.Rows[0]["OpenTo"].ToString().IndexOf("下属单位") >= 0)
                {
                    chkObj2.Checked = true;
                }
                if (dt.Rows[0]["OpenTo"].ToString().IndexOf("合作单位") >= 0)
                {
                    chkObj3.Checked = true;
                }
                if (dt.Rows[0]["OpenTo"].ToString().IndexOf("外来单位") >= 0)
                {
                    chkObj4.Checked = true;
                }
                txtLaboratory_Admin.Value = dt.Rows[0]["Manager_Name"].ToString();
                txtLaboratory_Tel.Value = dt.Rows[0]["Manager_Tel"].ToString();
                txtLaboratory_Mail.Value = dt.Rows[0]["Manager_Mail"].ToString();
                txtLaboratory_Note.Text = dt.Rows[0]["Remark"].ToString();
                txtLaboratory_System.Text = dt.Rows[0]["Use_Rule"].ToString();

            }
            else
            {
                int s = Convert.ToInt32(bll.GetLaboratoryCount()) + 1;
                txtLaboratory_Code.Value = "LAB" + s.ToString().PadLeft(4, '0');
            }
        }
        protected void btn_save_Click(object sender, EventArgs e)
        {
            DataRow row = null;
            string KeyID = "";
            if (!string.IsNullOrWhiteSpace(Laboratory_ID))//修改
            {

            }
            else//新增
            {

                row = bll.GetLaboratoryByID(Laboratory_ID).Tables[0].NewRow();
                row["LaboratoryID"] = CommonBLL.NewID("DEMO_Laboratory");//新增时的主键ID
                KeyID = row["LaboratoryID"].ToString();
                row["Laboratory_Code"] = txtLaboratory_Code.Value.Trim();//实验室编号
                row["Laboratory_Name"] = txtLaboratory_Name.Value.Trim();//实验室名称
                if (rdType1.Checked == true)//类型
                {
                    row["Type"] = 0;//生物
                }
                else if (rdType2.Checked == true)
                {
                    row["Type"] = 1;//水产
                }
                else if (rdType3.Checked == true)
                {
                    row["Type"] = 2;//电子
                }

                if (txtLaboratory_Income.Value == "市政府")//财政来源
                {
                    row["Fiinancial_Source"] = 0;
                }
                if (txtLaboratory_Income.Value == "省政府")
                {
                    row["Fiinancial_Source"] = 1;
                }
                if (txtLaboratory_Income.Value == "中央政府")
                {
                    row["Fiinancial_Source"] = 2;
                }
                //row["Fiinancial_Source"] = txtLaboratory_Income.Value;//财政来源

                string openTo = "";//开放对象
                if (chkObj1.Checked == true)
                {
                    openTo += chkObj1.Value + ",";
                }
                if (chkObj2.Checked == true)
                {
                    openTo += chkObj2.Value + ",";
                }
                if (chkObj3.Checked == true)
                {
                    openTo += chkObj3.Value + ",";
                }
                if (chkObj4.Checked == true)
                {
                    openTo += chkObj4.Value + ",";
                }
                if (openTo != "")
                {
                    row["OpenTo"] = openTo.Substring(0, openTo.Length - 1);
                }
                else
                {
                    row["OpenTo"] = "";
                }

                //row["BulidPay"] = Convert.ToInt32(txtLaboratory_Cost.Value);//耗费资金
                if (!string.IsNullOrEmpty(txtLaboratory_Cost.Value))
                {
                    row["BulidPay"] = Math.Round(Decimal.Parse(txtLaboratory_Cost.Value), 2);
                }
                else
                {
                    row["BulidPay"] = Math.Round(0.00, 2);
                }

                row["CreateTime"] = txtLaboratory_Time.Value == "" ? Convert.DBNull : txtLaboratory_Time.Value;//建成时间                
                row["Remark"] = txtLaboratory_Note.Text;//备注
                row["Use_Rule"] = txtLaboratory_System.Text;//使用制度
                row["Manager_Name"] = txtLaboratory_Admin.Value;//管理员
                row["Manager_Tel"] = txtLaboratory_Tel.Value;//管理员电话
                row["Manager_Mail"] = txtLaboratory_Mail.Value;//管理员邮箱
                if (!string.IsNullOrWhiteSpace(Laboratory_ID))//修改
                {
                    bll.UpdateLaboratory(row);//保存事件        

                }
                else
                {
                    bll.InsertLaboratory(row);//新增事件
                    Response.Write("<Script Language=JavaScript>alert('保存成功！');</Script>");

                }
                AttachmentMore1.SaveAffix("DEMO_Laboratory", KeyID);
                Response.Redirect("List.aspx");
            }
        }

        protected void btn_cancel_ServerClick(object sender, EventArgs e)
        {
            //System.Web.HttpContext.Current.Response.Redirect("List.aspx");
            Response.Redirect("List.aspx");
        }
}
}