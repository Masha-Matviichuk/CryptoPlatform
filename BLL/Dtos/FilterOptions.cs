using System;
using DAL.Entities;

namespace Api.Models
{
    public class FilterOptions
    {
        public string CategoryId { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
    }
}