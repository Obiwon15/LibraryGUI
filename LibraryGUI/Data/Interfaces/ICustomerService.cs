using LibraryGUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryGUI.Data.Services
{
    public interface ICustomerService
    {

        Task<Customer> GetCustomer(int? Id);
      
        Task<List<Customer>> GetAllCustomers();
        //Physician
     
        void AddCustomer(Customer customer);
        void RemoveCustomer(Customer customer);
        void Updatecustomer(Customer customer);
        //List<PatientForm> GetAllPatients(string physician);
       

        Task<bool> SaveChangesAsync();

    }
}
