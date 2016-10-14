﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="RepresentitiveCustomers.aspx.cs" Inherits="Queries_RepresentitiveCustomers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:DropDownList ID="EmployeeList" runat="server" 
        DataSourceID="EmployeeListODS"
         DataTextField="Name" 
        DataValueField="EmployeeId">
    </asp:DropDownList>
    <asp:ObjectDataSource ID="EmployeeListODS" runat="server" 
        OldValuesParameterFormatString="original_{0}" 
        SelectMethod="EmployeeNameList_Get" 
        TypeName="ChinookSystem.BLL.EmployeeController">
    </asp:ObjectDataSource>
    <br />
    <asp:Button ID="SubmitQuery" runat="server" Text="Fetch" /><br /><br />
    <asp:GridView ID="RepCustomerList" runat="server" AutoGenerateColumns="False" DataSourceID="RepCustomerODS" AllowPaging="True">
        <Columns>
            <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name"></asp:BoundField>
            <asp:BoundField DataField="City" HeaderText="City" SortExpression="City"></asp:BoundField>
            <asp:BoundField DataField="State" HeaderText="State" SortExpression="State"></asp:BoundField>
            <asp:BoundField DataField="Phone" HeaderText="Phone" SortExpression="Phone"></asp:BoundField>
            <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email"></asp:BoundField>
        </Columns>
    </asp:GridView>
    <asp:ObjectDataSource ID="RepCustomerODS" runat="server" 
        OldValuesParameterFormatString="original_{0}"
         SelectMethod="RepresentitiveCustomers_Get" 
        TypeName="ChinookSystem.BLL.CustomerController">
        <SelectParameters>
            <asp:ControlParameter ControlID="EmployeeList" PropertyName="SelectedValue" Name="employeeid" Type="Int32"></asp:ControlParameter>
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>

