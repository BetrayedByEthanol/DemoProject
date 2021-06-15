using System.ComponentModel.DataAnnotations;

namespace DemoProject.Core.Models
{
    public record BugModel : TicketModel
    {

        [Required]
        public EBugState state { get; init; }

    }
}
