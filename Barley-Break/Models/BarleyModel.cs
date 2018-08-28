using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Barley_Break.Models
{
    public class BarleyModel
    {
       public List<int> Sequence { get; set; }
        public int Button { get; set; }
        public BarleyModel()
        {
            Sequence = new List<int>();
        }
    }
}