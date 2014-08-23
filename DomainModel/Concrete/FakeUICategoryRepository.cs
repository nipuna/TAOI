using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainModel.Abstract;
using DomainModel.Entities;

namespace DomainModel.Concrete
{
    public class FakeUICategoryRepository : IUICategoryRepository
    {
        private IOTAEntities _entities;

        //Fake hard-coded list of UICategories 
        private static IQueryable<UICategory> fakeUICategories = new List<UICategory> { 

            new UICategory{ ID = 1, Name = "1", LevelInTree = 0, PositionInBranch = 1, Assignable = true  }, 
            new UICategory{ ID = 2, Name = "2", LevelInTree = 0, PositionInBranch = 2, Assignable = true  }, 
            new UICategory{ ID = 3, Name = "3", LevelInTree = 0, PositionInBranch = 3, Assignable = true  }, 
            new UICategory{ ID = 4, Name = "1.1", LevelInTree = 1, PositionInBranch = 1, Assignable = true  }, 
            new UICategory{ ID = 5, Name = "1.2", LevelInTree = 1, PositionInBranch = 2, Assignable = true  }, 
            new UICategory{ ID = 6, Name = "2.1", LevelInTree = 1, PositionInBranch = 1, Assignable = true  }, 
            new UICategory{ ID = 7, Name = "3.1", LevelInTree = 1, PositionInBranch = 1, Assignable = true  }, 

            }.AsQueryable();

        

        public IQueryable<UICategory> UICategories
        {
            get { return fakeUICategories; }
        }

        public List<DomainModel.Entities.UICategory> UICategoriesForUser(Int32 userId)
        {

            var user = (from u in _entities.Users
                        where u.ID == userId
                        select u).First();

            return (from c in _entities.UICategories.Include("SubCategories")
                    where c.LevelInTree == 1 && c.Users.Contains(user)
                    orderby c.LevelInTree ascending, c.PositionInBranch ascending
                    select c).ToList();

        }
        
    }

}
