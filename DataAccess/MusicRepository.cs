using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace DataAccess
{
    public class MusicRepository: ConnectionClass
    {
        public MusicRepository():base()
        { }

        public MusicRepository(bool isAdmin):base(isAdmin)
        { }

        public Audio GetAudios(int id)
        {
            return Entity.Audios.SingleOrDefault(x => x.audioID == id);
        }

        public IQueryable<Audio> GetAudios()
        {
            return Entity.Audios;
        }

        public void AddAudio(Audio i)
        {
            Entity.Audios.Add(i);
            Entity.SaveChanges();
        }

        public void DeleteAudio(Audio a)
        {
            Entity.Audios.Remove(a);
            Entity.SaveChanges();
        }
    }
}
