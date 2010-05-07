<%@ Page Inherits="System.Web.Mvc.ViewPage<IEnumerable<RecentPasteInfo>>" %>
<%@ Import Namespace="HS.PasteBin.Web.ViewModels"%>
<%@ Import Namespace="HS.PasteBin.Data"%>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <% if (!Model.Any()) { %>
    
            <p><%= HS.PasteBin.Web.Properties.Resources.NoPastesFoundText %></p>
            
    <% } else { %>
    
        <% foreach (var pasteInfo in Model) { %>
            <div>
            <a
                class="PastePreviewLink"
                href="<%= Url.Action("Show", new { pasteInfo.ID }) %>"
            >
                <span class="PastePreview">
                    <span class="Language"><%= pasteInfo.LanguageName %></span>
                    <span class="Date"><%= pasteInfo.CreatedAt.ToString("dddd, d MMMM yyyy, h:mm tt") %></span>
                    <span class="Text"><pre><code><%= pasteInfo.Preview %></code></pre></span>
                </span>
            </a>
            <div class="Clearing"></div>
            </div>
        <% } %>
        
    <% } %>
    
 </asp:Content>
