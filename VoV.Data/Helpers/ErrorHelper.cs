using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Reflection;
using VoV.Core.Enum;
using VoV.Data.DTOs;

namespace VoV.Data.Helpers
{
    public class ErrorHelper
    {
        public void InterfaceApiResponse()
        {

        }

        public void getCurrentProjectPath()
        {
            //string assemblyFile = (new System.Uri(Assembly.GetExecutingAssembly().CodeBase)).AbsolutePath;
            //string fullFilePath = Path.Combine((new System.Uri(Assembly.GetExecutingAssembly().CodeBase)).AbsolutePath.Split(new string[] { "/bin" }, StringSplitOptions.None)[0]
            //              , "@/Images/test.png");

            string startupPath = System.IO.Directory.GetCurrentDirectory();
            string startupPath1 = Environment.CurrentDirectory;
            var sqlFile = Path.Combine("~/Context/Scripts/SqlServer.StoredProcedures.sql");
        }


        // Below method extracts model state errors and assigns to the properties of Custom class.    
        public static BadRequestObjectResult CustomErrorResponse(ActionContext actionContext)
        {
            var modelState = actionContext.ModelState;
            List<string> lstError = new List<string>();
            List<string> lstErrorFields = new List<string>();
            // List<string> lstErrorCode = new List<string>();
            if (!modelState.IsValid)
            {
                var modelstateError = modelState.Where(modelError => modelError.Value.Errors.Count > 0).ToList();
                lstErrorFields = modelstateError.Select(x => x.Key).ToList();
                // lstErrorCode = GetErrorCodes(lstErrorFields);
                var errorList = modelState.Values.SelectMany(m => m.Errors).ToList();

                foreach (var error in errorList)
                {
                    if (!string.IsNullOrEmpty(error.ErrorMessage))
                    {
                        lstError.Add(error.ErrorMessage);
                    }
                    else if (error.Exception != null)
                    {
                        lstError.Add(error.Exception.Message);
                    }
                }
            }

            return new BadRequestObjectResult(new FailureModel()
            {
                //messageId = actionContext.HttpContext.Request.Headers["MessageId"],
                isSuccess = false,
                //status = ResponseStatusEnum.Failure.ToString(),
                //errorCode = Convert.ToInt16(InterfaceErrorEnum.InvalidModelState),
                message = string.Join(", ", lstError)
            });
        }

        public static ModelStateDictionary AddErrorsToModelState(IdentityResult identityResult, ModelStateDictionary modelState)
        {
            foreach (var e in identityResult.Errors)
            {
                modelState.TryAddModelError(e.Code, e.Description);
            }
            return modelState;
        }

        public static ModelStateDictionary AddErrorToModelState(string code, string description, ModelStateDictionary modelState)
        {
            modelState.TryAddModelError(code, description);
            return modelState;
        }

    }
}
