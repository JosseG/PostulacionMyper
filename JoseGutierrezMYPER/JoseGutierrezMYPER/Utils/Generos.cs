using JoseGutierrezMYPER.Models;

namespace JoseGutierrezMYPER.Utils;
public static class Generos
{
    public const string MASCULINO = "MASCULINO";
    public const string FEMENINO = "FEMENINO";



}
 

public static class TipoDocumentoStatic
{
    public static readonly List<TipoDocumento> ListaDeCadenas = new List<TipoDocumento>
            {
                new TipoDocumento
                {
                    Id = 0,
                    Nombre = "DNI"
                },
                new TipoDocumento
                {
                    Id = 0,
                    Nombre = "Pasaporte"
                },
                new TipoDocumento
                {
                    Id = 0,
                    Nombre = "Carnet"
                },

            };

}


