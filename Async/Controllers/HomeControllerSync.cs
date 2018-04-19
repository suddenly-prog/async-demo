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
    public class HomeControllerSync : Controller
    {
        [HttpGet]
        public IActionResult GetPageSize()
        {
            WebClient wc = new WebClient();

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
        }
    }
}