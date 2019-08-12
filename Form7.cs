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
    public partial class Form7 : Form
    {
        public Form7()
        {
            InitializeComponent();
        }

        private CitaDTO currentCita = new CitaDTO();

        private void Form7_Load(object sender, EventArgs e)
        {
            currentCita.Fecha = Form1.selectedCita.Fecha;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
                textBox2.Text = "SIME USFQ Cumbayá";
            else
                textBox2.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Cita updateCita = new Cita();
                updateCita.Paciente = label10.Text;
                updateCita.Fecha = dateTimePicker1.Value.Date + dateTimePicker2.Value.TimeOfDay;
                if (updateCita.Fecha.CompareTo(System.DateTime.Now) <= 0)
                {
                    MessageBox.Show("La fecha y hora para la cita no puede ser pasada.");
                    return;
                }
                updateCita.Lugar = textBox2.Text;
                updateCita.Observaciones = richTextBox1.Text;
                updateCita.Status = "AGENDADO";
                using (PMDatabaseEntities _entity = new PMDatabaseEntities())
                {
                    try
                    {
                        Cita cit = _entity.Citas.Where(x => x.Fecha == currentCita.Fecha).Select(x => x).FirstOrDefault();
                        _entity.Citas.Remove(cit);
                        Cita cita = new Cita();
                        cita.Fecha = updateCita.Fecha;
                        cita.Paciente = updateCita.Paciente;
                        cita.Lugar = updateCita.Lugar;
                        cita.Observaciones = updateCita.Observaciones;
                        cita.Status = updateCita.Status;
                        _entity.Citas.Add(cita);
                        _entity.SaveChanges();
                        MessageBox.Show("Cita actualizada correctamente!");
                        _entity.Dispose();
                    }
                    catch (Exception exc)
                    {
                        MessageBox.Show(exc.Message);
                    }
                    Close();
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (PMDatabaseEntities _entity = new PMDatabaseEntities())
            {
                try
                {
                    Cita cit = _entity.Citas.Where(x => x.Fecha == Form1.selectedCita.Fecha).Select(x => x).FirstOrDefault();
                    _entity.Citas.Remove(cit);
                    _entity.SaveChanges();
                    MessageBox.Show("Cita cancelada correctamente!");
                    _entity.Dispose();
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message);
                }
                Close();
            }
        }

        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
                textBox2.Text = "SIME USFQ Cumbayá";
            else
                textBox2.Clear();
        }
    }
}
