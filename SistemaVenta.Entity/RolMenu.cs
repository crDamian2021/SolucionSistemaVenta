using System;
using System.Collections.Generic;

namespace SistemaVenta.Entity
{
    public partial class RolMenu
    {
        public int IdRolMenu { get; set; }
        public bool? EsActivo { get; set; }
        public DateTime? FechaRegistro { get; set; }
        public int? IdMeun { get; set; }
        public int? IdRol { get; set; }

        public virtual Menu? IdMeunNavigation { get; set; }
        public virtual Rol? IdRolNavigation { get; set; }
    }
}
