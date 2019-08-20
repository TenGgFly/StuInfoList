<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InputDataList.aspx.cs" CodeBehind="InputDataList.aspx.cs" Inherits="YX.DEMO.DEMO.Laboratory.InputDataList" %>

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
    <script type="text/javascript">
        function myDownLoad() {
            __doPostBack("LinkDownLoad", "");
        }

        $(function () {

            $("#span6").css("display", ""); $("#titlt").text("实验室信息");
        })
        function myback() {
            location = 'List.aspx';
        }
    </script>
</head>
<body style="overflow: auto;">
    <form id="form1" runat="server">
    <table align="center" class="HeadTable" style="width: 75%; margin-top: 20px;">
        <tr>
            <td class="HeadTitle">
                <img src="/theme/icons/form.png" style="vertical-align: middle" /><span class="big3"><label
                    id="titlt" runat="server"></label></span>
            </td>
        </tr>
    </table>
    <table class="TableInfo" align="center" width="75%">
        <tr class="TableData" align="center">
            <td style="width: 100px;" align="right">
                <b>&nbsp;&nbsp;选择导入文件：</b>
            </td>
            <td style="width: 500px;" align="left">
                <asp:FileUpload ID="FileUpload1" class="BigInput" runat="server" />
            </td>
        </tr>
        <tr class="TableData" align="center">
            <td style="width: 120px;" align="right">
                <b>说明：</b>
            </td>
            <td style="width: 500px;" align="left">
<span id="span6" runat="server" style="display: none;">
                                        1、请导入.xls文件；
                                        <br />
                                        2、导入文件时，实验室编号、实验室名称为必填内容；<br />
                                        3、建成时间格式为：yyyy-MM-dd（如2013-01-01）。 </span>
                <br />
                <a href="javascript:myDownLoad();" id="btnOutPut" runat="server">[ 下载标准模板 ]</a>
            </td>
        </tr>
        <tr>
            <td class="TableControl" colspan="2" align="center">
                <asp:LinkButton ID="LinkInput" runat="server" data-options="iconCls:'icon-undo'"
                    class="easyui-linkbutton" OnClick="LinkInput_Click">导入</asp:LinkButton>
                <a id="btnBack" onclick="javascript:myback();" class="easyui-linkbutton" data-options="iconCls:'icon-back'">
                    返回</a>
            </td>
        </tr>
    </table>
    <table border="0" width="70%" align="center">
        <tr>
            <td style="color: Red; margin-top: 5px;">
                <asp:Literal ID="L_MSG" runat="server"></asp:Literal>
            </td>
        </tr>
    </table>
    <asp:LinkButton runat="server" ID="LinkDownLoad" OnClick="LinkDownLoad_Click">
    </asp:LinkButton>
    </form>
</body>
</html>
