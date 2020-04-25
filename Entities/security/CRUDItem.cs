namespace AriesWebApi.Entities.security
{
    public class CRUDItem : IPermiso
    {
        public CRUDName Nombre { get; set; }
        public bool TienePermiso { get; set; }

        public CRUDItem(CRUDName nombre, bool tienePermiso)
        {
            Nombre = nombre;
            TienePermiso = tienePermiso;
        }

        public CRUDItem()
        {
        }
    }
}
