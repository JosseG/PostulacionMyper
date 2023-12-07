using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JoseGutierrezMYPER.Models;

[Table("Provincia")]
public class Provincia
{
    [Key, Column("Id")]
    public int Id { get; set; }


    [Column("NombreProvincia")]
    public string Nombre { get; set; }

    [Column("IdDepartamento")]
    public int DepartamentoId { get; set; }

    public Departamento Departamento { get; set; } = null!;

    public List<Distrito>? Distritos { get; set; }
}