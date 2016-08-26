using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNES_Emulator.Emulator {
    public class MemoryLocation {
        public byte[] data;

        public MemoryLocation() { }
        public MemoryLocation(int size) {
            data = new byte[size];
        }

        public void resize(int newSize) {
            byte[] temp = new byte[newSize];
            for(int i = 0; i < temp.Length; i++) {
                temp[i] = data[i];
            }
            data = temp;
        }

        public byte this[int address] {
            get {
                try {
                    return data[address];
                } catch(Exception ex) {
                    return 0;
                }
            }
            set {
                try {
                    data[address] = value;
                } catch(Exception ex) {

                }
            }
        }
    }
}
