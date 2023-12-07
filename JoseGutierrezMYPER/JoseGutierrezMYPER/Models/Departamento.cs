using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JoseGutierrezMYPER.Models;

[Table("Departamento")]
public class Departamento
{
    [Key,Column("Id")]
    public int Id { get; set; }

    [Column("NombreDepartamento")]
    public string Nombre { get; set; }

    
    public List<Provincia>? Provincias { get; set; }
}