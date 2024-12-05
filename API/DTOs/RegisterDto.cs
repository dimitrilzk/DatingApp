using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class RegisterDto
    {
        [Required]//valida la prop se non è null e se non è stringa vuota
        public required string Username { get; set; }//questo required invece non fa vaildazione semplicemente il compilatore forzerà la sua inizializzazione
        [Required]
        public required string Password { get; set; }
    }
}
