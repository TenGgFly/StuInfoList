<%@ Page Language="C#" AutoEventWireup="true" CodeFile="List.aspx.cs" CodeBehind="List.aspx.cs" Inherits="LaboratoryList.DEMO_Laboratory_List" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link rel="stylesheet" type="text/css" href="<%="/theme/"+Skin+"/global.css"%>" />
    <link rel="stylesheet" type="text/css" href="<%="/theme/"+Skin+"/list.css"%>" />
    <link rel="stylesheet" type="text/css" href="<%="/theme/"+Skin+"/easyui.css"%>" />
    <link rel="stylesheet" type="text/css" href="<%="/theme/icon.css"%>" />
    <script type="text/javascript" src="/Script/js/jquery.min.js"></script>
    <script type="text/javascript" src="/Script/js/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="/Script/js/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript" src="/Script/js/global.js"></script>
    <script type="text/javascript" src="/Script/js/datagrid.js"></script>
 <script src="/Script/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
     <script type="text/javascript">
         //金额的格式转换方法
         function fmoney(s, n) {
             if (s >= 0) {
                 n = n > 0 && n <= 20 ? n : 2;
                 s = parseFloat((s + "").replace(/[^\d\.-]/g, "")).toFixed(n) + "";
                 var l = s.split(".")[0].split("").reverse();
                 r = s.split(".")[1];
                 t = "";
                 for (i = 0; i < l.length; i++) {
                     t += l[i] + ((i + 1) % 3 == 0 && (i + 1) != l.length ? "," : "");
                 }
                 return t.split("").reverse().join("") + "." + r;
             }
             else if (s < 0) {
                 n = n > 0 && n <= 20 ? n : 2;
                 s = parseFloat((s + "").replace(/[^\d\.-]/g, "")).toFixed(n) + "";
                 var l = s.replace("-", "").split(".")[0].split("").reverse();
                 r = s.split(".")[1];
                 t = "";
                 for (i = 0; i < l.length; i++) {
                     t += l[i] + ((i + 1) % 3 == 0 && (i + 1) != l.length ? "," : "");
                 }
                 return "-" + t.split("").reverse().join("") + "." + r;
             }
             else {
                 return "0.00";
             }
         }
         var pageindex = "<%=PageNumber %>"; //页码
         var pagesize = "<%=PageSize %>"; //每页显示多少行
         var pageindex = "1"; //页码
         var pagesize = "20"; //每页显示多少行
         //页面首次加载调用loadgrid()方法
         $(function () {
             loadgrid();
         });
         function Turn(id, code) {
             return "<a href=\'View.aspx?LaboratoryID=" + id + "\'>" + code + "</a>";
         }
         //formatter: function (value, rowData, rowIndex) {调用的方法就写在这两个括号之间}
         function loadgrid() {
             var columns = [[//列表中展示的列  
                { field: 'ck', checkbox: true, width: 50 }, //第一列显示Checkbox  sortable: true表示可以自动排序，align: 'center',居中 ,align: 'left'居左,align: 'right'居右    

                {
                    field: 'Laboratory_Code', title: '实验室编号', width: 80, sortable: true,
                    formatter: function (value, rowData, rowIndex) {
                        return Turn(rowData.LaboratoryID, rowData.Laboratory_Code)
                    }
                },

                { field: 'Laboratory_Name', title: '实验室名称', width: 80, sortable: true },

                {
                    field: 'Type', title: '类型', width: 50, sortable: true,
                    formatter: function (value, rowData, rowIndex) {
                        if (rowData.Type == '0') {
                            return '生物';
                        }
                        else if (rowData.Type == '1') {
                            return '水产';
                        }
                        else if (rowData.Type == '2') {
                            return '电子';
                        }
                    }
                },

                {
                    field: 'Fiinancial_Source', title: '财政来源', width: 50, sortable: true,
                    formatter: function (value, rowData, rowIndex) {
                        if (rowData.Fiinancial_Source == '0') {
                            return '研究所';
                        }
                        else if (rowData.Fiinancial_Source == '1') {
                            return '下属单位';
                        }
                        else if (rowData.Fiinancial_Source == '2') {
                            return '合作单位';
                        }
                        else if (rowData.Fiinancial_Source == '2') {
                            return '外来单位';
                        }
                    }
                },
                { field: 'OpenTo', title: '开放对象', width: 80, sortable: true },

                { field: 'BulidTime', title: '建成时间', width: 100, sortable: true, formatter: formatDatebox },

                { field: 'BulidPay', title: '耗费资金', width: 80, sortable: true },

                { field: 'Manager_Name', title: '管理员', width: 80, sortable: true },

                { field: 'Manager_Tel', title: '管理员电话', width: 80, sortable: true },

                { field: 'Manager_Mail', title: '管理员邮件', width: 80, sortable: true },

                {
                    field: 'action', title: '操作', width: 100, align: 'center',
                    formatter: function (value, rowData, rowIndex) {
                        var e = "";
                        var d = "";
                        e = '<a href="#" onclick="edit(\'' + rowData.LaboratoryID + '\')"><img src ="/theme/default/images/Icon/edit.gif" title="编辑" style="vertical-align:middle"/></a>';
                        d = '<a href="#" onclick="del(\'' + rowData.LaboratoryID + '\')"><img src ="/theme/default/images/Icon/delete.png" title="删除" style="vertical-align:middle"/></a>';
                        return e + "&nbsp;&nbsp;" + d;
                    }
                }
             ]];
             //pms是传到DataJosn.ashx的参数，sortName是排序的名称，sortOrder是排序的顺序(包括desc和asc)，各个参数之间用逗号隔开。

             var pms = {
                 sortname: "",
                 sortorder: "",
                 laboratory_code: $.trim($("#txtLaboratory_Code").val()),
                 Laboratory_Name: $.trim(encodeURI($("#txtLaboratory_Name").val())),
                 Laboratory_Type: $("#txtLaboratory_Type").val(),
                 txtLaboratory_Income: $.trim($("#txtLaboratory_Income").val()),
                 txtLaboratory_Admin: $.trim($("#txtLaboratory_Admin").val()),
                 txtLaboratory_Obj: $("#txtLaboratory_Obj").val(),
                 txtLaboratory_Tel: $.trim($("#txtLaboratory_Tel").val()),
                 txtLaboratory_Mail: $.trim($("#txtLaboratory_Mail").val()),
                 txtLaboratory_Time: $.trim($("#txtLaboratory_Time").val()),
                 txtLaboratory_Cost: $.trim($("#txtLaboratory_Cost").val()),
                 txtAutoSearch: $.trim(encodeURI($("#txtAutoSearch").val()))
             };

             //设置查询条件
             //   tablename：表名,
             //   dataurl：获取数据页面
             //   Params：自定义参数
             //   isFitColumns:是否自动适应列宽
             //   isPagination:是否需要分页
             //   Columns:要显示的列及其属性、方法等
             //   Number: 当前第几页
             //   Size:每页显示多少条

             loaddata('tt', 'DataJosn.ashx', pms, true, true, columns, pageindex, pagesize);

             //设置列表页面自定义宽度和高度
             //table外面一层div的名称tt表名
             SetMainStyle('dataMain', 'tt');
         }
         function edit(ID) {
             location.href = "Info.aspx?Laboratory_ID=" + ID;
         }
         function del(ID) {
             //第一个id是某一行的ID，hidid是储存前面一个id的隐藏控件id,linkid是触发删除这一事件的linkbottonid
             delete_record(ID, 'hdnLaboratory_ID', 'LinkDel');
         }
         function delall() {
             var ids = "";
             var rows = $('#tt').datagrid('getSelections'); //获取选中的所有行
             for (var i = 0; i < rows.length; i++) {
                 ids += rows[i]['LaboratoryID'] + ',';
             }
             if (ids == "") {
                 jQuery.messager.alert("提示", "请至少选择一条数据", "info");
                 return;
             }
             msg1 = '确认此删除操作？';
             jQuery.messager.confirm("提示", msg1, function (r) {//点击确认删除操作后执行 if (r) {}之间的代码
                 if (r) {
                     //确认删除执行ID为LinkAllDel按钮的Onclick事件
                     $("#hdnLaboratory_ID").val(ids);
                     __doPostBack("LinkAllDel", "");
                 }
             });
         }
         //重置
         function resetsearch() {
             $("#txtAutoSearch").val("");
             $("#txtLaboratory_Code").val("");
             $("#txtLaboratory_Name").val("");
             $("#txtLaboratory_Type").val("");
             $("#txtLaboratory_Income").val("");
             $("#txtLaboratory_Admin").val("");
             $("#txtLaboratory_Obj").val("");
             $("#txtLaboratory_Tel").val("");
             $("#txtLaboratory_Mail").val("");
             $("#txtLaboratory_Time").val("");
             $("#txtLaboratory_Cost").val("");
         }
         //查询
         function doSearch(t) {
             pageindex = "1";
             loadgrid();
             return false;
         }


         function input() {
             location = 'InputDataList.aspx?Flag=Patent';
         }
    </script>
