using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainModel.Abstract;
using DomainModel.Entities;

namespace DomainModel.Concrete
{
    public class SQLCustomerRepository: EntityContainer , ICustomerRepository
    {
        #region ICustomerRepository Members

        #region get Customers
        /// <summary>
        /// Gets Customers and total count of cutomers based upon pageSize and pageNo 
        /// </summary>
        /// <param name="page">PageNo/Set of records desired</param>
        /// <param name="pageSize">No Of records desired</param>
        /// <param name="totNumCustomers">total count of country records</param>
        /// <returns></returns>
        public IQueryable<Customer> getCustomers(int page, int pageSize, out int totNumCustomers)
        {
            var customers = (from b in _entities.Customers
                             select b).AsQueryable();

            totNumCustomers = customers.Count();

            return customers.OrderBy(b => b.Name).Skip((page - 1) * pageSize).Take(pageSize);
        }
        #endregion

        #region get All Customers
        /// <summary>
        /// Returns All the Customers
        /// </summary>
        /// <returns></returns>
        public IQueryable<Customer> getCustomers()
        {

            var Customers = (from b in _entities.Customers
                          select b).AsQueryable();
            return Customers.OrderBy(b => b.Name);

        }
        #endregion

        #region get Customers As IQueryable for displaying in the JQGrid
        /// <summary>
        /// get Customers As IQueryable for displaying in the JQGrid
        /// </summary>
        /// <param name="Customers"> IQueryable<Customer> All the Customers </param>
        /// <returns>IQueryable<CustomerDisplay></returns>
        public IQueryable<customerDisplay> getCustomersForDisplay(IQueryable<Customer> Customers)
        {
            List<Customer> bnd = Customers.ToList();
            List<customerDisplay> rows = new List<customerDisplay>();

            foreach (var customer in bnd)
            {
                rows.Add(new customerDisplay
                {
                    ID = customer.ID,
                    //Logo = "<img width=\"16px\" height=\"16px\" alt=\"\" src=\"" + "../.." + customer.Logo + "\" //>",
                    Logo = "<img width=\"32px\" height=\"32px\" alt=\"\" src=\"\" //>",
                    Name = customer.Name,
                    Action = "<div style=\"width:126px;margin-left:auto;margin-right:auto;\" ><a href=\"/Customers/Edit?Id=" + customer.ID.ToString() + "\" class=\"btnedit\" style=\"color:#FFF\" >Edit</a>" +
                       "<a href=\"/Customers/Delete?Id=" + customer.ID.ToString() + "\" class=\"btndelete\" onclick=\"return deleteConfirmation()\"  style=\"color:#FFF\" >Delete</a></div>"
                });
            }

            return rows.AsQueryable();
        }
        #endregion

        #region Edit Customer
        /// <summary>
        /// Edits the customer based upon the ID value passed
        /// </summary>
        /// <param name="customerId">Id of the customer to edit</param>
        public void editCustomer(int customerId)
        {
            var customer = from b in _entities.Customers
                        where b.ID == customerId
                        select b;
        }

        #endregion

        #region Get Customer
        /// <summary>
        /// 
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public IQueryable<Customer> getCustomer(int customerId)
        {
            var customer = from b in _entities.Customers
                        where b.ID == customerId
                        select b;
            customer.First().Brands.Load();
            return customer;
        }

        #endregion

        #region Save Customer

        public void saveCustomer(Customer customer)
        {

            var Customer = (from b in _entities.Customers
                        where b.ID == customer.ID
                        select b).First();
            Customer = customer;
            //User.Logo = imgPath;
            _entities.SaveChanges();
        }

        #endregion

