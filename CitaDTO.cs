using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Final
{
    public class CitaDTO
    {
        public string Paciente { get; set; }
        public System.DateTime Fecha { get; set; }
        public string Lugar { get; set; }
        public string Observaciones { get; set; }
        public string Status { get; set; }

        public override string ToString()
        {
            String str = Fecha.ToString("dd/MMM/yyyy hh:mm");
            PMDatabaseEntities _entity = new PMDatabaseEntities();      //Esto puede relentizar mucho el programa.
            var pac = _entity.Pacientes.Where(x => x.Cedula == Paciente).Select(x => x).FirstOrDefault();
            return str + " - " + pac.Apellidos.ToUpper() + " " + pac.Nombres.ToUpper();
        }
    }
}