</head>
<body class="easyui-layout">
    <form id="form1" runat="server">
    <div data-options="region:'north'" style="height: 35px; overflow: hidden;" id="Div1">
        <table align="center" width="100%" class="HeadTable">
            <tr>
                <td class="HeadTitle">
                    <img src="/theme/icons/form.png" style="vertical-align: middle" /><span>实验室信息管理</span>
                </td>
                <td>
                    <div class="right" id="form_head">
                        <table border="0" cellpadding="0" cellspacing="0" class="search_bar">
                            <tr>
                                <td class="search_td1">
                                    <asp:TextBox ID="txtAutoSearch" runat="server"></asp:TextBox>
                                </td>
                                <td class="search_td2">
                                    <a id="search" runat="server" href="javascript:;" title="搜索" onclick="ShowSearchPanel('search')">
                                    </a>
                                </td>
                                <td style="border: none;">
                                    <asp:ImageButton ID="ImageButton1" runat="server" CssClass="search_btn" ImageUrl="~/theme/default/images/index/search.png"
                                        OnClientClick="return doSearch(1)" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="right">
                        <a class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-add'" runat="server"
                            href="Info.aspx" id="Add">新增</a>&nbsp;&nbsp; <a class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-del'"
                                runat="server" id="delall" href="javascript:delall();">删除</a>

                                <a class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-undo'" id="btn_Input"
                            runat="server" href="#" onclick="input()">导入</a>&nbsp;&nbsp;
                        <asp:LinkButton class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-redo'"
                            id="Link_OutPut" runat="server" OnClick="Link_OutPut_Click">导出</asp:LinkButton>&nbsp;&nbsp;
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <div data-options="region:'center'" id="dataMain" style="border: 0px">
        <table id="tt">
        </table>
    </div>
    <div id="search_panel">
        <table cellpadding="2" cellspacing="5">
            <tr>
                <td align="right">
                    实验室编号：
                </td>
                <td align="left">
                    <input id="txtLaboratory_Code" type="text" runat="server" class="BigInput" />
                    <font style="color: Red;">*</font>
                </td>
                <td align="right">
                    实验室名称：
                </td>
                <td align="left">
                    <input id="txtLaboratory_Name" type="text" runat="server" class="BigInput" />
                    <font style="color: Red;">*</font>
                </td>
            </tr>
            <tr>

                <td align="right">
                    财政来源：
                </td>
                <td align="left">
                    <asp:DropDownList ID="txtLaboratory_Income" runat="server">
                        <asp:ListItem Value="">---请选择---</asp:ListItem>
                        <asp:ListItem Value="0">市财政</asp:ListItem>
                        <asp:ListItem Value="1">省财政</asp:ListItem>
                        <asp:ListItem Value="1">中央财政</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td align="right">
                    类型：
                </td>
                <td align="left">
                    <asp:DropDownList ID="txtLaboratory_Type" runat="server">
                        <asp:ListItem Value="">---请选择---</asp:ListItem>
                        <asp:ListItem Value="0">生物</asp:ListItem>
                        <asp:ListItem Value="1">水产</asp:ListItem>
                        <asp:ListItem Value="1">电子</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="right">
                    管理员：
                </td>
                <td align="left">
                    <input type="text" class="BigInput" runat="server" id="txtLaboratory_Admin"
                    style="width: 150px" required="true" maxlength="10" validtype="length[0,9]" /><font style="color: Red;">*</font>
                </td>
                <td align="right">
                    开放对象：
                </td>
                <td align="left">
                    <asp:DropDownList ID="txtLaboratory_Obj" runat="server">
                        <asp:ListItem Value="">---请选择---</asp:ListItem>
                        <asp:ListItem Value="0">研究所</asp:ListItem>
                        <asp:ListItem Value="1">下属单位</asp:ListItem>
                        <asp:ListItem Value="1">合作单位</asp:ListItem>
                        <asp:ListItem Value="1">外来单位</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="right">
                    管理员电话：
                </td>
                <td class="left">
                    <input type="text" id="txtLaboratory_Tel" runat="server" class="BigInput" style="width: 150px"
                        onclick="WdatePicker()" />
                </td>
                <td  align="right">
                管理员邮件：
            </td>
            <td align="left">
                <input type="text" class="BigInput" runat="server" id="txtLaboratory_Mail"
                    style="width: 150px" required pattern="^[a-z0-9]+([._\\-]*[a-z0-9])*@([a-z0-9]+[-a-z0-9]*[a-z0-9]+.){1,63}[a-z0-9]+$" />
            </td>
            </tr>
            <tr>
                <td align="right">
                    建成时间：
                </td>
                <td class="left">
                    <input type="text" id="txtLaboratory_Time" runat="server" class="BigInput" style="width: 150px"
                        onclick="WdatePicker()" />
                </td>
                <td  align="right">
                耗费资金：
            </td>
            <td align="left">
                <input type="text" class="BigInput" runat="server" id="txtLaboratory_Cost"
                    style="width: 150px" onkeyup="value=value.replace(/[^\d.]/g,'')" />
            </td>
            </tr>
            <tr>
                <td colspan="4" align="center">
                    <a href="#" class="search-ok" onclick="CloseSearchPanel('search');doSearch(2);">
                    </a>&nbsp<a href="#" class="search-cannel" onclick="CloseSearchPanel('search')"></a>&nbsp;
                    <a href="#" class="search-reset" onclick="resetsearch()"></a>
                </td>
            </tr>
        </table>
    </div>
    <asp:LinkButton runat="server" ID="LinkDel" OnClick="LinkDel_Click"></asp:LinkButton>
    <asp:LinkButton runat="server" ID="LinkAllDel" OnClick="LinkAllDel_Click"></asp:LinkButton>
    <input id="hdnLaboratory_ID" type="hidden" runat="server" />
    </form>
</body>
</html>
