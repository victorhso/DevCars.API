using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevCars.API.ViewModels
{
    public class CarDetailsViewModel
    {
        public CarDetailsViewModel(int id, string marca, string model, string vinCode, int ano, decimal preco, string colorCar, DateTime anoProducao)
        {
            Id = id;
            Marca = marca;
            Model = model;
            VinCode = vinCode;
            Ano = ano;
            Preco = preco;
            ColorCar = colorCar;
            AnoProducao = anoProducao;
        }

        public int Id { get; set; }
        public string Marca { get; set; }
        public string Model { get; set; }
        public string VinCode { get; set; }
        public int Ano { get; set; }
        public decimal Preco { get; set; }
        public string ColorCar { get; set; }
        public DateTime AnoProducao { get; set; }
    }
}
