using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainModel.Entities;

namespace DomainModel.Abstract
{
    public interface ICountryRepository
    {
        IQueryable<Country> getCountries();

        IQueryable<Country> getCountries(Int32 page, Int32 pageSize, out Int32 totNumCountries);

        IQueryable<countryDisplay> getCountriesForDisplay(IQueryable<Country> Countries);

        IQueryable<Country> getCountry(Int32 CountryId);

        void editCountry(Int32 CountryId);

        void saveCountry(Int32 id, string name, string imgPath);

        int deleteCountry(int CountryId);

        int createCountry(string name, string path);
    }
}

public class countryDisplay
{
    public int ID { get; set; }
    public string Logo { get; set; }
    public string Name { get; set; }
    public string Action { get; set; }

}