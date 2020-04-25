using AriesWebApi.Entities.Enums;
using AriesWebApi.Entities.security;

namespace AriesWebApi.Entities.Windows
{
    public class Ventana : IPermiso
    {
        public VentanaInfo VentanaInfo { get; set; }
        public int Code { get; set; }
        public string NombreInterno { get; set; }
        public string NombreExterno { get; set; }
        public string Comentarios { get; set; }
        public bool Activa { get; set; }
        public bool TienePermiso { get; set; }
        public CRUDItem CRUDInsert { get; set; }
        public CRUDItem CRUDUpdate { get; set; }
        public CRUDItem CRUDDeleted { get; set; }
        public CRUDItem CRUDLIst { get; set; }
        public Ventana(int code, string nombreInterno,
                       string nombreExterno, string comentarios,
                       bool activa, bool tienePermiso)
        {
            Code = code;
            NombreInterno = nombreInterno;
            NombreExterno = nombreExterno;
            Comentarios = comentarios;
            Activa = activa;
            TienePermiso = tienePermiso;
        }

        public Ventana(int code, string nombreInterno, string nombreExterno, 
            string comentarios, bool activa, bool tienePermiso, 
            CRUDItem cRUDInsert, CRUDItem cRUDUpdate, CRUDItem cRUDDeleted, CRUDItem cRUDLIst, VentanaInfo ventanaInfo) 
            : this(code, nombreInterno, nombreExterno, comentarios, activa, tienePermiso)
        {
            CRUDInsert = cRUDInsert;
            CRUDUpdate = cRUDUpdate;
            CRUDDeleted = cRUDDeleted;
            CRUDLIst = cRUDLIst;
            VentanaInfo = ventanaInfo;
        }

        public Ventana()
        {
        }
        public CRUDItem FindCRUD(CRUDName cRUDItem)
        {

            switch (cRUDItem)
            {
                case CRUDName.Insertar: return CRUDInsert;

                case CRUDName.Actualizar: return CRUDUpdate;

                case CRUDName.Eliminar: return CRUDDeleted;

                case CRUDName.Listar: return CRUDLIst;

                default: return null;

            }
        }  
    }
}