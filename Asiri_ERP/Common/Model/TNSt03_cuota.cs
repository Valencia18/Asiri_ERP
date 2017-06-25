namespace Common.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TNSt03_cuota
    {
        [Key]
        public long idCuota { get; set; }

        public decimal mtoDesembolso { get; set; }

        public decimal mtoPago { get; set; }

        [Column(TypeName = "date")]
        public DateTime? fecDesembolso { get; set; }

        [Column(TypeName = "date")]
        public DateTime fecPago { get; set; }

        public bool esCancelado { get; set; }

        public bool activo { get; set; }

        public DateTime fecRegistro { get; set; }

        public DateTime? fecModificacion { get; set; }

        public DateTime? fecEliminacion { get; set; }

        public short idMedioDePago { get; set; }

        public long idEstadoDeCuenta { get; set; }

        [Required]
        [StringLength(128)]
        public string idUsuario { get; set; }

        [StringLength(128)]
        public string idUsuarioModificar { get; set; }

        [StringLength(128)]
        public string idUsuarioEliminar { get; set; }

        public virtual MSTt01_medioDePago MSTt01_medioDePago { get; set; }

        public virtual TNSt05_estadoDeCuenta TNSt05_estadoDeCuenta { get; set; }
    }
}
