using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZHFS.Database
{
    public class SaleItem
    {
        public int Id { get; set; }
        public Sale? Sale { get; set; }
        public Product? Product { get; set; }
        public int Count { get; set; }
    }
}
