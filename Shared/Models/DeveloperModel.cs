using System.ComponentModel.DataAnnotations;

namespace Blazor.Learner.Shared.Models
{
    public class DeveloperModel
    {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        public decimal Experience { get; set; }
        [Required]
        public int PositionId { get; set; }
        public string? PositionName { get; set; }
    }
}
