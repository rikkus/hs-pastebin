<%@ Page Inherits="System.Web.Mvc.ViewPage<string>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Properties.Resources.InstallationResultTitle</h2>
    
    <p><%=ViewData["Message"]%></p>
    
    <% if (!(bool) ViewData["Result"]) { %>
    
        <p>
            <a href="<%=Url.Action("Install")%>"><%=
                HS.PasteBin.Web.Properties.Resources.InstallTryAgainText
            %></a>
        </p>
    <% } %>

</asp:Content>
