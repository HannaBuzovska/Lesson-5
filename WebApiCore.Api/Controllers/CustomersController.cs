using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiCore.Api;
using WebApiCore.Data.Models;

namespace WebApeCore.Api.Controllers
{
   [Route("api/[controller]")]
   [ApiController]
   public class CustomersController : ControllerBase
   {
      public IRepository<Customer> contextCustomers{ get; private set; }

      public CustomersController(
         IRepository<Customer> contextCustomers
      )
      {
         this.contextCustomers= contextCustomers;
      }
      [HttpPost("mega-post")]
      [HttpPost("mega-post/{routeName}")] //POST http://localhost:5000/api/customers/mega-post
      public IEnumerable<Customer> Post( 
         [FromQuery] string queryName,    // - http://domain.com/api/customers/mega-post?queryName=NameFromQuery&id=1
         [FromRoute] string routeName,    // - http://domain.com/api/customers/mega-post/NameFromRouter
         [FromForm] Customer customer1,   // - http://domain.com/api/customers/mega-post Form-Body:  Id: 15 Name: NameFromForm
         [FromBody] Customer customer2,   // - http://domain.com/api/customers/mega-post Body: { Id: 15, name: "NameFromBody"}
         [FromHeader] string headerName   // - http://domain.com/api/customers/mega-post Headers: headerName: NameFromHeader
      )
      {
         return contextCustomers.All;
      }

      /// <summary>
      /// Get All Customers.
      /// </summary>
      /// <remarks>
      /// Sample request:
      ///
      ///     GET /api/customers
      ///
      /// <remarks>
      /// <returns>A list of exited customers</returns>
      /// <response code="200">Success </response>
      /// <response code="400">If error occers</response>
      /// <response code="404">If no customers in database</response>
      [HttpGet]

      [ProducesResponseType(StatusCodes.Status404NotFound)]
      [ProducesResponseType(StatusCodes.Status201Created)]
      [ProducesResponseType(StatusCodes.Status400BadRequest)]
      public ActionResult<IEnumerable<Customer>> Get()
      {
         List<Customer> result = null;
         try
         {
            result = contextCustomers.All.ToList();
            if (result.Count == 0) {
               return NotFound();
            }
         }
         catch
         {
            return BadRequest();
         }
         return Ok(result);
      }

      [HttpGet("{id}")]
      public ActionResult<Customer> Get(int id)
      {
         return contextCustomers.FindById(id);
      }

      [HttpGet("{id}/teamlead")]
      public ActionResult<Customer> GetTeamlead(int id)
      {
         return contextCustomers.FindById(id);
      }

      [HttpPost]
      public void Post([FromQuery] Customer value)
      {
         contextCustomers.Update(value);
      }

      [HttpPut("{id}")]
      public void Put(int Id, [FromBody] Customer value)
      {
         contextCustomers.Add(value);
      }

      [HttpDelete("{id}")]
      public void Delete(int id)
      {
         var entity = contextCustomers.FindById(id);
         contextCustomers.Delete(entity);
      }
   } 
}