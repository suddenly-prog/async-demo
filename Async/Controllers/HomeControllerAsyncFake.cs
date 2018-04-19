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
    public class HomeControllerAsyncFake : Controller
    {
        [HttpGet]
        // for interface implementation when asynchronous flow is excessive
        // or for future :) realization
        public Task<IActionResult> GetPageSizeAsync()
        {
            WebClient wc = new WebClient();

            try
            {
                byte[] apressData = wc.DownloadData("http://apress.com");

                int result = apressData.Length;

                return Task.FromResult<IActionResult>(Ok(result));
            }
            catch (Exception ex)
            {
                // logging etc

                return Task.FromResult<IActionResult>(StatusCode(500));
            }
        }
    }
}