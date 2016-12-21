using MyProject.Models;
using MyProjectBLL.Persons;
using MyProjectDAL.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyProject.Controllers
{
    public class PersonController : Controller
    {

        private PersonManagement _PersonManagement;

        public PersonController() : this(new PersonManagement()) { }
        public PersonController(PersonManagement personManagement)
        {
            _PersonManagement = personManagement;
        }
        // GET: Person
        public ActionResult Index()
        {
            var model=  _PersonManagement.GetAll();
            
            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Person model)
        {
            try
            {
                if (model == null) return RedirectToAction("NotFound");

                if (ModelState.IsValid)
                {
                    _PersonManagement.AddPerson(model);
                    return RedirectToAction("Index");
                }
                model = null;
                return View(model);
            }
            catch(Exception)
            {
                throw;
                return RedirectToAction("NotFound");
            }
        }
        public ActionResult DisplayHttpNotFound()
        {
            var name = (string)RouteData.Values["id"];
            var model = _PersonManagement.GetByName(name);

            if (model == null)
                return HttpNotFound("Pet Not Found...");

            return View(model);
        }
        public JsonResult GetInfo()
        {
            var name = (string)RouteData.Values["id"];

            var pet = _PersonManagement.GetByName(name);

            return Json(pet, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetPhoto()
        {
            var name = (string)RouteData.Values["id"];
            ViewBag.Photo = string.Format("/Content/Uploads/{0}.jpg", name);

            return PartialView();
        }

        public ActionResult NotFound()
        {
            return View();
        }


        public FileResult DownloadPetPicture()
        {
            var name = (string)RouteData.Values["id"];
            var picture = "/Content/Uploads/" + name + ".jpg";
            var contentType = "image/jpg";
            var fileName = name + ".jpg";
            return File(picture, contentType, fileName);
        }

        public HttpStatusCodeResult CustomError()
        {
            return new HttpStatusCodeResult(410, "My Custom Error");
        }

        public HttpStatusCodeResult UnauthorizedError()
        {
            return new HttpUnauthorizedResult("Custom Unauthorized Error");
        }

        public ActionResult NotFoundError()
        {
            return HttpNotFound("Nothing here...");
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PictureUpload(PictureModel model)
        {
            if (model.PictureFile.ContentLength > 0)
            {
                var fileName = Path.GetFileName(model.PictureFile.FileName);
                var filePath = Server.MapPath("/Content/Uploads");
                string savedFileName = Path.Combine(filePath, fileName);
                model.PictureFile.SaveAs(savedFileName);
                _PersonManagement.CreateThumbnail(fileName, filePath, 100, 100, true);
            }
            return View(model);
        }

        public ActionResult Display()
        {
            var name = (string)RouteData.Values["id"];
            var model = _PersonManagement.GetByName(name);
            if (model == null)
                return RedirectToAction("NotFound");
            return View(model);
        }

        public FileResult DownloadPersonPicture()
        {
            var name = (string)RouteData.Values["id"];
            var picture = "/Content/Uploads/" + name + ".jpg";
            var contentType = "image/jpg";
            var fileName = name + ".jpg";
            return File(picture, contentType, fileName);
        }

    }
}
