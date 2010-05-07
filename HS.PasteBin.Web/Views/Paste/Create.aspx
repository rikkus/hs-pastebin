<%@ Page Inherits="System.Web.Mvc.ViewPage<LanguageSelection>" %>
<%@ Import Namespace="HS.SyntaxHighlighting"%>
<%@ Import Namespace="HS.PasteBin.Web.ViewModels"%>
<%@ Import Namespace="System.Web.Configuration"%>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <form method="post" action="<%= Url.Action("Save") %>">
        <div class="Languages">
        
            <%
                Model.DefaultLanguages.EachWithIndex
                    (
                    (language, index) =>
                    Html.RenderPartial
                        (
                        "LanguageRadioButton",
                        new LanguageRadioModel { Language = language, Selected = index == 0 }
                        )
                    );
            %>
               
            <div class="Language">
                <input type="radio" name="Language" id="lang-other" value="<%= Constants.RequestKey.LanguageOther %>" title="<%= HS.PasteBin.Web.Properties.Resources.OtherLanguagesText %>" />
                <label class="Language" for="lang-other"><%= HS.PasteBin.Web.Properties.Resources.OtherLanguagesText %></label>
            </div>
            
            <select class="Language"
                name="<%= Constants.RequestKey.LanguageOther %>"
                onchange="document.getElementById('lang-other').checked = true;"
            >
                <% foreach (var language in Model.OtherLanguages) { %>
        			<option value="<%= language.Key %>"><%= language.Value %></option> 
                <% } %>
			</select>
			
        </div>
        <div class="Editor">
            <textarea
                id="PasteText"
                name="Text"
                rows="32"
                cols="100"
            ></textarea>
            <div class="Clearing" />
            <input type="submit" value="Submit" />
        </div>
    </form>
</asp:Content>
