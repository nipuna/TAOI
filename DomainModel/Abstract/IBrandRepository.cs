using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainModel.Entities;

namespace DomainModel.Abstract
{
    public interface IBrandRepository
    {
        IQueryable<Brand> getBrands();

        IQueryable<Brand> getBrands(Int32 page, Int32 pageSize, out Int32 totNumBrands);

        IQueryable<brandDisplay> getBrandsForDisplay(IQueryable<Brand> Brands);

        IQueryable<Brand> getBrand(Int32 brandId);

        void editBrand(Int32 brandId);

        void saveBrand(Int32 id, string name, string imgPath);

        int deleteBrand(int brandId);

        int createBrand(string name, string path);

        IQueryable<Brand> getBrandsForCustomer(Int32 customerId);

        IQueryable<Brand> getAllBrandsHavingCustm();

    }
}


public class brandDisplay
{
    public int ID { get; set; }
    public string Logo { get; set; }
    public string Name { get; set; }
    public string Action { get; set; }

}