using DemoBS23.BLL.Services;
using System;
using System.Collections.Generic;

namespace DemoBS23.API.Utilities
{
    public static class AppExceptionHandlerExtension
    {
        public static IList<string> AppExceptionHandler(this Exception ex)
        {
            IList<string> ErrorMessages = new List<string>();
            //ErrorMessages.Add(ex.StackTrace);
            ErrorMessages.Add(ex.Message);

            if (ex != null)
            {
                var innerEx = ex.InnerException;

                while (innerEx != null)
                {
                    ErrorMessages.Add(innerEx.Message);
                    innerEx = innerEx.InnerException;
                }
            }
            return ErrorMessages;
            //ResultSet<IList<string>> errors = new ResultSet<IList<string>>();
        }
    }
}
