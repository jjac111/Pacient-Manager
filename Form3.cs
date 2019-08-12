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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
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
                        newPacient.FechaNac = new DateTime(int.Parse(textBox3.Text), comboBox2.SelectedIndex + 1, int.Parse(textBox5.Text));
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
                    Paciente _pac = thing.Pacientes.Where(x => x.Cedula == newPacient.Cedula).Select(x => x).FirstOrDefault();
                    _pac.Nombres = newPacient.Nombres;
                    _pac.Apellidos = newPacient.Apellidos;
                    _pac.SexoGenero = newPacient.SexoGenero;
                    _pac.Cedula = newPacient.Cedula;
                    _pac.FechaNac = newPacient.FechaNac;
                    _pac.Email = newPacient.Email;
                    _pac.Detalles = newPacient.Detalles;
                    thing.SaveChanges();
                    MessageBox.Show("Paciente actualizado Correctamente!");
                    thing.Dispose();
                }
                catch (Exception exc)
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

        private void button3_Click(object sender, EventArgs e)
        {
            PMDatabaseEntities thing = new PMDatabaseEntities();
            try
            {
                Paciente _pac = thing.Pacientes.Where(x => x.Cedula == textBox4.Text).Select(x => x).FirstOrDefault();
                thing.Pacientes.Remove(_pac);
                thing.SaveChanges();
                MessageBox.Show("Paciente eliminado Correctamente!");
                thing.Dispose();
                Close();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
