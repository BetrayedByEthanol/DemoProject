using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Core.Models
{
    public record StoryModel
    {
        [Key]
        public Guid id { get; init; }
    }
}
