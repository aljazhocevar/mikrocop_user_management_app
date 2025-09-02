using System;
using System.ComponentModel.DataAnnotations;

namespace UserRepositoryService.Models
{
    public class Client
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string ApiKey { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
