using System.ComponentModel.DataAnnotations;

namespace DemoProject.Core.Models
{
    public record FeatureTicketModel : TicketModel
    {

        [Required]
        public ETicketState state { get; init; }

        public FeatureTicketModel()
        {
        }
    }
}
