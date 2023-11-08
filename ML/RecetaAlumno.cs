using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ML
{
    public class RecetaAlumno
    {
        public int Id { get; set; }
        public Alumno Alumno { get; set; }
        public Receta Receta { get; set; }

        public List<ML.RecetaAlumno> Objects { get; set; }
    }
}