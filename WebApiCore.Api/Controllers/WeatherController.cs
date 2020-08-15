using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebApiCore.Api;
using WebApiCore.Data.Models;

namespace WebApeCore.Api.Controllers
{
   [Route("api/[controller]")]
   [ApiController]
   public class WeatherController : ControllerBase
   {
      public IRepository<CurrentWeather> contextWeather { get; private set; }
      
      public WeatherController(
         IRepository<CurrentWeather> contextWeather
      )
      {
         this.contextWeather = contextWeather;
      }

      [HttpGet]
      public IEnumerable<CurrentWeather> Get()
      {
         return (IEnumerable<CurrentWeather>)contextWeather.All;
      }

      [HttpGet("{id}")]
      public ActionResult<CurrentWeather> Get(int id)
      {
         return contextWeather.FindById(id);
      }

      [HttpPost]
      public void Post([FromQuery] CurrentWeather value)
      {
         contextWeather.Update(value);
      }

      [HttpPut("{id}")]
      public void Put(int Id, [FromBody] CurrentWeather value)
      {
         contextWeather.Add(value);
      }

      [HttpDelete("{id}")]
      public void Delete(int id)
      {
         var entity = contextWeather.FindById(id);
         contextWeather.Delete(entity);
      }
   } 
}