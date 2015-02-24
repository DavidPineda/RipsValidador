using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;

namespace ControlesAsp.EstructuraDeDatos
{
    public class Nodo<K, T>
    {

        public K key { get; set; }
        public T item { get; set; }

        public Nodo()
        {
            this.key = default(K);
            this.item = default(T);
        }

        public Nodo(K key, T item)
        {
            this.key = key;
            this.item = item;
        }
    }
}
