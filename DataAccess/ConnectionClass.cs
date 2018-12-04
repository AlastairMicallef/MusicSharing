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
        public MusicSharingDBEntities Entity { get; set;}

        public ConnectionClass()
        {
            Entity = new MusicSharingDBEntities();
        }
        
        public ConnectionClass(bool isAdmin)
        {
            if(isAdmin)
            {
                Entity = new MusicSharingDBEntities();
            }
            else
            {
                Entity = new MusicSharingDBEntities();
                
            }
        }

    }
}
