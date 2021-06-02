using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DemoProject.Core.Models
{
    public record TicketModel 
    {
        [Key]
        public Guid id { get; init; }

        [Required, MinLength(3), MaxLength(32)]
        public string name { get; init; }

        [Required, MinLength(3), MaxLength(256)]
        public string description { get; init; }

        [ForeignKey("id")]
        public UserModel user { get; init; }

        public TicketModel()
        {

        }
    }
}
