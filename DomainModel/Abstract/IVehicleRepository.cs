using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainModel.Entities;

namespace DomainModel.Abstract
{
    public interface IVehicleRepository
    {
        IQueryable<vehicleDisplay> getVehicles();

        IQueryable<vehicleDisplay> getVehiclesForDisplay(IQueryable<vehicleDisplay> Vehicles);

        IQueryable<vehicleDisplay> getVehicle(Int32 VehicleId);

        IQueryable<vehicleDisplay> getVehicleForBrand(Int32 BrandId);

        void editVehicle(Int32 VehicleId);

        void saveVehicle(Int32 id, string model);

        int deleteVehicle(int VehicleId);

        int createVehicle(vehicleDisplay vehicle);

        Dictionary<String, int> getCustAndBrandIdForVeh(int VehicleId);
        
    }
}

public class vehicleDisplay
{
    public int ID { get; set; }
    public string Customer { get; set; }
    public string Brand { get; set; }
    public string Model { get; set; }
    public string Action { get; set; }

}