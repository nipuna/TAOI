using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainModel.Abstract;
using DomainModel.Entities;

namespace DomainModel.Concrete
{
    public class FakeBrandRepository : IBrandRepository
    {
        
        public static IQueryable<Brand> fakeBrands = new List<Brand> { 
                    new Brand{ ID = 1, Name = "HTC", Logo="a"}, 
                    new Brand{ ID = 2, Name = "APPLE", Logo="b"}, 
                    new Brand{ ID = 3, Name = "SAMSUNG", Logo="c"}, 
                    new Brand{ ID = 4, Name = "NOKIA", Logo="d"}, 
                    new Brand{ ID = 5, Name = "SYMBIAN", Logo="e"}, 
                    new Brand{ ID = 6, Name = "LG", Logo="f"}, 
                    new Brand{ ID = 7, Name = "SONY", Logo="g"}, 
                    new Brand{ ID = 8, Name = "AT&T", Logo="g"}, 
                    new Brand{ ID = 9, Name = "Ericsson", Logo="g"}, 
                    new Brand{ ID = 10, Name = "Windows", Logo="g"}, 
                    new Brand{ ID = 11, Name = "BlackBerry", Logo="g"}, 
                    }.AsQueryable();

        public IQueryable<Entities.Brand> getBrands(int page, int pageSize, out int totNumBrands)
        {
            var brands = (from b in fakeBrands
                          select b).AsQueryable();

            totNumBrands = brands.Count();

            return brands.OrderBy(b => b.Name).Skip((page - 1) * pageSize).Take(pageSize);
        }

        public IQueryable<Entities.Brand> getBrand(int brandId)
        {
            var brand = from b in fakeBrands
                        where b.ID == brandId
                        select b;
            return brand;
        }

        public void editBrand(int brandId)
        {
            var brand = from b in fakeBrands
                        where b.ID == brandId
                        select b;
        }

        public void saveBrand(int id, string name, string imgPath)
        {
            throw new NotImplementedException();
        }

        public int deleteBrand(int brandId)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Brand> getBrands()
        {
            throw new NotImplementedException();
        }

        public IQueryable<brandDisplay> getBrandsForDisplay(IQueryable<Brand> Brands)
        {
            throw new NotImplementedException();
        }

        public int createBrand(string name, string path)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Brand> getBrandsForCustomer(Int32 customerId)
        {
            throw new NotImplementedException();
        }

        IQueryable<Brand> IBrandRepository.getAllBrandsHavingCustm()
        {
            throw new NotImplementedException();
        }


    }
}
