
using System.Collections.ObjectModel;

namespace WPF_N_Tier_Test.Model
{
    public class Customer : WPF_N_Tier_Test_Data_Access.DTOs.Person
    {
        public override string ToString()
        {
            return $"{Name} - id:{Id}";
        }

        public ObservableCollection<Order> Orders { get; set; } = new ObservableCollection<Order>();

        public int OrdersCount { get { return Orders.Count; } }

        public double Expenditure { get { return Orders.ToList().Sum(a => a.Total); }}

        public double Payement { get { return Orders.ToList().FindAll(a => a.IsPaid).Sum(a => a.Total); }}

        public double Debt { get { return Expenditure - Payement; }}

        public bool EligibleToDelete { get{ return Orders.ToList().Find(a => a.IsPaid) != null; }}

        

    }
}
