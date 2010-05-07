<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LanguageRadioModel>" %>
<%@ Import Namespace="HS.PasteBin.Web.ViewModels"%>
<div class="Language">
    <input
        type="radio"
        name="Language"
        <%= Model.Selected ? "checked=\"checked\"" : "" %>
        id="lang-<%= Model.Language.Key %>"
        value="<%= Model.Language.Key %>"
        title="<%= Model.Language.Name %>"
        onchange="document.getElementById('PasteText').focus();"
    />
    <label class="Language" for="lang-<%= Model.Language.Key %>">
        <%= Model.Language.Name %>
    </label>
</div>

