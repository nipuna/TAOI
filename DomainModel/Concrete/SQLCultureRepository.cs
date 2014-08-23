using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainModel.Abstract;
using DomainModel.Entities;

namespace DomainModel.Concrete
{

    public class SQLCultureRepository : EntityContainer, ICultureRepository
    {

        public IQueryable<Culture> Cultures
        {
            get
            {
                

                return (from c in _entities.Cultures
                        orderby c.Locale ascending
                        select c).AsQueryable();


            }

        }


        public void ToggleSupported(int id)
        {
            var culture = (from c in _entities.Cultures
                         where c.ID == id
                         select c).First();
            culture.IsSupported = !culture.IsSupported;
            _entities.SaveChanges();
            
        }
    }

}
