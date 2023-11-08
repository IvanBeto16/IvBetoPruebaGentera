using ML;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace SL
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "RecetaService" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione RecetaService.svc o RecetaService.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class RecetaService : IRecetaService
    {
        public bool RecetaAdd(Receta receta, int matricula)
        {
            bool correct = false;
            try
            {
                using (SqlConnection conexion = new SqlConnection(DL.Conexion.GetConnectionString()))
                {
                    RecetaAlumno nuevo = new RecetaAlumno();
                    nuevo.Receta = receta;
                    nuevo.Alumno = new Alumno();
                    nuevo.Alumno.Matricula = matricula;

                    string command = "RecetasAdd";
                    SqlCommand query = new SqlCommand(command, conexion);
                    query.CommandType = CommandType.StoredProcedure;
                    
                    //Bloque para la insercion en la tabla de recetas
                    SqlParameter[] param = new SqlParameter[4];
                    param[0] = new SqlParameter("@Matricula", SqlDbType.Int);
                    param[0].Value = nuevo.Alumno.Matricula;
                    param[1] = new SqlParameter("@Fecha", SqlDbType.DateTime);
                    param[1].Value = nuevo.Receta.Fecha;
                    param[2] = new SqlParameter("@Diagnostico", SqlDbType.NVarChar);
                    param[2].Value = nuevo.Receta.Diagnostico;
                    param[3] = new SqlParameter("@Tratamiento", SqlDbType.NVarChar);
                    param[3].Value = nuevo.Receta.Tratamiento;
                    query.Parameters.AddRange(param);
                    query.Connection.Open();

                    //query2.Connection.Open();

                    int inserts = query.ExecuteNonQuery();
                    if (inserts > 0)
                    {
                        correct = true;
                    }
                    else
                    {
                        correct = false;
                    }
                }
            }
            catch (Exception ex)
            {
                correct = false;
            }
            return correct;
        }

        public List<RecetaAlumno> RecetaList()
        {
            List<RecetaAlumno> Objects = new List<RecetaAlumno>();
            using (SqlConnection conexion = new SqlConnection(DL.Conexion.GetConnectionString()))
            {
                string comando = "SELECT Receta.IdReceta,Receta.Fecha,Receta.Diagnostico,Receta.Tratamiento,Alumno.Matricula,Alumno.Nombre,Alumno.ApellidoPaterno,Alumno.ApellidoMaterno,Alumno.FechaNacimiento " +
                    "FROM Receta INNER JOIN RecetaAlumno ON Receta.IdReceta = RecetaAlumno.IdReceta INNER JOIN Alumno ON RecetaAlumno.Matricula = Alumno.Matricula";
                SqlCommand query = new SqlCommand(comando, conexion);


                SqlDataAdapter adapter = new SqlDataAdapter(query);
                DataTable tabla = new DataTable();
                adapter.Fill(tabla);

                if (tabla.Rows.Count > 0)
                {
                    foreach (DataRow row in tabla.Rows)
                    {
                        RecetaAlumno recetalumn = new RecetaAlumno();
                        recetalumn.Receta = new Receta();
                        recetalumn.Alumno = new Alumno();

                        recetalumn.Receta.IdReceta = int.Parse(row[0].ToString());
                        recetalumn.Receta.Fecha = Convert.ToDateTime(row[1].ToString());
                        recetalumn.Receta.Diagnostico = row[2].ToString();
                        recetalumn.Receta.Tratamiento = row[3].ToString();
                        recetalumn.Alumno.Matricula = int.Parse(row[4].ToString());
                        recetalumn.Alumno.Nombre = row[5].ToString();
                        recetalumn.Alumno.ApellidoPaterno = row[6].ToString();
                        recetalumn.Alumno.ApellidoMaterno = row[7].ToString();
                        recetalumn.Alumno.FechaNacimiento = Convert.ToDateTime(row[8].ToString());

                        Objects.Add(recetalumn);
                    }
                }
                return Objects;
            }
        }

        public bool RecetaUpdate(Receta receta)
        {
            bool correct = false;
            try
            {
                using (SqlConnection conexion = new SqlConnection(DL.Conexion.GetConnectionString()))
                {
                    string comando = "UPDATE Receta SET Diagnostico=@Diagnostico, Tratamiento=@Tratamiento, Fecha=@Fecha WHERE IdReceta=@IdReceta";

                    SqlCommand query = new SqlCommand(comando, conexion);

                    SqlParameter[] param = new SqlParameter[4];
                    param[0] = new SqlParameter("@Diagnostico", SqlDbType.NVarChar);
                    param[0].Value = receta.Diagnostico;
                    param[1] = new SqlParameter("@Tratamiento", SqlDbType.NVarChar);
                    param[1].Value = receta.Tratamiento;
                    param[2] = new SqlParameter("@Fecha", SqlDbType.Date);
                    param[2].Value = receta.Fecha;
                    param[3] = new SqlParameter("@IdReceta", SqlDbType.Int);
                    param[3].Value = receta.IdReceta;

                    query.Parameters.AddRange(param);
                    query.Connection.Open();
                    int actualizaciones = query.ExecuteNonQuery();
                    if (actualizaciones > 0)
                    {
                        correct = true;
                    }
                    else
                    {
                        correct = false;
                    }
                }

            }
            catch (Exception ex)
            {
                correct = false;
            }
            return correct;
        }


        public ML.Receta RecetaGetById(int idReceta)
        {
            RecetaAlumno nueva = new RecetaAlumno();
            nueva.Receta = new Receta();
            nueva.Receta.IdReceta = idReceta;
            using (SqlConnection conexion = new SqlConnection(DL.Conexion.GetConnectionString()))
            {
                string comando = "RecetaGetById";
                SqlCommand query = new SqlCommand(comando, conexion);
                query.CommandType = CommandType.StoredProcedure;

                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@IdReceta", SqlDbType.Int);
                param[0].Value = nueva.Receta.IdReceta;

                query.Parameters.AddRange(param);

                SqlDataAdapter adapter = new SqlDataAdapter(query);
                DataTable tabla = new DataTable();
                adapter.Fill(tabla);
                if(tabla.Rows.Count > 0)
                {
                    DataRow fila = tabla.Rows[0];
                    ML.RecetaAlumno auxiliar = new RecetaAlumno();
                    auxiliar.Receta = new Receta();
                    auxiliar.Receta.IdReceta = int.Parse(fila[0].ToString());
                    auxiliar.Receta.Fecha = Convert.ToDateTime(fila[1].ToString());
                    auxiliar.Receta.Diagnostico = fila[2].ToString();
                    auxiliar.Receta.Tratamiento = fila[3].ToString();
                    auxiliar.Alumno = new Alumno();
                    auxiliar.Alumno.Matricula = int.Parse(fila[4].ToString());
                    auxiliar.Alumno.Nombre = fila[5].ToString();
                    auxiliar.Alumno.ApellidoPaterno = fila[6].ToString();
                    auxiliar.Alumno.ApellidoMaterno = fila[7].ToString();
                    auxiliar.Alumno.FechaNacimiento = Convert.ToDateTime(fila[8].ToString());

                    nueva.Receta = auxiliar.Receta;
                }
            }
            return nueva.Receta;
        }
    }
}
