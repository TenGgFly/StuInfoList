using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using YX.SSO.BLL;
using YX.Security;
using YX.DEMO.BLL;

namespace YX.DEMO.DEMO.Laboratory
{
    public partial class InputDataList : BasePage
    {
        LijmBLL bll = new LijmBLL();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void LinkDownLoad_Click(object sender, EventArgs e)
        {
            string strWhere = string.Empty;
            string TemplateFileName = "";
            TemplateFileName = "实验室信息模板" + ".xls";
            string DirectoryPath = Server.MapPath("/Templates/");
            string filePath = DirectoryPath + TemplateFileName;


            string UserAgent = Request.ServerVariables["http_user_agent"].ToLower();
            string cnFilename = "";
            if (UserAgent.IndexOf("firefox") == -1)//非火狐浏览器
            {
                cnFilename = HttpUtility.UrlEncode(TemplateFileName, System.Text.Encoding.UTF8);
            }
            else
            {
                cnFilename = TemplateFileName;
            }
            String filename = string.Format("{0}", cnFilename); //文件默认命名方式，可以自定义

            //以字符流的形式下载文件
            FileStream fs = new FileStream(filePath, FileMode.Open);
            byte[] bytes = new byte[(int)fs.Length];
            fs.Read(bytes, 0, bytes.Length);
            fs.Close();
            Response.ContentType = "application/octet-stream";
            //通知浏览器下载文件而不是打开
            Response.AddHeader("Content-Disposition", "attachment;  filename=" + filename);
            Response.BinaryWrite(bytes);
            Response.Flush();
            Response.End();
        }

