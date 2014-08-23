using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainModel.Entities;

namespace DomainModel.Abstract
{
    public interface IRegionRepository
    {
        IQueryable<Region> getRegions();

        IQueryable<Region> getRegions(Int32 page, Int32 pageSize, out Int32 totNumRegions);

        IQueryable<regionDisplay> getRegionsForDisplay(IQueryable<Region> Regions);

        IQueryable<Region> getRegion(Int32 RegionId);

        void editRegion(Int32 RegionId);

        void saveRegion(Int32 id, string name);

        int deleteRegion(int RegionId);

        int createRegion(string name, string path);
    }
}

public class regionDisplay
{
    public int ID { get; set; }
    public string Name { get; set; }
    public string Action { get; set; }

}