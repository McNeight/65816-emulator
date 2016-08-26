using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNES_Emulator.Emulator {
    public class AddressingModes {

        private static byte fetchOperand(byte index, CPU cpu, Memory memory) {
            return memory[(cpu.PBR << 16) + cpu.PC + 1 + index];
        }

        public static AddressingModeResult AbsoluteData(CPU cpu, Memory memory) {
            byte low = fetchOperand(0, cpu, memory);
            byte high = fetchOperand(1, cpu, memory);
            return new AddressingModeResult((cpu.DBR << 16) + low + (high << 8),2);
        }
        public static AddressingModeResult AbsoluteControl(CPU cpu, Memory memory) {
            byte low = fetchOperand(0, cpu, memory);
            byte high = fetchOperand(1, cpu, memory);
            return new AddressingModeResult((cpu.PBR << 16) + low + (high << 8), 2);
        }
        public static AddressingModeResult AbsoluteX(CPU cpu, Memory memory) {
            byte low = fetchOperand(0,cpu, memory);
            byte high = fetchOperand(1, cpu, memory);
            if (cpu.emulation) {
                int bank = cpu.DBR << 16;
                ushort rest = (ushort)(low + (high << 8));
                rest += (byte)cpu.X;
                int address = bank + rest;
                
                return new AddressingModeResult(address,2);
            } else {
                int address = cpu.DBR << 16;
                address += (ushort)(low + (high << 8));

                ushort index = cpu.X;
                if (cpu.IRS) {
                    index = (byte)index;
                }

                address += index;

                return new AddressingModeResult(address, 2);
            }
        }
        public static AddressingModeResult AbsoluteY(CPU cpu, Memory memory) {
            byte low = fetchOperand(0, cpu, memory);
            byte high = fetchOperand(1, cpu, memory);
            if (cpu.emulation) {
                int bank = cpu.DBR << 16;
                ushort rest = (ushort)(low + (high << 8));
                rest += (byte)cpu.Y;
                int address = bank + rest;

                return new AddressingModeResult(address,2);
            } else {
                int address = cpu.DBR << 16;
                address += (ushort)(low + (high << 8));

                ushort index = cpu.Y;
                if (cpu.IRS) {
                    index = (byte)index;
                }

                address += index;

                return new AddressingModeResult(address, 2);
            }
        }
        public static AddressingModeResult AbsoluteIndexedIndirectX(CPU cpu, Memory memory) {
            byte low = fetchOperand(0, cpu, memory);
            byte high = fetchOperand(1, cpu, memory);
            int bank = (cpu.PBR << 16);
            ushort rest = (ushort)(low + (high << 8));
            ushort index = cpu.X;
            if (cpu.IRS)
                index = (byte)index;
            rest += index;
            int address = bank + rest;

            return new AddressingModeResult(memory[address] + (memory[address+1] << 8) + bank,2);
            
        }
        public static AddressingModeResult AbsoluteIndirect(CPU cpu, Memory memory) {
            byte low = fetchOperand(0, cpu, memory);
            byte high = fetchOperand(1, cpu, memory);
            ushort indirect = (ushort)(low + (high << 8));
            return new AddressingModeResult(memory[indirect] + (memory[indirect + 1] << 8) + (cpu.PBR << 16),2);
        }
        public static AddressingModeResult AbsoluteIndirectLong(CPU cpu, Memory memory) {
            byte low = fetchOperand(0, cpu, memory);
            byte high = fetchOperand(1, cpu, memory);
            ushort indirect = (ushort)(low + (high << 8));
            return new AddressingModeResult(memory[indirect] + (memory[indirect + 1] << 8) + (memory[indirect + 2] << 16),2);
        }
        public static AddressingModeResult AbsoluteLong(CPU cpu, Memory memory) {
            byte low = fetchOperand(0,cpu, memory);
            byte high = fetchOperand(1, cpu, memory);
            byte bank = fetchOperand(2, cpu, memory);
            return new AddressingModeResult(low + (high << 8) + (bank << 16),3);
        }
        public static AddressingModeResult AbsoluteLongIndexedX(CPU cpu, Memory memory) {
            byte low = fetchOperand(0, cpu, memory);
            byte high = fetchOperand(1, cpu, memory);
            byte bank = fetchOperand(2, cpu, memory);
            ushort index = cpu.X;
            if (cpu.IRS) {
                index = (byte)index;
            }
            return new AddressingModeResult(low + (high << 8) + (bank << 16) + index,3);
        }

        public static AddressingModeResult DirectPage(CPU cpu, Memory memory) {
            byte operand = fetchOperand(0, cpu, memory);
            if (cpu.emulation && cpu.D == 0) {
                byte low = (byte)cpu.D;
                byte high = (byte)(cpu.D >> 8);
                low += operand;
                return new AddressingModeResult(low + (high << 8),1);
            } else {
                ushort rest = cpu.D;
                rest += operand;
                return new AddressingModeResult(rest,1);
            }
        }
        public static AddressingModeResult DirectPageX(CPU cpu, Memory memory) {
            byte operand = fetchOperand(0, cpu, memory);
            if (cpu.emulation && cpu.D == 0) {
                byte low = (byte)cpu.D;
                byte high = (byte)(cpu.D >> 8);
                low += operand;
                low += (byte)cpu.X;
                return new AddressingModeResult(low + (high << 8),1);
            } else {
                ushort rest = cpu.D;
                rest += operand;
                ushort index = cpu.X;
                if (cpu.IRS)
                    index = (byte)index;
                rest += index;
                return new AddressingModeResult(rest,1);
            }
        }
        public static AddressingModeResult DirectPageY(CPU cpu, Memory memory) {
            byte operand = fetchOperand(0, cpu, memory);
            if (cpu.emulation && cpu.D == 0) {
                byte low = (byte)cpu.D;
                byte high = (byte)(cpu.D >> 8);
                low += operand;
                low += (byte)cpu.Y;
                return new AddressingModeResult(low + (high << 8),1);
            } else {
                ushort rest = cpu.D;
                rest += operand;
                ushort index = cpu.Y;
                if (cpu.IRS)
                    index = (byte)index;
                rest += index;
                return new AddressingModeResult(rest,1);
            }
        }
        public static AddressingModeResult DirectPageIndexedIndirectX(CPU cpu, Memory memory) {
            int indirect = DirectPageX(cpu,memory).address;
            return new AddressingModeResult(memory[indirect] + (memory[indirect + 1] << 8) + (cpu.DBR << 16),1);
        }
        public static AddressingModeResult DirectPageIndirect(CPU cpu, Memory memory) {
            int indirect = DirectPage(cpu, memory).address;
            return new AddressingModeResult(memory[indirect] + (memory[indirect + 1] << 8) + (cpu.DBR << 16),1);
        }
        public static AddressingModeResult DirectPageIndirectLong(CPU cpu, Memory memory) {
            int indirect = DirectPage(cpu, memory).address;
            return new AddressingModeResult(memory[indirect] + (memory[indirect + 1] << 8) + (memory[indirect + 2] << 16),1);
        }
        public static AddressingModeResult DirectPageIndirectIndexedY(CPU cpu, Memory memory) {
            ushort indirect = (ushort)(DirectPage(cpu, memory).address);
            ushort index = cpu.Y;
            if (cpu.IRS)
                index = (byte)index;
            int effective = memory[indirect] + (memory[indirect+1] << 8) + (cpu.DBR << 16) + index;
            return new AddressingModeResult(effective,1);
        }
        public static AddressingModeResult DirectPageIndirectLongIndexedY(CPU cpu, Memory memory) {
            ushort indirect = (ushort)(DirectPage(cpu, memory).address);
            ushort index = cpu.Y;
            if (cpu.IRS)
                index = (byte)index;
            int effective = memory[indirect] + (memory[indirect + 1] << 8) + (memory[indirect+2] << 16) + index;
            return new AddressingModeResult(effective, 1);
        }

        public static AddressingModeResult ProgramCounterRelative(CPU cpu, Memory memory) {
            sbyte operand = (sbyte)(fetchOperand(0, cpu, memory));
            int bank = cpu.PBR << 16;
            int rest = cpu.PC;
            rest += operand;
            rest += 2;
                
            return new AddressingModeResult(bank + rest,1);
        }
        public static AddressingModeResult ProgramCounterRelativeLong(CPU cpu, Memory memory) {
            byte low = fetchOperand(0, cpu, memory);
            byte high = fetchOperand(1, cpu, memory);
            short operand = (short)(low + (high << 8));
            int bank = cpu.PBR << 16;

            int rest = cpu.PC;
            rest += operand;
            rest += 2;
            return new AddressingModeResult(bank + rest,2);
        }

        public static AddressingModeResult StackRelativeAddressing(CPU cpu, Memory memory) {
            byte operand = fetchOperand(0, cpu, memory);
            return new AddressingModeResult((ushort)(operand + cpu.S),1);
        }
        public static AddressingModeResult StackRelativeIndirectIndexedY(CPU cpu, Memory memory) {
            byte operand = fetchOperand(0, cpu, memory);
            ushort indirect = (ushort)(operand + cpu.S);
            int address = memory[indirect] + (memory[indirect + 1] << 8);
            address += cpu.DBR << 16;
            ushort index = cpu.Y;
            if (cpu.IRS)
                index = (byte)index;
            address += index;
            return new AddressingModeResult(address,1); 
        }


        //data based on flag
        public static AddressingModeResult ImmediateFlag(CPU cpu, Boolean flag) {
            return new AddressingModeResult((cpu.PBR << 16) + cpu.PC + 1,flag?1:2);
        }

        public class AddressingModeResult {
            public int address;
            public int PCP;
            public AddressingModeResult(int address, int PCP) {
                this.address = address;
                this.PCP = PCP;
            }
        }

    }
}
