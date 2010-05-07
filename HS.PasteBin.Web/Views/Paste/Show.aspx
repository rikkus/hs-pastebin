<%@ Page Inherits="System.Web.Mvc.ViewPage<PasteInfo>" %>
<%@ Import Namespace="HS.PasteBin.Web.ViewModels"%>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <% if (Model == null) {%>
    
        <p><%= HS.PasteBin.Web.Properties.Resources.PasteNotFoundText %></p>
        
    <% } else { %>
    
        <div class="Paste">
            <div class="Meta">
                <span class="Date"><%= Model.CreatedAt.ToString("dddd, d MMMM yyyy, h:mm tt") %></span>
                <span class="Language"><%= Model.Language %></span>
            </div>
            <div class="Text"><pre><code><%= Model.Html %></code></pre></div>
        </div>
        
    <% } %>
    
</asp:Content>
