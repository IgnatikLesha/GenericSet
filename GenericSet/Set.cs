using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace GenericSet
{
    public class Set<T> : IEnumerable<T> where T: IEquatable<T>
    {
        private T[] set;
        private int current = 0;
        private int startCapacity = 2;


        public Set()
        {
            set = new T[startCapacity];
        }

        public Set(params T[] a)
        {
            if (a == null)
            {
                throw new ArgumentNullException();
            }
            set = new T[a.Length];
            Add(a);
        }
        public int Count
        {
            get
            {
                return current;
            }
        }
        public void Add(T elem)
        {
            if (Contains(elem))
            {
                return;
            }
            if (current == set.Length - 1)
            {
                Increasing();
            }
            set[current] = elem;
            current++;
        }

        public void Add(T[] elems)
        {
            foreach (var a in elems)
            {
                Add(a);
            }
        }



        public bool Contains(T elem)
        {
            if (elem == null)
            {
                return false;
            }
            for (int i = 0; i < current; i++)
            {
                if (elem.Equals(set[i]))
                {
                    return true;
                }
            }
            return false;
        }

        private void Increasing()
        {
            T[] newArray = new T[set.Length * 2];
            for (int i = 0; i < set.Length; i++)
            {
                newArray[i] = set[i];
            }
            set = newArray;
        }
        public bool IsEmpty()
        {
            if (current == 0)
            {
                return true;
            }
            return false;
        }

        public bool SetContains(Set<T> set)   //Is this set included in set 
        {
            if(ReferenceEquals(set, null))
                throw new ArgumentNullException();
            return SetContains(this, set);
        }

        public static bool SetContains(Set<T> a, Set<T> b)   // Set b include set a
        {
            if(ReferenceEquals(a, null) || ReferenceEquals(b, null))
                throw new ArgumentNullException();
            foreach (var elem in a)
            {
                if (!b.Contains(elem))
                    return false;
            }
            return true;
        }

        public Set<T> Union(Set<T> set)
        {
            if (ReferenceEquals(set, null))
                throw new ArgumentNullException();
            return Union(this, set);
        }

        public static Set<T> Union(Set<T> a, Set<T> b)
        {
            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                throw new ArgumentNullException();

            Set<T> result = a.Count >= b.Count ? a : b;
            Set<T> otherSet = a.Count <= b.Count ? a : b;

            foreach (T item in otherSet.set)
            {
                if (!result.Contains(item))
                {
                    result.Add(item);
                }
            }
            return result;
        }

        public Set<T> Intersection(Set<T> set)
        {
            if (ReferenceEquals(set, null))
                throw new ArgumentNullException();
            return Intersection(this, set);
        }

        public static Set<T> Intersection(Set<T> a, Set<T> b)
        {
            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                throw new ArgumentNullException();

            Set<T> result = new Set<T>();
            foreach (T item in a.set)
            {
                if (b.set.Contains(item))
                {
                    result.Add(item);
                }
            }
            return result;
        }

        public Set<T> Difference(Set<T> set)
        {
            if(ReferenceEquals(set, null))
                throw new ArgumentNullException();
            return Difference(this, set);
        }

        public static Set<T> Difference(Set<T> a, Set<T> b)    //Set a - set b
        {
            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                throw new ArgumentNullException();

            Set<T> result = new Set<T>();

            foreach (T elem in a.set)
            {
                if (!b.set.Contains(elem))
                    result.Add(elem);
            }
            return result;
        }

        public static Set<T> SymmetricDifference(Set<T> a, Set<T> b)
        {
            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                throw new ArgumentNullException();
            Set<T> union = a.Union(b);
            Set<T> intersection = a.Intersection(b);
            return union.Difference(intersection);
        }

        public Set<T> SymmetricDifference(Set<T> set)
        {
            if (ReferenceEquals(set, null))
                throw new ArgumentNullException();

            return SymmetricDifference(this, set);
        }



        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < current; i++)
            {
                yield return set[i];
            }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override int GetHashCode()
        {
            return GetType().ToString().Length * 13 * Count + GetHashCode();
        }

        public override bool Equals(object o)
        {
            if (ReferenceEquals(o, null))
                return false;
            if (ReferenceEquals(this, o))
                return true;
            Set<T> set = o as Set<T>;
            if (ReferenceEquals(set, null))
                return false;
            foreach (var elem in set)
                if (!this.Contains(elem))
                    return false;
            return true;
        }



    }
}
