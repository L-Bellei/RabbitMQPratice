using System.ComponentModel.DataAnnotations;

namespace RabbitMQ.Front.Models;

public class User
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Nome é obrigatório.")]
    public string? Name { get; set; }

    [Required(ErrorMessage = "E-mail é obrigatório.")]
    [EmailAddress(ErrorMessage = "E-mail inválido.")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "Senha é obrigatória.")]
    [MinLength(6, ErrorMessage = "A senha precisa ter pelo menos 6 caracteres.")]
    public string? Password { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
