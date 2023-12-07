using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JoseGutierrezMYPER.Models;

[Table("Trabajadores")]
public class Trabajador
{
    [Key,Column("Id")]
    public int Id { get; set; }

    [Column("TipoDocumento"), Display(Name = "Tipo de Documento")]
    public string TipoDocumento { get; set; } = null!;

    [Column("NumeroDocumento"), Display(Name = "Número de Documento"), RegularExpression("^[0-9]{7,10}$",
        ErrorMessage = "No se sigue el formato para número de documento")]
    public string NumeroDocumento { get; set; } = null!;

    [Column("Nombres"),RegularExpression("^[A-Za-z]{2,}$",
        ErrorMessage = "No se sigue el formato correcto para nombre")]
    public string Nombres { get; set; } = null!;

    [Column("Sexo"),MaxLength(1)]
    public string Sexo { get; set; }

    [Column("IdDepartamento"),Display(Name = "Departamento")]
    public int DepartamentoId { get; set; }

    [Column("IdProvincia"), Display(Name = "Provincia")]
    public int ProvinciaId { get; set; }

    [Column("IdDistrito"), Display(Name = "Distrito")]
    public int DistritoId { get; set; }


    public Departamento Departamento { get; set; }
    public Provincia Provincia { get; set; }
    public Distrito Distrito { get; set; }

    
}