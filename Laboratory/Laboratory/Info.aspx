<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Info.aspx.cs" CodeBehind="Info.aspx.cs" Inherits="LaboratoryInfo.DEMO_Laboratory_showInfo" %>

<%@ Register Src="/Attachment/AttachmentMore.ascx" TagName="AttachmentMore" TagPrefix="uc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link rel="stylesheet" type="text/css" href="<%="/theme/"+Skin+"/global.css"%>" />
    <link rel="stylesheet" type="text/css" href="<%="/theme/"+Skin+"/info.css"%>" />
    <link rel="stylesheet" type="text/css" href="<%="/theme/"+Skin+"/easyui.css"%>" />
    <link rel="stylesheet" type="text/css" href="/theme/icon.css" />
    <link rel="stylesheet" type="text/css" href="/Attachment/Attachment.css" />
    <script type="text/javascript" src="/Script/js/jquery.min.js"></script>
    <script type="text/javascript" src="/Script/ArtDialog/jquery.artdialog.js?skin=blue"></script>
    <script type="text/javascript" src="/Script/ArtDialog/plugins/iframeTools.js"></script>
    <script type="text/javascript" src="/Script/js/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="/Script/js/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript" src="/Script/js/global.js"></script>
    <script src="/Script/js/public.js" type="text/javascript"></script>
    <script type="text/javascript" src="/Attachment/Attach.js"></script>
    <script src="/Script/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script type="text/javascript">
         function CheckForm() {
             if (!jQuery('#form1').form("validate"))
                 return false;
             //var regex2 = /\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*/;
             var reg = new RegExp("^[a-z0-9]+([._\\-]*[a-z0-9])*@([a-z0-9]+[-a-z0-9]*[a-z0-9]+.){1,63}[a-z0-9]+$");

             var email=$("#txtLaboratory_Mail").val();
             if ($("#txtLaboratory_Mail").val() != "") {
                 if (!reg.test($("#txtLaboratory_Mail").val())) {
                     //console.log(reg.test($("#txtLaboratory_Mail").val() + " " + email));
                     jQuery.messager.alert("提示", "电子邮箱格式不正确", "info");
                     return false;
                 }
             }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <table border="0" align="center" class="HeadTable" style="width: 95%; margin-top: 10px;">
        <tr>
            <td class="HeadTitle">
                <img src="/theme/icons/form.png" style="vertical-align: middle" />
                <span>
                    <label id="title" runat="server">
                        新增实验室信息
                    </label>
                </span>
            </td>
        </tr>
    </table>
    <table class="TableInfo" style="width: 95%;" align="center">
        <tr>
            <td class="TableContent" align="right" style="width: 100px">
                实验室编号：
            </td>
            <td class="TableData">
                <input type="text" class="BigStatic" runat="server" id="txtLaboratory_Code" style="width: 150px"
                    readonly="readonly" /><font style="color: Red;">*</font>
            </td>
            <td class="TableContent" align="right">
                实验室名称：
            </td>
            <td class="TableData">
                <input type="text" class="easyui-validatebox BigInput" runat="server" id="txtLaboratory_Name"
                    style="width: 150px" required="true" maxlength="10" validtype="length[0,9]" /><font
                        style="color: Red;">*</font>
            </td>
        </tr>
        <tr>
            <td class="TableContent" align="right">
                财政来源：
            </td>
            <td class="TableData">
                <input type="text" class="easyui-validatebox BigInput" runat="server" id="txtLaboratory_Income"
                    style="width: 150px" list="select" />
                <datalist id="select">
                    <option>市财政</option>
                    <option>省财政</option>
                    <option>中央财政</option>
                </datalist>
            </td>
            <td class="TableContent" align="right">
                类型：
            </td>
            <td class="TableData">
                <input type="radio" id="rdType1" runat="server" name="type" checked />生物&nbsp;
                <input type="radio" id="rdType2" runat="server" name="type" />水产&nbsp;
                <input type="radio" id="rdType3" runat="server" name="type" />电子
                <font style="color: Red;">*</font>
            </td>
        </tr>
        
        <tr>
            <td class="TableContent" align="right">
                建成日期：
            </td>
            <td class="TableData">
                <input type="text" id="txtLaboratory_Time" runat="server" class="BigInput" style="width: 150px"
                    onclick="WdatePicker()" />
            </td>
            <td class="TableContent" align="right">
                耗费资金：
            </td>
            <td class="TableData">
                <input type="text" class="easyui-validatebox BigInput" runat="server" id="txtLaboratory_Cost"
                    style="width: 150px" onkeyup="value=value.replace(/[^\d.]/g,'')" />
            </td>
        </tr>
        <tr>
            <td class="TableContent" align="right">
                开放对象：
            </td>
            <td class="TableData">
                <input type="checkbox" id="chkObj1" runat="server" value="研究所" />研究所
                <input type="checkbox" id="chkObj2" runat="server" value="下属单位" />下属单位
                <input type="checkbox" id="chkObj3" runat="server" value="合作单位" />合作单位
                <input type="checkbox" id="chkObj4" runat="server" value="外来单位" />外来单位
            </td>
            <td class="TableContent" align="right">
                管理员：
            </td>
            <td class="TableData">
                <input type="text" class="easyui-validatebox BigInput" runat="server" id="txtLaboratory_Admin"
                    style="width: 150px" required="true" maxlength="10" validtype="length[0,9]" /><font style="color: Red;">*</font>
            </td>
        </tr>
        <tr>
            <td class="TableContent" align="right">
                管理员电话：
            </td>
            <td class="TableData">
                <input type="text" class="easyui-validatebox BigInput" runat="server" id="txtLaboratory_Tel"
                    style="width: 150px"  maxlength="11" validtype="length[0,9]" />
            </td>
            <td class="TableContent" align="right">
                管理员邮件：
            </td>
            <td class="TableData">
                <input type="text" class="easyui-validatebox BigInput" runat="server" id="txtLaboratory_Mail"
                    style="width: 150px"/>
            </td>
        </tr>
        <tr>
            <td class="TableContent" align="right">
                备注：
            </td>
            <td class="TableData" colspan="3">
                <asp:TextBox ID="txtLaboratory_Note" class="BigInput" runat="server" Rows="6" Style="width: 98%"
                    TextMode="MultiLine" >
                </asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="TableContent" align="right">
                使用制度：
            </td>
            <td class="TableData" colspan="3">
                <asp:TextBox ID="txtLaboratory_System" class="BigInput" runat="server" Rows="6" Style="width: 98%"
                    TextMode="MultiLine">
                </asp:TextBox>
            </td>
        </tr>
        
        <tr id="attachment1">
            <td class="TableContent" align="right">
                附件：
            </td>
            <td class="TableData" colspan="5">
                <uc1:AttachmentMore ID="AttachmentMore1" runat="server" TableName="DEMO_Employee" />
            </td>
        </tr>
        <tr>
            <td colspan="4" align="center" class="TableControl">
                <asp:LinkButton ID="btn_save" runat="server" class="easyui-linkbutton" data-options="iconCls:'icon-save'"
                    OnClick="btn_save_Click" OnClientClick="return CheckForm();">保存</asp:LinkButton>
                <asp:LinkButton ID="btn_back" runat="server" class="easyui-linkbutton" data-options="iconCls:'icon-back'"
                    OnClick="btn_cancel_ServerClick">返回</asp:LinkButton>
            </td>
        </tr>

    </table>
    </form>
</body>
</html>
