using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TygaSoft.Model
{
    public class DataGrid<T>
    {
        public int total { get; set; }

        public IEnumerable<T> rows { get; set; }
    }
}
