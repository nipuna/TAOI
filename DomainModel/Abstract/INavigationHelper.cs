using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DomainModel.Abstract
{
    public interface INavigationHelper
    {
        List<DomainModel.Entities.UICategory> SetSelectedCategory(int categoryID, List<DomainModel.Entities.UICategory> nav);
    }
}
