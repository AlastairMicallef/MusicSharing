using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using DataAccess;

namespace BusinessLogic
{
    
    public class AudioBL
    {
        MusicRepository mr = new MusicRepository(false);
        public AudioBL(bool isAdmin)
        {
            mr = new MusicRepository(isAdmin);
        }

        public IQueryable<Audio> GetAudios()
        {
            return mr.GetAudios();
        }

        public Audio GetAudio(int id)
        {
            return mr.GetAudios(id);
        }

        public void AddAudio(string name, string URL, int user, string genre)
        {
            Audio a = new Audio();
            a.audioname = name;
            a.audioURL = URL;
            a.userID = user;

            if(string.IsNullOrEmpty(URL) == false)
            {
                a.audioURL = URL;
            }

            mr.AddAudio(a);
        }

        public void DeleteAudio(int id)
        {
            mr = new MusicRepository();

            var myAudio = mr.GetAudios(id);
            if (myAudio != null)
            {
                mr.DeleteAudio(myAudio);
            }
        }

    }
}
