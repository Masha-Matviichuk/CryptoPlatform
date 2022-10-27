using System;
using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    public abstract class BaseEntity
    {
        [Key] 
        public string Id { get; set; }
        public bool IsDeleted { get; set; }

        protected BaseEntity()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}