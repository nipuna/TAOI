using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainModel.Entities;

namespace DomainModel.Abstract
{
    public interface ICWITextRepository
    {
        IQueryable<CWITextEdit> getCWITexts();

        IQueryable<CWITextDisplay> getCWITexts(Int32 page, Int32 pageSize, out Int32 totNumCWITexts);

        IQueryable<CWITextDisplay> getCWITextsForDisplay(IQueryable<CWITextEdit> CWITexts);

        IQueryable<CWITextEdit> getCWIText(Int32 CWITextId);

        void saveCWIText(Int32 cwiTextId, string cwiText, List<Int32> brandChoosen);

        int deleteCWIText(int CWITextId);

        void createCWIText(CWIText newCwiText);

        Dictionary<string, List<details>> getAllCwiTextEditDetails(Int32 cwiTextId);

        Dictionary<string, List<details>> getAllCwiTextCreateDetails();

        CWIText editedCWITextData(Int32 cwiTextId, string cwiText, List<Int32> brandChoosen);

        void createdCWITextData(string cwiText, List<Int32> brandChoosen);

        List<CwiBrandsForDp> getBrandsStatusForDropDown();

        IQueryable<CWITextDisplay> getCWITextsForFilterDisplay(List<Int32> brandIds);

    }
}

public class CWITextDisplay
{
    public int ID { get; set; }
    public string CWIText { get; set; }
    public int BrandId { get; set; }
    public string BrandName { get; set; }
    public string Action { get; set; }
}

public class CWITextEdit
{
    public int ID { get; set; }
    public string CWIText { get; set; }
    public int CWITextCultureDtlID { get; set; }
}

public class CwiBrandsForDp
{
    public CwiBrandsForDp(Int32 Id, string brandname, bool status)
    {
        ID = Id;
        Brandname = brandname;
        Status = status;
    }
    public int ID { get; set; }
    public string Brandname { get; set; }
    public bool Status { get; set; }
}


public class CwiBrands
{
    public int ID { get; set; }
    public string Brandname { get; set; }
}

