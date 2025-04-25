namespace Integracao.Domain.ValueObjects.Azure;
public sealed record CardAzureRequest(string Operacao, string Area, string Title, string Description, string History, string Requerente, string Observador,
    string ResponsavelAtendimento, string TipoChamado, string CaterogiraChamado, string Prioridade, string Impacto, string Urgencia, string Localizacao,
    string Data, string Link,string Status);