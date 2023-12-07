using JoseGutierrezMYPER.Models;

namespace JoseGutierrezMYPER.Services
{
    public interface IDepartamentoService
    {
        Task<List<Departamento>> Listar();
    }
}
