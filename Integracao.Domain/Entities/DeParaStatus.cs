namespace Integracao.Domain.Entities
{
    public partial class TDeParaStatus
    {
        public int AcodStatusGlpi { get; set; }

        public string? AdesStatusGlpi { get; set; }

        public int AcodStatusDevops { get; set; }

        public string? AdesStatusDevops { get; set; }
        
        public long AcodStatus {  get; set; }
    }
}
