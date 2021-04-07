using Dapper;
using DevCars.API.Entities;
using DevCars.API.InputModels;
using DevCars.API.Persistence;
using DevCars.API.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace DevCars.API.Controllers
{
    [Route("api/cars")]
    public class CarsController : ControllerBase
    {
        private readonly DevCarsDbContext _dbContext;
        private readonly string _connectionstring;

        public CarsController(DevCarsDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;

            //_connectionstring = _dbContext.Database.GetDbConnection().ConnectionString;
            _connectionstring = configuration.GetConnectionString("DevCarsCs");
        }

        //api/cars
        /// <summary>
        /// Pesquisar todos os carros
        /// </summary>
        /// <returns>Objeto retornado com sucesso</returns>
        /// <response code="200">Objeto retornado om sucesso.</response>
        /// <response code="404">Objeto não encontrado</response>
        /// /// <response code="500">Ocorreu um erro interno</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet]
        public IActionResult Get()
        {
            /*var carros = _dbContext.Carros;
            var carsViewModel = carros
                .Where(x => x.Status == StatusCarroEnum.Disponivel)
                .Select(c => new CarItemViewModel(c.Id, c.Marca, c.Modelo, c.Preco))
                .ToList();*/

            using (var sqlConnection = new SqlConnection(_connectionstring))
            {
                var query = "SELECT id, marca, modelo, preco FROM CARROS WHERE Status = 0";
                var carsViewModel = sqlConnection.Query<CarItemViewModel>(query);

                return Ok(carsViewModel);
            }
        }

        //api/cars/1...
        /// <summary>
        /// Pesquisar um carro por seu identificador
        /// </summary>
        /// <remarks>
        /// Requisição de exemplo:
        /// { 
        ///     Id = 1 
        /// }
        /// </remarks>
        /// <param name="id">Dados de um carro</param>
        /// <returns>Objeto retornado com sucesso</returns>
        /// <response code="200">Objeto retornado com sucesso</response>
        /// <response code="404">Objeto não encontrado</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public IActionResult GetbyId(int id)
        {
            //Se carro não existir, retorna NOT FOUND, se não retorna OK.
            var carro = _dbContext.Carros.SingleOrDefault(c => c.Id == id);

            if (carro == null)
                return NotFound();

            var carDetailViewModel = new CarDetailsViewModel(carro.Id, carro.Marca, carro.Modelo, carro.VinCode, carro.Ano, carro.Preco, carro.Cor, carro.DataProducao);

            return Ok(carDetailViewModel);
        }

        //POST /api/cars
        /// <summary>
        /// Cadastrar um carro
        /// </summary>
        /// <remarks>
        /// Requisição de exemplo:
        /// {
        ///     "Marca": "Chevrolet",
        ///     "Model": "Onix",
        ///     "vinCode": "ABC123",
        ///     "Ano": 2021,
        ///     "Preco": 10000,
        ///     "colorCar": "Branco",
        ///     "DataDe Producao": "2021-04-01"
        /// }
        /// </remarks>
        /// <param name="model">Dados de um novo carro</param>
        /// <returns>Objeto recém criado</returns>
        /// <response code="201">Objeto criado com sucesso</response>
        /// <response code="400">Objeto inválido</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Post([FromBody] AddCarInputModel model)
        {
            if (model.Model.Length > 50)
                return BadRequest("Modelo não pode ter mais do que 50 caracteres");

            var carro = new Carro(model.VinCode, model.Marca, model.Model, model.Ano, model.Preco, model.ColorCar, model.AnoProducao);

            _dbContext.Carros.Add(carro);
            _dbContext.SaveChanges();

            return CreatedAtAction(
                nameof(GetbyId),
                new { id = carro.Id },
                model
                );
        }

        /// <summary>
        /// Atualizar dados de um carro
        /// </summary>
        /// <remarks>
        /// Requisição de Exemplo:
        /// {
        ///     "Color": "Verde",
        ///     "Preco": 15000
        /// }
        /// </remarks>
        /// <param name="id">Identificador de um carro</param>
        /// <param name="model">Dados de alteração</param>
        /// <returns>Não tem retorno</returns>
        /// <response code="204">Atualização bem-sucedida</response>
        /// <response code="400">Dados inválidos</response>
        /// <response code="404">Carro não encontrado</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Put(int id, [FromBody] UpdateCarInputModel model)
        {
            var carro = _dbContext.Carros.SingleOrDefault(c => c.Id == id);

            if (carro == null)
                return NotFound();

            carro.Atualizar(model.Color, model.Preco);
            //_dbContext.SaveChanges();
            using (var sqlConnection = new SqlConnection(_connectionstring))
            {
                var query = "UPDATE CARROS SET Cor = @color, Preco = @preco WHERE id = @id";
                sqlConnection.Execute(query, new { color = carro.Cor, preco = carro.Preco, id = carro.Id });
            }

            return NoContent();
        }

        /// <summary>
        /// Deletar um carro
        /// </summary>
        /// <remarks>
        /// Requisição de Exemplo:
        /// {
        ///     "Id": 1
        /// }
        /// </remarks>
        /// <param name="id">Identificador de um carro</param>
        /// <returns></returns>
        /// <response code="200">Objeto excluído com sucesso</response>
        /// <response code="404">Objeto não encontrado</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id)
        {
            var carro = _dbContext.Carros.SingleOrDefault(c => c.Id == id);

            if (carro == null)
                return NotFound();

            carro.SetSuspender();
            _dbContext.SaveChanges();

            return NoContent();
        }
    }
}
