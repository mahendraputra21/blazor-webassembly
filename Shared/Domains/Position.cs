using System.ComponentModel.DataAnnotations;

namespace Blazor.Learner.Shared.Domains
{
    public class Position
    {
        [Required]
        public int PositionId { get; set; }
        public string? PositionName { get; set; }

    }
}
