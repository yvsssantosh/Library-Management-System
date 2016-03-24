﻿<%@ Page Title="" Language="C#" Theme="Theme1" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="delete.aspx.cs" Inherits="Library.Admin.a_delete" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css" >
		#tb_records_per_page
		{
			resize :horizontal;
			text-align: left;
		}
        #ContentPlaceHolder1_rptPager_lnkPage_0
        {
            padding-left:65px;
        }

	    .auto-style18 {
            width: 90%;
            overflow: auto;
        
            top: 381px;
            left: 166px;
            text-align: left;
            
        }
        #items
        {
            padding-left:61px;
        }
	</style>
	<script type="text/javascript">
		function isDelete() {
			return confirm("Do you want to delete this row ?");
		}
	</script>

</asp:Content>


<asp:Content ID="Content2" runat="server" contentplaceholderid="ContentPlaceHolder1">
   
	<div>
		<br />
		<br /><br />
	</div>
	<asp:ScriptManager ID="ScriptManager1" runat="server">
	</asp:ScriptManager>
	<asp:UpdatePanel ID="UpdatePanel1" runat="server">
		<ContentTemplate>
			<div id="items">
			<asp:TextBox ID="tb_search" runat="server" TextMode="Search"></asp:TextBox>
			&nbsp;&nbsp;&nbsp;&nbsp;
			<asp:Button ID="Button1" runat="server" Text="Search" OnClick="Button1_Click" />
            
			<br />
			<br />
			<br />
			
			<asp:Label ID="Label1" runat="server" Text="Page Size : "></asp:Label>
			&nbsp;&nbsp;&nbsp;
			
			<asp:TextBox ID="tb_records_per_page" value ="10" AutoPostBack="true" runat="server" OnTextChanged="tb_records_per_page_TextChanged" Width="24px"></asp:TextBox>
			<br />
			<br />
                <asp:Label ID="lb_reason" runat="server" Text="Reason To Delete :" Visible="False"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:TextBox ID="tb_reason" runat="server" Visible="False"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tb_reason" ErrorMessage="Reason to delete must be entered" ForeColor="Red"></asp:RequiredFieldValidator>
                <br />
                <br />
                </div>
            <div class="auto-style18" style="padding-left:61px;">
			<asp:GridView ID="GridView1" runat="server" OnRowEditing="GridView1_RowEditing" OnRowDeleting="GridView1_RowDeleting" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" OnSelectedIndexChanging="GridView1_SelectedIndexChanging" OnRowDataBound="GridView1_RowDataBound" >
				<Columns>
                    <asp:CommandField HeaderText="Delete" ShowDeleteButton="True" ShowHeader="True" />
                </Columns>
			</asp:GridView>
                </div>
			<br />
			<asp:Repeater ID="rptPager" runat="server" >
				<ItemTemplate>
					<asp:LinkButton ID="lnkPage" runat="server" Text = '<%#Eval("Text") %>' CommandArgument = '<%# Eval("Value") %>' Enabled = '<%# Eval("Enabled") %>' OnClick = "Page_Changed"></asp:LinkButton>
				</ItemTemplate>
			</asp:Repeater>
		</ContentTemplate>
	</asp:UpdatePanel>
</asp:Content>



