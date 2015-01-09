using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace UI.Utils
{
    static class ListExtension
    {
        public static void Sync<T>(this ObservableCollection<T> list, List<T> other)
        {

            var intersection = list.Intersect(other);
            var toAdd = other.Except(intersection);
            var toRemove = list.Except(intersection);
            foreach (var remove in toRemove.ToList())
            {
                list.Remove(remove);
            }
            foreach (var add in toAdd.ToList())
            {
                list.Add(add);
            }
        }
    }
}
