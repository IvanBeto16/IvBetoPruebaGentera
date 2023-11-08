using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ML
{
    public class Receta
    {
        public int IdReceta { get; set; }
        public DateTime Fecha { get; set; }
        public string Diagnostico { get; set; }
        public string Tratamiento { get; set; }

        public List<Receta> Recetas { get; set; }
    }
}