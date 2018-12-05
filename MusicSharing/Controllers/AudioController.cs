using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common;
using BusinessLogic;
using System.IO;

namespace MusicSharing.Controllers
{
    [Authorize]
    public class AudioController : Controller
    {
        // GET: Audio
        public ActionResult Index()
        {
            AudioBL myAudio = new AudioBL(User.IsInRole("Admin"));
            var list = myAudio.GetAudios().ToList();

            return View(list);
        }

        public ActionResult Details(string id)
        {
            try
            {
                var originalValue = Encryption.DecryptQueryString(id);

                AudioBL myAudios = new AudioBL(User.IsInRole("Admin"));
                var item = myAudios.GetAudio(Convert.ToInt32(originalValue));

                return View(item);
            }
            catch (Exception ex)
            {
                TempData["errormessage"] = "Access denied or value invalid";
                return RedirectToAction("Index");
            }
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Create(Audio a, HttpPostedFileBase fileData)
        {
            try
            {
                if (ModelState.IsValid)
                {
                   

                    string uniqueFilename = Guid.NewGuid() + Path.GetExtension(fileData.FileName);


                    string absolutePath = Server.MapPath(@"\Images") + @"\";

                    fileData.SaveAs(absolutePath + uniqueFilename); 

                    a.audioURL = @"\Audios\" + uniqueFilename;

                    new AudioBL(User.IsInRole("Admin")).AddAudio(a.audioname,a.audioURL,a.audioID,a.genreID);
                    TempData["message"] = "Item added successfully";
                    return RedirectToAction("Index");

                }



                return View(a);
            }
            catch (Exception ex)
            {
                

                TempData["errormessage"] = "Item was not added";
                return View(a);

            }


        }



        public ActionResult Delete(int id)
        {
            try
            {
                new AudioBL(User.IsInRole("Admin")).DeleteAudio(id);
                Logger.Log("", Request.Path, "Item " + id + " was deleted");

                TempData["message"] = "Item deleted successfully";
            }
            catch (Exception ex)
            {
                Logger.Log("", Request.Path, "Error: " + ex.Message);

                TempData["errormessage"] = "Item was not deleted";

            }

            return RedirectToAction("Index");
        }


        [Authorize]
        public ActionResult Download(int id)
        {
            Audio audio = new AudioBL(User.IsInRole("Admin")).GetAudio(id);

            if (audio.audioURL != null

                )
            {
                string absolutePath = Server.MapPath(audio.audioURL);

                if (System.IO.File.Exists(absolutePath))
                {
                    byte[] data = System.IO.File.ReadAllBytes(absolutePath);

                    MemoryStream msIn = new MemoryStream(data);
                    msIn.Position = 0;

                  

                    return File(data, System.Net.Mime.MediaTypeNames.Application.Octet,
                        Path.GetFileName(audio.audioURL)
                        );
                }
                else
                    return null;
            }
            else return null;


        }


    }



}
}