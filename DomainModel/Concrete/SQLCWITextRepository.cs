using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainModel.Abstract;
using DomainModel.Entities;
using System.Web.Script.Serialization;
using System.Text.RegularExpressions;
using System.Linq.Expressions;

namespace DomainModel.Concrete
{
    using wLinqExtensions;

    public class SQLCWITextRepository : EntityContainer, ICWITextRepository
    {

        #region ICWITextRepository Members

        #region get CWITexts
        /// <summary>
        /// Gets CWITexts and total count of cutomers based upon pageSize and pageNo 
        /// </summary>
        /// <param name="page">PageNo/Set of records desired</param>
        /// <param name="pageSize">No Of records desired</param>
        /// <param name="totNumCWITexts">total count of CWIText records</param>
        /// <returns></returns>
        public IQueryable<CWITextDisplay> getCWITexts(Int32 page, Int32 pageSize, out Int32 totNumCWITexts)
        {
            var CWITexts = (from cwi in _entities.CWITexts
                            from b in cwi.Brands
                            from cwiCult in cwi.CWITextCultureDetails
                            select new CWITextDisplay
                            {
                                ID = cwi.ID,
                                CWIText = cwiCult.CwiText,
                                BrandId = b.ID,
                                BrandName = b.Name,
                                Action = ""
                            }
                            ).AsQueryable();

            totNumCWITexts = CWITexts.Count();

            return CWITexts.OrderBy(b => b.BrandName).Skip((page - 1) * pageSize).Take(pageSize);

        }
        #endregion

        #region get All CWITexts
        /// <summary>
        /// Returns All the CWITexts
        /// </summary>
        /// <returns></returns>
        public IQueryable<CWITextEdit> getCWITexts()
        {

            var CWITexts = (from cwi in _entities.CWITexts
                            from cwiCult in cwi.CWITextCultureDetails
                            where cwiCult.CultureID == 23
                            select new CWITextEdit
                            {
                                ID = cwi.ID,
                                CWITextCultureDtlID = cwiCult.ID,
                                CWIText = cwiCult.CwiText,
                            }
                            ).AsQueryable();
            
            return CWITexts.Distinct().OrderBy(c => c.CWIText);

        }
        #endregion

        #region get CWITexts As IQueryable for displaying in the JQGrid
        /// <summary>
        /// get CWITexts As IQueryable for displaying in the JQGrid
        /// </summary>
        /// <param name="CWITexts"> IQueryable<CWIText> All the CWITexts </param>
        /// <returns>IQueryable<CWITextDisplay></returns>
        public IQueryable<CWITextDisplay> getCWITextsForDisplay(IQueryable<CWITextEdit> CWITexts)
        {
            JavaScriptSerializer sr = new JavaScriptSerializer();
            //List<CWITextEdit> cwiS = CWITexts.ToList();
            List<CWITextDisplay> rows = new List<CWITextDisplay>();

            foreach (var cwi in CWITexts)
            {
                
                #region Get Brands as string
                string[] brands = (from cw in _entities.CWITexts
                                   where cw.ID == cwi.ID
                                   from b in cw.Brands
                                   select b.Name).OrderBy(a => a).ToArray();

                string sBrands = sr.Serialize(brands).Replace("[", "").Replace("]", "").Replace("\"","");
                #endregion

                rows.Add(new CWITextDisplay
                {
                    ID = cwi.ID,
                    CWIText = cwi.CWIText,
                    BrandName = sBrands,
                    Action = "<div style=\"width:126px;margin-left:auto;margin-right:auto;\" ><a href=\"/CWITexts/Edit?Id=" + cwi.CWITextCultureDtlID.ToString() + "\" class=\"btnedit\" style=\"color:#FFF\" >Edit</a>" +
                       "<a href=\"/CWITexts/Delete?Id=" + cwi.CWITextCultureDtlID.ToString() + "\" class=\"btndelete\" onclick=\"return deleteConfirmation()\"  style=\"color:#FFF\" >Delete</a></div>"
                });

            }
            return rows.AsQueryable();
        }
        #endregion

