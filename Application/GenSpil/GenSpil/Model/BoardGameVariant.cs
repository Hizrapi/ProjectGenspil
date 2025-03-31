using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenSpil.Model
{
    class BoardGameVariant
    {
        public string Title { get; private set; }
        public string Variant { get; private set; }

        public BoardGameVariant(string title, string variant)
        {
            Title = title;
            Variant = variant;
        }
    }
}
