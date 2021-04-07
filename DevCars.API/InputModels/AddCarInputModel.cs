using System;

namespace DevCars.API.InputModels
{
    public class AddCarInputModel
    {
        public string Marca { get; set; }
        public string Model { get; set; }
        public string VinCode { get; set; }
        public int Ano { get; set; }
        public decimal Preco { get; set; }
        public string ColorCar { get; set; }
        public DateTime AnoProducao { get; set; }
    }
}
