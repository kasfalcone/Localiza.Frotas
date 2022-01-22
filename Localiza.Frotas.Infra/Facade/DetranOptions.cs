using System;
using System.Collections.Generic;
using System.Text;

namespace Localiza.Frotas.Infra.Facade
{
    public class DetranOptions
    {
        public Guid Id { get; } = Guid.NewGuid();
        public string BaseUrl { get; set; }
        public string VistoriaUri { get; set; }
        public int QuantidadeDiasParaAgendamento{ get; set; }
    }
}
