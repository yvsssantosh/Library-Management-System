<%@ Page Title="" Language="C#" Theme="Theme1" MasterPageFile="~/Student.master" AutoEventWireup="true" CodeBehind="profile.aspx.cs" Inherits="Library.s_profile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style18 {
            width: 90%;
            overflow: auto;
            padding-left:61px;
            top: 381px;
            left: 166px;
            text-align: left;
            
        }
        .auto-style20 {
            width: 187px;
            height: 134px;
            top: 293px;
            left: 433px;
        }
        .auto-style21 {
            width: 187px;
            height: 133px;
            top: 565px;
            left: 430px;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="auto-style18">
        <asp:GridView ID="GridView1" runat="server" CssClass="auto-style20">
        </asp:GridView>
        </div>
        <div class="auto-style18">
            <br />
            <br />
            <br />
        <asp:GridView ID="GridView2" runat="server" CssClass="auto-style21">
        </asp:GridView>
    </div>

</asp:Content>

