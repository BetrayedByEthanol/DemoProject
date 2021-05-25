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
        
        [Required,Range(3,32)]
        public string name { get; init; }

        [Required, Range(3, 256)]
        public string description { get; init; }

        [Required]
        public ETicketState state { get; init; }

        [ForeignKey("id")]
        public UserModel user { get; init; }

        public TicketModel()
        {
            name = "test";
            description = "DO some stuff";
            user = new UserModel()
            {
                username = "Clown"
            };
        }
    }
}
