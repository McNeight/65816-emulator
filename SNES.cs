using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNES_Emulator.Emulator {
    public class SNES {
        public CPU cpu;
        public Memory memory = new Memory();
        
        public SNES() {
            cpu = new CPU(this);
        }



    }
}
