using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;


namespace MicroXmlSerializer
{
    class EntityCollections
    {
        private readonly HashSet<Type> _collectionTypes = new HashSet<Type>
        {
            typeof(IEnumerable<>),
            typeof(ICollection<>),
            typeof(Collection<>),
            typeof(IList<>),
            typeof(List<>)
        };

        public bool IsCollection(Type type)
        {
            return type.IsArray ||
                (type.IsGenericType && _collectionTypes.Contains(type.GetGenericTypeDefinition()));
        }

        public Type GetItemType(Type collectionType)
        {
            if (IsCollection(collectionType))
            {
                if (collectionType.IsArray)
                    return collectionType.GetElementType();
                else
                    return collectionType.GetGenericArguments()[0];
            }
            else
                throw new ArgumentException($"collectionType is not collection. collectionType:{collectionType}");
        }

        public object CreateCollection(Type collectionType, ICollection items)
        {
            if (IsCollection(collectionType))
            {
                if (collectionType.IsArray)
                {
                    var itemType = collectionType.GetElementType();
                    var array = Array.CreateInstance(itemType, items.Count);
                    items.CopyTo(array, 0);
                    return array;
                }
                else
                {
                    var itemType = collectionType.GetGenericArguments()[0];
                    var list = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(new[] { itemType }));
                    foreach (var item in items) list.Add(item);
                    return list;
                }
            }
            else
                throw new ArgumentException($"collectionType is not collection. collectionType:{collectionType}");
        }
    }
}
