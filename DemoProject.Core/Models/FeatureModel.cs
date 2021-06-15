using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Core.Models
{
    public record FeatureModel
    {
        [Key]
        public Guid id { get; init; }
    }
}
