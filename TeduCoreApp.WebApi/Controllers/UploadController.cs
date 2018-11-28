using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;

namespace TeduCoreApp.WebApi.Controllers
{
    public class UploadController : ApiController
    {
        private readonly IHostingEnvironment _env;

        public UploadController(IHostingEnvironment env)
        {
            _env = env;
        }

        [HttpPost]
        [Route("saveImage")]
        public async Task<IActionResult> SaveImage(string type = "")
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            try
            {
                string webHost = _env.WebRootPath;

                var httpRequest = HttpContext.Request.Form;

                foreach (var postedFile in httpRequest.Files)
                {
                    if (postedFile != null && postedFile.Length > 0)
                    {                                          

                        IList<string> AllowedFileExtensions = new List<string> { ".jpg", ".gif", ".png" };
                        var ext = postedFile.FileName.Substring(postedFile.FileName.LastIndexOf('.'));
                        var extension = ext.ToLower();
                        if (!AllowedFileExtensions.Contains(extension))
                        {
                            var message = string.Format("Please Upload image of type .jpg,.gif,.png.");

                            dict.Add("error", message);
                            return new OkObjectResult(message);
                        }
                        else
                        {
                            string directory = string.Empty;
                            if (type == "avatar")
                            {
                                directory = @"\UploadedFiles\Avatars\";
                            }
                            else if (type == "product")
                            {
                                directory = @"\UploadedFiles\Products\";
                            }
                            else if (type == "post")
                            {
                                directory = @"\UploadedFiles\Posts\";
                            }
                            else if (type == "banner")
                            {
                                directory = @"\UploadedFiles\Banners\";
                            }
                            else
                            {
                                directory = @"\UploadedFiles\";
                            }

                            if (!Directory.Exists(webHost + directory))
                            {
                                Directory.CreateDirectory(webHost + directory);
                                
                            }
                            string path = Path.Combine(webHost + directory, postedFile.FileName);

                            using (var stream = new FileStream(path, FileMode.Create))
                            {
                                await postedFile.CopyToAsync(stream);
                            }
                            return new OkObjectResult(Path.Combine(directory, postedFile.FileName));
                        }
                    }

                    var message1 = string.Format("Image Uploaded Failed.");
                    return new BadRequestObjectResult(message1); 
                }
                var res = string.Format("Please Upload a image.");
                dict.Add("error", res);
                return new BadRequestObjectResult(dict);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex); 
            }
        }
    }
}