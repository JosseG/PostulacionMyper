using JoseGutierrezMYPER.Models;

namespace JoseGutierrezMYPER.Services;

public interface ITrabajadorService
{
    Task<Trabajador> Guardar(Trabajador trabajador);

    Task<bool> Eliminar(int id);

    Task<Trabajador> Actualizar(Trabajador trabajador);

    Task<List<Trabajador>> Listar();

    Task<Trabajador?> ObtenerPorId(int id);

}