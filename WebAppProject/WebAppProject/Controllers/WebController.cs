using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.UI.WebControls;
using WebAppProject.Models;

namespace WebAppProject.Controllers
{
    public class WebController : ApiController
    {
        WebDataAPIEntities db = new WebDataAPIEntities();
        [HttpGet]
        [Route("Category")]
        public IEnumerable<CatogryModel> GetCategory()
        {
            List<CatogryModel> catlist;
            return catlist = db.Categories.ToArray()

                             .Select(s => new CatogryModel()
                             {
                                 CatId = s.CatId,
                                 CatName = s.CatName

                             }).ToList<CatogryModel>();
        }
          [HttpGet]
        [ResponseType(typeof(Product))]
        public IQueryable<Product> GetProducts()
        {
            return db.Products;
        }

        [HttpPost]
       
        public IHttpActionResult PostAddProduct([FromBody]ProductModel model)
        {


            List<string> savedFilePath = new List<string>();
            // Check if the request contains multipart/form-data
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }
            //Get the path of folder where we want to upload all files.
            string rootPath = HttpContext.Current.Server.MapPath("~/Content/Images");
            var provider = new MultipartFileStreamProvider(rootPath);
            // Read the form data.
            //If any error(Cancelled or any fault) occurred during file read , return internal server error
            var task = Request.Content.ReadAsMultipartAsync(provider).
                ContinueWith<HttpResponseMessage>(t =>
                {
                    if (t.IsCanceled || t.IsFaulted)
                    {
                        Request.CreateErrorResponse(HttpStatusCode.InternalServerError, t.Exception);
                    }
                    foreach (MultipartFileData dataitem in provider.FileData)
                    {
                        try
                        {
                            //Replace / from file name
                            string name = dataitem.Headers.ContentDisposition.FileName.Replace("\"", "");
                            //Create New file name using GUID to prevent duplicate file name
                            string newFileName = Guid.NewGuid() + Path.GetExtension(name);
                            //Move file from current location to target folder.
                            File.Move(dataitem.LocalFileName, Path.Combine(rootPath, newFileName));


                        }
                        catch (Exception ex)
                        {
                            string message = ex.Message;
                        }
                    }

                    return Request.CreateResponse(HttpStatusCode.Created, savedFilePath);
                });


            Product prdct = new Product();
            prdct.Name = model.Name;
            prdct.price = model.price;
            prdct.Description = model.Description;
            //----------------------------------conversion--------------------------
            

            db.Products.Add(prdct);
            db.SaveChanges();
            return Ok(new { Message = "error message" });

        }

}
}
