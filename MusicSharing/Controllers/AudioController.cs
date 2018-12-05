using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common;
using BusinessLogic;

namespace MusicSharing.Controllers
{
    public class AudioController : Controller
    {
        // GET: Audio
        public ActionResult Index()
        {
            AudioBL myAudio = new AudioBL(User.IsInRole("Admin"));
            var list = myAudio.GetAudios().ToList();

            return View(list);
        }

       

    }
}