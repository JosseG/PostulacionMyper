using JoseGutierrezMYPER.Models;

namespace JoseGutierrezMYPER.Services
{
    public interface IProvinciaService
    {
        Task<List<Provincia>> Listar();
    }
}
