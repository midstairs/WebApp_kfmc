<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Developer.aspx.cs" Inherits="WebDevs.Demo.Developer" 
    EnableEventValidation="false" ValidateRequest="false"%>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <br />
    
    Welcome to my developer page ...<br />
    <br />
    <table class="nav-justified">
        <tr>
            <td colspan="2" style="height: 12px">
                <asp:Label ID="lblOutput" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td id="D" style="width: 173px; font-size: medium;"><strong>DeveloperId</strong></td>
            <td>
                <asp:TextBox ID="txtDeveloperId" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 173px; height: 22px; font-size: medium;"><strong>First Name</strong></td>
            <td style="height: 22px">
                <asp:TextBox ID="txtFName" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 173px; font-size: medium; height: 28px;"><strong>Last Name </strong> </td>
            <td style="height: 28px">
                <asp:TextBox ID="txtLName" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 173px; height: 22px; font-size: medium;"><strong>Email</strong></td>
            <td style="height: 22px">
                <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 173px; font-size: medium;"><strong>Courses</strong></td>
            <td>
                <asp:DropDownList ID="ddlCourses" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="width: 173px">&nbsp;</td>
            <td>
                <asp:Button ID="btnInser" runat="server" Text="Insert" OnClick="btnInser_Click" />
                <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click" />
                <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click" />
                <asp:Button ID="btnSelect" runat="server" Text="Select" OnClick="btnSelect_Click" />
                <asp:Button ID="btnExportToExcel" runat="server" Text="Export to Excel" OnClick="btnExportToExcel_Click" />
                <asp:Button ID="btnEmail" runat="server" Text="Email" OnClick="btnEmail_Click" />
                <asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click" Text="Clear" />
            </td>
        </tr>
    </table>
    <br />
    <div>
                <asp:GridView ID="gvDeveloper" runat="server" AutoGenerateColumns="False" DataKeyNames="DeveloperId" OnSelectedIndexChanged="gvDeveloper_SelectedIndexChanged">

            <Columns>
                <asp:BoundField DataField="DeveloperId" HeaderText="DeveloperId" ReadOnly="True" SortExpression="DeveloperId" />
                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="DeveloperId">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkupdate" runat="server"  
                                        CommandArgument='<%# Bind("DeveloperId") %>' OnClick="populateForm_Click"
                                        Text='<%# Eval("DeveloperId")  %>'></asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="left"></ItemStyle>
                            </asp:TemplateField>
                <asp:BoundField DataField="FName" HeaderText="FName" SortExpression="FName" />
                <asp:BoundField DataField="LName" HeaderText="LName" SortExpression="LName" />
                <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
                <asp:BoundField DataField="CourseName" HeaderText="CourseName" SortExpression="CourseName" />
                <asp:BoundField DataField="CourseDescription" HeaderText="CourseDescription" SortExpression="CourseDescription" />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
