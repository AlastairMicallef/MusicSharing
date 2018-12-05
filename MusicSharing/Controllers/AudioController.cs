using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common;
using BusinessLogic;

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



    }
}