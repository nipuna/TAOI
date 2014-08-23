using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainModel.Abstract;
using DomainModel.Entities;
using System.Data.Objects;
using System.Linq.Expressions;

namespace DomainModel.Concrete
{

    public class SqlUICategoryRepository : IUICategoryRepository
    {
        private IOTAEntities _entities;

        public SqlUICategoryRepository()
        {
            _entities = new IOTAEntities();
        }


        public IQueryable<DomainModel.Entities.UICategory> UICategories
        {
            get
            {


                return (from c in _entities.UICategories.Include("SubCategories")
                        where c.LevelInTree == 1
                        orderby c.LevelInTree ascending, c.PositionInBranch ascending
                        select c).AsQueryable();


            }
        }


        public List<DomainModel.Entities.UICategory> UICategoriesForUser(Int32 userId)
        {
            
            var CatForUser = from u in _entities.Users
                             where u.ID == userId
                             select u.UICategories;

            return CatForUser.First().ToList();

        }
        

    }

}
