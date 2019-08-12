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
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Cita newCita = new Cita();
                newCita.Paciente = label10.Text;
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
            catch (Exception exc)
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
