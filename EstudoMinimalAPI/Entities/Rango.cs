using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace EstudoMinimalAPI.Entities; 
public class Rango {

    [Key]
    public int Id { get; set; }
    [MaxLength(100)]
    public string? Nome { get; set; }

    public Rango()
    {
        
    }

    [SetsRequiredMembers]
    public Rango(int id, string nome)
    {
        Id = id;
        Nome = nome;
    }
}