        #region get CWITexts As IQueryable for displaying in the JQGrid on filter
        /// <summary>
        /// get CWITexts As IQueryable for displaying in the JQGrid on filter
        /// </summary>
        /// <param name="CWITexts"> IQueryable<CWIText> All the CWITexts </param>
        /// <returns>IQueryable<CWITextDisplay></returns>
        public IQueryable<CWITextDisplay> getCWITextsForFilterDisplay(List<Int32> brandIds)
        {
            JavaScriptSerializer sr = new JavaScriptSerializer();
            //List<CWITextEdit> cwiS = CWITexts.ToList();
            List<CWITextDisplay> rows = new List<CWITextDisplay>();
            
            var CwiTexts = (from cwi in _entities.CWITexts
                             from bnd in cwi.Brands
                             join ids in brandIds
                             on bnd.ID equals ids
                             from cwiCult in cwi.CWITextCultureDetails
                             where cwiCult.CultureID == 23
                             select new CWITextEdit
                             {
                                 ID = cwi.ID,
                                 CWITextCultureDtlID = cwiCult.ID,
                                 CWIText = cwiCult.CwiText,
                             }
                           ).AsQueryable();

            CwiTexts = CwiTexts.Distinct().OrderBy(c => c.CWIText);

            foreach (var cwi in CwiTexts)
            {

                #region Get Brands as string
                string[] brands = (from cw in _entities.CWITexts
                                   where cw.ID == cwi.ID
                                   from b in cw.Brands
                                   select b.Name).OrderBy(a => a).ToArray();

                string sBrands = sr.Serialize(brands).Replace("[", "").Replace("]", "").Replace("\"", "");
                #endregion

                rows.Add(new CWITextDisplay
                {
                    ID = cwi.ID,
                    CWIText = cwi.CWIText,
                    BrandName = sBrands,
                    Action = "<div style=\"width:126px;margin-left:auto;margin-right:auto;\" ><a href=\"/CWITexts/Edit?Id=" + cwi.CWITextCultureDtlID.ToString() + "\" class=\"btnedit\" style=\"color:#FFF\" >Edit</a>" +
                       "<a href=\"/CWITexts/Delete?Id=" + cwi.CWITextCultureDtlID.ToString() + "\" class=\"btndelete\" onclick=\"return deleteConfirmation()\"  style=\"color:#FFF\" >Delete</a></div>"
                });

            }
            return rows.AsQueryable();
        }
        #endregion

        #region Get CWIText
        /// <summary>
        /// 
        /// </summary>
        /// <param name="CWITextId"></param>
        /// <returns></returns>
        public IQueryable<CWITextEdit> getCWIText(int CWITextCultureDtlID)
        {
            //Dictionary<string, List<details>> brands = getAllCwiTextEditDetails(CWITextId);

            var CWITexts = (from cwiCult in _entities.CWITextCultureDetails
                            where cwiCult.ID == CWITextCultureDtlID
                            select new CWITextEdit
                            {
                                ID = cwiCult.CwiTextID ,
                                CWIText = cwiCult.CwiText,
                                //Brands = brands,
                            }
                            ).AsQueryable();

            return CWITexts;
        }

        #endregion

        #region Save CWIText

        public void saveCWIText(Int32 cwiTextId, string cwiText, List<Int32> brandChoosen)
        {
            CWIText editedCwi = _entities.CWITexts.Where(cwiCnt => cwiCnt.ID == cwiTextId).First();

            #region CWITextCultureDetails

            var cwiDetail = (from dtl in _entities.CWITextCultureDetails
                             where dtl.CwiTextID == cwiTextId && dtl.CultureID == 23
                             select dtl).First();
            cwiDetail.CwiText = cwiText;
            _entities.SaveChanges();
            #endregion

            #region brand
            editedCwi.Brands.Load();
            editedCwi.Brands.Clear();
            if (brandChoosen != null)
            {
                List<Brand> BrandsAssct = editedCwi.Brands.ToList();
                foreach (var id in brandChoosen)
                {
                    Brand inCheck = _entities.Brands.Where(brand => brand.ID == id).First();
                    if (!BrandsAssct.Contains(inCheck))
                    {
                        editedCwi.Brands.Add(inCheck);
                        //editedCWIText.Brands.Attach(inCheck);
                    }
                }
            }
            #endregion

            //_entities.SaveChanges();
            
        }

        #endregion

        #region Delete CWIText
        /// <summary>
        /// Deletes an existing CWIText
        /// </summary>
        /// <param name="CWITextId">Id of the CWIText to be deleted</param>
        public int deleteCWIText(int CWITextId)
        {
            int status = -1;
            var CWITextDtl = (from cwiT in _entities.CWITextCultureDetails
                              where cwiT.ID == CWITextId
                              select cwiT).First();

            var CWIText = (from cwi in _entities.CWITexts
                           where cwi.ID == CWITextDtl.CwiTextID
                           select cwi).First();

            
            _entities.DeleteObject(CWITextDtl);
            return status = _entities.SaveChanges();
        }

