
using BookingForHumanService.API.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Text.Json;

namespace BookingForHumanService.API.Middleware
{
    public class ExceptionHandlingMiddleWare
    {
        private readonly RequestDelegate _requestDelegate;
        private readonly ILogger<ExceptionHandlingMiddleWare> _logger;
        public ExceptionHandlingMiddleWare(RequestDelegate requestDelegate, ILogger<ExceptionHandlingMiddleWare> logger)
        {
            _requestDelegate = requestDelegate;
            _logger = logger;
        }
        // Method
           public async   Task InvokeAsync(HttpContext context) 
            {
            //try Request
            try
            {
                await _requestDelegate(context);
            }
            //catch Exception
            catch (Exception ex)
            {
                //log errors in Path
                _logger.LogError(ex, "Exception In : {Path} ",context.Request.Path);
                // throw Status Code
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                //return it in a json
                context.Response.ContentType= "application/json";
                // parse message to json
                var response = ApiResponse<string>.Fail(ex.Message);

                await context.Response.WriteAsync(JsonSerializer.Serialize(response));
            }



            }


       










    }
}
