using Microsoft.EntityFrameworkCore;
using WPF_N_Tier_Test.Model;
using WPF_N_Tier_Test_Data_Access.DataAccess;
using WPF_N_Tier_Test_Data_Access.DTOs;

namespace WPF_N_Tier_Test.Service
{
    public class SalesService
    {

        private SalesContext dbContext;

        public SalesService(SalesDesignTimeContextFactory contextFactory)
        {
            this.dbContext = contextFactory.CreateDbContext(null);
        }
        public Transaction<TransactionBatch> ToTransactionDTO(Order order)
        {
            var t = new Transaction<TransactionBatch>
            {
                Customer = order.Customer,
                DateTime = order.DateTime,
                discount = order.Discount,
                PaymentDate = order.PaymentDate,
                TransactedEntities = new(order.TransactedEntities.Select(ToBatchDTO()))
            };
            return t;

        }

        private static Func<ProductBatch, TransactionBatch> ToBatchDTO()
        {
            return x => new TransactionBatch
            {
                Id = x.Id,
                ProductId = x.ProductId,
                ProductName = x.ProductName,
                Category = x.Category,
                Article = x.Article,
                Model = x.Model,
                UnitMetric = x.UnitMetric,
                Quantity = x.Quantity,
                discount = x.Discount,
                UnitCost = x.UnitCost,
                UnitPrice = x.UnitPrice,
            };
        }
        private static Func<TransactionBatch, ProductBatch> BatchFromDTO()
        {
            return x => new ProductBatch
            {
                Id = x.Id,
                ProductId = x.ProductId,
                ProductName = x.ProductName,
                Category = x.Category,
                Article = x.Article,
                Model = x.Model,
                UnitMetric = x.UnitMetric,
                Quantity = x.Quantity,
                Discount = x.discount,
                UnitCost = x.UnitCost,
                UnitPrice = x.UnitPrice,
            };
        }

        internal async Task<bool> INSERT_Order(Order newOrder)
        {
            newOrder.Customer.Id = 0;
            var result = await dbContext.Orders.AddAsync(ToTransactionDTO(newOrder));
            if(result.State == Microsoft.EntityFrameworkCore.EntityState.Added) {
                await dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
        internal async Task<IEnumerable<Order>> GetAllOrders()
        {
            var a = dbContext.Customers;
            return dbContext.Orders
                .Include(x => x.TransactedEntities)
                .Include(x => x.Customer)
                .Select(x => OrderFromDTO(x));
        }

        private static Order OrderFromDTO(Transaction<TransactionBatch> x)
        {
            return  new Order
            {
                Customer = x.Customer,
                CustomerId = x.CustomerId,
                DateTime = x.DateTime,
                Discount = x.discount,
                PaymentDate = x.PaymentDate,
                TransactedEntities = new(x.TransactedEntities.Select(BatchFromDTO())),
            };
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
