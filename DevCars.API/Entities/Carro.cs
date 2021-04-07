using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevCars.API.Entities
{
    public class Carro
    {
        protected Carro() { }
        public Carro(string vinCode, string marca, string modelo, int ano, decimal preco, string cor, DateTime dataProducao)
        {
            VinCode = vinCode;
            Marca = marca;
            Modelo = modelo;
            Ano = ano;
            Preco = preco;
            Cor = cor;
            DataProducao = dataProducao;
            Status = StatusCarroEnum.Disponivel;
            Registrado = DateTime.Now;
        }

        public int Id { get; private set; }
        public string VinCode { get; private set; }
        public string Marca { get; private set; }
        public string Modelo { get; private set; }
        public int Ano { get; private set; }
        public decimal Preco { get; private set; }
        public string Cor { get; private set; }
        public DateTime DataProducao { get; private set; }
        public StatusCarroEnum Status { get; private set; }
        public DateTime Registrado { get; private set; }

        public void Atualizar(string cor, decimal preco)
        {
            this.Cor = cor;
            this.Preco = preco;
        }
        public void SetSuspender()
        {
            this.Status = StatusCarroEnum.Suspenso;
        }
    }
}
