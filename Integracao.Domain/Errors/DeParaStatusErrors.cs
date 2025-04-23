namespace Integracao.Domain.Errors;
public static class DeParaStatusErrors
{
    public static readonly Error StatusAzureNaoEncontrado = new("DeParaStatusErrors.StatusAzureNaoEncontrado", "Status do Azure não encontrado na tabela de Conversão.");
    public static readonly Error StatusGLPINaoEncontrado = new("DeParaStatusErrors.StatusGLPINaoEncontrado", "Status do GLPI não encontrado na tabela de Conversão.");
    public static readonly Error StatusGLPINaoNulo = new("DeParaStatusErrors.StatusGLPINaoNulo", "Status do GLPI não pode ser nulo.");
    public static readonly Error StatusAzureNaoNulo = new("DeParaStatusErrors.StatusAzureNaoNulo", "Status do Azure não pode ser nulo.");
    public static readonly Error StatusGLPIInvalido = new("DeParaStatusErrors.StatusGLPIInvalido", "Status GLPI Inválido.");
    public static readonly Error StatusAzureInvalido = new("DeParaStatusErrors.StatusAzureInvalido", "Status Azure Inválido.");
}
