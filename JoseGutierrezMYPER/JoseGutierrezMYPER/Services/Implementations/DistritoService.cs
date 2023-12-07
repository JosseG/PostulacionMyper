using JoseGutierrezMYPER.Context;
using JoseGutierrezMYPER.Models;
using Microsoft.EntityFrameworkCore;

namespace JoseGutierrezMYPER.Services.Implementations;

public class DistritoService : IDistritoService
{

    private readonly TrabajadoresPruebaDbContext _trabajadoresPruebaDbContext;

    public DistritoService(TrabajadoresPruebaDbContext trabajadoresPruebaDbContext)
    {
        _trabajadoresPruebaDbContext = trabajadoresPruebaDbContext;
    }
    public async Task<List<Distrito>> Listar()
    {
        return await _trabajadoresPruebaDbContext.Distritos.ToListAsync();
    }
}

