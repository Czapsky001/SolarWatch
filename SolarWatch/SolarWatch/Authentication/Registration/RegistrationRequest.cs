using System.ComponentModel.DataAnnotations;

namespace SolarWatch.Authentication.Registration
{
    public record RegistrationRequest
    (
        [Required]string Email,
        [Required]string UserName,
        [Required]string Password
    );
    
}
