using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JoseGutierrezMYPER.Models;

[Table("Distrito")]
public class Distrito
{
    [Key,Column("Id")]
    public int Id { get; set; }

    [Column("NombreDistrito")]
    public string Nombre { get; set; }

    [Column("IdProvincia")]
    public int ProvinciaId { get; set; }

    public Provincia Provincia { get; set; } = null!;

    
    
}