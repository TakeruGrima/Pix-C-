using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pix.MyCollectionTypes
{
    class List2D<T> : ICollection
    {
        #region Field region

        List<List<T>> arr = new List<List<T>>();

        public int Count => arr.Count;

        public object SyncRoot => throw new NotImplementedException();

        public bool IsSynchronized => throw new NotImplementedException();

        #endregion

        #region indexer

        // This is the indexer for List2D 
        public T this[int index1, int index2]
        {
            // This is the get accessor. 
            get
            {
                return arr[index1][index2];
            }
            set
            {
                arr[index1][index2] = value;
            }
        }

        // This is the indexer for List2D 
        public List<T> this[int index1]
        {
            // This is the get accessor. 
            get
            {
                return arr[index1];
            }
            set
            {
                arr[index1] = value;
            }
        }

        #endregion

        #region Constructor

        public List2D()
        {
            arr = new List<List<T>>();
        }

        #endregion

        #region Icollection methods

        public void CopyTo(Array array, int index)
        {
            throw new NotImplementedException();
        }

        public IEnumerator GetEnumerator()
        {
            return arr.GetEnumerator();
        }

        #endregion

        #region methods

        public void Add(List<T> list)
        {
            arr.Add(list);
        }

        public void Remove(List<T> list)
        {
            arr.Remove(list);
        }

        //get Id of a list in tilemap
        public int IndexOf(List<T> list)
        {
            return arr.IndexOf(list);
        }

        #endregion
    }
}
