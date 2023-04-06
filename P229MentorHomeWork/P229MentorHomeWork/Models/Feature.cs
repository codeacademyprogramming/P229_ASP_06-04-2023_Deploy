using System.ComponentModel.DataAnnotations;

namespace P229MentorHomeWork.Models
{
    public class Feature
    {
        public int Id { get; set; }
        [StringLength(255)]
        [Required]
        public string Icon { get; set; }
        [StringLength(255)]
        [Required]
        public string Title { get; set; }
    }
}
