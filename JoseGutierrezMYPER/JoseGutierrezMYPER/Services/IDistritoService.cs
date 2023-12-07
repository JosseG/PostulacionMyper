using JoseGutierrezMYPER.Models;

namespace JoseGutierrezMYPER.Services
{
    public interface IDistritoService
    {
        Task<List<Distrito>> Listar();
    }
}
