using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using System;
using System.IO;
using System.Threading.Tasks;

namespace SharedMolecules.UI.ViewRenderService
{
    public class ViewRenderService : IViewRenderService
    {
        #region Properties
        private readonly IRazorViewEngine RazorViewEngine;
        private readonly ITempDataProvider TempDataProvider;
        private readonly IServiceProvider ServiceProvider;
        #endregion

        #region Constructor
        public ViewRenderService(IRazorViewEngine razorViewEngine,
           ITempDataProvider tempDataProvider,
           IServiceProvider serviceProvider)
        {
            RazorViewEngine = razorViewEngine;
            TempDataProvider = tempDataProvider;
            ServiceProvider = serviceProvider;
        }
        #endregion

        #region Methods
        public async Task<string> RenderToStringAsync(string viewName, object model)
        {
            var actionContext = CreateAndReturnNewActionContext(CreateAndReturnNewDefaultHttpContext());

            using (var stringWriter = new StringWriter())
            {
                var viewResult = FindViewByName(viewName, actionContext);

                if (viewResult.View == null)
                {
                    throw ThrowNullException(viewName);
                }

                var viewContext = new ViewContext(
                    actionContext,
                    viewResult.View,
                    CreateViewDataDictionaryByModel(model),
                    new TempDataDictionary(actionContext.HttpContext, TempDataProvider),
                    stringWriter,
                    new HtmlHelperOptions()
                );

                await viewResult.View.RenderAsync(viewContext);
                return stringWriter.ToString();
            }
        }
        #region Private Methods
        private DefaultHttpContext CreateAndReturnNewDefaultHttpContext()
        {
            return new DefaultHttpContext { RequestServices = ServiceProvider };
        }

        private ActionContext CreateAndReturnNewActionContext(DefaultHttpContext httpContext)
        {
            return new ActionContext(httpContext, new RouteData(), new ActionDescriptor());
        }

        private ViewEngineResult FindViewByName(string viewName, ActionContext context)
        {
            return RazorViewEngine.FindView(context, viewName, false);
        }

        private ArgumentNullException ThrowNullException(string viewName)
        {
            return new ArgumentNullException($"{viewName} does not match any available view");
        }

        private ViewDataDictionary CreateViewDataDictionaryByModel(object model)
        {
            return new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary())
            {
                Model = model
            };
        }
        #endregion

        #endregion

    }
}
