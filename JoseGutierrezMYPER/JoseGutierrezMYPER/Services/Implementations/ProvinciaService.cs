using JoseGutierrezMYPER.Context;
using JoseGutierrezMYPER.Models;
using Microsoft.EntityFrameworkCore;

namespace JoseGutierrezMYPER.Services.Implementations;

public class ProvinciaService : IProvinciaService
{

    private readonly TrabajadoresPruebaDbContext _trabajadoresPruebaDbContext;

    public ProvinciaService(TrabajadoresPruebaDbContext trabajadoresPruebaDbContext)
    {
        _trabajadoresPruebaDbContext = trabajadoresPruebaDbContext;
    }
    public async Task<List<Provincia>> Listar()
    {
        return await _trabajadoresPruebaDbContext.Provincias.ToListAsync();
    }
}

