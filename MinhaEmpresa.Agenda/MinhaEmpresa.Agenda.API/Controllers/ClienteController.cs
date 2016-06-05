using MinhaEmpresa.Agenda.Dominio;
using MinhaEmpresa.Agenda.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MinhaEmpresa.Agenda.API.Controllers
{
    public class ClienteController : ApiController
    {
        // Função que Busca um cliente.
        public IHttpActionResult GetCliente(int codigo)
        {
            // Busca o primeiro Cliente com o código informado.
            var cliente = BancoDados.clientes.FirstOrDefault((p) => p.Codigo == codigo);

            // Verifica se retornou o cliente.
            if (cliente == null)
            {
                // Retorna que não foi encontrado o cliente com o código informado.
                return NotFound();
            }

            // Retorna o cliente com o código informado.
            return Ok(cliente);
        }

        // Função que Inclui um cliente.
        public IHttpActionResult PostCliente([FromBody] Cliente cliente)
        {
            // Verifica se o código do cliente informado é 0.
            if (cliente.Codigo == 0)
            {
                // Armazena na variável o ultimo código utilizado. 
                var ultimoIndice = BancoDados.clientes.OrderBy(x => x.Codigo).Last().Codigo;

                // Atribui ao cliente o proximo código disponível.
                cliente.Codigo = ultimoIndice + 1;
            }

            // Adiciono o cliente na "Memória" ou Lista.
            BancoDados.clientes.Add(cliente);

            // Retorna que o cliente foi adicionado com sucesso.
            return Ok(BancoDados.clientes);
        }

        public IHttpActionResult PutCliente([FromBody] Cliente cliente)
        {
            if (!Update(cliente))
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return Ok("Cliente atualizado com sucesso");
        }

        private bool Update(Cliente cliente)
        {
            if (cliente == null)
            {
                throw new ArgumentNullException("cliente");
            }
            // busca o indice 
            var index = BancoDados.clientes.FindIndex(x => x.Codigo == cliente.Codigo);
            if (index == -1)
            {
                return false;
            }
            BancoDados.clientes.RemoveAt(index);
            BancoDados.clientes.Add(cliente);
            return true;
        }

        public IHttpActionResult DeleteCliente(int codigo)
        {
            if (!Delete(codigo))
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return Ok("Cliente deletado com sucesso");
        }

        private bool Delete(int codigo)
        {
            // busca o indice 
            var index = BancoDados.clientes.FindIndex(x => x.Codigo == codigo);
            if (index == -1)
            {
                return false;
            }
            BancoDados.clientes.RemoveAt(index);
            return true;
        }
    }
}
