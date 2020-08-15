using System.Collections.Generic;
using System.Linq;
using WebApiCore.Data.Context;
using WebApiCore.Data.Models;

namespace WebApiCore.Data.Repository
{
   public class CustomerRepository : IRepository<Customer>
   {
      private readonly MongoContext context;

      public IEnumerable<Customer> All => context.Customers.ToList();

      public CustomerRepository(MongoContext context)
      {
         this.context = context;
      }

      public void Add(CustomerRepository entity)
      {
         context.Customers.Add(entity);
         context.SaveChanges();
      }

      public void Delete(Customer entity)
      {
         context.Customers.Remove(entity);
         context.SaveChanges();
      }

      public Customer FindById(string Id)
      {
         return context.Customers.FirstOrDefault(e => e.Id == id);
      }

      public void Update(Customer entity)
      {
         context.Customers.Update(entity);
         context.SaveChanges();
      }
   }
}