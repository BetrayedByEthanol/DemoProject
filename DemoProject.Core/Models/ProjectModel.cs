using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Core.Models
{
    public record ProjectModel
    {
        [Key]
        public Guid id { get; init; }
    }
}
