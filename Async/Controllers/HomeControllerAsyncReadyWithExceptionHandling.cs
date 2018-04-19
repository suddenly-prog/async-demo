using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace WebApp.Controllers
{
    [Route("api/data")]
    public class HomeControllerAsyncReadyWithExceptionHandling : Controller
    {
        [HttpGet]
        public Task<IActionResult> GetPageSizeAsync()
        {
            WebClient wc = new WebClient();

            Task<byte[]> apressDataTask = wc.DownloadDataTaskAsync("http://apress.com");

            Task<IActionResult> apressDataLengthTask = apressDataTask.ContinueWith<IActionResult>((Task<byte[]> task) =>
            {
                var errors = task.Exception as AggregateException;

                if (errors == null)
                {
                    return Ok(task.Result.Length);
                }
                else
                {
                    Exception actualException = errors.InnerExceptions.First();

                    // logging etc

                    return StatusCode(500);
                }
            });

            return apressDataLengthTask;
        }
    }
}