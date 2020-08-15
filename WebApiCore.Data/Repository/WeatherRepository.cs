using System.Collections.Generic;
using System.Linq;
using WebApiCore.Data.Context;
using WebApiCore.Data.Models;

namespace WebApiCore.Data.Repository
{
   public class WeatherRepository : IRepository<CurrentWeather>
   {
      private readonly WebApiCoreContext context;

      public IEnumerable<CurrentWeather> All => context.Weathers.ToList();

      public void Add(CurrentWeather entity)
      {
         context.Weathers.Add(entity);
      }
   }
}