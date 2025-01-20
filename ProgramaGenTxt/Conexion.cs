using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramaGenTxt
{
    class Conexion
    {
        public static SqlConnection GetConnection()
        {
            SqlConnection ocon = new SqlConnection("Data Source=MSI;Initial Catalog=NominaTesoreria;Integrated Security=True;");
            ocon.Open();
            return ocon;

        }
    }
}
