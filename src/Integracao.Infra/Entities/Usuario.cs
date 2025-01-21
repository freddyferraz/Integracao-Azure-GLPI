using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integracao.Infra.Entities;
public partial class Usuario
{
    public long AcodUsuario {  get; set; }
    public string AdesEmail { get; set; }
    public string AdesUsuario { get; set; }
}
