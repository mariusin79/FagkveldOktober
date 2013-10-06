using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace FagkveldOktober
{
    public static class HtmlExtension
    {
        public static IHtmlString Serialize(this HtmlHelper html, object model)
        {
            return html.Raw(JsonConvert.SerializeObject(model, new JsonSerializerSettings {ContractResolver = new CamelCasePropertyNamesContractResolver()}));

        }
    }
}
