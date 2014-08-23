using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainModel.Abstract;

namespace DomainModel.Concrete
{
    public class NavigationHelper : EntityContainer, INavigationHelper
    {
        public List<Entities.UICategory> SetSelectedCategory(int categoryID, List<Entities.UICategory> nav)
        {

            foreach (DomainModel.Entities.UICategory cat in nav)
            {
                if (RecurseCategory(categoryID, cat) == true || (cat.ID == categoryID))
                {
                    cat.IsSelected = true;
                }
                else cat.IsSelected = false;
            }

            return nav;

        }

        private bool RecurseCategory(int categoryID, Entities.UICategory cat)
        {

            bool selected = false;

            foreach (DomainModel.Entities.UICategory subcat in cat.SubCategories)
            {
                if (subcat != null)
                {
                    if (RecurseCategory(categoryID, subcat) == true || (subcat.ID == categoryID))
                    {
                        selected = true;
                        subcat.IsSelected = true;
                    }
                    else subcat.IsSelected = false;
                }
            }

            return selected;

        }

        public bool getCategoryStatusForUser(Int32 userId, string controller)
        {
            
            var cat = (from u in _entities.Users
                       from c in u.UICategories
                       where u.ID == userId && c.Controller == controller
                      select c.Controller ).ToList();

            if (cat.Count == 0)
                return false;   
            //var c = _entities.UICategories.Where(cat => cat.Users.Contains(
            return true;
        }

    }

}
