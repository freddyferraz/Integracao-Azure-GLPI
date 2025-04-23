namespace Integracao.Domain.Entities
{
    public partial class Ticket

    {
        public long AcodTickets { get; set; }

        public long AcodTicketsglpi { get; set; }

        public long AcodTicketsDevops { get; set; }

        public long AcodStatus { get; set; }

        public required string AdatAlteracao { get; set; }
        
        public string? AcodUsuario { get; set; }

        
    }
}
