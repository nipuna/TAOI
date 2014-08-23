using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainModel.Entities;

namespace DomainModel.Abstract
{
    public interface ICWIRangesRepository
    {

        IQueryable<Brand> getBrandsForALLDevices();

        IQueryable<deviceInfo> getDevicesForbrand(Int32 brandId);

        IQueryable<deviceInfo> getDetailsForDevice(Int32 deviceId);

        IQueryable<Brand> getBrandsForProducts(Int32 brandId);

        IQueryable<CWIRange> getRangesForDeviceTested(Int32 deviceId, Int32 productId);

        IQueryable<cwiRangeDisplay> getCWIRangesForDisplay(Int32 vehicleId);

        Int32 editCWIRange(Int32 deviceId, Int32 productId, rangesDetail[] rangesChoosen);

        void saveCWIRange(CWIRange CWIRange);

        void deleteCWIRange(int CWIRangeId);

        void createCWIRange(CWIRange newCWIRange);

    }
}


public class deviceInfo
{

    //public deviceInfo(Int32 id, Int32 brandID, string software, string hardware, string serialNumber, string country, Int32 nextGenID)
    //{
    //    this.ID = id;
    //    this.BrandID = brandID;
    //    this.Software = software;
    //    this.Hardware = hardware;
    //    this.SerialNumber = serialNumber;
    //    this.Country = country;
    //    this.NextGenID = nextGenID;
    //}
    public int ID { get; set; }
    public int BrandID { get; set; }
    public string Model { get; set; }
    public string Software { get; set; }
    public string Hardware { get; set; }
    public string SerialNumber { get; set; }
    public string Country { get; set; }
    public string NextGenID { get; set; }
}

public class cwiRangeDisplay
{
    public string Type { get; set; }
    public string From { get; set; }
    public string To { get; set; }
    public string IsLatest { get; set; }
    public string System { get; set; }
}

public class rangesDetail
{
    public string RangeStart { get; set; }
    public string RangeEnd { get; set; }
    public string RegularExpression { get; set; }
    public string IsLatest { get; set; }
}