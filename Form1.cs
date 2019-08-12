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
using System.Net.Mail;



namespace Proyecto_Final
{
    public partial class Form1 : Form
    {
        public static CitaDTO selectedCita;
        public static PacienteDTO selectedPaciente;
        private PMDatabaseEntities thing = new PMDatabaseEntities();

        public void Display1(string search)  
        {
            PMDatabaseEntities _entity = new PMDatabaseEntities();
            if(search == "")
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

        public void Display2()
        {
            PMDatabaseEntities _entity = new PMDatabaseEntities();
            var cistasPorVenirList = (from x in _entity.Citas
                                      where x.Fecha.CompareTo(System.DateTime.Now) > 0
                                      select new CitaDTO
                                      {
                                          Paciente = x.Paciente,
                                          Fecha = x.Fecha,
                                          Lugar = x.Lugar,
                                          Observaciones = x.Observaciones,
                                          Status = x.Status
                                      }).ToList();
            listBox2.DataSource = cistasPorVenirList;
        }

        public void Display3(int idx)
        {
            PMDatabaseEntities _entity = new PMDatabaseEntities();
            if(idx == 0)
            {
                var cistasPasadasList = (from x in _entity.Citas
                                         where x.Fecha.CompareTo(System.DateTime.Now) <= 0
                                         select new CitaDTO
                                         {
                                             Paciente = x.Paciente,
                                             Fecha = x.Fecha,
                                             Lugar = x.Lugar,
                                             Observaciones = x.Observaciones,
                                             Status = x.Status
                                         }).ToList();
                listBox3.DataSource = cistasPasadasList;
            }
            else if (idx == 1)
            {
                var cistasPasadasList = (from x in _entity.Citas
                                         where x.Fecha.CompareTo(System.DateTime.Now) <= 0 && x.Fecha.Year == System.DateTime.Now.Year
                                         select new CitaDTO
                                         {
                                             Paciente = x.Paciente,
                                             Fecha = x.Fecha,
                                             Lugar = x.Lugar,
                                             Observaciones = x.Observaciones,
                                             Status = x.Status
                                         }).ToList();
                listBox3.DataSource = cistasPasadasList;
            }
            else if (idx == 2)
            {
                var cistasPasadasList = (from x in _entity.Citas
                                         where x.Fecha.CompareTo(System.DateTime.Now) <= 0 && x.Fecha.Month == System.DateTime.Now.Month
                                         select new CitaDTO
                                         {
                                             Paciente = x.Paciente,
                                             Fecha = x.Fecha,
                                             Lugar = x.Lugar,
                                             Observaciones = x.Observaciones,
                                             Status = x.Status
                                         }).ToList();
                listBox3.DataSource = cistasPasadasList;
            }
            
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            Display1("");
            Display2();
            Display3(0);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form8 detallesCita = new Form8();
            CitaDTO cit = (CitaDTO)listBox3.SelectedItem;
            using (PMDatabaseEntities _entity = new PMDatabaseEntities())
            {
                var pac = _entity.Pacientes.Where(x => x.Cedula == cit.Paciente).Select(x => x).FirstOrDefault();
                String name = pac.Nombres + " " + pac.Apellidos;
                detallesCita.label5.Text = name.ToUpper();
                detallesCita.label6.Text = pac.SexoGenero;

                DateTime zeroTime = new DateTime(1, 1, 1);

                TimeSpan años = System.DateTime.Now - pac.FechaNac.Value;
                int _años = (zeroTime + años).Year - 1;
                detallesCita.label8.Text = _años.ToString();
                detallesCita.label10.Text = pac.Cedula;
                detallesCita.label12.Text = pac.Email;
            }
            detallesCita.textBox2.Text = cit.Lugar;
            detallesCita.dateTimePicker1.Value = cit.Fecha.Date;
            detallesCita.dateTimePicker2.Value = cit.Fecha;
            detallesCita.richTextBox1.Text = cit.Observaciones;
            detallesCita.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form2 addPaciente = new Form2();
            addPaciente.Show();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Display1(textBox1.Text);
        }

        private void Form1_Enter(object sender, EventArgs e)
        {
            Display1(textBox1.Text);
            Display2();
            Display3(0);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form3 updatePaciente = new Form3();
            PacienteDTO pac = (PacienteDTO)listBox1.SelectedItem;
            updatePaciente.textBox1.Text = pac.Nombres;
            updatePaciente.textBox2.Text = pac.Apellidos;
            updatePaciente.comboBox2.Text = pac.SexoGenero;
            updatePaciente.textBox4.Text = pac.Cedula;
            updatePaciente.textBox5.Text = pac.FechaNac.Value.Day.ToString();
            updatePaciente.textBox3.Text = pac.FechaNac.Value.Year.ToString();
            updatePaciente.comboBox1.SelectedIndex = pac.FechaNac.Value.Month-1;
            updatePaciente.textBox6.Text = pac.Email;
            updatePaciente.richTextBox1.Text = pac.Detalles;
            updatePaciente.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Form5 agendarCita = new Form5();
            agendarCita.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form6 agendarCitaPara = new Form6();
            PacienteDTO pac = (PacienteDTO)listBox1.SelectedItem;
            String name = pac.Nombres + " " + pac.Apellidos;
            agendarCitaPara.label5.Text = name.ToUpper();
            agendarCitaPara.label6.Text = pac.SexoGenero;

            DateTime zeroTime = new DateTime(1, 1, 1);

            TimeSpan años = System.DateTime.Now - pac.FechaNac.Value;
            int _años = (zeroTime + años).Year - 1;
            agendarCitaPara.label8.Text = _años.ToString();
            agendarCitaPara.label10.Text = pac.Cedula;
            agendarCitaPara.label12.Text = pac.Email;
            agendarCitaPara.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form7 editarCita = new Form7();
            CitaDTO cit = (CitaDTO)listBox2.SelectedItem;
            using (PMDatabaseEntities _entity = new PMDatabaseEntities())
            {
                var pac = _entity.Pacientes.Where(x => x.Cedula == cit.Paciente).Select(x => x).FirstOrDefault();
                String name = pac.Nombres + " " + pac.Apellidos;
                editarCita.label5.Text = name.ToUpper();
                editarCita.label6.Text = pac.SexoGenero;

                DateTime zeroTime = new DateTime(1, 1, 1);

                TimeSpan años = System.DateTime.Now - pac.FechaNac.Value;
                int _años = (zeroTime + años).Year - 1;
                editarCita.label8.Text = _años.ToString();
                editarCita.label10.Text = pac.Cedula;
                editarCita.label12.Text = pac.Email;
            }
            editarCita.dateTimePicker1.Value = cit.Fecha;
            editarCita.dateTimePicker2.Value = cit.Fecha;
            editarCita.textBox2.Text = cit.Lugar;
            editarCita.richTextBox1.Text = cit.Observaciones;
            
            editarCita.Show();
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedCita = (CitaDTO)listBox2.SelectedItem;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedPaciente = (PacienteDTO)listBox1.SelectedItem;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form9 verCitas = new Form9();
            verCitas.Show();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Display3(comboBox1.SelectedIndex);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            PMDatabaseEntities _entity = new PMDatabaseEntities();
            CitaDTO cit = (CitaDTO)listBox2.SelectedItem;
            Paciente pac = _entity.Pacientes.Where(x => x.Cedula == cit.Paciente).Select(x => x).FirstOrDefault();
            try
            {
                MailAddress email = new MailAddress(pac.Email);
                MailMessage mail = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                mail.From = new MailAddress("juanjavierarosemena@gmail.com");
                mail.To.Add(pac.Email);
                mail.Subject = "Cita Psicología";
                string body = "Saludos " + pac.Nombres + ",\n" + "Estás recibiendo este correo para confirmar tu cita de Psicología.\n\n";
                body += "Fecha:\t" + cit.Fecha.DayOfWeek + ", " + cit.Fecha.Date.ToString("dd/MMM/yyyy");
                body += "\nHora:\t" + cit.Fecha.TimeOfDay.ToString();
                body += "\nLugar:\t" + cit.Lugar;
                body += "\n\nEspero verte en la cita y que tengas un buen día,\n\nJuan Javier Arosemena\nTotalmente no un psicólogo.";
                mail.Body = body;
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Credentials = new NetworkCredential("juanjavierarosemena@gmail.com", "Ar053m3n@");
                smtp.Send(mail);
                MessageBox.Show("E-mail enviado correctamente !!", "Message Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch(Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
            
        }
    }
}
