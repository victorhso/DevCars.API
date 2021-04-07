using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevCars.API.InputModels
{
    public class AddOrderInputModel
    {
        public int idCar { get; set; }
        public int idCustomer { get; set; }
        public List<ItensExtraInputModel> itensExtras { get; set; }
    }

    public class ItensExtraInputModel
    {
        public string  Descricao { get; set; }
        public decimal Preco { get; set; }
    }
}
