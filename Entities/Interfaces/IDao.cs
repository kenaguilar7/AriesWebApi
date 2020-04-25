using System.Collections.Generic;
using System.Data;
using AriesWebApi.Entities.Companies;
using AriesWebApi.Entities.Users;

namespace AriesWebApi.Entities.Interfaces
{
    public interface IDao <T>
    {
        string Insert(T t,Usuario user);
        string Update(T t, Usuario user);
        string Delete(T t, Usuario user);
        List<T> GetAll(Compañia t,Usuario user);
        DataTable GetDataTable(Compañia t, Usuario user);

    }
}
