using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.Controllers
{
    [Route("api/data")]
    public class HomeControllerAsyncReady : Controller
    {
        [HttpGet]
        public Task<IActionResult> GetPageSizeAsync()
        {
            WebClient wc = new WebClient();

            Task<byte[]> apressDataTask = wc.DownloadDataTaskAsync("http://apress.com");
            
            Task<IActionResult> apressDataLengthTask = apressDataTask.ContinueWith<IActionResult>((Task<byte[]> task) =>
            {
                return Ok(task.Result.Length);
            });

            return apressDataLengthTask;
        }
    }
}