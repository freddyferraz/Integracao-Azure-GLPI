namespace Integracao.Domain.Errors;
public static class TicketErrors
{
    public static readonly Error IdAzureNaoEncontrado = new("Ticket.IdAzureNaoEncontrado", "O Id do Ticket do Azure não foi encontrado");
    public static readonly Error IdAzureNaoPodeNulo = new("Ticket.IdAzureNaoPodeNulo", "O Id do Ticket do Azure não pode ser nulo");
    public static readonly Error IdGLPINaoEncontrado = new("Ticket.IdAzureNaoEncontrado", "O Id do Ticket do GLPI não foi encontrado");
    public static readonly Error IdGLPINaoPodeNulo = new("Ticket.IdAzureNaoEncontrado", "O Id do Ticket do GLPI não pode ser nulo");
    public static readonly Error ErroAtualizarTicketGLPI = new("Ticket.ErroAtualizarTicketGLPI", "Erro ao atualizar o Ticket no GLPI");
    public static readonly Error ErroAtualizarCardAzure = new("Ticket.ErroAtualizarCardAzure", "Erro ao atualizar o Card no Azure");
}
