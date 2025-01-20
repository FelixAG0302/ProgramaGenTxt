using System;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;

namespace ProgramaGenTxt
{
    public partial class Form1 : Form
    {
        private string fileName = "C:/Users/felix/Downloads/NominaApec.txt";

        public Form1()
        {
            InitializeComponent();
        }

        private void Generar_Click(object sender, EventArgs e)
        {

            try
            {
                DateTime fechaInicio = dateTimePicker1.Value.Date;
                DateTime fechaFin = dateTimePicker2.Value.Date;

                if (fechaInicio > fechaFin)
                {
                    MessageBox.Show("La fecha de inicio no puede ser mayor que la fecha final.");
                    return;
                }

                SqlConnection ocon = Conexion.GetConnection();
                if (ocon == null || ocon.State != System.Data.ConnectionState.Open)
                {
                    throw new Exception("No se pudo establecer la conexión con la base de datos.");
                }

                SqlDataReader reader;
                string sSQL = "SELECT TipoRegistro, CedulaEmpleado, Ocupacion, Salario, FechaIngreso, EstatusLaboral ";
                sSQL += "FROM Nomina ";
                sSQL += "WHERE FechaGeneracion BETWEEN @FechaInicio AND @FechaFin ";
                sSQL += "ORDER BY CedulaEmpleado";

                SqlCommand ocmd = new SqlCommand(sSQL, ocon);
                ocmd.Parameters.AddWithValue("@FechaInicio", fechaInicio);
                ocmd.Parameters.AddWithValue("@FechaFin", fechaFin);

                reader = ocmd.ExecuteReader();

                if (!reader.HasRows)
                {
                    MessageBox.Show("Archivo no generado, no existen datos entre las fecha proporcionada");
                    reader.Close();
                    ocmd.Dispose();
                    ocon.Close();
                    return;
                }

                using (StreamWriter writer = new StreamWriter(fileName, false))
                {
                    while (reader.Read())
                    {
                        string line = string.Join(",",
                            reader.IsDBNull(0) ? "" : reader.GetValue(0).ToString(),
                            reader.IsDBNull(1) ? "" : reader.GetValue(1).ToString(),
                            reader.IsDBNull(2) ? "" : reader.GetValue(2).ToString(),
                            reader.IsDBNull(3) ? "" : reader.GetValue(3).ToString(),
                            reader.IsDBNull(4) ? "" : reader.GetValue(4).ToString(),
                            reader.IsDBNull(5) ? "" : reader.GetValue(5).ToString()
                        );
                        writer.WriteLine(line);
                    }
                }

                reader.Close();
                ocmd.Dispose();
                ocon.Close();
                MessageBox.Show("Archivo generado correctamente");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al leer los datos de la nómina: {ex.Message}");
            }
        }

        private void writeFileLine(string v)
        {
            using (StreamWriter w = File.AppendText(fileName))
            {
                w.WriteLine(v);
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}

