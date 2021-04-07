using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevCars.API.ViewModels
{
    public class CarItemViewModel
    {
        public CarItemViewModel(int id, string marca, string modelo, decimal preco)
        {
            Id = id;
            Marca = marca;
            Modelo = modelo;
            Preco = preco;
        }

        public int Id { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public decimal Preco { get; set; }
    }
}
