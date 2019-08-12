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
    public partial class Form9 : Form
    {
        public void Display()
        {
            PMDatabaseEntities _entity = new PMDatabaseEntities();
            var cits = _entity.Citas.Where(x => x.Paciente == Form1.selectedPaciente.Cedula).Select(x => x).ToList();
            listBox1.DataSource = cits;
        }

        public Form9()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Form9_Load(object sender, EventArgs e)
        {
            Display();
            Text += " " + Form1.selectedPaciente.Apellidos + " " + Form1.selectedPaciente.Nombres;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Cita cit = (Cita)listBox1.SelectedItem;
            dateTimePicker1.Value = cit.Fecha;
            dateTimePicker2.Value = cit.Fecha;
            textBox2.Text = cit.Lugar;
            textBox5.Text = cit.Status;
            richTextBox1.Text = cit.Observaciones;
        }
    }
}
