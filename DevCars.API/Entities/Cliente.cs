using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevCars.API.Entities
{
    public class Cliente
    {
        protected Cliente() { }
        public Cliente(string nome, string documento, DateTime dataNascimento)
        {
            Nome = nome;
            Documento = documento;
            DataNascimento = dataNascimento;
            Pedido = new List<Pedido>();
        }

        public int Id { get; private set; }
        public string Nome { get; private set; }
        public string Documento { get; private set; }
        public DateTime DataNascimento { get; private set; }
        public List<Pedido> Pedido { get; private set; }

        public void AdicionaPedido(Pedido pedido)
        {
            Pedido.Add(pedido);
        }
    }
}
