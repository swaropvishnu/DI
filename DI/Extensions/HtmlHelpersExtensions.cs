
using Jmelosegui.Mvc.GoogleMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
namespace DI
{
    public static class HtmlHelpersExtensions
    {
        public static ExampleConfigurator Configurator(this HtmlHelper instance, string title)
        {
            return new ExampleConfigurator(instance).Title(title);
        }

        public static IHtmlString CheckBox(this HtmlHelper html, string id, bool isChecked, string labelText)
        {
            return (html.CheckBox(id, isChecked) + new HtmlElement("label").Attribute("for", id).Html(labelText).ToString()).Raw();
        }

        public static IHtmlString Raw(this string value)
        {
            return new HtmlString(value);
        }
        public static TAttribute GetModelAttribute<TAttribute>
(this ViewDataDictionary viewData, bool inherit = false) where TAttribute : Attribute
        {
            if (viewData == null) throw new ArgumentException("ViewData");
            var containerType = viewData.ModelMetadata.ContainerType;
            return ((TAttribute[])containerType.GetProperty(viewData.ModelMetadata.PropertyName).GetCustomAttributes(typeof(TAttribute), inherit)).FirstOrDefault();

        }
        public static IHtmlString RenderScripts(this HtmlHelper htmlHelper)
        {
            var scripts = htmlHelper.ViewContext.HttpContext.Items["Scripts"] as IList<string>;
            if (scripts != null)
            {
                var builder = new StringBuilder();
                foreach (var script in scripts)
                {
                    builder.AppendLine(script);
                }
                return new MvcHtmlString(builder.ToString());
            }
            return null;
        }
    }
}
