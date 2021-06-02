using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
