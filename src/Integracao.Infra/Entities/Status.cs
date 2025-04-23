using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integracao.Infra.Entities
{
    public partial class TDEPARAStatus
    {
        public int AcodStatusGlpi { get; set; }

        public string? AdesStatusGlpi { get; set; }

        public int AcodStatusDevops { get; set; }

        public string? AdesStatusDevops { get; set; }
        
        public long AcodStatus {  get; set; }
    }
}
