using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class RecetaController : Controller
    {
        // GET: Receta
        [HttpGet]
        public ActionResult FormReceta(int idReceta)
        {
            ML.Receta receta = new ML.Receta();
            ServiceReferenceReceta.RecetaServiceClient service = new ServiceReferenceReceta.RecetaServiceClient();
            if(idReceta == 0)
            {
                receta.IdReceta = idReceta;
                ML.RecetaAlumno nuevo = new ML.RecetaAlumno();
                nuevo.Receta = new ML.Receta();
                nuevo.Receta.IdReceta = idReceta;
            }
            else
            {
                receta = service.RecetaGetById(idReceta);
            }

            return View(receta);
        }

        [HttpPost]
        public ActionResult FormReceta(ML.Receta receta, int matricula)
        {
            ML.Alumno alumno = new ML.Alumno();
            alumno.Matricula = matricula;
            ServiceReferenceReceta.RecetaServiceClient service = new ServiceReferenceReceta.RecetaServiceClient();
            if (receta.IdReceta == 0)
            {
                bool correct = service.RecetaAdd(receta, alumno.Matricula);
                if (correct)
                {
                    return RedirectToAction("GetAllRecetas", "Receta");
                }
                else
                {
                    ViewBag.Message = "No se ha podido agregar tu receta, ha ocurrido un error";
                    return PartialView("Modal");
                }
            }
            else
            {
                bool correct = service.RecetaUpdate(receta);
                if (correct)
                {
                    return RedirectToAction("GetAllRecetas", "Receta");
                }
                else
                {
                    ViewBag.Message = "Ha ocurrido un error al momento de actualizar el elemento";
                    return PartialView("Modal");
                }
            }
        }

        [HttpGet]
        public ActionResult GetAllRecetas()
        {
            ML.RecetaAlumno nuevo = new ML.RecetaAlumno();
            nuevo.Objects = new List<ML.RecetaAlumno>();
            ServiceReferenceReceta.RecetaServiceClient service = new ServiceReferenceReceta.RecetaServiceClient();
            nuevo.Objects = service.RecetaList().ToList();

            return View(nuevo);
        }
    }
}