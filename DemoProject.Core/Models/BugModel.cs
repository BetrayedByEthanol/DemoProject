using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DemoProject.Core.Models
{
    public record BugModel : TicketModel
    {

        [Required]
        public EBugState state { get; init; }

    }
}
