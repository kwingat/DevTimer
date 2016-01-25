﻿using System;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace DevTimer.Helpers
{
    public static class ActionLinkHelpers
    {
        // Render BootStrap menu item with active class
        public static MvcHtmlString MenuItem(this HtmlHelper htmlHelper, string text, string action, string controller,
            object routeValues = null, object htmlAttributes = null)
        {
            TagBuilder li = new TagBuilder("li");
            RouteData routeData = htmlHelper.ViewContext.RouteData;
            string currentAction = routeData.GetRequiredString("action");
            string currentController = routeData.GetRequiredString("controller");

            if (string.Equals(currentAction, action, StringComparison.OrdinalIgnoreCase) &&
                string.Equals(currentController, controller, StringComparison.OrdinalIgnoreCase))
            {
                li.AddCssClass("active");
            }

            if (routeValues != null)
            {
                li.InnerHtml = (htmlAttributes != null)
                    ? htmlHelper.ActionLink(text, action, controller, routeValues, htmlAttributes).ToHtmlString()
                    : htmlHelper.ActionLink(text, action, controller, routeValues).ToHtmlString();
            }
            else
            {
                li.InnerHtml = (htmlAttributes != null)
                    ? htmlHelper.ActionLink(text, action, controller, null, htmlAttributes).ToHtmlString()
                    : htmlHelper.ActionLink(text, action, controller).ToHtmlString();
            }

            return MvcHtmlString.Create(li.ToString());
        }


        // As the text the: "<span class='glyphicon glyphicon-plus'></span>" can be entered
        public static MvcHtmlString NoEncodeActionLink(this HtmlHelper htmlHelper, string text, string title,
            string action, string controller, object routeValues = null, object htmlAttributes = null)
        {
            UrlHelper urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);

            TagBuilder builder = new TagBuilder("a");
            builder.InnerHtml = text;
            builder.Attributes["title"] = title;
            builder.Attributes["href"] = urlHelper.Action(action, controller, routeValues);
            builder.MergeAttributes(new RouteValueDictionary(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes)));

            return MvcHtmlString.Create(builder.ToString());
        }

        public static MvcHtmlString If(this MvcHtmlString value, bool evaluation)
        {
            return evaluation ? value : MvcHtmlString.Empty;
            //@Html.ActionLink("Create New", "Create").If(User.IsInRole("Administrators"))
        }
    }
}