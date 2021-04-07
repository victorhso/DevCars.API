using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevCars.API.ViewModels
{
    public class OrderDetailsViewModel
    {
        public OrderDetailsViewModel(int idcar, int idCustomer, decimal totalCost, List<string> itensExtras)
        {
            Idcar = idcar;
            IdCustomer = idCustomer;
            TotalCost = totalCost;
            ItensExtras = itensExtras;
        }

        public int Idcar { get; set; }
        public int IdCustomer { get; set; }
        public decimal TotalCost { get; set; }
        public List<string> ItensExtras { get; set; }
    }
}
