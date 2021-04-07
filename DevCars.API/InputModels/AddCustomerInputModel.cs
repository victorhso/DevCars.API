using System;

namespace DevCars.API.InputModels
{
    public class AddCustomerInputModel
    {
        public string Nome { get; set; }
        public string Documento{ get; set; }
        public DateTime Aniversario{ get; set; }
    }
}
