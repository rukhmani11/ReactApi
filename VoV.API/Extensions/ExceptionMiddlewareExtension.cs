using Newtonsoft.Json;
using VoV.Core.Enum;
using VoV.Data.DTOs;
using Serilog;
using System.Net;

namespace VoV.API.Extensions
{
    //Handling Errors Globally with the Built-In Middleware
    public class ExceptionMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                string errorId = Guid.NewGuid().ToString();
                //LogContext.PushProperty("ErrorId", errorId);
                //LogContext.PushProperty("StackTrace", ex.StackTrace);

                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                string typeOfException = ex.GetType().Name, exceptionMessage = string.Empty;

                //Log.Error(ex.StackTrace);

                if (string.IsNullOrEmpty(exceptionMessage))
                {
                    if (ex.InnerException != null)
                    {
                        if (ex.InnerException.InnerException != null)
                        {
                            exceptionMessage = ex.InnerException.InnerException.Message;
                        }
                        else
                        {
                            exceptionMessage = ex.InnerException.Message;
                        }
                    }
                    else
                    {
                        exceptionMessage = ex.Message;
                    }
                }

                #region Handle typeOfExceptions accordingly

                //Int32 errorCode = (ex.InnerException != null ? ((SqlException)ex.InnerException).Number : 0);

                // Entity Validation Errors
                if (typeOfException == "DbUpdateException")
                {
                    // Cannot delete this record. Its been referred in another table(s)
                    //Delete, ForeignKey Conflict
                    //if (errorCode == 547)
                    //{
                    int startIndex = 0, endIndex = 0;
                    string conflictedColumn = string.Empty;

                    if (exceptionMessage.Contains("The DELETE statement conflicted"))
                    {
                        startIndex = exceptionMessage.IndexOf("\"") + 1;
                        if (startIndex > 1)
                        {
                            endIndex = exceptionMessage.IndexOf("\"", startIndex);
                            conflictedColumn = exceptionMessage.Substring(startIndex, (endIndex - startIndex));
                        }
                        exceptionMessage = "Cannot delete record. Its been referred in another table(s). " + (!string.IsNullOrEmpty(conflictedColumn) ? "'" + conflictedColumn + "'" : string.Empty);
                    }
                    else if (exceptionMessage.Contains("conflicted with the FOREIGN KEY constraint"))
                    {
                        startIndex = exceptionMessage.IndexOf("column ") + 7;
                        if (startIndex > 7)
                        {
                            endIndex = exceptionMessage.IndexOf(".", startIndex);
                            conflictedColumn = exceptionMessage.Substring(startIndex, (endIndex - startIndex));
                        }
                        exceptionMessage = "Foreign Key Confliction : value of column " + conflictedColumn + " not found in table.";
                    }
                    //}

                }

                FailureModel errorResult = new FailureModel()
                {
                    //destinationSystem = context.Request.Headers["SourceSystem"],
                    //sourceSystem = context.Request.Headers["DestinationSystem"],
                    //messageId = context.Request.Headers["MessageId"],
                    //errorId = errorId,
                    //source = ex.TargetSite?.DeclaringType?.FullName,
                    isSuccess = false,
                    //status = ResponseStatusEnum.Failure.ToString(),
                    //errorCode = context.Response.StatusCode,
                    message = string.IsNullOrEmpty(exceptionMessage) ? ex.Message : exceptionMessage,
                    //SupportMessage = $"Provide the Error Id: {errorId} to the support team for further analysis."
                };

                switch (ex)
                {
                    //case CustomException e:
                    //    errorResult.StatusCode = (int)e.StatusCode;
                    //    if (e.ErrorMessages is not null)
                    //    {
                    //        errorResult.Messages = e.ErrorMessages;
                    //    }

                    //    break;

                    //case KeyNotFoundException:
                    //    errorResult.StatusCode = (int)HttpStatusCode.NotFound;
                    //    break;

                    //default:
                    //    errorResult.StatusCode = (int)HttpStatusCode.InternalServerError;
                    //    break;
                }
                Log.Error(exceptionMessage);
                Log.Error("******************************************************************************************");
                var response = context.Response;
                if (!response.HasStarted)
                {
                    response.ContentType = "application/json";
                    //response.StatusCode = errorResult.errorCode;
                    await response.WriteAsync(JsonConvert.SerializeObject(errorResult));
                }
                else
                {
                    Log.Warning("Can't write error response. Response has already started.");
                }
                #endregion
            }//ends catch
        }
    }
}
