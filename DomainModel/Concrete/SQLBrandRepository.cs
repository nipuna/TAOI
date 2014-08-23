using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainModel.Abstract;
using DomainModel.Entities;

namespace DomainModel.Concrete
{

    public class SQLBrandRepository : EntityContainer, IBrandRepository
    {

        #region IBrandRepository Members

        #region get Brands
        /// <summary>
        /// Gets Brands and total count of cutomers based upon pageSize and pageNo 
        /// </summary>
        /// <param name="page">PageNo/Set of records desired</param>
        /// <param name="pageSize">No Of records desired</param>
        /// <param name="totNumBrands">total count of Brand records</param>
        /// <returns></returns>
        public IQueryable<Brand> getBrands(Int32 page, Int32 pageSize, out Int32 totNumBrands)
        {
            var brands = (from b in _entities.Brands
                          select b).AsQueryable();

            totNumBrands = brands.Count();

            return brands.OrderBy(b => b.Name).Skip((page - 1) * pageSize).Take(pageSize);

        }
        #endregion

        #region get All Brands
        /// <summary>
        /// Returns All the Brands
        /// </summary>
        /// <returns></returns>
        public IQueryable<Brand> getBrands()
        {

            var Brands = (from b in _entities.Brands
                             select b).AsQueryable();
            return Brands.OrderBy(b => b.Name);

        }
        #endregion

        #region get Brands for Customer
        /// <summary>
        /// Returns All the Brands
        /// </summary>
        /// <returns></returns>
        public IQueryable<Brand> getBrandsForCustomer(Int32 customerId)
        {

            var Brands = (from c in _entities.Customers
                          where c.ID == customerId
                          from b in c.Brands
                          select b).AsQueryable();
            return Brands.OrderBy(b => b.Name);

        }
        #endregion

        #region get Brands As IQueryable for displaying in the JQGrid
        /// <summary>
        /// get Brands As IQueryable for displaying in the JQGrid
        /// </summary>
        /// <param name="Brands"> IQueryable<Brand> All the brands </param>
        /// <returns>IQueryable<brandDisplay></returns>
        public IQueryable<brandDisplay> getBrandsForDisplay(IQueryable<Brand> Brands)
        {
            List<Brand> bnd = Brands.ToList();
            List<brandDisplay> rows = new List<brandDisplay>();

            foreach (var brand in bnd)
            {
                rows.Add(new brandDisplay
                {
                    ID = brand.ID,
                    Logo = "<img width=\"32px\" height=\"32px\" alt=\"\" src=\"" + "../.." + brand.Logo + "\" //>",
                    Name = brand.Name,
                    Action = "<div style=\"width:126px;margin-left:auto;margin-right:auto;\" ><a href=\"/Brands/Edit?Id=" + brand.ID.ToString() + "\" class=\"btnedit\" style=\"color:#FFF\" >Edit</a>" +
                       "<a href=\"/Brands/Delete?Id=" + brand.ID.ToString() + "\" class=\"btndelete\" onclick=\"return deleteConfirmation()\"  style=\"color:#FFF\" >Delete</a></div>"
                });
            }

            return rows.AsQueryable();
        }
        #endregion

        #region get All Brands Like(starting with a set of characters)
        /// <summary>
        /// Returns All the Brands Like
        /// </summary>
        /// <returns></returns>
        public string[] getBrandsLike(string like)
        {
            string[] Brands = (from b in _entities.Brands
                          where b.Name.StartsWith(like)
                          orderby b.Name ascending
                          select b.Name).ToArray();
            return Brands;
        }
        #endregion

        #region Edit Brand
        /// <summary>
        /// Edits the brand based upon the ID value passed
        /// </summary>
        /// <param name="brandId">Id of the brand to edit</param>
        public void editBrand(int brandId)
        {
            var brand = from b in _entities.Brands
                        where b.ID == brandId
                        select b;
        }

        #endregion

        #region Get Brand
        /// <summary>
        /// 
        /// </summary>
        /// <param name="brandId"></param>
        /// <returns></returns>
        public IQueryable<Brand> getBrand(int brandId)
        {
            var brand = from b in _entities.Brands
                        where b.ID == brandId
                        select b;

            return brand;
        }

        #endregion

        #region Get Brand for name
        /// <summary>
        /// Get Brand for name
        /// </summary>
        /// <param name="brandId"></param>
        /// <returns></returns>
        public IQueryable<Brand> getBrandForName(string name)
        {
            var brand = from b in _entities.Brands
                        where b.Name == name
                        select b;

            return brand;
        }

        #endregion

        #region Save Brand

        public void saveBrand(int brandId, string name, string imgPath)
        {

            var brand = (from b in _entities.Brands
                         where b.ID == brandId
                         select b).First();
            brand.Name = name;
            brand.Logo = imgPath;
            _entities.SaveChanges();

        }

        #endregion

        #region Delete Brand
        /// <summary>
        /// Deletes an existing brand
        /// </summary>
        /// <param name="brandId">Id of the brand to be deleted</param>
        public int deleteBrand(int brandId)
        {
            int status = -1;
            var brand = (from b in _entities.Brands
                         where b.ID == brandId
                         select b).First();
            brand.Vehicles.Load(); brand.Accessories.Load(); brand.Customers.Load(); brand.Devices.Load();
            if (brand.Vehicles.Count == 0 && brand.Accessories.Count == 0 && brand.Customers.Count == 0 && brand.Devices.Count == 0)
            {
                _entities.DeleteObject(brand);
                return status = _entities.SaveChanges();
            }
            else
            {
                return status;
            }
        }

        #endregion

        #region Create Brand
        /// <summary>
        /// Creates a new Brand
        /// </summary>
        /// <param name="name"></param>
        /// <param name="p"></param>
        public int createBrand(string name, string path)
        {
            int count;
            List<Brand> b = _entities.Brands.Where(brand => brand.Name == name).ToList();

            if (b.Count == 0)
            {
                var Brand = new Brand();
                //var lastBrand = _entities.BrandsSet.ToList().Last();
                //Brand.ID = lastBrand.ID + 1;
                Brand.Name = name;
                Brand.Logo = path;
                _entities.Brands.AddObject(Brand);
                count =  _entities.SaveChanges();
                return count;
            }
            else
            {
                return -1;
            }
        }

        #endregion

        public IQueryable<Brand> getAllBrandsHavingCustm()
        {
            var brandsA = (from b in _entities.Brands
                               from c in b.Customers
                               select b);
            return brandsA;
        }

        #endregion

    }

}
