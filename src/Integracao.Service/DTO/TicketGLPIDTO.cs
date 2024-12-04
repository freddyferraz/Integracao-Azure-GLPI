using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegracaoGLPI_DEvOps.Service.DTO
{
    public class TicketGLPIDTO
    {
        public int id {  get; set; }
        public int status { get; set; }
        public int items_id { get; set; }
        public string itemtype { get; set; }
        public string content { get; set; }
    }
}
