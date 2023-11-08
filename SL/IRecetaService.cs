using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace SL
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IRecetaService" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IRecetaService
    {
        [OperationContract]
        bool RecetaAdd(ML.Receta receta, int matricula);

        [OperationContract]
        bool RecetaUpdate(ML.Receta receta);

        [OperationContract]
        [ServiceKnownType(typeof(ML.RecetaAlumno))]
        List<ML.RecetaAlumno> RecetaList();

        [OperationContract]
        [ServiceKnownType(typeof(ML.Receta))]
        ML.Receta RecetaGetById(int ideReceta);
    }
}
