using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using AccesoDatos;

namespace InsertarCliente_EntityFrameWork
{
    public partial class Form1 : Form
    {
        // Constructor del Formulario
        public Form1()
        {
            InitializeComponent();
            BtnObtenerTodos.Click += BtnObtenerTodos_Click; // Evento para obtener todos los clientes
            label8.Text = "Agregar Cliente"; // Cambiar el texto del label8
        }

        // Evento del botón Obtener Todos
        private void BtnObtenerTodos_Click(object sender, EventArgs e)
        {
            var clientes = new CustomerRepository().ObtenerTodos();

            // Verificar si hay datos recuperados
            MessageBox.Show($"Clientes encontrados: {clientes.Count}");

            // Asignar los datos al DataGridView
            dgvCustomers.DataSource = clientes;
        }

        // Evento del botón Obtener por ID
        private void btnObtenerID_Click(object sender, EventArgs e)
        {
            try
            {
                CustomerRepository cr = new CustomerRepository();
                var cliente = cr.ObtenerPorID(txtCustomerID.Text);

                if (cliente != null)
                {
                    List<Customers> lista = new List<Customers> { cliente };
                    dgvCustomers.DataSource = lista;
                }
                else
                {
                    MessageBox.Show("Cliente no encontrado.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        // Evento del botón Insertar Cliente
        private void btnInsertar_Click(object sender, EventArgs e)
        {
            try
            {
                var cliente = crearCliente();
                CustomerRepository cr = new CustomerRepository();
                var resultado = cr.InsertarCliente(cliente);

                if (resultado > 0)
                {
                    MessageBox.Show($"Se insertó {resultado} registro(s).");

                    // Actualizar automáticamente el DataGridView
                    dgvCustomers.DataSource = cr.ObtenerTodos();
                }
                else
                {
                    MessageBox.Show("No se pudo insertar el cliente.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        // Método para crear un cliente a partir de los TextBox
        private Customers crearCliente()
        {
            return new Customers
            {
                CustomerID = txbCustomerID.Text,
                CompanyName = txbCompanyName.Text,
                ContactName = txbContactName.Text,
                Address = txbAddress.Text,
                ContactTitle = txbContactTitle.Text
            };
        }

        // Evento del botón Eliminar Cliente
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string id = txbCustomerID.Text;
                CustomerRepository cr = new CustomerRepository();
                var resultado = cr.EliminarCliente(id);

                if (resultado > 0)
                {
                    MessageBox.Show($"Cliente con ID {id} eliminado correctamente.");

                    // Actualizar automáticamente el DataGridView
                    dgvCustomers.DataSource = cr.ObtenerTodos();
                }
                else
                {
                    MessageBox.Show("Cliente no encontrado o no se pudo eliminar.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        // Evento Click del Label8 (opcional)
        private void label8_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Formulario para agregar un cliente.");
        }
    }
}
