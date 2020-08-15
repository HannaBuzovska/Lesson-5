using System;
using System.ComponentModel.DataAnnotations;

namespace WebApiCore.Data.Models
{
   public class Customer
   {
      [Key]
      public string Id {get; set;}
      public string Name {get;set;}
      public string Email{get; set;}
      public DateTime BirthData{get; set;}
   }
}