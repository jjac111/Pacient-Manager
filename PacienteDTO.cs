using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Final
{
    public class PacienteDTO
    {
        public string Cedula { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string SexoGenero { get; set; }
        public Nullable<System.DateTime> FechaNac { get; set; }
        public string Email { get; set; }
        public string Detalles { get; set; }

        public override string ToString()
        {
            return (Apellidos + " " + Nombres).ToUpper();
        }
    }
}
