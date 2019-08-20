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

namespace LaboratoryList
{
    public partial class DEMO_Laboratory_List : BasePage
    {
        public string PageNumber = "1";//页码
        public string PageSize = "20";//每页显示多少条
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }
        LijmBLL bll = new LijmBLL();
        protected void LinkDel_Click(object sender, EventArgs e)
        {
            bll.DelLaboratory(hdnLaboratory_ID.Value);//删除某个实验室信息(HidPersonID储存的是人员ID)删除的时候将deletemark这个标记置为1
            Response.Redirect("List.aspx");
        }
        protected void LinkAllDel_Click(object sender, EventArgs e)
        {
            string[] arry = hdnLaboratory_ID.Value.Substring(0, hdnLaboratory_ID.Value.Length - 1).Split(',');//删除选中的员工信息
            for (int i = 0; i < arry.Length; i++)
            {
                bll.DelLaboratory(arry[i]);
            }
            Response.Redirect("List.aspx");
        }
        protected void Link_OutPut_Click(object sender, EventArgs e)
        {
            try
            {
                string query = " and 1=1 ";

                if (!string.IsNullOrWhiteSpace(txtAutoSearch.Text))//判断当前传下来的这个值是不是为空
                {
                    query += " and (Laboratory_Code like '%" + txtAutoSearch.Text + "%' or Laboratory_Code like '%" + txtAutoSearch + "%')";
                }

                if (!string.IsNullOrWhiteSpace(txtLaboratory_Code.Value))
                {
                    query += " and Laboratory_Code like '%" + txtLaboratory_Code.Value + "%'";
                }

                if (!string.IsNullOrWhiteSpace(txtLaboratory_Name.Value))
                {
                    query += " and Laboratory_Name like '%" + txtLaboratory_Name.Value + "%'";
                }

                 if (!string.IsNullOrWhiteSpace(txtLaboratory_Income.SelectedValue))
                {
                    query += " and Fiinancial_Source='" + txtLaboratory_Income.SelectedValue + "'";
                }
                 if (!string.IsNullOrWhiteSpace(txtLaboratory_Type.SelectedValue))
                {
                    query += " and Type='" + txtLaboratory_Type.SelectedValue + "'";
                }

                if (!string.IsNullOrWhiteSpace(txtLaboratory_Admin.Value))
                {
                    query += " and Manager_Name like '%" + txtLaboratory_Admin.Value + "%'";
                }

                if (!string.IsNullOrWhiteSpace(txtLaboratory_Obj.SelectedValue))
                {
                    query += " and OpenTo='" + txtLaboratory_Obj.SelectedValue + "'";
                }

                if (!string.IsNullOrWhiteSpace(txtLaboratory_Tel.Value))
                {
                    query += " and Manager_Tel like '%" + txtLaboratory_Tel.Value + "%'";
                }


                if (!string.IsNullOrEmpty(txtLaboratory_Mail.Value ))
                {
                    query += " and Manager_Mail like'%" + txtLaboratory_Mail.Value + "%'";
                }

                if (!string.IsNullOrEmpty(txtLaboratory_Time.Value))
                {
                    query += " and BulidTime'" + Convert.ToDateTime(txtLaboratory_Time.Value) + "'";
                }
                if (!string.IsNullOrWhiteSpace(txtLaboratory_Cost.Value))
                {
                    query += " and BulidPay like '%" + txtLaboratory_Cost.Value + "%'";
                }
                DataTable dt = bll.GetOutData(query);
                int[] ColumnWidths = new int[12] { 10, 10, 20, 10, 10, 20, 20, 30, 10, 20, 20 ,20};

                Aspose.Cells.WorkbookDesigner designer = new Aspose.Cells.WorkbookDesigner();
                Aspose.Cells.Workbook workbook = new Aspose.Cells.Workbook(); //工作簿 
                Aspose.Cells.Worksheet sheet = workbook.Worksheets[0];
                Aspose.Cells.Cells cells = sheet.Cells;//单元格 
                Aspose.Cells.Style styleTitle = workbook.Styles[workbook.Styles.Add()];//新增样式 
                styleTitle.HorizontalAlignment = Aspose.Cells.TextAlignmentType.Center;//文字居中 
                styleTitle.Font.Name = "宋体";//文字字体 
                styleTitle.Font.Size = 18;//文字大小 
                styleTitle.Font.IsBold = true;//粗体

                ////加背景水印图片
                //string filepath = Server.MapPath("/templates/背景.jpg");
                //if (System.IO.File.Exists(filepath) == true)
                //{
                //    FileStream fsMyfile = new FileStream(filepath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                //    //创建一个二进制数据流读入器，和打开的文件关联
                //    BinaryReader brMyfile = new BinaryReader(fsMyfile);
                //    //把文件指针重新定位到文件的开始
                //    brMyfile.BaseStream.Seek(0, SeekOrigin.Begin);
                //    byte[] bytes = brMyfile.ReadBytes(Convert.ToInt32(fsMyfile.Length.ToString()));
                //    //关闭以上的new的各个对象
                //    sheet.SetBackground(bytes);
                //    fsMyfile.Close();
                //    brMyfile.Close();
                //}

                ////样式2 
                Aspose.Cells.Style style2 = workbook.Styles[workbook.Styles.Add()];//新增样式 
                style2.HorizontalAlignment = Aspose.Cells.TextAlignmentType.Center;//文字居中 
                style2.Font.Name = "宋体";//文字字体 
                style2.Font.Size = 12;//文字大小 
                style2.Font.IsBold = true;
                style2.Borders[Aspose.Cells.BorderType.LeftBorder].LineStyle = Aspose.Cells.CellBorderType.Thin;
                style2.Borders[Aspose.Cells.BorderType.RightBorder].LineStyle = Aspose.Cells.CellBorderType.Thin;
                style2.Borders[Aspose.Cells.BorderType.TopBorder].LineStyle = Aspose.Cells.CellBorderType.Thin;
                style2.Borders[Aspose.Cells.BorderType.BottomBorder].LineStyle = Aspose.Cells.CellBorderType.Thin;

                ////样式3 
                Aspose.Cells.Style style3 = workbook.Styles[workbook.Styles.Add()];//新增样式 
                style3.HorizontalAlignment = Aspose.Cells.TextAlignmentType.Center;//文字居中 
                style3.Font.Name = "宋体";//文字字体 
                style3.Font.Size = 12;//文字大小 
                style3.IsTextWrapped = true;//单元格内容自动换行 
                style3.Borders[Aspose.Cells.BorderType.LeftBorder].LineStyle = Aspose.Cells.CellBorderType.Thin;
                style3.Borders[Aspose.Cells.BorderType.RightBorder].LineStyle = Aspose.Cells.CellBorderType.Thin;
                style3.Borders[Aspose.Cells.BorderType.TopBorder].LineStyle = Aspose.Cells.CellBorderType.Thin;
                style3.Borders[Aspose.Cells.BorderType.BottomBorder].LineStyle = Aspose.Cells.CellBorderType.Thin;

                int Colnum = dt.Columns.Count;//表格列数 
                int Rownum = dt.Rows.Count;//表格行数
                cells.Merge(0, 0, 1, Colnum);//合并单元格 
                cells[0, 0].PutValue("实验室信息 ");//填写内容 
                cells[0, 0].SetStyle(styleTitle);
                cells.SetRowHeight(0, 30);
                for (int i = 0; i < Colnum; i++)
                {
                    cells[1, i].PutValue(dt.Columns[i].ColumnName);
                    cells[1, i].SetStyle(style2);
                    cells.SetRowHeight(1, 30);
                    cells.SetColumnWidth(i, ColumnWidths[i]);
                }
                for (int i = 0; i < Rownum; i++)
                {
                    for (int k = 0; k < Colnum; k++)
                    {
                        cells[2 + i, k].PutValue(dt.Rows[i][k].ToString());
                        cells[2 + i, k].SetStyle(style3);
                    }
                    cells.SetRowHeight(2 + i, 30);
                }

                string UserAgent = Request.ServerVariables["http_user_agent"].ToLower();
                string cnFilename = "";
                if (UserAgent.IndexOf("firefox") == -1)//非火狐浏览器
                {
                    cnFilename = HttpUtility.UrlEncode("实验室信息 ", System.Text.Encoding.UTF8);
                }
                else
                {
                    cnFilename = "实验室信息 ";
                }

                String filename = string.Format("{0}{1}.xls", cnFilename, Convert.ToDateTime(DateTime.Now).ToString("yyyyMMddhhmmss")); //文件默认命名方式，可以自定义
                Response.ContentType = "application/ms-excel;charset=utf-8";
                Response.AddHeader("content-disposition", "attachment; filename=" + filename);
                System.IO.MemoryStream memStream = workbook.SaveToStream();
                Response.BinaryWrite(memStream.ToArray());
                Response.End();
            }
            catch (Exception c)
            {
                c.Message.ToString();
            }
        }
}
}