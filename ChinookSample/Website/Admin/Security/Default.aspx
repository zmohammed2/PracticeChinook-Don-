﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Admin_Security_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="row jumbotron">
        <h1>User and Role Administration</h1>
    </div>

    <div class="row">
        <div class="col-md-12">
            <!--nav tabs-->
            <ul class="nav nav-tabs">
                <li class="active"><a href="#users" data-toggle="tab">Users</a></li>
                <li ><a href="#roles" data-toggle="tab">Roles</a></li>
                <li ><a href="#unregistered" data-toggle="tab">UnRegistered Users</a></li>
            </ul>
            <!--tab Content area-->
            <div class="tab-content">
                <!--User tab -->
                <div class="tab-pane fade in active" id="users">
                     <h1>Users</h1>
                </div> <%--eop--%>
                 <!--Role tab -->
                <div class="tab-pane fade" id="roles">
                   <%-- <%DataKeyNames contains the considered pKey field of class
                        that is being used insert, update or Delete --%>

                  <%--  RefreshAll will call a generic method in my code behind that will cause the ODS sets to re-bind their data--%>
                    <asp:ListView ID="RoleListView" runat="server" DataSourceID="RoleListViewODS" ItemType="ChinookSystem.Security.RoleProfile" InsertItemPosition="LastItem"
                        DataKeyNames="RoleId" OnItemInserted="RefreshAll" OnItemDeleted="RefreshAll">
                        <EmptyDataTemplate>
                            <span>No Security Roles Have been Set up.</span>
                        </EmptyDataTemplate>
                        <LayoutTemplate>
                           <div class="row bginfo">
                            <div class="col-sm-3 h4">Action</div>
                            <div class="col-sm-3 h4">RoleName</div>
                            <div class="col-sm-6 h4">Users</div>
                          </div>
                          <div  id="itemPlaceHolder" runat="server">
                          </div>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <div class="row">
                             <div class="col-sm-3 ">
                                 <asp:LinkButton ID="RemoveRole" runat="server" CommandName="Delete">Remove</asp:LinkButton>
                             </div>
                            <div class="col-sm-3 ">
                                <%# Item.RoleName %>
                            </div>
                            <div class="col-sm-6">
                                <asp:Repeater ID="RoleUsers" runat="server"
                                    DataSource="<%#Item.UserNames %>" ItemType="System.String">
                                    <ItemTemplate>
                                         <%# Item %>
                                    </ItemTemplate>
                                    <SeparatorTemplate>, </SeparatorTemplate>
                                </asp:Repeater>
                            </div></div>
                        </ItemTemplate>
                        <InsertItemTemplate>
                             <div class="row">
                            <div class="col-sm-3 ">
                                 <asp:LinkButton ID="InsertRole" runat="server" CommandName="Insert">Insert</asp:LinkButton>
                                  <asp:LinkButton ID="Cancel" runat="server">Cancel</asp:LinkButton>
                             </div>
                            <div class="col-sm-3 ">
                                <asp:TextBox ID="RoleName" runat="server" text='<%# BindItem.RoleName %>' placeholder="Role Name"></asp:TextBox>
                            </div></div>
                        </InsertItemTemplate>
                    </asp:ListView>
                    <asp:ObjectDataSource ID="RoleListViewODS" runat="server" 
                        DataObjectTypeName="ChinookSystem.Security.RoleProfile" 
                        DeleteMethod="RemoveRole" InsertMethod="AddRole" OldValuesParameterFormatString="original_{0}" 
                        SelectMethod="ListAllRoles" TypeName="ChinookSystem.Security.RoleManager">

                    </asp:ObjectDataSource>
                </div> <%--eop--%>
                 <!--User tab  
                      OnSelectedIndexChanging is during event and OnSelectedIndexChanged is during after the event-->
                <div class="tab-pane fade" id="unregistered">
                     <asp:GridView ID="UnregisteredUsersGridView" runat="server" 
                        AutoGenerateColumns="False" 
                        DataSourceID="UnregisteredUsersODS"
                         DataKeyNames="Id"
                      
                         ItemType="ChinookSystem.Security.UnRegisteredUserProfile" OnSelectedIndexChanging="UnregisteredUsersGridView_SelectedIndexChanging">
                        <Columns>
                            <asp:CommandField SelectText="Register" ShowSelectButton="True"></asp:CommandField>
                            <asp:BoundField DataField="UserType" HeaderText="UserType" SortExpression="UserType"></asp:BoundField>
                            <asp:BoundField DataField="FirstName" HeaderText="FirstName" SortExpression="FirstName"></asp:BoundField>
                            <asp:BoundField DataField="Lastname" HeaderText="Lastname" SortExpression="Lastname"></asp:BoundField>
                            <asp:TemplateField HeaderText="AssignedUserName" SortExpression="AssignedUserName">
                                <ItemTemplate>
                                    <asp:TextBox runat="server" Text='<%# Bind("AssignedUserName") %>' 
                                        ID="AssignedUserName"></asp:TextBox>
                                </ItemTemplate>
                               
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="AssignedEmail" SortExpression="AssignedEmail">
                                <ItemTemplate>
                                    <asp:TextBox runat="server" Text='<%# Bind("AssignedEmail") %>' 
                                        ID="AssignedEmail"></asp:TextBox>
                                </ItemTemplate>
                               
                            </asp:TemplateField>

                        </Columns>
                         <EmptyDataTemplate>
                             No unRegistered Users to Process.
                         </EmptyDataTemplate>
                    </asp:GridView>
                    <asp:ObjectDataSource ID="UnregisteredUsersODS" runat="server" 
                        OldValuesParameterFormatString="original_{0}" 
                        SelectMethod="ListAllUnRegisteredUsers" 
                        TypeName="ChinookSystem.Security.UserManager">
                    </asp:ObjectDataSource>
                </div> <%--eop--%>
            </div>
        </div>
    </div>
</asp:Content>

