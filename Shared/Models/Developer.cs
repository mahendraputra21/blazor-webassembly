using System.ComponentModel.DataAnnotations;

namespace Blazor.Learner.Shared.Models
{
    public class Developer
    {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        public decimal Experience { get; set; }
        public int PositionId { get; set; }

    }
}
