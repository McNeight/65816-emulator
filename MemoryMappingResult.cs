using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNES_Emulator.Emulator {
    public class MemoryMappingResult {
        public MemoryLocation location;
        public int address;
        public MemoryMappingResult(MemoryLocation location, int address) {
            this.location = location;
            this.address = address;
        }


    }
}
