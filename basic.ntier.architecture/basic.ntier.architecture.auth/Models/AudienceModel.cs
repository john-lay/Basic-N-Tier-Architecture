namespace basic.ntier.architecture.auth.Models
{
    using System.ComponentModel.DataAnnotations;

    public class AudienceModel
    {
        [MaxLength(100)]
        [Required]
        public string Name { get; set; }
    }
}