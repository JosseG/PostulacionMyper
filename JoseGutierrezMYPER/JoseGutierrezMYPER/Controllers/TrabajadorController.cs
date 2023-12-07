using JoseGutierrezMYPER.Context;
using JoseGutierrezMYPER.Models;
using JoseGutierrezMYPER.Services;
using JoseGutierrezMYPER.Services.Implementations;
using JoseGutierrezMYPER.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Diagnostics;
using System.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace JoseGutierrezMYPER.Controllers;

public class TrabajadorController : Controller
{
    private ITrabajadorService _trabajadorService;
    private IDepartamentoService _departamentoService;
    private IDistritoService _distritoService;
    private IProvinciaService _provinciaService;
    
    public TrabajadorController(TrabajadoresPruebaDbContext trabajadoresPruebaDbContext)
    {
        _trabajadorService = new TrabajadorService(trabajadoresPruebaDbContext);
        _distritoService = new DistritoService(trabajadoresPruebaDbContext);
        _provinciaService = new ProvinciaService(trabajadoresPruebaDbContext);
        _departamentoService = new DepartamentoService(trabajadoresPruebaDbContext);
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        IEnumerable<Trabajador> trabajadores = await _trabajadorService.Listar();
        IEnumerable<Distrito> distritos = await _distritoService.Listar();
        IEnumerable<Provincia> provincias = await _provinciaService.Listar();
        IEnumerable<Departamento> departamentos = await _departamentoService.Listar();

        return View(trabajadores);
    }

    [HttpGet]
    public async Task<IActionResult> Guardar(int? idDepartamento,int? idProvincia)
    {
        IEnumerable<Departamento> departamentos = await _departamentoService.Listar();

        ViewBag.Departamentos = new SelectList(departamentos, "Id", "Nombre");
        ViewBag.TipoDocumentos = new SelectList(TipoDocumentoStatic.ListaDeCadenas,"Nombre","Nombre");
        return PartialView("Agregar", new Trabajador());
    }




    [HttpPost]
    public async Task<IActionResult> Guardar(Trabajador trabajador)
    {
        try
        {

            Trabajador trabajadorGuardado = await _trabajadorService.Guardar(trabajador);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return RedirectToAction("Index");
        }
        
        return RedirectToAction("Index");

    }

    public async Task<IActionResult> Eliminar(int id)
    {
        var result = await _trabajadorService.Eliminar(id);

        if (!result)
        {
            return Json(new { response = false });
        }
        
        return Json(new { response = true });
    }

    [HttpGet]
    public async Task<IActionResult> Actualizar(int id)
    {
        var trabajadorObtenido = await _trabajadorService.ObtenerPorId(id);

        IEnumerable<Departamento> departamentos = await _departamentoService.Listar();
        IEnumerable<Distrito> distritos = await _distritoService.Listar();
        IEnumerable<Provincia> provincias = await _provinciaService.Listar();


        var groupProvincias = from provincia in provincias
                              group provincia by provincia.DepartamentoId into newGroup
                              orderby newGroup.Key
                              select newGroup;
        List<Provincia> provinciasNewList = new List<Provincia>();

        foreach (var group in groupProvincias)
        {
            foreach (var provincia in group)
            {

                if (provincia.DepartamentoId == trabajadorObtenido.DepartamentoId)
                {
                    provinciasNewList.Add(provincia);
                }
            }
        }


        var groupDistritos = from distrito in distritos
                              group distrito by distrito.ProvinciaId into newGroup
                              orderby newGroup.Key
                              select newGroup;
        List<Distrito> distritosNewList = new List<Distrito>();

        foreach (var group in groupDistritos)
        {
            foreach (var distrito in group)
            {

                if (distrito.ProvinciaId == trabajadorObtenido.ProvinciaId)
                {
                    distritosNewList.Add(distrito);
                }
            }
        }


        ViewBag.Departamentos = new SelectList(departamentos, "Id", "Nombre");
        ViewBag.Distritos = new SelectList(distritosNewList, "Id", "Nombre");
        ViewBag.Provincias = new SelectList(provinciasNewList, "Id", "Nombre");
        ViewBag.TipoDocumentos = new SelectList(TipoDocumentoStatic.ListaDeCadenas, "Nombre", "Nombre");

        if (trabajadorObtenido != null)
        {

            return PartialView("Actualizar", trabajadorObtenido);

        }
        return PartialView("Actualizar",trabajadorObtenido);
    }
    
    [HttpPost]
    public async Task<IActionResult> Actualizar(Trabajador trabajador)
    {
        try
        {
            Debug.WriteLine("CODIGO  " + trabajador.Id);

            Trabajador trabajadorGuardado = await _trabajadorService.Actualizar(trabajador);

        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }

        return RedirectToAction("Index");
    }
    
    
    [HttpGet]
    public IActionResult Filtrar()
    {
        return Json(new { response = true });
    }



    [HttpGet]
    public async Task<IActionResult> FiltrarDepartamentoProvincia(int idDepartamento = 0,int idProvincia = 0)
    {
        IEnumerable<Departamento> departamentos = await _departamentoService.Listar();

        ViewBag.Departamentos = new SelectList(departamentos, "Id", "Nombre");

        
        if (idDepartamento > 0)
        {
            IEnumerable<Provincia> provincias = await _provinciaService.Listar();

            Debug.WriteLine(provincias.Count());

            var groupProvincias = from provincia in provincias
                                  group provincia by provincia.DepartamentoId into newGroup
                                  orderby newGroup.Key
                                  select newGroup;

            List<Provincia> provinciasNewList = new List<Provincia>();

            foreach (var group in groupProvincias)
            {
                foreach (var provincia in group)
                {
                    
                    if(provincia.DepartamentoId == idDepartamento)
                    {
                        provinciasNewList.Add(provincia);
                    }
                }
            }

            if (idProvincia > 0)
            {
                IEnumerable<Distrito> distritos = await _distritoService.Listar();

                var groupDistritos = from distrito in distritos
                                     group distrito by distrito.ProvinciaId into newGroup
                                      orderby newGroup.Key
                                      select newGroup;

                List<Distrito> distritosNewList = new List<Distrito>();

                foreach (var group in groupDistritos)
                {
                    foreach (var distrito in group)
                    {
                        if (distrito.ProvinciaId == idProvincia)
                        {
                            distritosNewList.Add(distrito);
                        }
                    }
                }

                ViewBag.Departamentos = new SelectList(departamentos, "Id", "Nombre", idDepartamento);
                ViewBag.Provincias = new SelectList(provinciasNewList, "Id", "Nombre",idProvincia);
                ViewBag.Distritos = new SelectList(distritosNewList, "Id", "Nombre");

                return Json(new { response = ViewBag.Distritos });

            }

            ViewBag.Departamentos = new SelectList(departamentos, "Id", "Nombre",idDepartamento);
            ViewBag.Provincias = new SelectList(provinciasNewList.AsEnumerable(), "Id", "Nombre");

            return Json(new { response = ViewBag.Provincias });
        }


        return Json(new { response = ViewBag.Departamentos });
    }

}