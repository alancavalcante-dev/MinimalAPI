using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace EstudoMinimalAPI.Entities;
public class Ingrediente {

    [Key]
    public int Id { get; set; }
    [MaxLength(100)]
    public string? Nome { get; set; }

    public Ingrediente() {

    }

    [SetsRequiredMembers]
    public Ingrediente(int id, string nome) {
        Id = id;
        Nome = nome;
    }
}
