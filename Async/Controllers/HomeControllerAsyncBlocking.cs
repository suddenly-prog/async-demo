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
    public class HomeControllerAsyncBlocking : Controller
    {
        [HttpGet]
        public Task<IActionResult> GetPageSizeAsync()
        {
            WebClient wc = new WebClient();

            return Task<IActionResult>.Factory.StartNew(() =>
            {
                try
                {
                    byte[] apressData = wc.DownloadData("http://apress.com");

                    int result = apressData.Length;

                    return Ok(result);
                }
                catch (Exception ex)
                {
                    // logging etc

                    return StatusCode(500);
                }
            });
        }
    }
}