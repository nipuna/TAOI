using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainModel.Abstract;
using DomainModel.Entities;

namespace DomainModel.Concrete
{
    public class SQLVehicleRepository : EntityContainer, IVehicleRepository
    {

        #region IVehicleRepository Members

        #region get All Vehicles
        /// <summary>
        /// Returns All the Vehicles
        /// </summary>
        /// <returns></returns>
        public IQueryable<vehicleDisplay> getVehicles()
        {

            var Vehicles = (from v in _entities.Vehicles
                            from c in v.Brand.Customers
                            select new vehicleDisplay
                            {
                                ID = v.ID,
                                Customer = c.Name,
                                Brand = v.Brand.Name,
                                Model = v.Model,
                                Action = ""
                            }).AsQueryable();

           
            return Vehicles.OrderBy(v => v.Customer);

        }
        #endregion

        #region get Vehicles As IQueryable for displaying in the JQGrid
        /// <summary>
        /// get Vehicles As IQueryable for displaying in the JQGrid
        /// </summary>
        /// <param name="Vehicles"> IQueryable<Vehicle> All the Vehicles </param>
        /// <returns>IQueryable<vehicleDisplay></returns>
        public IQueryable<vehicleDisplay> getVehiclesForDisplay(IQueryable<vehicleDisplay> Vehicles)
        {
            List<vehicleDisplay> vhl = Vehicles.ToList();
            List<vehicleDisplay> rows = new List<vehicleDisplay>();

            foreach (var Vehicle in vhl)
            {
                rows.Add(new vehicleDisplay
                {
                    ID = Vehicle.ID,
                    Customer = Vehicle.Customer,
                    Brand = Vehicle.Brand,
                    Model = Vehicle.Model,
                    Action = "<div style=\"width:126px;margin-left:auto;margin-right:auto;\" ><a href=\"/Vehicles/Edit?Id=" + Vehicle.ID.ToString() + "\" class=\"btnedit\" style=\"color:#FFF\" >Edit</a>" +
                       "<a href=\"/Vehicles/Delete?Id=" + Vehicle.ID.ToString() + "\" class=\"btndelete\" onclick=\"return deleteConfirmation()\"  style=\"color:#FFF\" >Delete</a></div>"
                });
            }

            return rows.AsQueryable();
        }
        #endregion

        #region Edit Vehicle
        /// <summary>
        /// Edits the Vehicle based upon the ID value passed
        /// </summary>
        /// <param name="VehicleId">Id of the Vehicle to edit</param>
        public void editVehicle(int VehicleId)
        {
            var Vehicle = from b in _entities.Vehicles
                          where b.ID == VehicleId
                          select b;

        }

        #endregion

        #region Get Vehicle
        /// <summary>
        /// 
        /// </summary>
        /// <param name="VehicleId"></param>
        /// <returns></returns>
        public IQueryable<vehicleDisplay> getVehicle(int VehicleId)
        {
            
            var Vehicle = (from v in _entities.Vehicles
                           where v.ID == VehicleId
                           from c in v.Brand.Customers
                           select new vehicleDisplay
                           {
                               ID = v.ID,
                               Customer = c.Name,
                               Brand = v.Brand.Name,
                               Model = v.Model,
                               Action = ""
                           });

            return Vehicle;
        }

        #endregion

        #region Get Vehicle
        /// <summary>
        /// get Vehicle For Brand
        /// </summary>
        /// <param name="BrandId"></param>
        /// <returns></returns>
        public IQueryable<vehicleDisplay> getVehicleForBrand(Int32 BrandId)
        {
            var Vehicle = (from b in _entities.Brands
                           where b.ID == BrandId
                           from v in b.Vehicles
                           select new vehicleDisplay
                           {
                               ID = v.ID,
                               Customer = "",
                               Brand = v.Brand.Name,
                               Model = v.Model,
                               Action = ""
                           }
                           );

            return Vehicle.OrderBy(v => v.Model);

        }

        #endregion

        #region Get Customer and brand id for Vehicle
        /// <summary>
        /// 
        /// </summary>
        /// <param name="VehicleId"></param>
        /// <returns></returns>
        public Dictionary<String,int> getCustAndBrandIdForVeh(int VehicleId)
        {
            //var Vehicle = from b in _entities.Vehicles
            //              where b.ID == VehicleId
            //              select b;

            var Vehicle = (from v in _entities.Vehicles
                           where v.ID == VehicleId
                           from c in v.Brand.Customers
                           select new 
                           {
                               CustomerId = c.ID ,
                               BrandID = v.Brand.ID,
                           });
            
            Dictionary<String, int> values = new Dictionary<string, int>();

            if (Vehicle.Count() > 0)
            {
                values["customerId"] = Vehicle.First().CustomerId;
                values["brandId"] = Vehicle.First().BrandID;    
            }
            else
            {
                values["customerId"] = 0;
                values["brandId"] = 0;    
            }
            return values;
        }

        #endregion
        
        #region Get Vehicle for name
        /// <summary>
        /// Get Vehicle for name
        /// </summary>
        /// <param name="VehicleId"></param>
        /// <returns></returns>
        public IQueryable<Vehicle> getVehicleForName(string name)
        {
            var Vehicle = from b in _entities.Vehicles
                          where b.Model == name
                          select b;

            return Vehicle;
        }

        #endregion

        #region Save Vehicle
        /// <summary>
        /// saves the changes to vehicle whose Id is passed
        /// </summary>
        /// <param name="VehicleId">Id of the vehicle</param>
        /// <param name="model"></param>
        public void saveVehicle(int VehicleId, string model)
        {

            var Vehicle = (from b in _entities.Vehicles
                           where b.ID == VehicleId
                           select b).First();
            Vehicle.Model = model;
            _entities.SaveChanges();

        }

        #endregion

        #region Delete Vehicle
        /// <summary>
        /// Deletes an existing Vehicle
        /// </summary>
        /// <param name="VehicleId">Id of the Vehicle to be deleted</param>
        public int deleteVehicle(int VehicleId)
        {
            int status = -1;
            var Vehicle = (from b in _entities.Vehicles
                           where b.ID == VehicleId
                           select b).First();
            Vehicle.CWIRanges.Load();
            if (Vehicle.CWIRanges.Count == 0)
            {
                _entities.DeleteObject(Vehicle);
                return status = _entities.SaveChanges();
            }
            else
            {
                return status;
            }
        }

        #endregion

        #region Create Vehicle
        /// <summary>
        /// Creates a new Vehicle
        /// </summary>
        /// <param name="name"></param>
        /// <param name="p"></param>
        public int createVehicle(vehicleDisplay vehicle)
        {
            int count;
            Int32 brandId = Convert.ToInt32(vehicle.Brand);
            List<Vehicle> veh = _entities.Vehicles.Where(Vehicle => Vehicle.Model == vehicle.Model && Vehicle.BrandID == brandId).ToList();

            if (veh.Count == 0)
            {
                var Vehicle = new Vehicle();
                var lastVehicle = _entities.Vehicles.ToList().Last();
                Vehicle.ID = lastVehicle.ID + 1;
                Vehicle.BrandID = brandId;
                Vehicle.Model = vehicle.Model;
                _entities.Vehicles.AddObject(Vehicle);
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
