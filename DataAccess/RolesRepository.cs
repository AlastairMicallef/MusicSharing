using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace DataAccess
{
    class RolesRepository : ConnectionClass
    {
        public RolesRepository() : base()
        { }


        public UserRole GetRole(int roleId)
        {
            return Entity.UserRoles.SingleOrDefault(x => x.roleID == roleId);

        }

        public void AllocateRoleToUser(User u, UserRole r)
        {
            u.UserRoles.Add(r);
            Entity.SaveChanges();

        }
    }
}