        #region Delete Customer
        /// <summary>
        /// Deletes an existing customer
        /// </summary>
        /// <param name="customerId">Id of the customer to be deleted</param>
        public int deleteCustomer(int customerId)
        {
            int status = -1;
            var customer = (from b in _entities.Customers
                         where b.ID == customerId
                         select b).First();
            customer.Addresses.Load(); customer.Brands.Load(); customer.Contacts.Load();
            if (customer.Addresses.Count == 0 && customer.Brands.Count == 0 && customer.Contacts.Count == 0)
            {
                _entities.DeleteObject(customer);
                return status = _entities.SaveChanges();
            }
            else
            {
                return status;
            }
        }

        #endregion

        #region Create Customer
        /// <summary>
        /// Creates a new Customer
        /// </summary>
        /// <param name="name"></param>
        /// <param name="p"></param>
        public void createCustomer(Customer newCustomer)
        {
            //var User = new User();
            //var lastUser = _entities.Users.ToList().Last();
            //User.ID = lastUser.ID + 1;
            //User.Username = name;
            //User.Logo = path;
            _entities.Customers.AddObject(newCustomer);
            _entities.SaveChanges();
        }
        

        #endregion


        #region Fills/Creates the user and the related entites with the data choosen by the User
        /// <summary>
        /// Fills/Creates the user and the related entites with the data choosen by the User
        /// </summary>
        /// <param name="customer">user entity to be modified</param>
        /// <param name="brands">List containing Id's of the customers choosen</param>
        /// <param name="cultureChoosen">List containing Id's of the cultures choosen</param>
        /// <param name="countryChoosen">List containing Id's of the coutries choosen</param>
        /// <returns></returns>
        public Customer createdCustomerData(Customer customer, List<Int32> brands)
        {
            Customer createdCustomer = new Customer();
            createdCustomer.Name = customer.Name;
            createdCustomer.CompanyNumber = customer.CompanyNumber;
            createdCustomer.Website = customer.Website;
            createdCustomer.Comments = customer.Comments;
            createdCustomer.VATNumber = customer.VATNumber;

            #region Customer
            //editedCustomer.Brands.Load();
            //editedCustomer.Brands.Clear();
            if (brands != null)
            {
                List<Brand> brandsAssct = createdCustomer.Brands.ToList();
                foreach (var id in brands)
                {
                    Brand inCheck = _entities.Brands.Where(brand => brand.ID == id).First();
                    if (!brandsAssct.Contains(inCheck))
                    {
                        createdCustomer.Brands.Add(inCheck);
                        //editedUser.Customers.Attach(inCheck);
                    }
                }
            }
            #endregion
            
            return createdCustomer;
        }
        #endregion


        #region Fills/Creates the user and the related entites with the data choosen by the User
        /// <summary>
        /// Fills/Creates the user and the related entites with the data choosen by the User
        /// </summary>
        /// <param name="customer">user entity to be modified</param>
        /// <param name="brands">List containing Id's of the customers choosen</param>
        /// <param name="cultureChoosen">List containing Id's of the cultures choosen</param>
        /// <param name="countryChoosen">List containing Id's of the coutries choosen</param>
        /// <returns></returns>
        public Customer editedCustomerData(Customer customer, List<Int32> brands)
        {
            Customer editedCustomer = _entities.Customers.Where(cust => cust.ID == customer.ID).First();
            editedCustomer.Name = customer.Name;
            editedCustomer.CompanyNumber = customer.CompanyNumber;
            editedCustomer.Website = customer.Website;
            editedCustomer.Comments = customer.Comments;
            editedCustomer.VATNumber = customer.VATNumber;

            #region Customer
            editedCustomer.Brands.Load();
            editedCustomer.Brands.Clear();
            if (brands != null)
            {
                List<Brand> brandsAssct = editedCustomer.Brands.ToList();
                foreach (var id in brands)
                {
                    Brand inCheck = _entities.Brands.Where(brand => brand.ID == id).First();
                    if (!brandsAssct.Contains(inCheck))
                    {
                        editedCustomer.Brands.Add(inCheck);
                        //editedUser.Customers.Attach(inCheck);
                    }
                }
            }
            #endregion

            return editedCustomer;
        }
        #endregion
        #endregion

    }

}
