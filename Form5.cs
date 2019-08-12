using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto_Final
{
    public partial class Form5 : Form
    {
        public void Display1(string search)
        {
            PMDatabaseEntities _entity = new PMDatabaseEntities();
            if (search == "")
            {
                var pacientesList = (from x in _entity.Pacientes
                                     select new PacienteDTO
                                     {
                                         Nombres = x.Nombres,
                                         Apellidos = x.Apellidos,
                                         Cedula = x.Cedula,
                                         SexoGenero = x.SexoGenero,
                                         FechaNac = x.FechaNac,
                                         Detalles = x.Detalles,
                                         Email = x.Email
                                     }).ToList();
                listBox1.DataSource = pacientesList;
            }
            else
            {
                var pacientesList = (from x in _entity.Pacientes
                                     where x.Apellidos.Contains(search) || x.Nombres.Contains(search)
                                     select new PacienteDTO
                                     {
                                         Nombres = x.Nombres,
                                         Apellidos = x.Apellidos,
                                         Cedula = x.Cedula,
                                         SexoGenero = x.SexoGenero,
                                         FechaNac = x.FechaNac,
                                         Detalles = x.Detalles,
                                         Email = x.Email
                                     }).ToList();
                listBox1.DataSource = pacientesList;
            }
        }

        public Form5()
        {
            InitializeComponent();
            Display1("");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Display1(textBox1.Text);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Cita newCita = new Cita();
                newCita.Paciente = ((PacienteDTO)listBox1.SelectedItem).Cedula;
                newCita.Fecha = dateTimePicker1.Value.Date + dateTimePicker2.Value.TimeOfDay;
                if (newCita.Fecha.CompareTo(System.DateTime.Now) <= 0)
                {
                    MessageBox.Show("La fecha y hora para la cita no puede ser pasada.");
                    return;
                }
                newCita.Lugar = textBox2.Text;
                newCita.Observaciones = richTextBox1.Text;
                newCita.Status = "AGENDADO";
                using (PMDatabaseEntities _entity = new PMDatabaseEntities())
                {
                    _entity.Citas.Add(newCita);
                    _entity.SaveChanges();
                    MessageBox.Show("Cita agendada correctamente!");
                    Close();
                }
            }
            catch(Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
                textBox2.Text = "SIME USFQ Cumbayá";
            else
                textBox2.Clear();
        }
    }
}
