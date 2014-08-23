using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Objects;
using System.Text;
using NUnit.Framework;
using WebUI.Controllers;
using DomainModel.Abstract;
using DomainModel.Concrete;
using DomainModel.Entities;

namespace Tests.AdminTests
{
    
    public class BrandsControllerTests
    {
        BrandsController controller;

        public BrandsControllerTests() 
        {
            controller = new BrandsController(new FakeBrandRepository()); 
        }

        [Test]
        public void List_Presents_Correct_Page_Of_Brands(int page )
        {
            //Arrange
            controller.PageSize = 2;
            //Act
            var result = controller.Index() as IList<Brand>;

            #region Assert

                #region If no data is returned and no view is rendered
                Assert.IsNotNull(result, "Didnt render view");
                #endregion

                #region If data returned and to check whether the data returned is correct
                Assert.AreEqual(FakeBrandRepository.fakeBrands.Skip((page - 1) * controller.PageSize).Take(controller.PageSize).ToList() , result);
                #endregion

            #endregion

        }

        [Test]
        public void To_Check_Brands_Edit(int ID, string brandName, string brandLogo)
        {

            #region Assert
                var result = controller.Edit(ID);
                IQueryable<Brand> brand = from b in FakeBrandRepository.fakeBrands
                            where b.ID == ID
                            select b;
                #region Verfiy the results
                Assert.AreEqual(brand.First().Name, brandName);
                Assert.AreEqual(brand.First().Logo, brandLogo);
                #endregion

            #endregion

        }

        [Test]
        public void To_Check_Brands_Delete(int ID)
        {

            #region Assert
            var result = controller.delete(ID);
            IQueryable<Brand> brand = from b in FakeBrandRepository.fakeBrands
                                       where b.ID == ID
                                       select b;

            #region Verfiy the results
            Assert.IsNotNull(brand , "Brand Not deleted");
            #endregion

            #endregion

        }

    }

}




