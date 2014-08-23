using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainModel.Abstract;
using DomainModel.Entities;

namespace DomainModel.Concrete
{
   
    public class SQLCountryRepository : EntityContainer, ICountryRepository
    {

        #region ICountryRepository Members

        #region get Countries
        /// <summary>
        /// Gets Countries and total count of cutomers based upon pageSize and pageNo 
        /// </summary>
        /// <param name="page">PageNo/Set of records desired</param>
        /// <param name="pageSize">No Of records desired</param>
        /// <param name="totNumCountries">total count of country records</param>
        /// <returns></returns>
        public IQueryable<Country> getCountries(int page, int pageSize, out int totNumCountries)
        {
            
            var countries = (from b in _entities.Countries
                             select b).AsQueryable();

            totNumCountries = countries.Count();

            return countries.OrderBy(b => b.Name).Skip((page - 1) * pageSize).Take(pageSize);

        }

        #endregion

        #region get All Countries 
        /// <summary>
        /// Returns All the Countries
        /// </summary>
        /// <returns></returns>
        public IQueryable<Country> getCountries()
        {

            var countries = (from c in _entities.Countries
                             select c).AsQueryable();
            return countries.OrderBy(c => c.Name);

        }

        #endregion

        #region get Countries As IQueryable
        /// <summary>
        /// get Countries As IQueryable
        /// </summary>
        /// <param name="Countries"></param>
        /// <returns></returns>
        public IQueryable<countryDisplay> getCountriesForDisplay(IQueryable<Country> Countries)
        {
            List<Country> cnt = Countries.ToList();
            List<countryDisplay> rows = new List<countryDisplay>();

            foreach (var country in cnt)
            {
                rows.Add(new countryDisplay
                {
                    ID = country.ID,
                    Logo = "<img width=\"16px\" height=\"16px\" alt=\"\" src=\"" + "../.." + country.Logo + "\" //>",
                    Name = country.Name,
                    Action = "<div style=\"width:126px;margin-left:auto;margin-right:auto;\" ><a href=\"/countries/Edit?Id=" + country.ID.ToString() + "\" class=\"btnedit\" style=\"color:#FFF\" >Edit</a>" +
                       "<a href=\"/countries/Delete?Id=" + country.ID.ToString() + "\" class=\"btndelete\" onclick=\"return deleteConfirmation()\" style=\"color:#FFF\" >Delete</a></div>"
                });
            }

            return rows.AsQueryable();
        }
        #endregion

        #region Edit Country
        /// <summary>
        /// Edits the Country based upon the ID value passed
        /// </summary>
        /// <param name="CountryId">Id of the Country to edit</param>
        public void editCountry(int CountryId)
        {
            var Country = from b in _entities.Countries
                        where b.ID == CountryId
                        select b;

        }

        #endregion

        #region Get Country

        /// <summary>
        /// 
        /// </summary>
        /// <param name="CountryId"></param>
        /// <returns></returns>
        public IQueryable<Country> getCountry(int CountryId)
        {
            var Country = from b in _entities.Countries
                        where b.ID == CountryId
                        select b;

            return Country;
        }

        #endregion

        #region Save Country

        public void saveCountry(int CountryId, string name, string imgPath)
        {

            var Country = (from b in _entities.Countries
                         where b.ID == CountryId
                         select b).First();
            Country.Name = name;
            Country.Logo = imgPath;
            _entities.SaveChanges();

        }

        #endregion

        #region Delete Country

        public int deleteCountry(int CountryId)
        {
            int status = -1;
            var Country = (from b in _entities.Countries
                         where b.ID == CountryId
                         select b).First();

            Country.Accessories.Load(); Country.Addresses.Load(); Country.Connections.Load(); 
            Country.Devices.Load();
            if (Country.Accessories.Count == 0 && Country.Addresses.Count == 0 && Country.Connections.Count == 0 && Country.Devices.Count == 0)
            {
                _entities.DeleteObject(Country);
                return status = _entities.SaveChanges();
            }
            else
            {
                return status;
            }
        }

        #endregion

        #region Create Country
        /// <summary>
        /// Creates a new country
        /// </summary>
        /// <param name="name"></param>
        /// <param name="p"></param>
        public int createCountry(string name, string path)
        {
            int count;
            List<Brand> b = _entities.Brands.Where(brand => brand.Name == name).ToList();

            if (b.Count == 0)
            {
                var Country = new Country();
                //var lastCountry = _entities.CountriesSet.ToList().Last();
                //Country.ID = lastCountry.ID + 1;
                Country.Name = name;
                Country.Logo = path;
                _entities.Countries.AddObject(Country);
                count = _entities.SaveChanges();
                return count;
            }
            else
            {
                return -1;
            }
        }

        #endregion

        #endregion

    }

    

}
