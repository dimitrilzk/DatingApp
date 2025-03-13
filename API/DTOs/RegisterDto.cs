using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class RegisterDto
    {
        [Required]//valida la prop se non è null e se non è stringa vuota
        public string Username { get; set; } = string.Empty; //questo required invece non fa vaildazione semplicemente il compilatore forzerà la sua inizializzazione
        [Required]
        public string? KnownAs { get; set; }
        [Required]
        public string? Gender { get; set; }
        [Required]
        public string? DateOfBirth { get; set; }
        [Required]
        public string? City { get; set; }
        [Required]
        public string? Country { get; set; }
        [Required]
        [StringLength(8, MinimumLength = 4)]
        public string Password { get; set; } = string.Empty;
    }
}
