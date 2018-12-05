using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace DataAccess
{
    public class ConnectionClass
    {
        public MusicSharingDBEntities2 Entity { get; set;}

        public ConnectionClass()
        {
            Entity = new MusicSharingDBEntities2();
        }
        
        public ConnectionClass(bool isAdmin)
        {
            if(isAdmin)
            {
                Entity = new MusicSharingDBEntities2();
            }
            else
            {
                Entity = new MusicSharingDBEntities2();
                
            }
        }

    }
}
