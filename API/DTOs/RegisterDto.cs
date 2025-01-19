using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class RegisterDto
    {
        [Required]//valida la prop se non è null e se non è stringa vuota
        public string Username { get; set; } = string.Empty; //questo required invece non fa vaildazione semplicemente il compilatore forzerà la sua inizializzazione

        [Required]
        [StringLength(8, MinimumLength = 4)]
        public string Password { get; set; } = string.Empty;
    }
}
