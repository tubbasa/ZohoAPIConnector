using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Connector.Core.Infastructure
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> getRecords(string  obj);
        T getRecordById(T obj,string obj2);
        List<T> insertRecords(List<T> obj);
        List<T> updateRecords(T obj);
        void deleteRecords(string id);
        
    }

}
