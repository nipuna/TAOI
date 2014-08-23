using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainModel.Abstract;
using DomainModel.Entities;

namespace DomainModel.Concrete
{

    public class SQLCWIRangesRepository : EntityContainer , ICWIRangesRepository
    {

        #region ICWIRangesRepository Members

        #region getBrandsForALLDevices
        /// <summary>
        /// get Brands For which we have Bluetooth devices tested
        /// </summary>
        /// <param name="brandId">id of the brand</param>
        /// <returns>IQuearble list of devices</returns>
        public IQueryable<Brand> getBrandsForALLDevices()
        {
            var bnd = (from b in _entities.Brands
                       from dv in b.Devices
                       where dv.DeviceTypeID == 1
                       select b);
            return bnd.Distinct().OrderBy(d => d.Name);
        }
        #endregion

        #region getDevicesForbrand
        /// <summary>
        /// gets devices for Brand
        /// </summary>
        /// <param name="brandId">id of the brand</param>
        /// <returns>IQuearble list of devices</returns>
        public IQueryable<deviceInfo> getDevicesForbrand(int brandId)
        {
            var dvcs = (from b in _entities.Brands
                        where b.ID == brandId
                        from dv in b.Devices
                        where dv.DeviceTypeID == 1
                        select new deviceInfo
                        {
                            ID = dv.ID,
                            BrandID = dv.BrandID,
                            Model = dv.Model,
                            Software = dv.Software,
                            Hardware = dv.Hardware,
                            SerialNumber = dv.SerialNumber,
                            Country = dv.Country.Name,
                            NextGenID = dv.NextGenID
                        }
                        );
            return dvcs.Distinct().OrderBy(dv => dv.Model);
        }
        #endregion

        #region getBrandsForProducts
        /// <summary>
        /// get Brands For which Products exist 
        /// </summary>
        /// <param name="brandId">id of the brand</param>
        /// <returns>IQuearble list of devices</returns>
        //public IQueryable<Brand> getBrandsForProducts()
        //{
        //    var bnd = (from b in _entities.Brands
        //                from v in b.Vehicles
        //                select  b);
        //    return bnd.Distinct().OrderBy(b => b.Name);
        //}
        #endregion

        #region getBrandsForProducts
        /// <summary>
        /// get Brands For which Products exist 
        /// </summary>
        /// <param name="brandId">id of the brand</param>
        /// <returns>IQuearble list of devices</returns>
        public IQueryable<Brand> getBrandsForProducts(Int32 brandId)
        {
            if (brandId == 0)
            {
                var bnd = (from b in _entities.Brands
                           from v in b.Vehicles
                           select b);
                return bnd.Distinct().OrderBy(b => b.Name);
            }
            else
            {
                var bnd = (from br in _entities.Brands
                           where br.ID == brandId
                           from c in br.Customers
                           from b in c.Brands
                           from v in b.Vehicles
                           select b).Distinct();

                return bnd.Distinct().OrderBy(b => b.Name);
            }
        }
        #endregion

        #region getDetailsForDevice
        /// <summary>
        /// get Details For Device
        /// </summary>
        /// <param name="deviceId"></param>
        /// <returns></returns>
        public IQueryable<deviceInfo> getDetailsForDevice(int deviceId)
        {

            var detail = (from d in _entities.Devices
                          where d.ID == deviceId 
                          select new deviceInfo
                          {
                              ID = d.ID,
                              BrandID = d.BrandID,
                              Model = d.Model,
                              Software = d.Software,
                              Hardware = d.Hardware,
                              SerialNumber = d.SerialNumber,
                              Country = d.Country.Name,
                              NextGenID = d.NextGenID
                          }
                        );

            return detail;
        }
        #endregion

        #region getRangesForDeviceSelected
        /// <summary>
        /// get Ranges For Device Selected
        /// </summary>
        /// <param name="deviceId">id of the system</param>
        /// <param name="productId">id of the product against which system as been tested</param>
        /// <returns>IQuearble list of devices</returns>
        public IQueryable<CWIRange> getRangesForDeviceTested(Int32 deviceId, Int32 productId)
        {
            var cwiR = (from cwi in _entities.CWIRanges1
                       where cwi.DeviceID == deviceId && cwi.VehicleID == productId 
                       select cwi);

            return cwiR.Distinct();
        }
        #endregion

        #region get CWIRanges As IQueryable for displaying in the JQGrid
        /// <summary>
        /// get CWIRanges As IQueryable for displaying in the JQGrid
        /// </summary>
        /// <param name="CWIRanges"> IQueryable<CWIRange> All the CWIRanges </param>
        /// <returns>IQueryable<cwiRangeDisplay></returns>
        public IQueryable<cwiRangeDisplay> getCWIRangesForDisplay(Int32 vehicleId)
        {
            List<CWIRange> cwiRanges = (from cwi in _entities.CWIRanges1
                                        where cwi.VehicleID == vehicleId
                                        select cwi).ToList();

            List<cwiRangeDisplay> rows = new List<cwiRangeDisplay>();

            foreach (var cwiRange in cwiRanges)
            {
                var dev = (from d in _entities.Devices
                          where d.ID == cwiRange.DeviceID
                          select d).First();
                
                var cnt = (from c in _entities.Countries
                           where c.ID == dev.CountryID
                           select c).First();

                rows.Add(new cwiRangeDisplay
                {
                    Type = cwiRange.RangeType == 1 ? "Range" : (cwiRange.RangeType == 2 ? "Regular Expression" : "") ,
                    From = cwiRange.RangeStart,
                    To = cwiRange.RangeEnd,
                    IsLatest = "<input checked=\"" + (cwiRange.IsLatest == true ? "true" : "" ) + "\" type=\"checkbox\"></input>",
                    System = dev.Model + "(" + dev.Software + "/" + dev.Hardware + "/" + dev.SerialNumber + "/" + cnt.Name + ")"
                });
            }
            //'System(Software/Hardware/Serial/Country)'

            return rows.AsQueryable();
        }
        #endregion

        public Int32 editCWIRange(Int32 deviceId, Int32 productId, rangesDetail[] rangesChoosen)
        {
            Int32 count = -1;
            Int32 status;
            var cwiRanges = (from cwiR in _entities.CWIRanges1
                             where cwiR.DeviceID == deviceId && cwiR.VehicleID == productId
                             select cwiR).ToList();
            #region deleting the Old ranges
            if (cwiRanges.Count() > 0)
            {
                foreach (var oldRange in cwiRanges)
                {
                    _entities.DeleteObject(oldRange);
                    status = _entities.SaveChanges();
                }
            }
            #endregion
            #region creating the new ranges
            foreach (var range in rangesChoosen)
            {
                var newRange = new CWIRange();
                //var lastBrand = _entities.BrandsSet.ToList().Last();
                //Brand.ID = lastBrand.ID + 1;
                newRange.RangeStart = range.RangeStart;
                newRange.RangeEnd = range.RangeEnd;
                newRange.RegularExpression = range.RegularExpression;
                newRange.DeviceID = deviceId;
                newRange.VehicleID = productId;
                newRange.IsLatest = range.IsLatest == "true" ? true : false;
                _entities.CWIRanges1.AddObject(newRange);
            }
            count = _entities.SaveChanges();

            #endregion

            return count;
        }

        public void saveCWIRange(Entities.CWIRange CWIRange)
        {
            throw new NotImplementedException();
        }

        public void deleteCWIRange(int CWIRangeId)
        {
            throw new NotImplementedException();
        }

        public void createCWIRange(Entities.CWIRange newCWIRange)
        {
            throw new NotImplementedException();
        }

        #endregion
    }

}
