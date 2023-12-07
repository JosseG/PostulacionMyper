using JoseGutierrezMYPER.Context;
using JoseGutierrezMYPER.Models;
using Microsoft.EntityFrameworkCore;

namespace JoseGutierrezMYPER.Services.Implementations;
public class DepartamentoService : IDepartamentoService
{
    private readonly TrabajadoresPruebaDbContext _trabajadoresPruebaDbContext;

    public DepartamentoService(TrabajadoresPruebaDbContext trabajadoresPruebaDbContext)
    {
        _trabajadoresPruebaDbContext = trabajadoresPruebaDbContext;
    }
    public async Task<List<Departamento>> Listar()
    {
        return await _trabajadoresPruebaDbContext.Departamentos.ToListAsync();
    }
}

