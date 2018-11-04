﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TeduCoreApp.WebApi.Provider;

namespace TeduCoreApp.WebApi.Controllers
{

    public class ProductController : ApiController
    {
       
        [HttpPost]
        [Permission(Action = "Read", Function = "USER")]
        public IActionResult Get()
        {
            return new OkObjectResult("success");
        }
    }
}