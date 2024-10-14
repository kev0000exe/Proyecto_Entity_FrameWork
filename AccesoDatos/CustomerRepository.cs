using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos
{
    public class CustomerRepository
    {
        public NorthwindEntities contexto = new NorthwindEntities();

        public List<Customers> ObtenerTodos()
        {
            var cliente = from custM in contexto.Customers
                          select custM;

            return cliente.ToList();
        }

        public Customers ObtenerPorID(string id)
        {
            var clientes = from cm in contexto.Customers
                           where cm.CustomerID == id
                           select cm;

            return clientes.FirstOrDefault();
        }


        public int InsertarCliente(Customers cliente)
        {
            // Agregar el cliente a la entidad Customers
            contexto.Customers.Add(cliente);

            // Guardar los cambios en la base de datos y retornar el número de cambios
            return contexto.SaveChanges();
        }

        public int EliminarCliente(string id)
        {
            // Buscar el cliente por ID
            var cliente = contexto.Customers.FirstOrDefault(c => c.CustomerID == id);

            if (cliente != null)
            {
                // Eliminar el cliente del contexto
                contexto.Customers.Remove(cliente);

                // Guardar los cambios en la base de datos
                return contexto.SaveChanges();
            }

            // Si no se encontró, retornar 0 cambios
            return 0;
        }

    }
}
