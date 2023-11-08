﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ML
{
    public class Alumno
    {
        public int Matricula { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public DateTime FechaNacimiento { get; set; }

        public List<Alumno> Alumnos { get; set; }
    }
}