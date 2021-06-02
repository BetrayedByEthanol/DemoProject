using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DemoProject.Core.Models
{
    public record ProjectModel
    {
        [Key]
        public Guid id { get; init; }
    }
}