        #endregion

        #region Create CWIText
        /// <summary>
        /// Creates a new CWIText
        /// </summary>
        /// <param name="name"></param>
        /// <param name="p"></param>
        public void createCWIText(CWIText newCwiText)
        {
            _entities.CWITexts.AddObject(newCwiText);
            _entities.SaveChanges();
        }

        #endregion

        #region Gets all the details required for rendering the Edit UI
        /// <summary>
        /// Gets all the details required for rendering the Edit UI
        /// </summary>
        public Dictionary<string, List<details>> getAllCwiTextEditDetails(Int32 cwiTextId)
        {
            Dictionary<string, List<details>> allDetails = new Dictionary<string, List<details>>();
            CWIText contextCwiText = _entities.CWITexts.Where(CWIT => CWIT.ID == cwiTextId).First();

            #region get all Brands
            //get all the existing Brands for the CWIText
            contextCwiText.Brands.Load();
            //CWIText.Brands.Load();
            Brand[] CWITextBrands = contextCwiText.Brands.OrderBy(brand => brand.Name).ToArray();
            //get all the brands
            //Brand[] brandsAO = _entities.Brands.OrderBy(brand => brand.Name).ToArray();
            Brand[] brandsA = (from b in _entities.Brands
                               from c in b.Customers
                               select b).ToArray();
            //create custom list of brands
            List<details> brandsDetails = new List<details>();
            //Add 'All' to the collection
            //brandsDetails.Add(new details(0, "All", false));
            foreach (var brand in brandsA)
            {
                details brandDetail;
                if (CWITextBrands.Contains(brand))
                {
                    brandDetail = new details(brand.ID, brand.Name, true);
                }
                else
                {
                    brandDetail = new details(brand.ID, brand.Name, false);
                }
                brandsDetails.Add(brandDetail);
            }

            allDetails.Add("brands", brandsDetails);
            #endregion

            return allDetails;
        }
        #endregion

        #region Gets all the details required for rendering the Create UI
        /// <summary>
        /// Gets all the details required for rendering the Create UI
        /// </summary>
        public Dictionary<string, List<details>> getAllCwiTextCreateDetails()
        {
            Dictionary<string, List<details>> allDetails = new Dictionary<string, List<details>>();

            #region get all Brands
            //Brand[] BrandsA = _entities.Brands.OrderBy(brand => brand.Name).ToArray();
            Brand[] BrandsA = (from b in _entities.Brands
                               from c in b.Customers
                               select b).ToArray();
            List<details> BrandsDetails = new List<details>();

            BrandsDetails.Add(new details(0, "All", false));

            foreach (var brand in BrandsA)
            {
                details brandDetail;
                brandDetail = new details(brand.ID, brand.Name, false);
                BrandsDetails.Add(brandDetail);
            }
            allDetails.Add("brands", BrandsDetails);
            #endregion

            return allDetails;
        }
        #endregion

        #region Fills/Edits the CWIText and the related entites with the data choosen by the CWIText
        /// <summary>
        /// Fills/Edits the CWIText and the related entites with the data choosen by the CWIText
        /// </summary>
        /// <param name="CWIText">CWIText entity to be modified</param>
        /// <param name="brandChoosen">List containing Id's of the Brands choosen</param>
        /// <param name="cultureChoosen">List containing Id's of the cultures choosen</param>
        /// <param name="regionChoosen">List containing Id's of the coutries choosen</param>
        /// <returns></returns>
        public CWIText editedCWITextData(Int32 cwiTextId, string cwiText, List<Int32> brandChoosen)
        {
            CWIText editedCwi = _entities.CWITexts.Where(cwiCnt => cwiCnt.ID == cwiTextId).First();
            
            #region CWITextCultureDetails

            var cwiDetail = (from dtl in _entities.CWITextCultureDetails
                             where dtl.CwiTextID == cwiTextId && dtl.CultureID == 23
                             select dtl).First();
            cwiDetail.CwiText = cwiText;
            _entities.SaveChanges();
            #endregion

            #region brand
            editedCwi.Brands.Load();
            editedCwi.Brands.Clear();
            if (brandChoosen != null)
            {
                List<Brand> BrandsAssct = editedCwi.Brands.ToList();
                foreach (var id in brandChoosen)
                {
                    Brand inCheck = _entities.Brands.Where(brand => brand.ID == id).First();
                    if (!BrandsAssct.Contains(inCheck))
                    {
                        editedCwi.Brands.Add(inCheck);
                        //editedCWIText.Brands.Attach(inCheck);
                    }
                }
            }
            #endregion

            _entities.SaveChanges();
            return editedCwi;
        }
        #endregion

