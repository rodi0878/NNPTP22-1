using NNPTPZ1.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNPTPZ1 {
    class Polynomial {
        public List<ComplexNumber> Coeficients { get; set; }

        public Polynomial() {
            Coeficients = new List<ComplexNumber>();
        }

        public void Add(ComplexNumber coeficient) {
            Coeficients.Add(coeficient);
        }
    }
}
