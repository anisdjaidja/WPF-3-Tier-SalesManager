using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_N_Tier_Test.Model;

namespace WPF_N_Tier_Test.Service
{
    public class CustomerService
    {
        internal async Task<bool> DELETE(int id)
        {
            return false;
        }

        internal async Task<IEnumerable<Customer>> GetAllCustomers()
        {
            return new List<Customer>();
        }

        internal async Task<Customer> INSERT(Customer customer)
        {
            return new Customer();
        }

        internal async Task<Order> INSERT_Order(Order newOrder)
        {
            return new Order();
        }

        internal async Task<bool> PAY_Order(int oID)
        {
            return false;
        }

        internal async Task<bool> REVOKE_Order(int oID)
        {
            return false;
        }

        internal async Task<IEnumerable<Customer>> SearchPatient(List<string> subsets, int v)
        {
            return new List<Customer>();
        }

        internal async Task<Customer> UPDATE(int id, string personName, string phone, string address, string company, string fax, string nif, string nis, string n_a, DateTime birthDate)
        {
            return new Customer();
        }

        internal async Task<bool> VALIDATE_Order(int oID)
        {
            return false;
        }
    }
}
