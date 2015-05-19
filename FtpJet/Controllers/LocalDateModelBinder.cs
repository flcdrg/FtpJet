using System;
using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;
using System.Web.Http.ValueProviders;
using NodaTime;
using NodaTime.Text;

namespace FtpJet.Controllers
{
    internal class LocalDateModelBinder : IModelBinder
    {
        public bool BindModel(HttpActionContext actionContext, ModelBindingContext bindingContext)
        {
            if (bindingContext.ModelType != typeof(LocalDate))
                return false;

            ValueProviderResult val = bindingContext.ValueProvider.GetValue(
            bindingContext.ModelName);
            if (val == null)
            {
                return false;
            }

            string text = val.RawValue as string;
            if (text == null)
            {
                bindingContext.ModelState.AddModelError(bindingContext.ModelName, "Wrong value type");
                return false;
            }

            bindingContext.Model = LocalDatePattern.IsoPattern.Parse(text).Value;
            return true;
        }
    }
}