<%@ Page Title="" Language="C#" MasterPageFile="~/Sitecore/Admin/MySite.Master" AutoEventWireup="true" CodeBehind="BrowseDoco.aspx.cs" Inherits="MyDoco.BrowseDoco" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-md-10">
            <div class="container">
                <h2>About Item</h2>
                <div class="panel panel-default">
                    <div class="panel-heading">Details</div>
                    <div class="panel-body">

                        <dl>
                            <dt>Path</dt>
                            <dd>- 
                        <asp:Label ID="lblContentItemName" runat="server" Text="Label"></asp:Label></dd>
                            <dt>Name</dt>
                            <dd>- 
                        <asp:Label ID="lblItemName" runat="server" Text="Label"></asp:Label></dd>
                            <dt>Template</dt>
                            <dd>- 
                        <asp:Label ID="lblTemplateName" runat="server" Text="Label"></asp:Label></dd>

                        </dl>

                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-10">
            <div class="container">
                <h2>Fields Details</h2>
                <p>An item is made up of fields and field sections. For e.g field can be single, rich text or multi text and so on</p>
                <asp:Table ID="tblFields" runat="server" CssClass="table table-bordered">
                    <asp:TableHeaderRow>
                        <asp:TableHeaderCell>Field Name</asp:TableHeaderCell>
                        <asp:TableHeaderCell>Field Type</asp:TableHeaderCell>
                        <asp:TableHeaderCell>Field Section</asp:TableHeaderCell>
                    </asp:TableHeaderRow>
                    <asp:TableFooterRow></asp:TableFooterRow>
                </asp:Table>
            </div>

        </div>
    </div>
    <div class="row">
        <div class="col-md-10">
            <div class="container">
                <h2>Layout</h2>
                <p>An item can have one layout per device.All components/Rendering have this layout.</p>
                <asp:Table ID="tblLayout" runat="server" CssClass="table table-bordered">
                    <asp:TableHeaderRow>
                        <asp:TableHeaderCell>Layout Id</asp:TableHeaderCell>
                        <asp:TableHeaderCell>Layout Name</asp:TableHeaderCell>
                        <asp:TableHeaderCell>Device</asp:TableHeaderCell>
                    </asp:TableHeaderRow>
                    <asp:TableFooterRow></asp:TableFooterRow>
                </asp:Table>
            </div>

        </div>
    </div>
    <div class="row">
        <div class="col-md-10">
            <div class="container">
                <h2>Component</h2>
                <p>A Placeholder is a sitecore control that allows you to bind components to an area on a page. It is identified with the key. </p>
                <p>A Datasource can be any item in the sitecore tree that contains data.</p>
                <asp:Table ID="tblPresentation" runat="server" CssClass="table table-bordered">
                    <asp:TableHeaderRow>
                        <asp:TableHeaderCell>Rendering Name</asp:TableHeaderCell>
                        <asp:TableHeaderCell>Placeholder</asp:TableHeaderCell>
                        <asp:TableHeaderCell>Datasource</asp:TableHeaderCell>
                    </asp:TableHeaderRow>
                    <asp:TableFooterRow></asp:TableFooterRow>
                </asp:Table>
            </div>

        </div>
    </div>
    <div class="row">
        <div class="col-md-10">
            <div class="container">
                <h2>Data Template</h2>
                <p>
                    An item type is determined by the data template used to create it. All data templates inherit from standard template(which can be custom base template that is derived from standard template)
                    The data template have field sections,fields, template icon and Base template inheritance.
                </p>
                <asp:Table ID="tblDataTemplate" runat="server" CssClass="table table-bordered">
                    <asp:TableHeaderRow>
                        <asp:TableHeaderCell>ID</asp:TableHeaderCell>
                        <asp:TableHeaderCell>Name</asp:TableHeaderCell>
                        <asp:TableHeaderCell>Help Text</asp:TableHeaderCell>
                    </asp:TableHeaderRow>
                    <asp:TableFooterRow></asp:TableFooterRow>
                </asp:Table>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-10">
            <div class="container">
                <h2>Standard Values Rendering</h2>
                <p>Data template -Standard Values: Presentation Rendering:The below set of renderings are part of standard values of data template.</p>
                <asp:Table ID="tblStandardValues" runat="server" CssClass="table table-bordered">
                    <asp:TableHeaderRow>
                        <asp:TableHeaderCell>Rendering Name</asp:TableHeaderCell>
                        <asp:TableHeaderCell>Placeholder</asp:TableHeaderCell>
                        <asp:TableHeaderCell>Datasource</asp:TableHeaderCell>
                    </asp:TableHeaderRow>
                    <asp:TableFooterRow></asp:TableFooterRow>
                </asp:Table>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-10">
            <div class="container">
                <h2>Base Template</h2>
                <p>All data templates inherit from standard template.If modify the base template,it will effect the item immediately.</p>
                <asp:Table ID="tblTemplate" runat="server" CssClass="table table-bordered">
                    <asp:TableHeaderRow>
                        <asp:TableHeaderCell>Sr.No</asp:TableHeaderCell>
                        <asp:TableHeaderCell>ID</asp:TableHeaderCell>
                        <asp:TableHeaderCell>Name</asp:TableHeaderCell>
                    </asp:TableHeaderRow>
                    <asp:TableFooterRow></asp:TableFooterRow>
                </asp:Table>
            </div>
        </div>
    </div>

    <div class="row">
        <div class="col-md-10">
            <div class="container">
                <h2>Base template fields</h2>
                <div class="panel panel-default">
                    <div class="panel-heading">Fields Defination</div>
                    <div class="panel-body">
                        <p>The below are the list of template fields desinged inside a base template and data template.</p>
                        <asp:PlaceHolder ID="plhBaseTemplateFields" runat="server"></asp:PlaceHolder>
                    </div>
                </div>
            </div>
        </div>
    </div>
    </div>
</asp:Content>
