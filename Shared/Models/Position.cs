using System.ComponentModel.DataAnnotations;

namespace Blazor.Learner.Shared.Models
{
    public class Position
    {
        public Position()
        {
            Developers = new HashSet<Developer>();
        }

        public int PositionId { get; set; }

        [Required]
        public string PositionName { get; set; }

        public HashSet<Developer> Developers { get; set; }
    }
}