        #region Fills/Creates the CWIText and the related entites with the data choosen by the CWIText
         //<summary>
         //Fills/Creates the CWIText and the related entites with the data choosen by the CWIText
         //</summary>
         //<param name="CWIText">CWIText entity to be modified</param>
         //<param name="brandChoosen">List containing Id's of the Brands choosen</param>
         //<param name="cultureChoosen">List containing Id's of the cultures choosen</param>
         //<param name="regionChoosen">List containing Id's of the coutries choosen</param>
         //<returns></returns>
        public void createdCWITextData(string cwiText, List<Int32> brandChoosen)
        {

            CWIText createdCwi = _entities.CWITexts.CreateObject();

            _entities.CWITexts.AddObject(createdCwi);
            _entities.SaveChanges();

            #region CWITextCultureDetails
            CWITextCultureDetail cwiDetail = new CWITextCultureDetail();
            //var cwiDetail = (from dtl in _entities.CWITextCultureDetails
            //                 where dtl.CwiTextID == cwiTextId && dtl.CultureID == 23
            //                 select dtl).First();
            cwiDetail.CwiText = cwiText;
            cwiDetail.CultureID = 23;
            cwiDetail.CwiTextID = createdCwi.ID;
            createdCwi.CWITextCultureDetails.Add(cwiDetail);
            _entities.SaveChanges();
            #endregion

            #region brand
            //createdUser.Brands.Load();
            //createdUser.Brands.Clear();
            if (brandChoosen != null)
            {
                foreach (var id in brandChoosen)
                {
                    Brand inCheck = _entities.Brands.Where(brand => brand.ID == id).First();
                    createdCwi.Brands.Add(inCheck);
                    //editedUser.Brands.Attach(inCheck);
                }
            }
            #endregion
            //_entities.SaveChanges();

        }
        #endregion

        #region get Brands Status For DropDown
        /// <summary>
        /// getBrandsStatusForDropDown
        /// </summary>
        /// <param name="brands">list of brands</param>
        /// <returns></returns>
        public List<CwiBrandsForDp> getBrandsStatusForDropDown()
        {
            Brand[] brandsA = (from b in _entities.Brands
                           from c in b.Customers
                           select b).ToArray();

            List<CwiBrandsForDp> brandsWithStatus = new List<CwiBrandsForDp>();

            foreach (var brand in brandsA)
            {
                Brand[] br = (from b in _entities.Brands
                              from cw in b.CWITexts
                              select b).ToArray();
                if (br.Contains(brand))
                {
                    brandsWithStatus.Add(new CwiBrandsForDp( brand.ID, brand.Name, true ));
                }
                else
                {
                    brandsWithStatus.Add(new CwiBrandsForDp(brand.ID, brand.Name, false));
                }
            }
            return brandsWithStatus;
        }
        #endregion

        #endregion

    }

}

namespace wLinqExtensions
{
    public static class generalLinqExtensions
    {
        public static IQueryable<T> WhereIn<T, TValue>(this IQueryable<T> source, Expression<Func<T, TValue>> propertySelector, params TValue[] values)
        {
            return source.Where(GetWhereInExpression(propertySelector, values));
        }

        public static IQueryable<T> WhereIn<T, TValue>(this IQueryable<T> source, Expression<Func<T, TValue>> propertySelector, IEnumerable<TValue> values)
        {
            return source.Where(GetWhereInExpression(propertySelector, values));
        }

        private static Expression<Func<T, bool>> GetWhereInExpression<T, TValue>(Expression<Func<T, TValue>> propertySelector, IEnumerable<TValue> values)
        {
            ParameterExpression p = propertySelector.Parameters.Single();
            if (!values.Any())
                return e => false;

            var equals = values.Select(value => (Expression)Expression.Equal(propertySelector.Body, Expression.Constant(value, typeof(TValue))));
            var body = equals.Aggregate<Expression>((accumulate, equal) => Expression.Or(accumulate, equal));

            return Expression.Lambda<Func<T, bool>>(body, p);
        }

    }
}