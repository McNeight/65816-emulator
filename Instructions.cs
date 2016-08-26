using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNES_Emulator.Emulator {
    public class Instructions {

        public class AddressingModeException : Exception {
            public AddressingModeException() : base("Addressing Mode Result is null") {

            }
        }




        public static void ADCByOpcode(byte opcode, CPU cpu, Memory memory) {
            AddressingModes.AddressingModeResult dataAddress = null;
            //determine addressing mode.
            switch (opcode) {
                case 0x69:
                    dataAddress = AddressingModes.ImmediateFlag(cpu, cpu.ARS);
                    break;
                case 0x6D:
                    dataAddress = AddressingModes.AbsoluteData(cpu, memory);
                    break;
                case 0x6F:
                    dataAddress = AddressingModes.AbsoluteLong(cpu, memory);
                    break;
                case 0x65:
                    dataAddress = AddressingModes.DirectPage(cpu, memory);
                    break;
                case 0x72:
                    dataAddress = AddressingModes.DirectPageIndirect(cpu, memory);
                    break;
                case 0x67:
                    dataAddress = AddressingModes.DirectPageIndirectLong(cpu, memory);
                    break;
                case 0x7D:
                    dataAddress = AddressingModes.AbsoluteX(cpu, memory);
                    break;
                case 0x7F:
                    dataAddress = AddressingModes.AbsoluteLongIndexedX(cpu, memory);
                    break;
                case 0x79:
                    dataAddress = AddressingModes.AbsoluteY(cpu, memory);
                    break;
                case 0x75:
                    dataAddress = AddressingModes.DirectPageX(cpu, memory);
                    break;
                case 0x61:
                    dataAddress = AddressingModes.DirectPageIndexedIndirectX(cpu, memory);
                    break;
                case 0x71:
                    dataAddress = AddressingModes.DirectPageIndirectIndexedY(cpu, memory);
                    break;
                case 0x77:
                    dataAddress = AddressingModes.DirectPageIndirectLongIndexedY(cpu, memory);
                    break;
                case 0x63:
                    dataAddress = AddressingModes.StackRelativeAddressing(cpu, memory);
                    break;
                case 0x73:
                    dataAddress = AddressingModes.StackRelativeIndirectIndexedY(cpu, memory);
                    break;
            }
            if (dataAddress == null) {
                //throw new AddressingModeException();
            } else {
                if (!cpu.ARS) {
                    byte low = memory[dataAddress.address];
                    byte high = memory[dataAddress.address + 1];
                    byte[] operands = { low, high };
                    ADC(operands, cpu);
                    cpu.PC += (ushort)(dataAddress.PCP + 1);
                } else {
                    byte[] operands = { memory[dataAddress.address] };
                    ADC(operands, cpu);
                    cpu.PC += (ushort)(dataAddress.PCP + 1);
                }
            }
        }//1
        public static void ANDByOpcode(byte opcode, CPU cpu, Memory memory) {
            AddressingModes.AddressingModeResult dataAddress = null;
            switch (opcode) {
                case 0x29:
                    dataAddress = AddressingModes.ImmediateFlag(cpu, cpu.ARS);
                    break;
                case 0x2D:
                    dataAddress = AddressingModes.AbsoluteData(cpu, memory);
                    break;
                case 0x2F:
                    dataAddress = AddressingModes.AbsoluteLong(cpu, memory);
                    break;
                case 0x25:
                    dataAddress = AddressingModes.DirectPage(cpu, memory);
                    break;
                case 0x32:
                    dataAddress = AddressingModes.DirectPageIndirect(cpu, memory);
                    break;
                case 0x27:
                    dataAddress = AddressingModes.DirectPageIndirectLong(cpu, memory);
                    break;
                case 0x3D:
                    dataAddress = AddressingModes.AbsoluteX(cpu, memory);
                    break;
                case 0x3F:
                    dataAddress = AddressingModes.AbsoluteLongIndexedX(cpu, memory);
                    break;
                case 0x39:
                    dataAddress = AddressingModes.AbsoluteY(cpu, memory);
                    break;
                case 0x35:
                    dataAddress = AddressingModes.DirectPageX(cpu, memory);
                    break;
                case 0x21:
                    dataAddress = AddressingModes.DirectPageIndexedIndirectX(cpu, memory);
                    break;
                case 0x31:
                    dataAddress = AddressingModes.DirectPageIndirectIndexedY(cpu, memory);
                    break;
                case 0x37:
                    dataAddress = AddressingModes.DirectPageIndirectLongIndexedY(cpu, memory);
                    break;
                case 0x23:
                    dataAddress = AddressingModes.StackRelativeAddressing(cpu, memory);
                    break;
                case 0x33:
                    dataAddress = AddressingModes.StackRelativeIndirectIndexedY(cpu, memory);
                    break;
            }
            if (dataAddress == null) {
                //throw new AddressingModeException();
            } else {
                if (!cpu.ARS) {
                    byte low = memory[dataAddress.address];
                    byte high = memory[dataAddress.address + 1];
                    byte[] operands = { low, high };
                    AND(operands, cpu);
                    cpu.PC += (ushort)(dataAddress.PCP + 1);
                } else {
                    byte[] operands = { memory[dataAddress.address] };
                    AND(operands, cpu);
                    cpu.PC += (ushort)(dataAddress.PCP + 1);
                }
            }
        }//1
        public static void ASLByOpcode(byte opcode, CPU cpu, Memory memory) {
            AddressingModes.AddressingModeResult dataAddress = null;
            switch (opcode) {
                case 0x0A:
                    dataAddress = new AddressingModes.AddressingModeResult(0, 0);
                    break;
                case 0x0E:
                    dataAddress = AddressingModes.AbsoluteData(cpu, memory);
                    break;
                case 0x06:
                    dataAddress = AddressingModes.DirectPage(cpu, memory);
                    break;
                case 0x1E:
                    dataAddress = AddressingModes.AbsoluteX(cpu, memory);
                    break;
                case 0x16:
                    dataAddress = AddressingModes.DirectPageX(cpu, memory);
                    break;
            }

            if (dataAddress == null) {
                //throw new AddressingModeException();
            } else {
                if (opcode == 0x0A)
                    ASLA(cpu);
                else
                    ASLM(dataAddress.address, cpu, memory);
                cpu.PC += (ushort)(dataAddress.PCP + 1);
            }

        }//2
        public static void BITByOpcode(byte opcode, CPU cpu, Memory memory) {
            AddressingModes.AddressingModeResult dataAddress = null;
            switch (opcode) {
                case 0x89:
                    dataAddress = AddressingModes.ImmediateFlag(cpu, cpu.ARS);
                    break;
                case 0x2C:
                    dataAddress = AddressingModes.AbsoluteData(cpu, memory);
                    break;
                case 0x24:
                    dataAddress = AddressingModes.DirectPage(cpu, memory);
                    break;
                case 0x3C:
                    dataAddress = AddressingModes.AbsoluteX(cpu, memory);
                    break;
                case 0x34:
                    dataAddress = AddressingModes.DirectPageX(cpu, memory);
                    break;
            }
            if (dataAddress == null) {
                //throw new AddressingModeException();
            } else {
                if (!cpu.ARS) {
                    byte low = memory[dataAddress.address];
                    byte high = memory[dataAddress.address + 1];
                    byte[] operands = { low, high };
                    BIT(operands, cpu, opcode == 0x89);
                } else {
                    byte[] operands = { memory[dataAddress.address] };
                    BIT(operands, cpu, opcode == 0x89);
                }
                cpu.PC += (ushort)(dataAddress.PCP + 1);
            }


        }//not following any scheme
        public static void CMPByOpcode(byte opcode, CPU cpu, Memory memory) {
            AddressingModes.AddressingModeResult dataAddress = null;
            switch (opcode) {
                case 0xC9:
                    dataAddress = AddressingModes.ImmediateFlag(cpu, cpu.ARS);
                    break;
                case 0xCD:
                    dataAddress = AddressingModes.AbsoluteData(cpu, memory);
                    break;
                case 0xCF:
                    dataAddress = AddressingModes.AbsoluteLong(cpu, memory);
                    break;
                case 0xC5:
                    dataAddress = AddressingModes.DirectPage(cpu, memory);
                    break;
                case 0xD2:
                    dataAddress = AddressingModes.DirectPageIndirect(cpu, memory);
                    break;
                case 0xC7:
                    dataAddress = AddressingModes.DirectPageIndirectLong(cpu, memory);
                    break;
                case 0xDD:
                    dataAddress = AddressingModes.AbsoluteX(cpu, memory);
                    break;
                case 0xDF:
                    dataAddress = AddressingModes.AbsoluteLongIndexedX(cpu, memory);
                    break;
                case 0xD9:
                    dataAddress = AddressingModes.AbsoluteY(cpu, memory);
                    break;
                case 0xD5:
                    dataAddress = AddressingModes.DirectPageX(cpu, memory);
                    break;
                case 0xC1:
                    dataAddress = AddressingModes.DirectPageIndexedIndirectX(cpu, memory);
                    break;
                case 0xD1:
                    dataAddress = AddressingModes.DirectPageIndirectIndexedY(cpu, memory);
                    break;
                case 0xD7:
                    dataAddress = AddressingModes.DirectPageIndirectLongIndexedY(cpu, memory);
                    break;
                case 0xC3:
                    dataAddress = AddressingModes.StackRelativeAddressing(cpu, memory);
                    break;
                case 0xD3:
                    dataAddress = AddressingModes.StackRelativeIndirectIndexedY(cpu, memory);
                    break;
            }
            if (dataAddress == null) {
                //throw new AddressingModeException();
            } else {
                if (!cpu.ARS) {
                    byte low = memory[dataAddress.address];
                    byte high = memory[dataAddress.address + 1];
                    byte[] operands = { low, high };
                    CMP(operands, cpu);
                    cpu.PC += (ushort)(dataAddress.PCP + 1);
                } else {
                    byte[] operands = { memory[dataAddress.address] };
                    CMP(operands, cpu);
                    cpu.PC += (ushort)(dataAddress.PCP + 1);
                }
            }
        }//1
        public static void CPXByOpcode(byte opcode, CPU cpu, Memory memory) {
            AddressingModes.AddressingModeResult dataAddress = null;
            switch (opcode) {
                case 0xE0:
                    dataAddress = AddressingModes.ImmediateFlag(cpu, cpu.IRS);
                    break;
                case 0xEC:
                    dataAddress = AddressingModes.AbsoluteData(cpu, memory);
                    break;
                case 0xE4:
                    dataAddress = AddressingModes.DirectPage(cpu, memory);
                    break;
            }
            if (dataAddress == null) {
                //throw new AddressingModeException();
            } else {
                if (!cpu.IRS) {
                    byte low = memory[dataAddress.address];
                    byte high = memory[dataAddress.address + 1];
                    byte[] operands = { low, high };
                    CPX(operands, cpu);
                    cpu.PC += (ushort)(dataAddress.PCP + 1);
                } else {
                    byte[] operands = { memory[dataAddress.address] };
                    CPX(operands, cpu);
                    cpu.PC += (ushort)(dataAddress.PCP + 1);
                }
            }
        }//Index Register Compares
        public static void CPYByOpcode(byte opcode, CPU cpu, Memory memory) {
            AddressingModes.AddressingModeResult dataAddress = null;
            switch (opcode) {
                case 0xC0:
                    dataAddress = AddressingModes.ImmediateFlag(cpu, cpu.IRS);
                    break;
                case 0xCC:
                    dataAddress = AddressingModes.AbsoluteData(cpu, memory);
                    break;
                case 0xC4:
                    dataAddress = AddressingModes.DirectPage(cpu, memory);
                    break;
            }
            if (dataAddress == null) {
                //throw new AddressingModeException();
            } else {
                if (!cpu.IRS) {
                    byte low = memory[dataAddress.address];
                    byte high = memory[dataAddress.address + 1];
                    byte[] operands = { low, high };
                    CPY(operands, cpu);
                    cpu.PC += (ushort)(dataAddress.PCP + 1);
                } else {
                    byte[] operands = { memory[dataAddress.address] };
                    CPY(operands, cpu);
                    cpu.PC += (ushort)(dataAddress.PCP + 1);
                }
            }
        }//Index Register Compares
        public static void DECByOpcode(byte opcode, CPU cpu, Memory memory) {
            AddressingModes.AddressingModeResult dataAddress = null;
            switch (opcode) {
                case 0x3A:
                    dataAddress = new AddressingModes.AddressingModeResult(0, 0);
                    break;
                case 0xCE:
                    dataAddress = AddressingModes.AbsoluteData(cpu, memory);
                    break;
                case 0xC6:
                    dataAddress = AddressingModes.DirectPage(cpu, memory);
                    break;
                case 0xDE:
                    dataAddress = AddressingModes.AbsoluteX(cpu, memory);
                    break;
                case 0xD6:
                    dataAddress = AddressingModes.DirectPageX(cpu, memory);
                    break;
            }
            if (dataAddress == null) {
                //throw new AddressingModeException();
            } else {
                if (opcode == 0x3A)
                    DECA(cpu);
                else
                    DECM(dataAddress.address, cpu, memory);
                cpu.PC += (ushort)(dataAddress.PCP + 1);
            }
        }//2
        public static void EORByOpcode(byte opcode, CPU cpu, Memory memory) {
            AddressingModes.AddressingModeResult dataAddress = null;
            switch (opcode) {
                case 0x49:
                    dataAddress = AddressingModes.ImmediateFlag(cpu, cpu.ARS);
                    break;
                case 0x4D:
                    dataAddress = AddressingModes.AbsoluteData(cpu, memory);
                    break;
                case 0x4F:
                    dataAddress = AddressingModes.AbsoluteLong(cpu, memory);
                    break;
                case 0x45:
                    dataAddress = AddressingModes.DirectPage(cpu, memory);
                    break;
                case 0x52:
                    dataAddress = AddressingModes.DirectPageIndirect(cpu, memory);
                    break;
                case 0x47:
                    dataAddress = AddressingModes.DirectPageIndirectLong(cpu, memory);
                    break;
                case 0x5D:
                    dataAddress = AddressingModes.AbsoluteX(cpu, memory);
                    break;
                case 0x5F:
                    dataAddress = AddressingModes.AbsoluteLongIndexedX(cpu, memory);
                    break;
                case 0x59:
                    dataAddress = AddressingModes.AbsoluteY(cpu, memory);
                    break;
                case 0x55:
                    dataAddress = AddressingModes.DirectPageX(cpu, memory);
                    break;
                case 0x41:
                    dataAddress = AddressingModes.DirectPageIndexedIndirectX(cpu, memory);
                    break;
                case 0x51:
                    dataAddress = AddressingModes.DirectPageIndirectIndexedY(cpu, memory);
                    break;
                case 0x57:
                    dataAddress = AddressingModes.DirectPageIndirectLongIndexedY(cpu, memory);
                    break;
                case 0x43:
                    dataAddress = AddressingModes.StackRelativeAddressing(cpu, memory);
                    break;
                case 0x53:
                    dataAddress = AddressingModes.StackRelativeIndirectIndexedY(cpu, memory);
                    break;
            }
            if (dataAddress == null) {
                //throw new AddressingModeException();
            } else {
                if (!cpu.ARS) {
                    byte low = memory[dataAddress.address];
                    byte high = memory[dataAddress.address + 1];
                    byte[] operands = { low, high };
                    EOR(operands, cpu);
                    cpu.PC += (ushort)(dataAddress.PCP + 1);
                } else {
                    byte[] operands = { memory[dataAddress.address] };
                    EOR(operands, cpu);
                    cpu.PC += (ushort)(dataAddress.PCP + 1);
                }
            }
        }//1
        public static void INCByOpcode(byte opcode, CPU cpu, Memory memory) {
            AddressingModes.AddressingModeResult dataAddress = null;
            switch (opcode) {
                case 0x1A:
                    dataAddress = new AddressingModes.AddressingModeResult(0, 0);
                    break;
                case 0xEE:
                    dataAddress = AddressingModes.AbsoluteData(cpu, memory);
                    break;
                case 0xE6:
                    dataAddress = AddressingModes.DirectPage(cpu, memory);
                    break;
                case 0xFE:
                    dataAddress = AddressingModes.AbsoluteX(cpu, memory);
                    break;
                case 0xF6:
                    dataAddress = AddressingModes.DirectPageX(cpu, memory);
                    break;
            }
            if (dataAddress == null) {
                //throw new AddressingModeException();
            } else {
                if (opcode == 0x1A)
                    INCA(cpu);
                else
                    INCM(dataAddress.address, cpu, memory);
                cpu.PC += (ushort)(dataAddress.PCP + 1);
            }
        }//2
        public static void JMPByOpcode(byte opcode, CPU cpu, Memory memory) {
            AddressingModes.AddressingModeResult nextInstr = null;
            switch (opcode) {
                case 0x4C:
                    nextInstr = AddressingModes.AbsoluteControl(cpu, memory);
                    break;
                case 0x6C:
                    nextInstr = AddressingModes.AbsoluteIndirect(cpu, memory);
                    break;
                case 0x7C:
                    nextInstr = AddressingModes.AbsoluteIndexedIndirectX(cpu, memory);
                    break;
                case 0x5C:
                    nextInstr = AddressingModes.AbsoluteLong(cpu, memory);
                    break;
                case 0xDC:
                    nextInstr = AddressingModes.AbsoluteIndirectLong(cpu, memory);
                    break;
            }
            if (nextInstr == null) {
                //throw new AddressingModeException();
            } else {
                JMP(nextInstr.address, cpu);
            }
        }//not following any scheme
        public static void JSRByOpcode(byte opcode, CPU cpu, Memory memory) {
            AddressingModes.AddressingModeResult nextInstr = null;
            switch (opcode) {
                case 0x20:
                    nextInstr = AddressingModes.AbsoluteControl(cpu, memory);
                    break;
                case 0xFC:
                    nextInstr = AddressingModes.AbsoluteIndexedIndirectX(cpu, memory);
                    break;
                case 0x22:
                    nextInstr = AddressingModes.AbsoluteLong(cpu, memory);
                    break;
            }
            if (nextInstr == null) {
                //throw new AddressingModeException();
            } else {
                if (opcode == 0x22)
                    JSL(nextInstr.address, cpu, memory);
                else
                    JSR(nextInstr.address, cpu, memory);
            }
        }//not following any scheme
        public static void LDAByOpcode(byte opcode, CPU cpu, Memory memory) {
            AddressingModes.AddressingModeResult dataAddress = null;
            switch (opcode) {
                case 0xA9:
                    dataAddress = AddressingModes.ImmediateFlag(cpu, cpu.ARS);
                    break;
                case 0xAD:
                    dataAddress = AddressingModes.AbsoluteData(cpu, memory);
                    break;
                case 0xAF:
                    dataAddress = AddressingModes.AbsoluteLong(cpu, memory);
                    break;
                case 0xA5:
                    dataAddress = AddressingModes.DirectPage(cpu, memory);
                    break;
                case 0xB2:
                    dataAddress = AddressingModes.DirectPageIndirect(cpu, memory);
                    break;
                case 0xA7:
                    dataAddress = AddressingModes.DirectPageIndirectLong(cpu, memory);
                    break;
                case 0xBD:
                    dataAddress = AddressingModes.AbsoluteX(cpu, memory);
                    break;
                case 0xBF:
                    dataAddress = AddressingModes.AbsoluteLongIndexedX(cpu, memory);
                    break;
                case 0xB9:
                    dataAddress = AddressingModes.AbsoluteY(cpu, memory);
                    break;
                case 0xB5:
                    dataAddress = AddressingModes.DirectPageX(cpu, memory);
                    break;
                case 0xA1:
                    dataAddress = AddressingModes.DirectPageIndexedIndirectX(cpu, memory);
                    break;
                case 0xB1:
                    dataAddress = AddressingModes.DirectPageIndirectIndexedY(cpu, memory);
                    break;
                case 0xB7:
                    dataAddress = AddressingModes.DirectPageIndirectLongIndexedY(cpu, memory);
                    break;
                case 0xA3:
                    dataAddress = AddressingModes.StackRelativeAddressing(cpu, memory);
                    break;
                case 0xB3:
                    dataAddress = AddressingModes.StackRelativeIndirectIndexedY(cpu, memory);
                    break;
            }
            if (dataAddress == null) {
                //throw new AddressingModeException();
            } else {
                if (!cpu.ARS) {
                    byte low = memory[dataAddress.address];
                    byte high = memory[dataAddress.address + 1];
                    byte[] operands = { low, high };
                    LDA(operands, cpu);
                    cpu.PC += (ushort)(dataAddress.PCP + 1);
                } else {
                    byte[] operands = { memory[dataAddress.address] };
                    LDA(operands, cpu);
                    cpu.PC += (ushort)(dataAddress.PCP + 1);
                }
            }
        }//1
        public static void LDXByOpcode(byte opcode, CPU cpu, Memory memory) {
            AddressingModes.AddressingModeResult dataAddress = null;
            switch (opcode) {
                case 0xA2:
                    dataAddress = AddressingModes.ImmediateFlag(cpu, cpu.IRS);
                    break;
                case 0xAE:
                    dataAddress = AddressingModes.AbsoluteData(cpu, memory);
                    break;
                case 0xA6:
                    dataAddress = AddressingModes.DirectPage(cpu, memory);
                    break;
                case 0xBE:
                    dataAddress = AddressingModes.AbsoluteY(cpu, memory);
                    break;
                case 0xB6:
                    dataAddress = AddressingModes.DirectPageY(cpu, memory);
                    break;
            }
            if (dataAddress == null) {
                //throw new AddressingModeException();
            } else {
                if (!cpu.IRS) {
                    byte low = memory[dataAddress.address];
                    byte high = memory[dataAddress.address + 1];
                    byte[] operands = { low, high };
                    LDX(operands, cpu);
                    cpu.PC += (ushort)(dataAddress.PCP + 1);
                } else {
                    byte[] operands = { memory[dataAddress.address] };
                    LDX(operands, cpu);
                    cpu.PC += (ushort)(dataAddress.PCP + 1);
                }
            }
        }//Load Index Registers
        public static void LDYByOpcode(byte opcode, CPU cpu, Memory memory) {
            AddressingModes.AddressingModeResult dataAddress = null;
            switch (opcode) {
                case 0xA0:
                    dataAddress = AddressingModes.ImmediateFlag(cpu, cpu.IRS);
                    break;
                case 0xAC:
                    dataAddress = AddressingModes.AbsoluteData(cpu, memory);
                    break;
                case 0xA4:
                    dataAddress = AddressingModes.DirectPage(cpu, memory);
                    break;
                case 0xBC:
                    dataAddress = AddressingModes.AbsoluteX(cpu, memory);
                    break;
                case 0xB4:
                    dataAddress = AddressingModes.DirectPageX(cpu, memory);
                    break;
            }
            if (dataAddress == null) {
                //throw new AddressingModeException();
            } else {
                if (!cpu.IRS) {
                    byte low = memory[dataAddress.address];
                    byte high = memory[dataAddress.address + 1];
                    byte[] operands = { low, high };
                    LDY(operands, cpu);
                    cpu.PC += (ushort)(dataAddress.PCP + 1);
                } else {
                    byte[] operands = { memory[dataAddress.address] };
                    LDY(operands, cpu);
                    cpu.PC += (ushort)(dataAddress.PCP + 1);
                }
            }
        }//Load Index Registers
        public static void LSRByOpcode(byte opcode, CPU cpu, Memory memory) {
            AddressingModes.AddressingModeResult dataAddress = null;
            switch (opcode) {
                case 0x4A:
                    dataAddress = new AddressingModes.AddressingModeResult(0, 0);
                    break;
                case 0x4E:
                    dataAddress = AddressingModes.AbsoluteData(cpu, memory);
                    break;
                case 0x46:
                    dataAddress = AddressingModes.DirectPage(cpu, memory);
                    break;
                case 0x5E:
                    dataAddress = AddressingModes.AbsoluteX(cpu, memory);
                    break;
                case 0x56:
                    dataAddress = AddressingModes.DirectPageX(cpu, memory);
                    break;
            }
            if (dataAddress == null) {
                //throw new AddressingModeException();
            } else {
                if (opcode == 0x4A)
                    LSRA(cpu);
                else
                    LSRM(dataAddress.address, cpu, memory);
                cpu.PC += (ushort)(dataAddress.PCP + 1);
            }
        }//2
        public static void ORAByOpcode(byte opcode, CPU cpu, Memory memory) {
            AddressingModes.AddressingModeResult dataAddress = null;
            switch (opcode) {
                case 0x09:
                    dataAddress = AddressingModes.ImmediateFlag(cpu, cpu.ARS);
                    break;
                case 0x0D:
                    dataAddress = AddressingModes.AbsoluteData(cpu, memory);
                    break;
                case 0x0F:
                    dataAddress = AddressingModes.AbsoluteLong(cpu, memory);
                    break;
                case 0x05:
                    dataAddress = AddressingModes.DirectPage(cpu, memory);
                    break;
                case 0x12:
                    dataAddress = AddressingModes.DirectPageIndirect(cpu, memory);
                    break;
                case 0x07:
                    dataAddress = AddressingModes.DirectPageIndirectLong(cpu, memory);
                    break;
                case 0x1D:
                    dataAddress = AddressingModes.AbsoluteX(cpu, memory);
                    break;
                case 0x1F:
                    dataAddress = AddressingModes.AbsoluteLongIndexedX(cpu, memory);
                    break;
                case 0x19:
                    dataAddress = AddressingModes.AbsoluteY(cpu, memory);
                    break;
                case 0x15:
                    dataAddress = AddressingModes.DirectPageX(cpu, memory);
                    break;
                case 0x01:
                    dataAddress = AddressingModes.DirectPageIndexedIndirectX(cpu, memory);
                    break;
                case 0x11:
                    dataAddress = AddressingModes.DirectPageIndirectIndexedY(cpu, memory);
                    break;
                case 0x17:
                    dataAddress = AddressingModes.DirectPageIndirectLongIndexedY(cpu, memory);
                    break;
                case 0x03:
                    dataAddress = AddressingModes.StackRelativeAddressing(cpu, memory);
                    break;
                case 0x13:
                    dataAddress = AddressingModes.StackRelativeIndirectIndexedY(cpu, memory);
                    break;
            }
            if (dataAddress == null) {
                //throw new AddressingModeException();
            } else {
                if (!cpu.ARS) {
                    byte low = memory[dataAddress.address];
                    byte high = memory[dataAddress.address + 1];
                    byte[] operands = { low, high };
                    ORA(operands, cpu);
                    cpu.PC += (ushort)(dataAddress.PCP + 1);
                } else {
                    byte[] operands = { memory[dataAddress.address] };
                    ORA(operands, cpu);
                    cpu.PC += (ushort)(dataAddress.PCP + 1);
                }
            }
        }//1
        public static void ROLByOpcode(byte opcode, CPU cpu, Memory memory) {
            AddressingModes.AddressingModeResult dataAddress = null;
            switch (opcode) {
                case 0x2A:
                    dataAddress = new AddressingModes.AddressingModeResult(0, 0);
                    break;
                case 0x2E:
                    dataAddress = AddressingModes.AbsoluteData(cpu, memory);
                    break;
                case 0x26:
                    dataAddress = AddressingModes.DirectPage(cpu, memory);
                    break;
                case 0x3E:
                    dataAddress = AddressingModes.AbsoluteX(cpu, memory);
                    break;
                case 0x36:
                    dataAddress = AddressingModes.DirectPageX(cpu, memory);
                    break;
            }
            if (dataAddress == null) {
                //throw new AddressingModeException();
            } else {
                if (opcode == 0x2A)
                    ROLA(cpu);
                else
                    ROLM(dataAddress.address, cpu, memory);
                cpu.PC += (ushort)(dataAddress.PCP + 1);
            }
        }//2
        public static void RORByOpcode(byte opcode, CPU cpu, Memory memory) {
            AddressingModes.AddressingModeResult dataAddress = null;
            switch (opcode) {
                case 0x6A:
                    dataAddress = new AddressingModes.AddressingModeResult(0, 0);
                    break;
                case 0x6E:
                    dataAddress = AddressingModes.AbsoluteData(cpu, memory);
                    break;
                case 0x66:
                    dataAddress = AddressingModes.DirectPage(cpu, memory);
                    break;
                case 0x7E:
                    dataAddress = AddressingModes.AbsoluteX(cpu, memory);
                    break;
                case 0x76:
                    dataAddress = AddressingModes.DirectPageX(cpu, memory);
                    break;
            }
            if (dataAddress == null) {
                //throw new AddressingModeException();
            } else {
                if (opcode == 0x6A)
                    RORA(cpu);
                else
                    RORM(dataAddress.address, cpu, memory);
                cpu.PC += (ushort)(dataAddress.PCP + 1);
            }
        }//2
        public static void SBCByOpcode(byte opcode, CPU cpu, Memory memory) {
            AddressingModes.AddressingModeResult dataAddress = null;
            //determine addressing mode.
            switch (opcode) {
                case 0xE9:
                    dataAddress = AddressingModes.ImmediateFlag(cpu, cpu.ARS);
                    break;
                case 0xED:
                    dataAddress = AddressingModes.AbsoluteData(cpu, memory);
                    break;
                case 0xEF:
                    dataAddress = AddressingModes.AbsoluteLong(cpu, memory);
                    break;
                case 0xE5:
                    dataAddress = AddressingModes.DirectPage(cpu, memory);
                    break;
                case 0xF2:
                    dataAddress = AddressingModes.DirectPageIndirect(cpu, memory);
                    break;
                case 0xE7:
                    dataAddress = AddressingModes.DirectPageIndirectLong(cpu, memory);
                    break;
                case 0xFD:
                    dataAddress = AddressingModes.AbsoluteX(cpu, memory);
                    break;
                case 0xFF:
                    dataAddress = AddressingModes.AbsoluteLongIndexedX(cpu, memory);
                    break;
                case 0xF9:
                    dataAddress = AddressingModes.AbsoluteY(cpu, memory);
                    break;
                case 0xF5:
                    dataAddress = AddressingModes.DirectPageX(cpu, memory);
                    break;
                case 0xE1:
                    dataAddress = AddressingModes.DirectPageIndexedIndirectX(cpu, memory);
                    break;
                case 0xF1:
                    dataAddress = AddressingModes.DirectPageIndirectIndexedY(cpu, memory);
                    break;
                case 0xF7:
                    dataAddress = AddressingModes.DirectPageIndirectLongIndexedY(cpu, memory);
                    break;
                case 0xE3:
                    dataAddress = AddressingModes.StackRelativeAddressing(cpu, memory);
                    break;
                case 0xF3:
                    dataAddress = AddressingModes.StackRelativeIndirectIndexedY(cpu, memory);
                    break;
            }
            if (dataAddress == null) {
                //throw new AddressingModeException();
            } else {
                if (!cpu.ARS) {
                    byte low = memory[dataAddress.address];
                    byte high = memory[dataAddress.address + 1];
                    byte[] operands = { low, high };
                    SBC(operands, cpu);
                    cpu.PC += (ushort)(dataAddress.PCP + 1);
                } else {
                    byte[] operands = { memory[dataAddress.address] };
                    SBC(operands, cpu);
                    cpu.PC += (ushort)(dataAddress.PCP + 1);
                }
            }
        }//1
        public static void STAByOpcode(byte opcode, CPU cpu, Memory memory) {
            AddressingModes.AddressingModeResult dataAddress = null;
            switch (opcode) {
                case 0x8D:
                    dataAddress = AddressingModes.AbsoluteData(cpu, memory);
                    break;
                case 0x8F:
                    dataAddress = AddressingModes.AbsoluteLong(cpu, memory);
                    break;
                case 0x85:
                    dataAddress = AddressingModes.DirectPage(cpu, memory);
                    break;
                case 0x92:
                    dataAddress = AddressingModes.DirectPageIndirect(cpu, memory);
                    break;
                case 0x87:
                    dataAddress = AddressingModes.DirectPageIndirectLong(cpu, memory);
                    break;
                case 0x9D:
                    dataAddress = AddressingModes.AbsoluteX(cpu, memory);
                    break;
                case 0x9F:
                    dataAddress = AddressingModes.AbsoluteLongIndexedX(cpu, memory);
                    break;
                case 0x99:
                    dataAddress = AddressingModes.AbsoluteY(cpu, memory);
                    break;
                case 0x95:
                    dataAddress = AddressingModes.DirectPageX(cpu, memory);
                    break;
                case 0x81:
                    dataAddress = AddressingModes.DirectPageIndexedIndirectX(cpu, memory);
                    break;
                case 0x91:
                    dataAddress = AddressingModes.DirectPageIndirectIndexedY(cpu, memory);
                    break;
                case 0x97:
                    dataAddress = AddressingModes.DirectPageIndirectLongIndexedY(cpu, memory);
                    break;
                case 0x83:
                    dataAddress = AddressingModes.StackRelativeAddressing(cpu, memory);
                    break;
                case 0x93:
                    dataAddress = AddressingModes.StackRelativeIndirectIndexedY(cpu, memory);
                    break;
            }
            if (dataAddress == null) {
                //throw new AddressingModeException();
            } else {
                STA(dataAddress.address, cpu, memory);
                cpu.PC += (ushort)(dataAddress.PCP + 1);
            }
        }//1
        public static void STXByOpcode(byte opcode, CPU cpu, Memory memory) {
            AddressingModes.AddressingModeResult dataAddress = null;
            switch (opcode) {
                case 0x8E:
                    dataAddress = AddressingModes.AbsoluteData(cpu, memory);
                    break;
                case 0x86:
                    dataAddress = AddressingModes.DirectPage(cpu, memory);
                    break;
                case 0x96:
                    dataAddress = AddressingModes.DirectPageY(cpu, memory);
                    break;
            }
            if (dataAddress == null) {
                //throw new AddressingModeException();
            } else {
                STX(dataAddress.address, cpu, memory);
                cpu.PC += (ushort)(dataAddress.PCP + 1);
            }
        }//2
        public static void STYByOpcode(byte opcode, CPU cpu, Memory memory) {
            AddressingModes.AddressingModeResult dataAddress = null;
            switch (opcode) {
                case 0x8C:
                    dataAddress = AddressingModes.AbsoluteData(cpu, memory);
                    break;
                case 0x84:
                    dataAddress = AddressingModes.DirectPage(cpu, memory);
                    break;
                case 0x94:
                    dataAddress = AddressingModes.DirectPageX(cpu, memory);
                    break;
            }
            if (dataAddress == null) {
                //throw new AddressingModeException();
            } else {
                STY(dataAddress.address, cpu, memory);
                cpu.PC += (ushort)(dataAddress.PCP + 1);
            }
        }//2
        public static void STZByOpcode(byte opcode, CPU cpu, Memory memory) {
            AddressingModes.AddressingModeResult dataAddress = null;
            switch (opcode) {
                case 0x9C:
                    dataAddress = AddressingModes.AbsoluteData(cpu, memory);
                    break;
                case 0x64:
                    dataAddress = AddressingModes.DirectPage(cpu, memory);
                    break;
                case 0x9E:
                    dataAddress = AddressingModes.AbsoluteX(cpu, memory);
                    break;
                case 0x74:
                    dataAddress = AddressingModes.DirectPageX(cpu, memory);
                    break;
            }
            if (dataAddress == null) {
                //throw new AddressingModeException();
            } else {
                STZ(dataAddress.address, cpu, memory);
                cpu.PC += (ushort)(dataAddress.PCP + 1);
            }
        }//not following any scheme
        public static void TRBByOpcode(byte opcode, CPU cpu, Memory memory) {
            AddressingModes.AddressingModeResult dataAddress = null;
            switch (opcode) {
                case 0x1C:
                    dataAddress = AddressingModes.AbsoluteData(cpu, memory);
                    break;
                case 0x14:
                    dataAddress = AddressingModes.DirectPage(cpu, memory);
                    break;
            }
            if(dataAddress == null) {
                //throw new AddressingModeException();
            } else {
                TRB(dataAddress.address, cpu, memory);
                cpu.PC += (ushort)(dataAddress.PCP + 1);
            }


        }//Test-and-Change-Bits Instructions
        public static void TSBByOpcode(byte opcode, CPU cpu, Memory memory) {
            AddressingModes.AddressingModeResult dataAddress = null;
            switch (opcode) {
                case 0x0C:
                    dataAddress = AddressingModes.AbsoluteData(cpu, memory);
                    break;
                case 0x04:
                    dataAddress = AddressingModes.DirectPage(cpu, memory);
                    break;
            }
            if (dataAddress == null) {
                //throw new AddressingModeException();
            } else {
                TSB(dataAddress.address, cpu, memory);
                cpu.PC += (ushort)(dataAddress.PCP + 1);
            }


        }//Test-and-Change-Bits Instructions

        private static void ADC(byte[] operands, CPU cpu) {

            if (!cpu.ARS) {
                //16-bit addition
                byte low = operands[0];
                byte high = operands[1];

                if (cpu.decimalMode) {
                    //decimal addition
                    byte decimal0 = (byte)(low & 0xF);
                    byte decimal1 = (byte)((low >> 4) & 0xF);
                    byte decimal2 = (byte)((high) & 0xF);
                    byte decimal3 = (byte)((high >> 4) & 0xF);
                    ushort hex = (ushort)(decimal0 + decimal1 * 10 + decimal2 * 100 + decimal3 * 1000);
                    byte adecimal0 = (byte)(cpu.A & 0xF);
                    byte adecimal1 = (byte)((cpu.A >> 4) & 0xF);
                    byte adecimal2 = (byte)((cpu.A >> 8) & 0xF);
                    byte adecimal3 = (byte)((cpu.A >> 12) & 0xF);
                    ushort ahex = (ushort)(adecimal0 + adecimal1 * 10 + adecimal2 * 100 + adecimal3 * 1000);
                    ushort hexResult = (ushort)(ahex + hex + (cpu.carry ? 1 : 0));

                    if (hexResult >= 10000) {
                        cpu.carry = true;
                        hexResult %= 10000;
                    } else {
                        cpu.carry = false;
                    }

                    ushort rhex0 = (ushort)(hexResult & 0xF);
                    ushort rhex1 = (ushort)(hexResult & 0xF0);
                    ushort rhex2 = (ushort)(hexResult & 0xF00);
                    ushort rhex3 = (ushort)(hexResult & 0xF000);

                    //first Hex
                    byte rdecimal0 = (byte)(rhex0 % 10);
                    byte rdecimal1 = (byte)(rhex0 / 10);
                    byte rdecimal2 = 0;
                    byte rdecimal3 = 0;

                    for (int i = 0; i <= 2; i++) {
                        ushort rhexi = 0;
                        switch (i) {
                            case 0:
                                rhexi = rhex1;
                                break;
                            case 1:
                                rhexi = rhex2;
                                break;
                            case 2:
                                rhexi = rhex3;
                                break;
                        }
                        //remaining Hex's
                        rdecimal0 += (byte)(rhexi % 10);
                        rdecimal1 += (byte)((rhexi / 10) % 10);
                        rdecimal2 += (byte)((rhexi / 100) % 10);
                        rdecimal3 += (byte)((rhexi / 1000) % 10);

                        rdecimal3 += (byte)(rdecimal2 / 10);
                        rdecimal2 += (byte)(rdecimal1 / 10);
                        rdecimal1 += (byte)(rdecimal0 / 10);

                        rdecimal0 %= 10;
                        rdecimal1 %= 10;
                        rdecimal2 %= 10;
                        rdecimal3 %= 10;
                    }
                    ushort newAcc = (ushort)((rdecimal3 << 12) + (rdecimal2 << 8) + (rdecimal1 << 4) + rdecimal0);
                    cpu.A = newAcc;
                    cpu.zero = hexResult == 0;
                    cpu.overflow = false;

                    cpu.negative = (cpu.A >> 15) == 1 ? true : false;
                } else {
                    short signedOld = (short)cpu.A;
                    //hexadecimal addition
                    int result = cpu.A + (low + (high << 8)) + (cpu.carry ? 1 : 0);
                    if (result >= 0x10000) {
                        result %= 0x10000;
                        cpu.carry = true;
                    } else {
                        cpu.carry = false;
                    }
                    cpu.zero = result == 0;
                    cpu.A = (ushort)(result);
                    short signedNew = (short)cpu.A;
                    cpu.overflow = (signedNew < signedOld);

                    cpu.negative = cpu.A >> 15 == 1;

                }

            } else {
                //8-bit addition
                byte operand = operands[0];

                if (cpu.decimalMode) {
                    //decimal addition
                    byte oDecimal0 = (byte)(operand & 0xF);
                    byte oDecimal1 = (byte)(operand >> 4);
                    byte oHex = (byte)(oDecimal0 + oDecimal1 * 10);
                    byte aDecimal0 = (byte)(cpu.ALow & 0xF);
                    byte aDecimal1 = (byte)(cpu.ALow >> 4);
                    byte aHex = (byte)(aDecimal0 + aDecimal1 * 10);
                    byte hexResult = (byte)(aHex + oHex + (cpu.carry ? 1 : 0));

                    if (hexResult >= 100) {
                        cpu.carry = true;
                        hexResult %= 100;
                    } else {
                        cpu.carry = false;
                    }

                    byte rhex0 = (byte)(hexResult & 0xF);
                    byte rhex1 = (byte)(hexResult & 0xF0);

                    byte rdecimal0 = (byte)(rhex0 % 10);
                    byte rdecimal1 = (byte)(rhex0 / 10);

                    rdecimal0 += (byte)(rhex1 % 10);
                    rdecimal1 += (byte)((rhex1 / 10) % 10);

                    rdecimal1 += (byte)(rdecimal0 / 10);

                    rdecimal0 %= 10;
                    rdecimal1 %= 10;

                    byte newAcc = (byte)((rdecimal1 << 4) + rdecimal0);
                    cpu.ALow = newAcc;
                    cpu.zero = hexResult == 0;

                    cpu.overflow = false;

                    cpu.negative = (cpu.ALow >> 7) == 1 ? true : false;



                } else {
                    //hexadecimal addition
                    short signedOld = (short)cpu.ALow;
                    ushort result = (ushort)(operand + cpu.ALow + (cpu.carry ? 1 : 0));
                    if (result >= 0x100) {
                        result %= 0x100;
                        cpu.carry = true;
                    } else {
                        cpu.carry = false;
                    }
                    cpu.zero = result == 0;
                    cpu.ALow = (byte)(result);
                    short signedNew = (short)cpu.ALow;
                    cpu.overflow = signedNew < signedOld;

                    cpu.negative = cpu.A >> 7 == 1;
                }
            }
        }
        private static void AND(byte[] operands, CPU cpu) {
            if (!cpu.ARS) {
                //16-bit AND
                byte low = operands[0];
                byte high = operands[1];
                ushort ander = (ushort)(low + (high << 8));
                cpu.A &= ander;
                cpu.zero = cpu.A == 0;
                cpu.negative = (cpu.A >> 15) == 1;
            } else {
                //8-bit AND
                cpu.ALow &= operands[0];
                cpu.zero = cpu.ALow == 0;
                cpu.negative = (cpu.ALow >> 7) == 1;
            }
        }

        private static void ASLA(CPU cpu) {
            if (!cpu.ARS) {
                cpu.carry = cpu.A >> 15 == 1;
                cpu.A <<= 1;
                cpu.zero = cpu.A == 0;
                cpu.negative = cpu.A >> 15 == 1;
            } else {
                cpu.carry = cpu.ALow >> 7 == 1;
                cpu.ALow <<= 1;
                cpu.zero = cpu.ALow == 0;
                cpu.negative = cpu.ALow >> 7 == 1;
            }

        }
        private static void ASLM(int dataAddress, CPU cpu, Memory memory) {
            if (!cpu.ARS) {
                byte low = memory[dataAddress];
                byte high = memory[dataAddress + 1];
                ushort concat = (ushort)(low + (high << 8));
                cpu.carry = concat >> 15 == 1;
                concat <<= 1;
                cpu.zero = concat == 0;
                cpu.negative = concat >> 15 == 1;
                memory[dataAddress] = (byte)concat;
                memory[dataAddress + 1] = (byte)(concat >> 8);
            } else {
                byte low = memory[dataAddress];
                cpu.carry = low >> 7 == 1;
                low <<= 1;
                cpu.zero = low == 0;
                cpu.negative = low >> 7 == 1;
                memory[dataAddress] = low;
            }
        }

        private static void branchBasedOnFlag(CPU cpu, Memory memory, Boolean flag) {
            AddressingModes.AddressingModeResult addressing = AddressingModes.ProgramCounterRelative(cpu, memory);
            if (flag) {
                int address = addressing.address;
                cpu.PBR = (byte)(address >> 16);
                cpu.PC = (ushort)(address & 0xFFFF);
            } else {
                cpu.PC += (ushort)(addressing.PCP + 1);
            }
        }

        public static void BCC(CPU cpu, Memory memory) {
            branchBasedOnFlag(cpu, memory, !cpu.carry);
        }
        public static void BCS(CPU cpu, Memory memory) {
            branchBasedOnFlag(cpu, memory, cpu.carry);
        }
        public static void BEQ(CPU cpu, Memory memory) {
            branchBasedOnFlag(cpu, memory, cpu.zero);
        }

        private static void BIT(byte[] operands, CPU cpu, Boolean immediate) {
            if (!cpu.ARS) {
                byte low = operands[0];
                byte high = operands[1];
                ushort concat = (ushort)(low + (high << 8));
                if (!immediate) {
                    cpu.negative = (high >> 7) == 1;
                    cpu.overflow = ((high >> 6) & 1) == 1;
                }
                cpu.zero = (cpu.A & concat) == 0;
            } else {
                byte low = operands[0];
                if (!immediate) {
                    cpu.negative = (low >> 7) == 1;
                    cpu.overflow = ((low >> 6) & 1) == 1;
                }
                cpu.zero = (cpu.ALow & low) == 0;
            }
        }

        public static void BMI(CPU cpu, Memory memory) {
            branchBasedOnFlag(cpu, memory, cpu.negative);
        }
        public static void BNE(CPU cpu, Memory memory) {
            branchBasedOnFlag(cpu, memory, !cpu.zero);
        }
        public static void BPL(CPU cpu, Memory memory) {
            branchBasedOnFlag(cpu, memory, !cpu.negative);
        }
        public static void BRA(CPU cpu, Memory memory) {
            branchBasedOnFlag(cpu, memory, true);
        }

        public static void BRK(CPU cpu, Memory memory) {
            if (cpu.emulation) {
                cpu.PC += 2;
                cpu.pushByte(cpu.PCHigh, memory);
                cpu.pushByte(cpu.PCLow, memory);
                cpu.pushByte((byte)(cpu.P | (1 << 4)), memory);
                cpu.IRQMask = true;
                cpu.PCLow = memory[0xFFFE];
                cpu.PCHigh = memory[0xFFFF];
                cpu.D = 0;
            } else {
                cpu.pushByte(cpu.PBR, memory);
                cpu.PC += 2;
                cpu.pushByte(cpu.PCHigh, memory);
                cpu.pushByte(cpu.PCLow, memory);
                cpu.pushByte(cpu.P, memory);
                cpu.IRQMask = true;
                cpu.PBR = 0;
                cpu.PCLow = memory[0xFFE6];
                cpu.PCHigh = memory[0xFFE7];
                cpu.decimalMode = false;
            }
        }

        public static void BRL(CPU cpu, Memory memory) {
            AddressingModes.AddressingModeResult addressing = AddressingModes.ProgramCounterRelativeLong(cpu, memory);
            int address = addressing.address;
            cpu.PBR = (byte)(address >> 16);
            cpu.PC = (ushort)(address & 0xFFFF);
        }
        public static void BVC(CPU cpu, Memory memory) {
            branchBasedOnFlag(cpu, memory, !cpu.overflow);
        }
        public static void BVS(CPU cpu, Memory memory) {
            branchBasedOnFlag(cpu, memory, cpu.overflow);
        }

        public static void CLC(CPU cpu, Memory memory) {
            cpu.carry = false;
            cpu.PC++;
        }
        public static void CLD(CPU cpu, Memory memory) {
            cpu.decimalMode = false;
            cpu.PC++;
        }
        public static void CLI(CPU cpu, Memory memory) {
            cpu.IRQMask = false;
            cpu.PC++;
        }
        public static void CLV(CPU cpu, Memory memory) {
            cpu.overflow = false;
            cpu.PC++;
        }

        private static void CMP(byte[] operands, CPU cpu) {
            if (!cpu.ARS) {
                byte low = operands[0];
                byte high = operands[1];
                ushort concat = (ushort)(low + (high << 8));
                ushort result = (ushort)(cpu.A - concat);
                cpu.carry = result > cpu.A;
                cpu.zero = result == 0;
                cpu.negative = result >> 15 == 1;
            } else {
                byte operand = operands[0];
                byte result = (byte)(cpu.ALow - operand);
                cpu.carry = result > cpu.ALow;
                cpu.zero = result == 0;
                cpu.negative = result >> 7 == 1;
            }
        }

        public static void COP(CPU cpu, Memory memory) {
            if (cpu.emulation) {
                cpu.PC += 2;
                cpu.pushByte(cpu.PCHigh, memory);
                cpu.pushByte(cpu.PCLow, memory);
                cpu.pushByte(cpu.P, memory);
                cpu.IRQMask = true;
                cpu.PCLow = memory[0xFFF4];
                cpu.PCHigh = memory[0xFFF5];
                cpu.decimalMode = false;
            } else {
                cpu.pushByte(cpu.PBR, memory);
                cpu.PC += 2;
                cpu.pushByte(cpu.PCHigh, memory);
                cpu.pushByte(cpu.PCLow, memory);
                cpu.pushByte(cpu.P, memory);
                cpu.IRQMask = true;
                cpu.PBR = 0;
                cpu.PCLow = memory[0xFFE4];
                cpu.PCHigh = memory[0xFFE5];
                cpu.decimalMode = false;
            }
        }

        private static void CPX(byte[] operands, CPU cpu) {
            if (!cpu.IRS) {
                byte low = operands[0];
                byte high = operands[1];
                ushort concat = (ushort)(low + (high << 8));
                ushort result = (ushort)(cpu.X - concat);
                cpu.carry = result > cpu.X;
                cpu.zero = result == 0;
                cpu.negative = result >> 15 == 1;
            } else {
                byte operand = operands[0];
                byte result = (byte)(cpu.XLow - operand);
                cpu.carry = result > cpu.XLow;
                cpu.zero = result == 0;
                cpu.negative = result >> 7 == 1;
            }
        }
        private static void CPY(byte[] operands, CPU cpu) {
            if (!cpu.IRS) {
                byte low = operands[0];
                byte high = operands[1];
                ushort concat = (ushort)(low + (high << 8));
                ushort result = (ushort)(cpu.Y - concat);
                cpu.carry = result > cpu.Y;
                cpu.zero = result == 0;
                cpu.negative = result >> 15 == 1;
            } else {
                byte operand = operands[0];
                byte result = (byte)(cpu.YLow - operand);
                cpu.carry = result > cpu.YLow;
                cpu.zero = result == 0;
                cpu.negative = result >> 7 == 1;
            }
        }

        private static void DECA(CPU cpu) {
            if (!cpu.ARS) {
                cpu.A--;
                cpu.negative = cpu.A >> 15 == 1;
                cpu.zero = cpu.A == 0;
            } else {
                cpu.ALow--;
                cpu.negative = cpu.ALow >> 7 == 1;
                cpu.zero = cpu.ALow == 0;
            }
        }
        private static void DECM(int dataAddress, CPU cpu, Memory memory) {
            if (!cpu.ARS) {
                byte low = memory[dataAddress];
                byte high = memory[dataAddress + 1];
                ushort concat = (ushort)(low + (high << 8));
                concat--;
                cpu.negative = concat >> 15 == 1;
                cpu.zero = concat == 0;
                memory[dataAddress] = (byte)concat;
                memory[dataAddress + 1] = (byte)(concat >> 8);
            } else {
                byte concat = memory[dataAddress];
                concat--;
                cpu.negative = concat >> 7 == 1;
                cpu.zero = concat == 0;
                memory[dataAddress] = (byte)concat;
            }
        }

        public static void DEX(CPU cpu) {
            if (!cpu.IRS) {
                cpu.X--;
                cpu.negative = cpu.X >> 15 == 1;
                cpu.zero = cpu.X == 0;
            } else {
                cpu.XLow--;
                cpu.negative = cpu.XLow >> 7 == 1;
                cpu.zero = cpu.XLow == 0;
            }
            cpu.PC++;
        }
        public static void DEY(CPU cpu) {
            if (!cpu.IRS) {
                cpu.Y--;
                cpu.negative = cpu.Y >> 15 == 1;
                cpu.zero = cpu.Y == 0;
            } else {
                cpu.YLow--;
                cpu.negative = cpu.YLow >> 7 == 1;
                cpu.zero = cpu.YLow == 0;
            }
            cpu.PC++;
        }

        private static void EOR(byte[] operands, CPU cpu) {
            if (!cpu.ARS) {
                byte low = operands[0];
                byte high = operands[1];
                ushort concat = (ushort)(low + (high << 8));
                cpu.A ^= concat;
                cpu.negative = cpu.A >> 15 == 1;
                cpu.zero = cpu.A == 0;
            } else {
                byte data = operands[0];
                cpu.ALow ^= data;
                cpu.negative = cpu.ALow >> 7 == 1;
                cpu.zero = cpu.ALow == 0;
            }
        }

        private static void INCA(CPU cpu) {
            if (!cpu.ARS) {
                cpu.A--;
                cpu.negative = cpu.A >> 15 == 1;
                cpu.zero = cpu.A == 0;
            } else {
                cpu.ALow--;
                cpu.negative = cpu.ALow >> 7 == 1;
                cpu.zero = cpu.ALow == 0;
            }
        }
        private static void INCM(int dataAddress, CPU cpu, Memory memory) {
            if (!cpu.ARS) {
                byte low = memory[dataAddress];
                byte high = memory[dataAddress + 1];
                ushort concat = (ushort)(low + (high << 8));
                concat++;
                cpu.negative = concat >> 15 == 1;
                cpu.zero = concat == 0;
                memory[dataAddress] = (byte)concat;
                memory[dataAddress + 1] = (byte)(concat >> 8);
            } else {
                byte concat = memory[dataAddress];
                concat++;
                cpu.negative = concat >> 7 == 1;
                cpu.zero = concat == 0;
                memory[dataAddress] = (byte)concat;
            }
        }

        public static void INX(CPU cpu) {
            if (!cpu.IRS) {
                cpu.X++;
                cpu.negative = cpu.X >> 15 == 1;
                cpu.zero = cpu.X == 0;
            } else {
                cpu.XLow++;
                cpu.negative = cpu.XLow >> 7 == 1;
                cpu.zero = cpu.XLow == 0;
            }
            cpu.PC++;
        }
        public static void INY(CPU cpu) {
            if (!cpu.IRS) {
                cpu.Y++;
                cpu.negative = cpu.Y >> 15 == 1;
                cpu.zero = cpu.Y == 0;
            } else {
                cpu.YLow++;
                cpu.negative = cpu.YLow >> 7 == 1;
                cpu.zero = cpu.YLow == 0;
            }
            cpu.PC++;
        }

        private static void JMP(int address, CPU cpu) {
            cpu.PBR = (byte)(address >> 16);
            cpu.PC = (ushort)(address & 0xFFFF);
        }

        private static void JSR(int address, CPU cpu, Memory memory) {
            cpu.PC += 2;
            cpu.pushByte(cpu.PCHigh, memory);
            cpu.pushByte(cpu.PCLow, memory);
            cpu.PC = (ushort)(address);
        }
        private static void JSL(int address, CPU cpu, Memory memory) {
            cpu.PC += 3;
            cpu.pushByte(cpu.PBR, memory);
            cpu.pushByte(cpu.PCHigh, memory);
            cpu.pushByte(cpu.PCLow, memory);
            cpu.PBR = (byte)(address >> 16);
            cpu.PC = (ushort)(address);
        }

        private static void LDA(byte[] operands, CPU cpu) {
            if (!cpu.ARS) {
                ushort concat = (ushort)(operands[0] + (operands[1] << 8));
                cpu.A = concat;
                cpu.negative = cpu.A >> 15 == 1;
                cpu.zero = cpu.A == 0;
            } else {
                cpu.ALow = operands[0];
                cpu.negative = cpu.ALow >> 7 == 1;
                cpu.zero = cpu.ALow == 0;
            }
        }
        private static void LDX(byte[] operands, CPU cpu) {
            if (!cpu.IRS) {
                ushort concat = (ushort)(operands[0] + (operands[1] << 8));
                cpu.X = concat;
                cpu.negative = cpu.X >> 15 == 1;
                cpu.zero = cpu.X == 0;
            } else {
                cpu.XLow = operands[0];
                cpu.negative = cpu.XLow >> 7 == 1;
                cpu.zero = cpu.XLow == 0;
            }
        }
        private static void LDY(byte[] operands, CPU cpu) {
            if (!cpu.IRS) {
                ushort concat = (ushort)(operands[0] + (operands[1] << 8));
                cpu.Y = concat;
                cpu.negative = cpu.Y >> 15 == 1;
                cpu.zero = cpu.Y == 0;
            } else {
                cpu.YLow = operands[0];
                cpu.negative = cpu.YLow >> 7 == 1;
                cpu.zero = cpu.YLow == 0;
            }
        }

        private static void LSRA(CPU cpu) {
            if (!cpu.ARS) {
                cpu.carry = (cpu.A & 1) == 1;
                cpu.negative = false;
                cpu.A >>= 1;
                cpu.zero = cpu.A == 0;
            } else {
                cpu.carry = (cpu.ALow & 1) == 1;
                cpu.negative = false;
                cpu.ALow >>= 1;
                cpu.zero = cpu.ALow == 0;
            }
        }
        private static void LSRM(int address, CPU cpu, Memory memory) {
            if (!cpu.ARS) {
                ushort concat = (ushort)(memory[address] + (memory[address + 1] << 8));
                cpu.carry = (concat & 1) == 1;
                cpu.negative = false;
                concat >>= 1;
                cpu.zero = concat == 0;
                memory[address] = (byte)concat;
                memory[address + 1] = (byte)(concat >> 8);
            } else {
                byte data = memory[address];
                cpu.carry = (data & 1) == 1;
                cpu.negative = false;
                data >>= 1;
                cpu.zero = data == 0;
                memory[address] = data;
            }
        }

        public static void MVN(CPU cpu, Memory memory) {
            AddressingModes.AddressingModeResult operands = AddressingModes.ImmediateFlag(cpu, false);
            if (operands == null) {
                //throw new AddressingModeException();
            } else {
                byte destBank = memory[operands.address];
                byte srcBank = memory[operands.address + 1];
                while (cpu.A != 0xFFFF) {
                    memory[(destBank << 16) + cpu.Y] = memory[(srcBank << 16) + cpu.X];
                    cpu.Y++; cpu.X++; cpu.A--;
                }
                cpu.DBR = destBank;
            }
            cpu.PC += (ushort)(operands.PCP + 1);
        }
        public static void MVP(CPU cpu, Memory memory) {
            AddressingModes.AddressingModeResult operands = AddressingModes.ImmediateFlag(cpu, false);
            if (operands == null) {
                //throw new AddressingModeException();
            } else {
                byte destBank = memory[operands.address];
                byte srcBank = memory[operands.address + 1];
                while (cpu.A != 0xFFFF) {
                    memory[(destBank << 16) + cpu.Y] = memory[(srcBank << 16) + cpu.X];
                    cpu.Y--; cpu.X--; cpu.A--;
                }
                cpu.DBR = destBank;
            }
            cpu.PC += (ushort)(operands.PCP + 1);
        }

        public static void NOP(CPU cpu) {
            cpu.PC++;
        }

        private static void ORA(byte[] operands, CPU cpu) {
            if (!cpu.ARS) {
                //16-bit OR
                byte low = operands[0];
                byte high = operands[1];
                ushort orrer = (ushort)(low + (high << 8));
                cpu.A |= orrer;
                cpu.zero = cpu.A == 0;
                cpu.negative = (cpu.A >> 15) == 1;
            } else {
                //8-bit OR
                cpu.ALow |= operands[0];
                cpu.zero = cpu.ALow == 0;
                cpu.negative = (cpu.ALow >> 7) == 1;
            }
        }

        public static void PEA(CPU cpu, Memory memory) {
            AddressingModes.AddressingModeResult operands = AddressingModes.ImmediateFlag(cpu, false);
            if (operands == null) {
                //throw new AddressingModeException();
            } else {
                byte low = memory[operands.address];
                byte high = memory[operands.address + 1];
                cpu.pushByte(high, memory);
                cpu.pushByte(low, memory);
            }
            cpu.PC += (ushort)(operands.PCP + 1);
        }
        public static void PEI(CPU cpu, Memory memory) {
            AddressingModes.AddressingModeResult operands = AddressingModes.DirectPageIndirect(cpu, memory);
            if (operands == null) {
                //throw new AddressingModeException();
            } else {
                byte low = memory[operands.address];
                byte high = memory[operands.address + 1];
                cpu.pushByte(high, memory);
                cpu.pushByte(low, memory);
            }
            cpu.PC += (ushort)(operands.PCP + 1);
        }
        public static void PER(CPU cpu, Memory memory) {
            AddressingModes.AddressingModeResult operands = AddressingModes.ProgramCounterRelativeLong(cpu, memory);
            if (operands == null) {
                //throw new AddressingModeException();
            } else {
                byte low = memory[operands.address];
                byte high = memory[operands.address + 1];
                cpu.pushByte(high, memory);
                cpu.pushByte(low, memory);
            }
            cpu.PC += (ushort)(operands.PCP + 1);
        }

        public static void PHA(CPU cpu, Memory memory) {
            if (!cpu.ARS) {
                cpu.pushByte(cpu.AHigh, memory);
                cpu.pushByte(cpu.ALow, memory);
            } else {
                cpu.pushByte(cpu.ALow, memory);
            }
            cpu.PC++;
        }
        public static void PHB(CPU cpu, Memory memory) {
            cpu.pushByte(cpu.PBR, memory);
            cpu.PC++;
        }
        public static void PHD(CPU cpu, Memory memory) {
            cpu.pushByte(cpu.DHigh, memory);
            cpu.pushByte(cpu.DLow, memory);
            cpu.PC++;
        }
        public static void PHK(CPU cpu, Memory memory) {
            cpu.pushByte(cpu.DBR, memory);
            cpu.PC++;
        }
        public static void PHP(CPU cpu, Memory memory) {
            cpu.pushByte(cpu.P, memory);
            cpu.PC++;
        }
        public static void PHX(CPU cpu, Memory memory) {
            if (!cpu.IRS) {
                cpu.pushByte(cpu.XHigh, memory);
                cpu.pushByte(cpu.XLow, memory);
            } else {
                cpu.pushByte(cpu.XLow, memory);
            }
            cpu.PC++;
        }
        public static void PHY(CPU cpu, Memory memory) {
            if (!cpu.IRS) {
                cpu.pushByte(cpu.YHigh, memory);
                cpu.pushByte(cpu.YLow, memory);
            } else {
                cpu.pushByte(cpu.YLow, memory);
            }
            cpu.PC++;
        }

        public static void PLA(CPU cpu, Memory memory) {
            if (!cpu.ARS) {
                cpu.ALow = cpu.pullByte(memory);
                cpu.AHigh = cpu.pullByte(memory);
                cpu.negative = cpu.A >> 15 == 1;
                cpu.zero = cpu.A == 0;
            } else {
                cpu.ALow = cpu.pullByte(memory);
                cpu.negative = cpu.ALow >> 7 == 1;
                cpu.zero = cpu.ALow == 0;
            }
            cpu.PC++;
        }
        public static void PLB(CPU cpu, Memory memory) {
            cpu.PBR = cpu.pullByte(memory);
            cpu.negative = cpu.PBR >> 7 == 1;
            cpu.zero = cpu.PBR == 0;
            cpu.PC++;
        }
        public static void PLD(CPU cpu, Memory memory) {
            cpu.DLow = cpu.pullByte(memory);
            cpu.DHigh = cpu.pullByte(memory);
            cpu.negative = cpu.D >> 15 == 1;
            cpu.zero = cpu.D == 0;
            cpu.PC++;
        }
        public static void PLP(CPU cpu, Memory memory) {
            cpu.P = cpu.pullByte(memory);
            cpu.PC++;
        }
        public static void PLX(CPU cpu, Memory memory) {
            if (!cpu.IRS) {
                cpu.XLow = cpu.pullByte(memory);
                cpu.XHigh = cpu.pullByte(memory);
                cpu.negative = cpu.X >> 15 == 1;
                cpu.zero = cpu.X == 0;
            } else {
                cpu.XLow = cpu.pullByte(memory);
                cpu.negative = cpu.XLow >> 7 == 1;
                cpu.zero = cpu.XLow == 0;
            }
            cpu.PC++;
        }
        public static void PLY(CPU cpu, Memory memory) {
            if (!cpu.IRS) {
                cpu.YLow = cpu.pullByte(memory);
                cpu.YHigh = cpu.pullByte(memory);
                cpu.negative = cpu.Y >> 15 == 1;
                cpu.zero = cpu.Y == 0;
            } else {
                cpu.YLow = cpu.pullByte(memory);
                cpu.negative = cpu.YLow >> 7 == 1;
                cpu.zero = cpu.YLow == 0;
            }
            cpu.PC++;
        }

        public static void REP(CPU cpu, Memory memory) {
            int address = AddressingModes.ImmediateFlag(cpu, true).address;
            byte operand = memory[address];
            if (cpu.emulation) {
                byte Pold = cpu.P;
                cpu.P &= (byte)(~operand);
                cpu.P &= 0xCF;
                cpu.P |= (byte)(Pold & 0x30);
            } else {
                cpu.P &= (byte)(~operand);
            }
            cpu.PC += 2;
        }

        private static void ROLA(CPU cpu) {
            if (!cpu.ARS) {
                Boolean oldCarry = cpu.carry;
                cpu.carry = cpu.A >> 15 == 1;
                cpu.A <<= 1;
                cpu.A |= (ushort)(oldCarry ? 1 : 0);
                cpu.negative = cpu.A >> 15 == 1;
                cpu.zero = cpu.A == 0;
            } else {
                Boolean oldCarry = cpu.carry;
                cpu.carry = cpu.ALow >> 7 == 1;
                cpu.ALow <<= 1;
                cpu.ALow |= (byte)(oldCarry ? 1 : 0);
                cpu.negative = cpu.ALow >> 7 == 1;
                cpu.zero = cpu.ALow == 0;
            }
        }
        private static void ROLM(int address, CPU cpu, Memory memory) {
            if (!cpu.ARS) {
                ushort concat = (ushort)(memory[address] + (memory[address + 1] << 8));

                Boolean oldCarry = cpu.carry;
                cpu.carry = concat >> 15 == 1;
                concat <<= 1;
                concat |= (ushort)(oldCarry ? 1 : 0);
                cpu.negative = concat >> 15 == 1;
                cpu.zero = concat == 0;

                memory[address] = (byte)concat;
                memory[address + 1] = (byte)(concat >> 8);
            } else {
                byte data = memory[address];

                Boolean oldCarry = cpu.carry;
                cpu.carry = data >> 7 == 1;
                data <<= 1;
                data |= (byte)(oldCarry ? 1 : 0);
                cpu.negative = data >> 7 == 1;
                cpu.zero = data == 0;

                memory[address] = data;
            }
        }

        private static void RORA(CPU cpu) {
            if (!cpu.ARS) {
                Boolean oldCarry = cpu.carry;
                cpu.carry = (cpu.A & 1) == 1;
                cpu.A >>= 1;
                cpu.A |= (ushort)((oldCarry ? 1 : 0) << 15);
                cpu.negative = oldCarry;
                cpu.zero = cpu.A == 0;
            } else {
                Boolean oldCarry = cpu.carry;
                cpu.carry = (cpu.ALow & 1) == 1;
                cpu.ALow >>= 1;
                cpu.ALow |= (byte)((oldCarry ? 1 : 0 << 7));
                cpu.negative = oldCarry;
                cpu.zero = cpu.ALow == 0;
            }
        }
        private static void RORM(int address, CPU cpu, Memory memory) {
            if (!cpu.ARS) {
                ushort concat = (ushort)(memory[address] + (memory[address + 1] << 8));

                Boolean oldCarry = cpu.carry;
                cpu.carry = (concat & 1) == 1;
                concat >>= 1;
                concat |= (ushort)((oldCarry ? 1 : 0) << 15);
                cpu.negative = oldCarry;
                cpu.zero = concat == 0;

                memory[address] = (byte)concat;
                memory[address + 1] = (byte)(concat >> 8);
            } else {
                byte data = memory[address];

                Boolean oldCarry = cpu.carry;
                cpu.carry = (data & 1) == 1;
                data >>= 1;
                data |= (byte)((oldCarry ? 1 : 0 << 7));
                cpu.negative = oldCarry;
                cpu.zero = data == 0;

                memory[address] = data;
            }
        }

        public static void RTI(CPU cpu, Memory memory) {
            if (!cpu.emulation) {
                cpu.P = cpu.pullByte(memory);
                cpu.PCLow = cpu.pullByte(memory);
                cpu.PCHigh = cpu.pullByte(memory);
                cpu.PBR = cpu.pullByte(memory);
            } else {
                cpu.P = cpu.pullByte(memory);
                cpu.PCLow = cpu.pullByte(memory);
                cpu.PCHigh = cpu.pullByte(memory);
            }
        }
        public static void RTL(CPU cpu, Memory memory) {
            cpu.PCLow = cpu.pullByte(memory);
            cpu.PCHigh = cpu.pullByte(memory);
            cpu.PBR = cpu.pullByte(memory);
            cpu.PC++;
        }
        public static void RTS(CPU cpu, Memory memory) {
            cpu.PCLow = cpu.pullByte(memory);
            cpu.PCHigh = cpu.pullByte(memory);
            cpu.PC++;
        }

        private static void SBC(byte[] operands, CPU cpu) {

            if (!cpu.ARS) {
                //16-bit addition
                byte low = operands[0];
                byte high = operands[1];

                if (cpu.decimalMode) {
                    //decimal addition
                    byte decimal0 = (byte)(low & 0xF);
                    byte decimal1 = (byte)((low >> 4) & 0xF);
                    byte decimal2 = (byte)((high) & 0xF);
                    byte decimal3 = (byte)((high >> 4) & 0xF);
                    ushort hex = (ushort)(decimal0 + decimal1 * 10 + decimal2 * 100 + decimal3 * 1000);
                    byte adecimal0 = (byte)(cpu.A & 0xF);
                    byte adecimal1 = (byte)((cpu.A >> 4) & 0xF);
                    byte adecimal2 = (byte)((cpu.A >> 8) & 0xF);
                    byte adecimal3 = (byte)((cpu.A >> 12) & 0xF);
                    ushort ahex = (ushort)(adecimal0 + adecimal1 * 10 + adecimal2 * 100 + adecimal3 * 1000);
                    short hexResult = (short)(ahex - hex - (!cpu.carry ? 1 : 0));

                    if (hexResult <= -10000) {
                        cpu.carry = false;
                        hexResult %= 10000;
                    } else {
                        cpu.carry = true;
                    }
                    if (hexResult < 0)
                        hexResult = Math.Abs(hexResult);


                    ushort rhex0 = (ushort)(hexResult & 0xF);
                    ushort rhex1 = (ushort)(hexResult & 0xF0);
                    ushort rhex2 = (ushort)(hexResult & 0xF00);
                    ushort rhex3 = (ushort)(hexResult & 0xF000);

                    //first Hex
                    byte rdecimal0 = (byte)(rhex0 % 10);
                    byte rdecimal1 = (byte)(rhex0 / 10);
                    byte rdecimal2 = 0;
                    byte rdecimal3 = 0;

                    for (int i = 0; i <= 2; i++) {
                        ushort rhexi = 0;
                        switch (i) {
                            case 0:
                                rhexi = rhex1;
                                break;
                            case 1:
                                rhexi = rhex2;
                                break;
                            case 2:
                                rhexi = rhex3;
                                break;
                        }
                        //remaining Hex's
                        rdecimal0 += (byte)(rhexi % 10);
                        rdecimal1 += (byte)((rhexi / 10) % 10);
                        rdecimal2 += (byte)((rhexi / 100) % 10);
                        rdecimal3 += (byte)((rhexi / 1000) % 10);

                        rdecimal3 += (byte)(rdecimal2 / 10);
                        rdecimal2 += (byte)(rdecimal1 / 10);
                        rdecimal1 += (byte)(rdecimal0 / 10);

                        rdecimal0 %= 10;
                        rdecimal1 %= 10;
                        rdecimal2 %= 10;
                        rdecimal3 %= 10;
                    }
                    ushort newAcc = (ushort)((rdecimal3 << 12) + (rdecimal2 << 8) + (rdecimal1 << 4) + rdecimal0);
                    cpu.A = newAcc;
                    cpu.zero = hexResult == 0;
                    cpu.overflow = false;

                    cpu.negative = (cpu.A >> 15) == 1 ? true : false;
                } else {
                    short signedOld = (short)cpu.A;
                    //hexadecimal addition
                    int result = cpu.A - (low - (high << 8)) - (!cpu.carry ? 1 : 0);
                    if (result <= -0x10000) {
                        result %= 0x10000;
                        cpu.carry = false;
                    } else {
                        cpu.carry = true;
                    }

                    if (result < 0)
                        result = Math.Abs(result);

                    cpu.zero = result == 0;
                    cpu.A = (ushort)(result);
                    short signedNew = (short)cpu.A;
                    cpu.overflow = (signedNew > signedOld);

                    cpu.negative = cpu.A >> 15 == 1;

                }

            } else {
                //8-bit addition
                byte operand = operands[0];

                if (cpu.decimalMode) {
                    //decimal addition
                    byte oDecimal0 = (byte)(operand & 0xF);
                    byte oDecimal1 = (byte)(operand >> 4);
                    byte oHex = (byte)(oDecimal0 + oDecimal1 * 10);
                    byte aDecimal0 = (byte)(cpu.ALow & 0xF);
                    byte aDecimal1 = (byte)(cpu.ALow >> 4);
                    byte aHex = (byte)(aDecimal0 + aDecimal1 * 10);
                    sbyte hexResult = (sbyte)(aHex - oHex - (!cpu.carry ? 1 : 0));

                    if (hexResult <= -100) {
                        cpu.carry = false;
                        hexResult %= 100;
                    } else {
                        cpu.carry = true;
                    }

                    if (hexResult < 0)
                        hexResult = Math.Abs(hexResult);

                    byte rhex0 = (byte)(hexResult & 0xF);
                    byte rhex1 = (byte)(hexResult & 0xF0);

                    byte rdecimal0 = (byte)(rhex0 % 10);
                    byte rdecimal1 = (byte)(rhex0 / 10);

                    rdecimal0 += (byte)(rhex1 % 10);
                    rdecimal1 += (byte)((rhex1 / 10) % 10);

                    rdecimal1 += (byte)(rdecimal0 / 10);

                    rdecimal0 %= 10;
                    rdecimal1 %= 10;

                    byte newAcc = (byte)((rdecimal1 << 4) + rdecimal0);
                    cpu.ALow = newAcc;
                    cpu.zero = hexResult == 0;

                    cpu.overflow = false;

                    cpu.negative = (cpu.ALow >> 7) == 1 ? true : false;



                } else {
                    //hexadecimal addition
                    short signedOld = (short)cpu.ALow;
                    short result = (short)(operand - cpu.ALow - (!cpu.carry ? 1 : 0));
                    if (result < -0x100) {
                        result %= 0x100;
                        cpu.carry = false;
                    } else {
                        cpu.carry = true;
                    }

                    if (result < 0)
                        result = Math.Abs(result);


                    cpu.zero = result == 0;
                    cpu.ALow = (byte)(result);
                    short signedNew = (short)cpu.ALow;
                    cpu.overflow = signedNew > signedOld;

                    cpu.negative = cpu.A >> 7 == 1;
                }
            }
        }

        public static void SEC(CPU cpu, Memory memory) {
            cpu.carry = true;
            cpu.PC++;
        }
        public static void SED(CPU cpu, Memory memory) {
            cpu.decimalMode = true;
            cpu.PC++;
        }
        public static void SEI(CPU cpu, Memory memory) {
            cpu.IRQMask = true;
            cpu.PC++;
        }
        public static void SEP(CPU cpu, Memory memory) {
            int address = AddressingModes.ImmediateFlag(cpu, true).address;
            byte operand = memory[address];
            if (cpu.emulation) {
                byte Pold = cpu.P;
                cpu.P &= (byte)(~operand);
                cpu.P |= (byte)(operand);
                cpu.P &= 0xCF;
                cpu.P |= (byte)(Pold & 0x30);
            } else {
                cpu.P &= (byte)(~operand);
                cpu.P |= (byte)(operand);
            }
            cpu.PC += 2;
        }

        private static void STA(int address, CPU cpu, Memory memory) {
            if (!cpu.ARS) {
                memory[address] = cpu.ALow;
                memory[address + 1] = cpu.AHigh;
            } else {
                memory[address] = cpu.ALow;
            }
        }

        public static void STP(CPU cpu, Memory memory) { }

        private static void STX(int address, CPU cpu, Memory memory) {
            if (!cpu.IRS) {
                memory[address] = cpu.XLow;
                memory[address + 1] = cpu.XHigh;
            } else {
                memory[address] = cpu.XLow;
            }
        }
        private static void STY(int address, CPU cpu, Memory memory) {
            if (!cpu.IRS) {
                memory[address] = cpu.YLow;
                memory[address + 1] = cpu.YHigh;
            } else {
                memory[address] = cpu.YLow;
            }
        }
        private static void STZ(int address, CPU cpu, Memory memory) {
            if (!cpu.ARS) {
                memory[address] = 0;
                memory[address + 1] = 0;
            } else {
                memory[address] = 0;
            }
        }

        public static void TAX(CPU cpu) {
            if(!cpu.IRS) {
                cpu.X = cpu.A;
                cpu.negative = cpu.A >> 15 == 1;
                cpu.zero = cpu.A == 0;
            } else {
                cpu.XLow = cpu.ALow;
                cpu.negative = cpu.ALow >> 7 == 1;
                cpu.zero = cpu.ALow == 0;
            }
            cpu.PC++;
        }
        public static void TAY(CPU cpu) {
            if (!cpu.IRS) {
                cpu.Y = cpu.A;
                cpu.negative = cpu.A >> 15 == 1;
                cpu.zero = cpu.A == 0;
            } else {
                cpu.YLow = cpu.ALow;
                cpu.negative = cpu.ALow >> 7 == 1;
                cpu.zero = cpu.ALow == 0;
            }
            cpu.PC++;
        }
        public static void TCD(CPU cpu) {
            cpu.D = cpu.A;
            cpu.negative = cpu.A >> 15 == 1;
            cpu.zero = cpu.A == 0;
            cpu.PC++;
        }
        public static void TCS(CPU cpu) {
            cpu.S = cpu.A;
            if (cpu.emulation) {
                cpu.S &= 0xFF;
                cpu.S |= 0x100;
            }
            cpu.PC++;
        }
        public static void TDC(CPU cpu) {
            cpu.A = cpu.D;
            cpu.negative = cpu.D >> 15 == 1;
            cpu.zero = cpu.D == 0;
            cpu.PC++;
        }

        private static void TRB(int address, CPU cpu, Memory memory) {
            if (!cpu.ARS) {
                ushort concat = (ushort)(memory[address] + (memory[address + 1] << 8));
                ushort anded = (ushort)(~cpu.A & concat);
                memory[address] = (byte)anded;
                memory[address + 1] = (byte)(anded >> 8);
                cpu.zero = (cpu.A & concat) == 0;
            } else {
                byte concat = memory[address];
                byte anded = (byte)(~cpu.ALow & concat);
                memory[address] = (byte)anded;
                cpu.zero = (cpu.ALow & concat) == 0;
            }
        }
        private static void TSB(int address, CPU cpu, Memory memory) {
            if (!cpu.ARS) {
                ushort concat = (ushort)(memory[address] + (memory[address + 1] << 8));
                ushort orred = (ushort)(cpu.A | concat);
                memory[address] = (byte)orred;
                memory[address + 1] = (byte)(orred >> 8);
                cpu.zero = (cpu.A & concat) == 0;
            } else {
                byte concat = memory[address];
                byte orred = (byte)(cpu.ALow | concat);
                memory[address] = (byte)orred;
                cpu.zero = (cpu.ALow & concat) == 0;
            }
        }

        public static void TSC(CPU cpu) {
            cpu.A = cpu.S;
            if (cpu.emulation) {
                cpu.AHigh = 1;
            }
            cpu.negative = cpu.S >> 15 == 1;
            cpu.zero = cpu.S == 0;
            cpu.PC++;
        }
        public static void TSX(CPU cpu) {
            cpu.X = cpu.S;
            if (cpu.emulation) {
                cpu.XHigh = 1;
            }
            cpu.negative = cpu.S >> 15 == 1;
            cpu.zero = cpu.S == 0;
            cpu.PC++;
        }
        public static void TXA(CPU cpu) {
            if (!cpu.ARS) {
                cpu.A = cpu.X;
                cpu.negative = cpu.X >> 15 == 1;
                cpu.zero = cpu.X == 0;
            } else {
                cpu.ALow = cpu.XLow;
                cpu.negative = cpu.XLow >> 7 == 1;
                cpu.zero = cpu.XLow == 0;
            }
            cpu.PC++;
        }
        public static void TXS(CPU cpu) {
            if (cpu.emulation) {
                cpu.S = cpu.X;
                cpu.S &= 0xFF;
                cpu.S |= 0x100;
            } else if (cpu.IRS) {
                cpu.S = cpu.XLow;
            } else {
                cpu.S = cpu.X;
            }
            cpu.PC++;
        }
        public static void TXY(CPU cpu) {
            if (!cpu.IRS) {
                cpu.Y = cpu.X;
                cpu.negative = cpu.X >> 15 == 1;
                cpu.zero = cpu.X == 0;
            } else {
                cpu.Y = cpu.XLow;
                cpu.negative = cpu.XLow >> 7 == 1;
                cpu.zero = cpu.XLow == 0;
            }
            cpu.PC++;
        }
        public static void TYA(CPU cpu) {
            if (!cpu.ARS) {
                cpu.A = cpu.Y;
                cpu.negative = cpu.Y >> 15 == 1;
                cpu.zero = cpu.Y == 0;
            } else {
                cpu.ALow = cpu.YLow;
                cpu.negative = cpu.YLow >> 7 == 1;
                cpu.zero = cpu.YLow == 0;
            }
            cpu.PC++;
        }
        public static void TYX(CPU cpu) {
            if (!cpu.IRS) {
                cpu.X = cpu.Y;
                cpu.negative = cpu.Y >> 15 == 1;
                cpu.zero = cpu.Y == 0;
            } else {
                cpu.X = cpu.YLow;
                cpu.negative = cpu.YLow >> 7 == 1;
                cpu.zero = cpu.YLow == 0;
            }
            cpu.PC++;
        }

        public static void WAI() { }

        public static void XBA(CPU cpu) {
            byte temp = cpu.ALow;
            cpu.ALow = cpu.AHigh;
            cpu.AHigh = temp;
            cpu.negative = cpu.ALow >> 7 == 1;
            cpu.zero = cpu.ALow == 0;
            cpu.PC++;
        }
        public static void XCE(CPU cpu) {
            Boolean temp = cpu.emulation;
            if (cpu.carry) {
                cpu.setEmulation();
            } else {
                cpu.resetEmulation();
            }
            cpu.carry = temp;
            cpu.PC++;
        }

    }
}
