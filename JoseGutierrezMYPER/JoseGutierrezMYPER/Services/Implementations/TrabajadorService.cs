using JoseGutierrezMYPER.Context;
using JoseGutierrezMYPER.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace JoseGutierrezMYPER.Services.Implementations;

public class TrabajadorService : ITrabajadorService
{
    private readonly TrabajadoresPruebaDbContext _trabajadoresPruebaDbContext;

    public TrabajadorService(TrabajadoresPruebaDbContext trabajadoresPruebaDbContext)
    {
        _trabajadoresPruebaDbContext = trabajadoresPruebaDbContext;
    }
    
    public async Task<Trabajador> Guardar(Trabajador trabajador)
    {
        try
        {
            Trabajador? trabajadorDb = await ObtenerPorId(trabajador.Id);

            if (trabajadorDb != null)
            {
                throw new Exception("El trabajador ya existe");
            }

            Departamento? departamento = await _trabajadoresPruebaDbContext.Departamentos.Where(x => x.Id == trabajador.DepartamentoId).FirstOrDefaultAsync();

            if (departamento == null)
            {
                throw new Exception("El dep ya existe");
            }

            Provincia? provincia = await _trabajadoresPruebaDbContext.Provincias.Where(x => x.Id == trabajador.ProvinciaId).FirstOrDefaultAsync();

            if (provincia == null)
            {
                throw new Exception("La prov no existe");
            }

            Distrito? distrito = await _trabajadoresPruebaDbContext.Distritos.Where(x => x.Id == trabajador.DistritoId).FirstOrDefaultAsync();

            if (distrito == null)
            {
                throw new Exception("El distrito no existe");
            }

            trabajadorDb = new Trabajador
            {
                Nombres = trabajador.Nombres,
                Departamento = departamento,
                DepartamentoId = departamento.Id,
                Distrito = distrito,
                DistritoId = distrito.Id,
                Id = trabajador.Id,
                NumeroDocumento = trabajador.NumeroDocumento,
                TipoDocumento = trabajador.TipoDocumento,
                Provincia = provincia,
                ProvinciaId = provincia.Id,
                Sexo = trabajador.Sexo
            };

            _trabajadoresPruebaDbContext.Trabajadores.FromSql($"SET IDENTITY_INSERT [dbo].[Trabajadores] ON");
            _trabajadoresPruebaDbContext.Attach(departamento);
            _trabajadoresPruebaDbContext.Attach(provincia);
            _trabajadoresPruebaDbContext.Attach(distrito);

            await _trabajadoresPruebaDbContext.Trabajadores.AddAsync(trabajadorDb);
            await _trabajadoresPruebaDbContext.SaveChangesAsync();
            _trabajadoresPruebaDbContext.Trabajadores.FromSql($"SET IDENTITY_INSERT [dbo].[Trabajadores] OFF");
            return trabajadorDb;

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        
        
        return new Trabajador();
    }

    public async Task<bool> Eliminar(int id)
    {
        var trabajador = await ObtenerPorId(id);
        if (trabajador == null)
        {
            return false;
        }
        
        _trabajadoresPruebaDbContext.Trabajadores.Remove(trabajador);
        return true;
    }

    public async Task<Trabajador> Actualizar(Trabajador trabajador)
    {
        try
        {
            Trabajador? trabajadorDb = await ObtenerPorId(trabajador.Id);

            if (trabajadorDb==null)
            {
                throw new Exception("El trabajador no existe existe");
            }

            Departamento? departamento = await _trabajadoresPruebaDbContext.Departamentos.Where(x => x.Id == trabajador.DepartamentoId).FirstOrDefaultAsync();

            if (departamento == null)
            {
                throw new Exception("El dep ya existe");
            }

            Provincia? provincia = await _trabajadoresPruebaDbContext.Provincias.Where(x => x.Id == trabajador.ProvinciaId).FirstOrDefaultAsync();

            if (provincia == null)
            {
                throw new Exception("La prov no existe");
            }

            Distrito? distrito = await _trabajadoresPruebaDbContext.Distritos.Where(x => x.Id == trabajador.DistritoId).FirstOrDefaultAsync();

            if (distrito == null)
            {
                throw new Exception("El distrito no existe");
            }

            trabajadorDb.Nombres = trabajador.Nombres;
            trabajadorDb.Departamento = departamento;
            trabajadorDb.DepartamentoId = departamento.Id;
            trabajadorDb.Distrito = distrito;
            trabajadorDb.DistritoId = distrito.Id;
            trabajadorDb.TipoDocumento = trabajador.TipoDocumento;
            trabajadorDb.NumeroDocumento = trabajador.NumeroDocumento;
            trabajadorDb.Provincia = provincia;
            trabajadorDb.ProvinciaId = provincia.Id;
            trabajadorDb.Sexo = trabajador.Sexo;

            _trabajadoresPruebaDbContext.Trabajadores.FromSql($"SET IDENTITY_UPDATE [dbo].[Trabajadores] ON");
            _trabajadoresPruebaDbContext.Attach(departamento);
            _trabajadoresPruebaDbContext.Attach(provincia);
            _trabajadoresPruebaDbContext.Attach(distrito);

            await _trabajadoresPruebaDbContext.SaveChangesAsync();
            _trabajadoresPruebaDbContext.Trabajadores.FromSql($"SET IDENTITY_UPDATE [dbo].[Trabajadores] OFF");
            return trabajadorDb;

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
        
        
        return new Trabajador();
    }

    public async Task<List<Trabajador>> Listar()
    {

        return await _trabajadoresPruebaDbContext.Trabajadores.ToListAsync();
    }

    public async Task<Trabajador?> ObtenerPorId(int id)
    {
        Trabajador? trabajadorDb = await  _trabajadoresPruebaDbContext.Trabajadores.Where(e => e.Id == id).FirstOrDefaultAsync();
        return trabajadorDb;
    }
}