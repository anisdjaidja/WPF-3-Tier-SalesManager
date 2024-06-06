using WPF_N_Tier_Test.Model;
using WPF_N_Tier_Test_Data_Access.DataAccess;
using WPF_N_Tier_Test_Data_Access.DTOs;

namespace WPF_N_Tier_Test.Service
{
    public class SalesService
    {

        private SalesContext dbContext;

        public SalesService(SalesContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public Transaction<TransactionBatch> ToTransactionDTO(Order order) 
        {
            var t = new Transaction<TransactionBatch>
            {
                Customer = order.Customer,
                DateTime = order.DateTime,
                discount = order.Discount,
                PaymentDate = order.PaymentDate,
                TransactedEntities = new(order.TransactedEntities.Select(x => new TransactionBatch()))
            };
            return t;

        }
        internal async Task<Order> INSERT_Order(Order newOrder)
        {
            await dbContext.Orders.AddAsync(ToTransactionDTO(newOrder));
            return newOrder;
        }

        internal async Task<bool> PAY_Order(int oID)
        {
            return false;
        }

        internal async Task<bool> REVOKE_Order(int oID)
        {
            return false;
        }
    }
}
