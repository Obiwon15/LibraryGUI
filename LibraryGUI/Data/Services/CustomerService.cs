using LibraryGUI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryGUI.Data.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ApplicationDbContext _ctx;

        public CustomerService(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        public void AddCustomer(Customer customer)
        {
            _ctx.Customers.Add(customer);
        }

        public async Task<List<Customer>> GetAllCustomers()
        {
            return await _ctx.Customers.ToListAsync();
        }

        public async Task<Customer> GetCustomer(int? Id)
        {
            return await _ctx.Customers.FirstOrDefaultAsync(a => a.CustomerId == Id);
        }

        public void RemoveCustomer(Customer customer)
        {
            _ctx.Customers.Remove(customer);
        }

        public async Task<bool> SaveChangesAsync()
        {
            if (await _ctx.SaveChangesAsync() > 0)
            {
                return true;
            }
            return false;
        }

        public void Updatecustomer(Customer customer)
        {
            _ctx.Customers.Update(customer);
        }
    }
}
