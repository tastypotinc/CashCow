#region Namespaces

using System;
using System.Web.Mvc;
using System.Web.Script.Serialization;

#endregion Namespaces

namespace CashCow.Web.MvcHelpers
{
    /// <summary>
    /// Class to deserialize JSON and construct required object. Originally by Steven Sanderson to handle full postback. 
    /// </summary>
    public class FromJsonAttribute : CustomModelBinderAttribute
    {
        private readonly static JavaScriptSerializer _serializer = new JavaScriptSerializer();

        public override IModelBinder GetBinder()
        {
            return new JsonModelBinder();
        }

        private class JsonModelBinder : IModelBinder
        {
            public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
            {
                // This was the original solution by Steven Sanderson to handle the full postback. This is not required now.
                //var stringified = controllerContext.HttpContext.Request[bindingContext.ModelName];

                // This is what I wrote. In case of partial postback, data is contained in first request param for both GET and POST methods.
                var model = new object();
                var jsonData = controllerContext.HttpContext.Request.Params.GetValues(null);
                
                if (jsonData != null && jsonData.Length > 0)
                {
                    try
                    {
                        var stringified = controllerContext.HttpContext.Server.UrlDecode(jsonData[0]);
                        model = _serializer.Deserialize(stringified, bindingContext.ModelType);
                    }
                    catch (Exception)
                    {
                        model = null;
                    }
                }

                return model;
            }
        }
    }
}