using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainModel.Entities;

namespace DomainModel.Abstract
{

    public interface ICultureRepository
    {

        IQueryable<Culture> Cultures { get; }

        void ToggleSupported(int id);

    }

}
