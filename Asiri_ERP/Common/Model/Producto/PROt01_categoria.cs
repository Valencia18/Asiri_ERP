namespace Common.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class PROt01_categoria
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PROt01_categoria()
        {
            PROt02_producto = new HashSet<PROt02_producto>();
        }

        [Key]
        public int idCategoria { get; set; }

        public DateTime fecRegistro { get; set; }

        public DateTime? fecModificacion { get; set; }

        public DateTime? fecEliminacion { get; set; }

        [Required]
        [StringLength(128)]
        public string idUsuario { get; set; }

        [StringLength(128)]
        public string idUsuarioModificar { get; set; }

        [StringLength(128)]
        public string idUsuarioEliminar { get; set; }

        [Required]
        [StringLength(70)]
        public string nombreCategoria { get; set; }

        [StringLength(500)]
        public string descCategoria { get; set; }

        public bool activo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PROt02_producto> PROt02_producto { get; set; }
    }
}
