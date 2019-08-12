using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using EAGetMail;

namespace Proyecto_Final
{
    public partial class Form2 : Form
    {
        
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            this.MinimizeBox = false;
            this.MaximizeBox = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PMDatabaseEntities thing = new PMDatabaseEntities();
            if (textBox4.Text.All(char.IsDigit) && textBox4.Text.Length == 10)
            {
                Paciente newPacient = new Paciente();
                newPacient.Nombres = textBox1.Text;
                newPacient.Apellidos = textBox2.Text;
                newPacient.SexoGenero = comboBox2.Text;
                newPacient.Cedula = textBox4.Text;
                if (int.Parse(textBox5.Text) > 0 && int.Parse(textBox3.Text) > 0)
                {
                    try
                    {
                        newPacient.FechaNac = new DateTime(int.Parse(textBox3.Text), comboBox2.SelectedIndex +1, int.Parse(textBox5.Text));
                    }
                    catch (Exception exc)
                    {
                        MessageBox.Show(exc.Message);
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Ingrese una fecha válida.");
                    return;
                }
                newPacient.Email = textBox6.Text;
                newPacient.Detalles = richTextBox1.Text;
                try
                {
                    List<Paciente> pa = new List<Paciente>();
                    pa.Add(newPacient);
                    thing.Pacientes.AddRange(pa);
                    thing.SaveChanges();
                    MessageBox.Show("Paciente agregado Correctamente!");
                    thing.Dispose();
                }
                catch(Exception exc)
                {
                    MessageBox.Show(exc.Message);
                }
            }
            else
            {
                MessageBox.Show("La cedula debe contener 10 caracteres numéricos.");
            }
            Close();
        }
    }
}
