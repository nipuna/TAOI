using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainModel.Entities;

namespace DomainModel.Abstract
{
    public interface ICustomerRepository
    {
        IQueryable<Customer> getCustomers();

        IQueryable<Customer> getCustomers(Int32 page, Int32 pageSize, out Int32 totNumCustomers);

        IQueryable<customerDisplay> getCustomersForDisplay(IQueryable<Customer> Customers);

        IQueryable<Customer> getCustomer(Int32 customerId);

        void editCustomer(Int32 customerId);

        void saveCustomer(Customer customer);

        int deleteCustomer(int customerId);

        void createCustomer(Customer newCustomer);

        Customer createdCustomerData(Customer customer, List<Int32> brands);

        Customer editedCustomerData(Customer customer, List<Int32> brands);

    }
}

public class customerDisplay
{
    public int ID { get; set; }
    public string Logo { get; set; }
    public string Name { get; set; }
    public string Action { get; set; }

}