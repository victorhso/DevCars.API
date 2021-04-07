using DevCars.API.Entities;
using DevCars.API.InputModels;
using DevCars.API.Persistence;
using DevCars.API.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DevCars.API.Controllers
{
    [Route("api/customers")]
    public class CustomersController : ControllerBase
    {
        private readonly DevCarsDbContext _dbContext;
        public CustomersController(DevCarsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //POST api/customers
        /// <summary>
        /// Cadastrar um cliente
        /// </summary>
        /// <remarks>
        /// Requisição de exemplo:
        /// {
        ///     "Nome": "João Silva",
        ///     "Documento": "12345678901",
        ///     "Aniversario": "01-01-2000",
        /// }
        /// </remarks>
        /// <param name="model">Dados de um novo cliente</param>
        /// <returns>Objeto recém criado</returns>
        /// <response code="201">Objeto criado com sucesso</response>
        /// <response code="400">Objeto inválido</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Post([FromBody] AddCustomerInputModel model)
        {
            var cliente = new Cliente(model.Nome, model.Documento, model.Aniversario);

            _dbContext.Clientes.Add(cliente);
            _dbContext.SaveChanges();

            return NoContent();
        }

        //POST api/customers/1/orders
        /// <summary>
        /// Cadastrar um pedido para um cliente
        /// </summary>
        /// <remarks>
        /// Requisição de exemplo:
        /// {
        ///     "IdCar": 1,
        ///     "IdCustomer": 1,
        ///     "Descricao": "Teto solar",
        ///     "Preco": 5000,
        /// }
        /// </remarks>
        /// <param name="model">Dados de um novo cliente</param>
        /// <returns>Objeto recém criado</returns>
        /// <response code="201">Objeto criado com sucesso</response>
        /// <response code="400">Objeto inválido</response>
        [HttpPost("{id}/orders")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult PostOrder(int id, [FromBody] AddOrderInputModel model)
        {
            var itensExtras = model.itensExtras.Select(e => new ItensPedidoExtra(e.Descricao, e.Preco)).ToList();
            var carro = _dbContext.Carros.SingleOrDefault(c => c.Id == model.idCar);
            var pedido = new Pedido(model.idCar, model.idCustomer, carro.Preco, itensExtras);

            _dbContext.Pedido.Add(pedido);
            _dbContext.SaveChanges();

            return CreatedAtAction(
                nameof(GetOrders),
                new {id = pedido.idCustomer, orderId = pedido.Id },
                model
                );
        }

        //GET /api/customers/1/orders/2
        /// <summary>
        /// Buscar pedido de um cliente com id carro
        /// </summary>
        /// <remarks>
        /// Requisição de exemplo:
        /// {
        ///     Id: 1
        ///     orderId: 1
        /// }
        /// </remarks>
        /// <param name="model">Dados de um cliente e seu pedido</param>
        /// <returns>Objeto recém criado</returns>
        /// <response code="200">Objeto retornado com sucesso</response>
        /// <response code="404">Objeto não encontrado</response>
        [HttpGet("{id}/orders/{orderId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetOrders(int id, int orderId)
        {    
            var pedido = _dbContext.Pedido
                .Include(x => x.ItensExtras)
                .SingleOrDefault(y => y.Id == orderId);

            if (pedido == null)
                return NotFound();

            var itensExtras = pedido.ItensExtras
                .Select(z => z.Descricao).ToList();

            var pedidoView = new OrderDetailsViewModel(pedido.IdCar, pedido.idCustomer, pedido.TotalCost, itensExtras);

            return Ok(pedidoView);
        }
    }
}
