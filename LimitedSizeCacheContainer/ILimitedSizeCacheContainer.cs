using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LimitedSizeCacheContainer
{

    public interface ILimitedSizeCacheContainer<T>
    {
        void AddItem(string key, T item);
        T RetrieveItem(string key);
        bool IsItemInCache(string key);
    }

}