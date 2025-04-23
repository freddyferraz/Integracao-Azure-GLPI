using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integracao.Infra.Entities
{
    public partial class Tickets

    {
        public long AcodTickets { get; set; }

        public long AcodTicketsglpi { get; set; }

        public long AcodTicketsDevops { get; set; }

        public long AcodStatus { get; set; }

        public required string AdatAlteracao { get; set; }
        
        public string? AcodUsuario { get; set; }

        
    }
}
