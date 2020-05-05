using System.Collections.Generic;

namespace AriesWebApi.Entities.Interfaces
{
    public interface IValidator<T>
    {
        bool IsValid(T entity, ref IEnumerable<string> brokenRules);
    }
}