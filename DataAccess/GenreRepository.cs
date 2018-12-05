using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace DataAccess
{
    public class GenreRepository : ConnectionClass
    {
      
            public GenreRepository() : base()
            { }

            public IQueryable<Genre> GetCategories()
            {
                return Entity.Genres;
            }
        }
    }

