using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainModel.Abstract;
using DomainModel.Entities;

namespace DomainModel.Concrete
{
   
    public class SQLRegionRepository : EntityContainer, IRegionRepository
    {

        #region IRegionRepository Members

        #region get Regions
        /// <summary>
        /// Gets Regions and total count of cutomers based upon pageSize and pageNo 
        /// </summary>
        /// <param name="page">PageNo/Set of records desired</param>
        /// <param name="pageSize">No Of records desired</param>
        /// <param name="totNumRegions">total count of Region records</param>
        /// <returns></returns>
        public IQueryable<Region> getRegions(int page, int pageSize, out int totNumRegions)
        {
            
            var Regions = (from b in _entities.Regions
                             select b).AsQueryable();

            totNumRegions = Regions.Count();

            return Regions.OrderBy(b => b.Name).Skip((page - 1) * pageSize).Take(pageSize);

        }

        #endregion

        #region get All Regions 
        /// <summary>
        /// Returns All the Regions
        /// </summary>
        /// <returns></returns>
        public IQueryable<Region> getRegions()
        {

            var Regions = (from c in _entities.Regions
                             select c).AsQueryable();
            return Regions.OrderBy(c => c.Name);

        }

        #endregion

        #region get Regions As IQueryable
        /// <summary>
        /// get Regions As IQueryable
        /// </summary>
        /// <param name="Regions"></param>
        /// <returns></returns>
        public IQueryable<regionDisplay> getRegionsForDisplay(IQueryable<Region> Regions)
        {
            List<Region> cnt = Regions.ToList();
            List<regionDisplay> rows = new List<regionDisplay>();

            foreach (var region in cnt)
            {
                rows.Add(new regionDisplay
                {
                    ID = region.ID,
                    Name = region.Name,
                    Action = "<a href=\"/Regions/Edit?Id=" + region.ID.ToString() + "\" class=\"btnedit\" style=\"color:#FFF\" >Edit</a>" +
                       "<a href=\"/Regions/Delete?Id=" + region.ID.ToString() + "\" class=\"btndelete\" onclick=\"return deleteConfirmation()\" style=\"color:#FFF\" >Delete</a>"
                });
            }

            return rows.AsQueryable();
        }
        #endregion

        #region Edit region
        /// <summary>
        /// Edits the region based upon the ID value passed
        /// </summary>
        /// <param name="regionId">Id of the Region to edit</param>
        public void editRegion(int RegionId)
        {
            var Region = from b in _entities.Regions
                        where b.ID == RegionId
                        select b;

        }

        #endregion

        #region Get Region

        /// <summary>
        /// 
        /// </summary>
        /// <param name="RegionId"></param>
        /// <returns></returns>
        public IQueryable<Region> getRegion(int RegionId)
        {
            var Region = from b in _entities.Regions
                        where b.ID == RegionId
                        select b;

            return Region;
        }

        #endregion

        #region Save Region

        public void saveRegion(int RegionId, string name)
        {

            var Region = (from b in _entities.Regions
                         where b.ID == RegionId
                         select b).First();
            Region.Name = name;
            _entities.SaveChanges();

        }

        #endregion

        #region Delete Region

        public int deleteRegion(int RegionId)
        {
            int status = -1;
            var Region = (from b in _entities.Regions
                         where b.ID == RegionId
                         select b).First();

            Region.Brands.Load(); Region.Users.Load();
            if (Region.Brands.Count == 0 && Region.Users.Count == 0 )
            {
                _entities.DeleteObject(Region);
                return status = _entities.SaveChanges();
            }
            else
            {
                return status;
            }
        }

        #endregion

        #region Create Region
        /// <summary>
        /// Creates a new Region
        /// </summary>
        /// <param name="name"></param>
        /// <param name="p"></param>
        public int createRegion(string name, string path)
        {
            int count;
            List<Brand> b = _entities.Brands.Where(brand => brand.Name == name).ToList();

            if (b.Count == 0)
            {
                var Region = new Region();
                //var lastRegion = _entities.RegionsSet.ToList().Last();
                //Region.ID = lastRegion.ID + 1;
                Region.Name = name;
                _entities.Regions.AddObject(Region);
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
