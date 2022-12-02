using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _9.heti
{
    public class  person
    {
        public int BirthYear { get; set; }
        public Gender Gender { get; set; }
        public int NbrOfChildren { get; set; }
        public bool IsAlive { get; set; }

        public person()
        {
            IsAlive = true;
        }
    }
}
