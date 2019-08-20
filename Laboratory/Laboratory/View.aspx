<%@ Page Language="C#" AutoEventWireup="true" CodeFile="View.aspx.cs" CodeBehind="View.aspx.cs" Inherits="YX.DEMO.DEMO.Laboratory.View" %>

<%@ Register Src="../../Attachment/AttachmentMore.ascx" TagName="AttachmentMore"
    TagPrefix="uc1" %>
<%@ Register Src="../../Attachment/AttachmentShow.ascx" TagName="AttachmentShow"
    TagPrefix="uc2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link rel="stylesheet" type="text/css" href="<%="/theme/"+Skin+"/global.css"%>" />
    <link rel="stylesheet" type="text/css" href="<%="/theme/"+Skin+"/info.css"%>" />
    <link rel="stylesheet" type="text/css" href="<%="/theme/"+Skin+"/easyui.css"%>" />
    <link rel="stylesheet" type="text/css" href="<%="/theme/icon.css"%>" />
    <script type="text/javascript" src="/Script/js/jquery.min.js"></script>
    <script type="text/javascript" src="/Script/js/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="/Script/js/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript" src="/Script/js/global.js"></script>
    <script src="/Script/js/public.js" type="text/javascript"></script>
    <script src="/Script/js/public.js" type="text/javascript"></script>
    <script src="/Script/js/attach.js" type="text/javascript"></script>

</head>
<body>
    <form id="form1" runat="server">
    <table border="0" align="center" class="HeadTable" style="width: 95%; margin-top: 10px;">
        <tr>
            <td class="HeadTitle">
                <img src="/theme/icons/form.png" style="vertical-align: middle" />
                <span>
                    <label id="title" runat="server">
                        查看实验室信息
                    </label>
                </span>
            </td>
        </tr>
    </table>
    <table class="TableInfo" style="width: 95%;" align="center">
        <tr>
            <td class="TableContent" align="right" style="width: 15%">
                实验室编号：
            </td>
            <td class="TableData" style="width: 35%">
                <label runat="server" id="txtLaboratory_Code">
                </label>
            </td>
            <td class="TableContent" align="right" style="width: 15%">
                实验室名称：
            </td>
            <td class="TableData" style="width: 35%">
                <label runat="server" id="txtLaboratory_Name">
                </label>
            </td>
        </tr>
        <tr>
            <td class="TableContent" align="right">
                类型：
            </td>
            <td class="TableData">
                <label runat="server" id="rdType">
                </label>
            </td>
            <td class="TableContent" align="right">
                财政来源：
            </td>
            <td class="TableData">
                <label runat="server" id="txtLaboratory_Income">
                </label>
            </td>
        </tr>
        <tr>
            <td class="TableContent" align="right">
                开放对象：
            </td>
            <td class="TableData">
                <label runat="server" id="chkObj">
                </label>
            </td>
            <td class="TableContent" align="right">
                建成时间：
            </td>
            <td class="TableData">
                <label runat="server" id="txtLaboratory_Time">
                </label>
            </td>
        </tr>
        <tr>
            <td class="TableContent" align="right">
                耗费资金：
            </td>
            <td class="TableData">
                <label runat="server" id="txtLaboratory_Cost">
                </label>
            </td>
            <td class="TableContent" align="right">
                管理员：
            </td>
            <td class="TableData">
                <label runat="server" id="txtLaboratory_Admin">
                </label>
            </td>
        </tr>
        <tr>
            <td class="TableContent" align="right">
                管理员电话：
            </td>
            <td class="TableData">
                <label runat="server" id="txtLaboratory_Tel">
                </label>
            </td>
            <td class="TableContent" align="right">
                管理员邮件：
            </td>
            <td class="TableData">
                <label runat="server" id="txtLaboratory_Mail">
                </label>
            </td>
        </tr>
        <tr>
            <td class="TableContent" align="right">
                备注：
            </td>
            <td class="TableData" colspan="3">
                <label runat="server" id="txtLaboratory_Note">
                </label>
            </td>
        </tr>
        <tr>
            <td class="TableContent" align="right">
                使用制度：
            </td>
            <td class="TableData" colspan="3">
                <label runat="server" id="txtLaboratory_System">
                </label>
            </td>
        </tr>
        <tr id="EDITOR">
            <td class="TableContent" style="width: 15%" align="right">
                附件：
            </td>
            <td class="TableData" colspan="3">
                <uc2:AttachmentShow ID="AttachmentShow1" IsDel="true" RotateStore="true" Tablename="DEMO_Employee"
                    runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="4" align="center" class="TableControl">
                <asp:LinkButton ID="btn_back" runat="server" class="easyui-linkbutton" data-options="iconCls:'icon-back'"
                    OnClick="btn_cancel_ServerClick">返回</asp:LinkButton>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>