﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TeduCoreApp.WebApi.Controllers
{

    public class ProductController : ApiController
    {
       
        [HttpGet]
        public IActionResult Get()
        {
            return new OkObjectResult("success");
        }
    }
}