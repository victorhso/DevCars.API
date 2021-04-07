using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevCars.API.Entities
{
    public class Pedido
    {
        protected Pedido() { }
        public Pedido(int idCar, int idCustomer, decimal preco, List<ItensPedidoExtra> itens)
        {
            IdCar = idCar;
            this.idCustomer = idCustomer;
            TotalCost = itens.Sum(x => x.Valor) + preco;
            ItensExtras = itens;
        }

        public int Id { get; private set; }
        public int IdCar { get; private set; }
        public Carro Car { get; set; }
        public int idCustomer { get; private set; }
        public Cliente Client { get; set; }
        public decimal TotalCost { get; private set; }
        public List<ItensPedidoExtra> ItensExtras { get; private set; }
    }

    public class ItensPedidoExtra
    {
        protected ItensPedidoExtra() { }
        public ItensPedidoExtra(string descricao, decimal valor)
        {
            Descricao = descricao;
            Valor = valor;
        }

        public int Id { get; set; }
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public int IdPedido { get; set; }
    }
}