        protected void LinkInput_Click(object sender, EventArgs e)
        {
            if (FileUpload1.HasFile == false)//HasFile用来检查FileUpload是否有指定文件
            {
                Response.Write("<script language='javascript'>alert('请您选择excel文件');</script>");
                return;//当无文件时,返回
            }
            string IsXls = System.IO.Path.GetExtension(FileUpload1.FileName).ToString().ToLower();//System.IO.Path.GetExtension获得文件的扩展名  guss 20120406
            if (IsXls != ".xls" && IsXls != ".xlsx")
            {
                Response.Write("<script language='javascript'>alert('只可以选择excel文件');</script>");
                return;//当选择的不是Excel文件时,返回
            }
            string filename = "result_" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".xls";
            if (!Directory.Exists(HttpContext.Current.Server.MapPath("/UploadFiles/")))
            {
                System.IO.Directory.CreateDirectory(@HttpContext.Current.Server.MapPath("/UploadFiles/"));
            }
            string savePath = Server.MapPath(("/UploadFiles/") + filename);//Server.MapPath 获得虚拟服务器相对路径
            FileUpload1.SaveAs(savePath); //将上传的文件保存到 指定的路径
            if (CheckData(savePath) != "")
            {
                L_MSG.Text = CheckData(savePath);
            }
            else
            {
                Aspose.Cells.Workbook wb = new Aspose.Cells.Workbook();
                wb.Open(savePath);
                for (int a = 0; a < wb.Worksheets.Count; a++)
                {
                    Aspose.Cells.Worksheet ws = wb.Worksheets[a];
                    Aspose.Cells.Cells cells = ws.Cells;
                    for (int rIndex = 1; rIndex < cells.Rows.Count; rIndex++)
                    {
                        Aspose.Cells.Row row = cells.Rows[rIndex];

                        #region Patent
                        string strWhere = " and  Laboratory_Code='" + row[0].StringValue.Trim() + "' and Laboratory_Name='" + row[1].StringValue.Trim() + "'";
                        bll.DelLaboratory(strWhere);
                        #endregion
                    }
                }
                DataSet ds = new DataSet();
                ds = bll.GetLaboratoryByID("-1");

                for (int i = 0; i < wb.Worksheets.Count; i++)
                {
                    Aspose.Cells.Worksheet ws = wb.Worksheets[i];
                    Aspose.Cells.Cells cells = ws.Cells;
                    string name = wb.Worksheets[i].Name;
                    for (int rIndex = 1; rIndex < cells.Rows.Count; rIndex++)
                    {
                        Aspose.Cells.Row row = cells.Rows[rIndex];
                        #region Patent
                        DataRow dr = ds.Tables[0].NewRow();
                        dr["LaboratoryID"] = CommonBLL.NewID("DEMO_Laboratory");
                        dr["Laboratory_Code"] = row[0].StringValue.Trim();
                        dr["Laboratory_Name"] = row[1].StringValue.Trim();
                        if (row[2].StringValue.Trim() != "")
                        {
                            if (row[2].StringValue.Trim() == "生物")
                            {
                                dr["Type"] = 0;
                            }
                            if (row[2].StringValue.Trim() == "水产")
                            {
                                dr["Type"] = 1;
                            }
                            if (row[2].StringValue.Trim() == "电子")
                            {
                                dr["Type"] = 2;
                            }
                        }
                        if (row[3].StringValue.Trim() != "")
                        {
                            if (row[3].StringValue.Trim() == "市财政")
                            {
                                dr["Fiinancial_Source"] = 0;
                            }
                            if (row[3].StringValue.Trim() == "省财政")
                            {
                                dr["Fiinancial_Source"] = 1;
                            }
                            if (row[3].StringValue.Trim() == "中央财政")
                            {
                                dr["Fiinancial_Source"] = 2;
                            }
                        }
                        dr["OpenTo"] = row[4].StringValue.Trim();
                        if (row[5].StringValue.Trim() != "")
                        {
                            dr["BulidTime"] = Convert.ToDateTime(row[5].StringValue.Trim());
                        }

                        if (!string.IsNullOrEmpty(row[6].StringValue.Trim()))
                        {
                            dr["BulidPay"] = Math.Round(Decimal.Parse(row[6].StringValue.Trim()), 2);
                        }
                        else
                        {
                            dr["BulidPay"] = Math.Round(0.00, 2);
                        }

                        //dr["BulidPay"] = row[6].StringValue.Trim().ToString();
                        dr["Remark"] = row[7].StringValue.Trim();
                        dr["Use_Rule"] = row[8].StringValue.Trim();
                        dr["ManagerID"] = row[9].StringValue.Trim();
                        dr["Manager_Name"] = row[10].StringValue.Trim();
                        dr["Manager_Tel"] = row[11].StringValue.Trim();
                        dr["Manager_Mail"] = row[12].StringValue.Trim();
                        dr["Creator"] = CurrentUserInfo.PersonID;
                        dr["CreateTime"] = DateTime.Now;
                        dr["LastUpdateMan"] = CurrentUserInfo.PersonID;
                        dr["LastUpdateTime"] = DateTime.Now;
                        dr["DeleteMark"] = 0;

                        bll.InsertLaboratory(dr);
                        #endregion
                    }
                }
                #region 导入后跳转
                string URL = "";
                URL = "List.aspx";
                Response.Redirect(URL);
                #endregion
            }
        }
        public string CheckData(string savePath)
        {
            string err = "";
            Aspose.Cells.Workbook wb = new Aspose.Cells.Workbook();
            wb.Open(savePath);
            for (int i = 0; i < wb.Worksheets.Count; i++)
            {
                Aspose.Cells.Worksheet ws = wb.Worksheets[i];
                Aspose.Cells.Cells cells = ws.Cells;
                string name = wb.Worksheets[i].Name;
                Aspose.Cells.Row r = cells.Rows[0];
                #region 核对列
                if (i == 0)
                {
                    #region
                    if (r[0].StringValue.Trim() != "实验室编号")
                    {
                        err += "第1列应为 [ 实验室编号 ]，请核对模板;<br/>";
                    }
                    if (r[1].StringValue.Trim() != "实验室名称")
                    {
                        err += "第2列应为 [ 实验室名称 ]，请核对模板;<br/>";
                    }
                    if (r[2].StringValue.Trim() != "类型")
                    {
                        err += "第3列应为 [ 类型 ]，请核对模板;<br/>";
                    }
                    if (r[3].StringValue.Trim() != "财政来源")
                    {
                        err += "第4列应为 [ 财政来源 ]，请核对模板;<br/>";
                    }
                    if (r[4].StringValue.Trim() != "开放对象")
                    {
                        err += "第5列应为 [ 开放对象 ]，请核对模板;<br/>";
                    }
                    if (r[5].StringValue.Trim() != "建成时间")
                    {
                        err += "第6列应为 [ 建成时间 ]，请核对模板;<br/>";
                    }
                    if (r[6].StringValue.Trim() != "耗费资金")
                    {
                        err += "第7列应为 [ 耗费资金 ]，请核对模板;<br/>";
                    }
                    if (r[7].StringValue.Trim() != "备注")
                    {
                        err += "第8列应为 [ 备注 ]，请核对模板;<br/>";
                    }
                    if (r[8].StringValue.Trim() != "使用制度")
                    {
                        err += "第9列应为 [ 使用制度 ]，请核对模板;<br/>";
                    }
                    if (r[9].StringValue.Trim() != "管理员编号")
                    {
                        err += "第10列应为 [ 管理员编号 ]，请核对模板;<br/>";
                    }
                    if (r[10].StringValue.Trim() != "管理员")
                    {
                        err += "第1列应为 [ 管理员 ]，请核对模板;<br/>";
                    }
                    if (r[11].StringValue.Trim() != "管理员电话")
                    {
                        err += "第12列应为 [ 管理员电话 ]，请核对模板;<br/>";
                    }
                    if (r[12].StringValue.Trim() != "管理员邮件")
                    {
                        err += "第13列应为 [ 管理员邮件 ]，请核对模板;<br/>";
                    }

                    #endregion
                }
                #endregion

                if (err == "")
                {
                    for (int rIndex = 1; rIndex < cells.Rows.Count; rIndex++)
                    {
                        Aspose.Cells.Row row = cells.Rows[rIndex];
                        #region Patent

                        if (row[0].StringValue == "")
                        {
                            err += name + "中第" + (rIndex + 1).ToString() + "行 实验室编号为空;<br/>";
                            break;
                        }
                        else if (row[1].StringValue == "")
                        {
                            err += name + "中第" + (rIndex + 1).ToString() + "行 实验室名称为空;<br/>";
                            break;
                        }

                        if (row[0].StringValue.Length > 10)
                        {
                            err += name + "中第" + (rIndex + 1).ToString() + "行 实验室编号过长(不能大于10字符);<br/>";
                            break;
                        }
                        else if (row[1].StringValue.Length > 20)
                        {
                            err += name + "中第" + (rIndex + 1).ToString() + "行 实验室名称过长(不能大于20字符);<br/>";
                            break;
                        }

                        if (row[2].StringValue.Trim() != "" && row[2].StringValue.Trim() != "0" && row[2].StringValue.Trim() != "1" && row[2].StringValue.Trim() != "2")
                        {
                            err += name + "中第" + (rIndex + 1).ToString() + "行 不存在 [" + row[2].StringValue + "] 该类型;<br/>";
                            break;
                        }

                        if (row[3].StringValue.Trim() != "" && row[3].StringValue.Trim() != "0" && row[3].StringValue.Trim() != "1" && row[3].StringValue.Trim() != "2")
                        {
                            err += name + "中第" + (rIndex + 1).ToString() + "行 不存在 [" + row[3].StringValue + "] 该财政来源;<br/>";
                            break;
                        }

                        string[] openTo = row[4].StringValue.Split(',');
                        int k;
                        for ( k = 0; k < openTo.Length; k++)
                        {
                            if (openTo[k] == "研究所" || openTo[k] == "下属单位" || openTo[k] == "合作单位" || openTo[k] != "外来单位")
                            {
                                
                            }
                            else
                            {
                                err += name + "中第" + (rIndex + 1).ToString() + "行 不存在 [" + openTo[k] + "] 该开放对象;<br/>";
                                break;
                            }
                        }


                        if (row[5].StringValue != "")
                        {
                            try
                            {

                                Convert.ToDateTime(row[5].StringValue);
                            }
                            catch (Exception)
                            {
                                err += name + "中第" + (rIndex + 1).ToString() + "行 建成时间格式错误;<br/>";
                                break;
                            }
                        }

                        #endregion
                    }
                }
            }
            return err;
        }

    }
}