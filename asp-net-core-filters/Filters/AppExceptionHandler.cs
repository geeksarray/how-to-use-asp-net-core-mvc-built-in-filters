using asp_net_core_filters.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace asp_net_core_filters.Filters
{
    public class AppExceptionHandler : IExceptionFilter
    {
        private readonly IModelMetadataProvider _modelMetadataProvider;
        public AppExceptionHandler(
            IModelMetadataProvider modelMetadataProvider)
        {
            _modelMetadataProvider = modelMetadataProvider;
        }

        public void OnException(ExceptionContext context)
        {
            ErrorViewModel errorViewModel = new ErrorViewModel();
            errorViewModel.ErrorMessage = context.Exception.Message;
            errorViewModel.Source = context.Exception.StackTrace;
             
            ViewResult errorViewResult = new ViewResult
            {
                ViewName = "error", 
                ViewData = new ViewDataDictionary(_modelMetadataProvider,
                                context.ModelState)
                {
                    Model = errorViewModel
                }
            };
            context.ExceptionHandled = true; 
            context.Result = errorViewResult;
        }
    }
}
