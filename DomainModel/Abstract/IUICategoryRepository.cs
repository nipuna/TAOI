using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainModel.Entities;

namespace DomainModel.Abstract
{
    public interface IUICategoryRepository
    {

        IQueryable<UICategory> UICategories { get; }

        List<DomainModel.Entities.UICategory> UICategoriesForUser(Int32 userId);

    }


}
