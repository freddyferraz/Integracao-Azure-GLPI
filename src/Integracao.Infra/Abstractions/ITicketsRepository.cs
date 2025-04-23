using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Integracao.Infra.Entities;

namespace Integracao.Infra.Abstractions
{
    public interface ITicketsRepository
    {
        ValueTask<Tickets> RetornaTicketsById(long id);
        ValueTask InserirTicket(Tickets ticket);
        ValueTask AtualizarTicket(Tickets tickets);
    }
}
