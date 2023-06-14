using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReceiptScanner
{
    public class ReceiptItem
    {
        public string Description { get; set; }
        public BoundingPoly BoundingPoly { get; set; }
    }
    public class BoundingPoly
    {
        public List<Vertex> Vertices { get; set; }
    }

    public class Vertex
    {
        public int X { get; set; }
        public int Y { get; set; }
    }
}