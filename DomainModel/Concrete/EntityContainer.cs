using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainModel.Abstract;
using DomainModel.Entities;

namespace DomainModel.Concrete
{
    public abstract class EntityContainer
    {
        protected IOTAEntities _entities;

        public EntityContainer()
        {
            _entities = new IOTAEntities();
        }

    }

}
