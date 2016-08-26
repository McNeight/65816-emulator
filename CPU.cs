using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNES_Emulator.Emulator {
    public class CPU {
        private SNES snes;
        public byte P, DBR, PBR;
        public ushort A, X, Y, D, S, PC;
        public byte ALow {
            set {
                A &= 0xFF00;
                A |= value;
            }
            get {
                return (byte)A;
            }
        }
        public byte AHigh {
            set {
                A &= 0x00FF;
                A |= (ushort)(value << 8);
            }
            get {
                return (byte)(A>>8);
            }
        }
        public byte PCLow {
            set {
                PC &= 0xFF00;
                PC |= value;
            }
            get {
                return (byte)PC;
            }
        }
        public byte PCHigh {
            set {
                PC &= 0x00FF;
                PC |= (ushort)(value << 8);
            }
            get {
                return (byte)(PC >> 8);
            }
        }
        public byte XLow {
            set {
                X &= 0xFF00;
                X |= value;
            }
            get {
                return (byte)X;
            }
        }
        public byte XHigh {
            set {
                X &= 0x00FF;
                X |= (ushort)(value << 8);
            }
            get {
                return (byte)(X >> 8);
            }
        }
        public byte YLow {
            set {
                Y &= 0xFF00;
                Y |= value;
            }
            get {
                return (byte)Y;
            }
        }
        public byte YHigh {
            set {
                Y &= 0x00FF;
                Y |= (ushort)(value << 8);
            }
            get {
                return (byte)(Y >> 8);
            }
        }
        public byte DLow {
            set {
                D &= 0xFF00;
                D |= value;
            }
            get {
                return (byte)D;
            }
        }
        public byte DHigh {
            set {
                D &= 0x00FF;
                D |= (ushort)(value << 8);
            }
            get {
                return (byte)(D >> 8);
            }
        }

        private Memory memory;
        public Boolean emulation = true;

        public CPU(SNES snes) {
            this.snes = snes;
            memory = this.snes.memory;
            reset();
        }

        public void reset() {
            PBR = 0;
            DBR = 0;
            S = 0x1FF;
            PCLow = snes.memory[0xFFFC];
            PCHigh = snes.memory[0xFFFD];
            D = 0;

            setEmulation();
        }

        public void cycle() {
            executeOPAtPC();
        }

        private void executeOPAtPC() {
            byte opcode = memory[(PBR << 16) + PC];
            ushort oldPC = PC;

            //group1
            byte firstThree = (byte)((opcode >> 5) & 7);
            switch (firstThree) {
                case 0:
                    Instructions.ORAByOpcode(opcode, this, memory);
                    if (PC != oldPC) return;
                    break;
                case 1:
                    Instructions.ANDByOpcode(opcode, this, memory);
                    if (PC != oldPC) return;
                    break;
                case 2:
                    Instructions.EORByOpcode(opcode, this, memory);
                    if (PC != oldPC) return;
                    break;
                case 3:
                    Instructions.ADCByOpcode(opcode, this, memory);
                    if (PC != oldPC) return;
                    break;
                case 4:
                    Instructions.STAByOpcode(opcode, this, memory);
                    if (PC != oldPC) return;
                    break;
                case 5:
                    Instructions.LDAByOpcode(opcode, this, memory);
                    if (PC != oldPC) return;
                    break;
                case 6:
                    Instructions.CMPByOpcode(opcode, this, memory);
                    if (PC != oldPC) return;
                    break;
                case 7:
                    Instructions.SBCByOpcode(opcode, this, memory);
                    if (PC != oldPC) return;
                    break;
            }
            //group2
            byte group2AndIndexOpcode = (byte)(opcode & ~0x1C);
            switch (group2AndIndexOpcode) {
                case 0x02:
                    Instructions.ASLByOpcode(opcode, this, memory);
                    if (PC != oldPC) return;
                    break;
                case 0xC2:
                    Instructions.DECByOpcode(opcode, this, memory);
                    if (PC != oldPC) return;
                    break;
                case 0xD2:
                    Instructions.INCByOpcode(opcode, this, memory);
                    if (PC != oldPC) return;
                    break;
                case 0x42:
                    Instructions.LSRByOpcode(opcode, this, memory);
                    if (PC != oldPC) return;
                    break;
                case 0x22:
                    Instructions.ROLByOpcode(opcode, this, memory);
                    if (PC != oldPC) return;
                    break;
                case 0x62:
                    Instructions.RORByOpcode(opcode, this, memory);
                    if (PC != oldPC) return;
                    break;
                case 0x82:
                    Instructions.STXByOpcode(opcode, this, memory);
                    if (PC != oldPC) return;
                    break;
                case 0x80:
                    Instructions.STYByOpcode(opcode, this, memory);
                    if (PC != oldPC) return;
                    break;
            }
            //index
            switch (group2AndIndexOpcode) {
                case 0xA2:
                    Instructions.LDXByOpcode(opcode, this, memory);
                    if (PC != oldPC) return;
                    break;
                case 0xA0:
                    Instructions.LDYByOpcode(opcode, this, memory);
                    if (PC != oldPC) return;
                    break;
            }
            //index compares
            byte indxCmpOp = (byte)(opcode & ~0xC);
            switch (indxCmpOp) {
                case 0xE0:
                    Instructions.CPXByOpcode(opcode, this, memory);
                    if (PC != oldPC) return;
                    break;
                case 0xC0:
                    Instructions.CPYByOpcode(opcode, this, memory);
                    if (PC != oldPC) return;
                    break;
            }
            //test and change
            byte TaCOp = (byte)(opcode & ~0x8);
            switch (indxCmpOp) {
                case 0x14:
                    Instructions.TRBByOpcode(opcode, this, memory);
                    if (PC != oldPC) return;
                    break;
                case 0x04:
                    Instructions.TSBByOpcode(opcode, this, memory);
                    if (PC != oldPC) return;
                    break;
            }

            //implied stuff
            switch (opcode) {
                case 0x90:
                    Instructions.BCC(this, memory);
                    if (PC != oldPC) return;
                    break;
                case 0xB0:
                    Instructions.BCS(this, memory);
                    if (PC != oldPC) return;
                    break;
                case 0xF0:
                    Instructions.BEQ(this, memory);
                    if (PC != oldPC) return;
                    break;
                case 0x30:
                    Instructions.BMI(this, memory);
                    if (PC != oldPC) return;
                    break;
                case 0xD0:
                    Instructions.BNE(this, memory);
                    if (PC != oldPC) return;
                    break;
                case 0x10:
                    Instructions.BPL(this, memory);
                    if (PC != oldPC) return;
                    break;
                case 0x80:
                    Instructions.BRA(this, memory);
                    if (PC != oldPC) return;
                    break;
                case 0x00:
                    Instructions.BRK(this, memory);
                    if (PC != oldPC) return;
                    break;
                case 0x82:
                    Instructions.BRL(this, memory);
                    if (PC != oldPC) return;
                    break;
                case 0x50:
                    Instructions.BVC(this, memory);
                    if (PC != oldPC) return;
                    break;
                case 0x70:
                    Instructions.BVS(this, memory);
                    if (PC != oldPC) return;
                    break;
                case 0x18:
                    Instructions.CLC(this, memory);
                    if (PC != oldPC) return;
                    break;
                case 0xD8:
                    Instructions.CLD(this, memory);
                    if (PC != oldPC) return;
                    break;
                case 0x58:
                    Instructions.CLI(this, memory);
                    if (PC != oldPC) return;
                    break;
                case 0xB8:
                    Instructions.CLV(this, memory);
                    if (PC != oldPC) return;
                    break;
                case 0x02:
                    Instructions.COP(this, memory);
                    if (PC != oldPC) return;
                    break;
                case 0xCA:
                    Instructions.DEX(this);
                    if (PC != oldPC) return;
                    break;
                case 0x88:
                    Instructions.DEY(this);
                    if (PC != oldPC) return;
                    break;
                case 0xE8:
                    Instructions.INX(this);
                    if (PC != oldPC) return;
                    break;
                case 0xC8:
                    Instructions.INY(this);
                    if (PC != oldPC) return;
                    break;
                case 0x54:
                    Instructions.MVN(this, memory);
                    if (PC != oldPC) return;
                    break;
                case 0x44:
                    Instructions.MVP(this, memory);
                    if (PC != oldPC) return;
                    break;
                case 0xEA:
                    Instructions.NOP(this);
                    if (PC != oldPC) return;
                    break;
                case 0xF4:
                    Instructions.PEA(this, memory);
                    if (PC != oldPC) return;
                    break;
                case 0xD4:
                    Instructions.PEI(this, memory);
                    if (PC != oldPC) return;
                    break;
                case 0x62:
                    Instructions.PER(this, memory);
                    if (PC != oldPC) return;
                    break;
                case 0x48:
                    Instructions.PHA(this, memory);
                    if (PC != oldPC) return;
                    break;
                case 0x8B:
                    Instructions.PHB(this, memory);
                    if (PC != oldPC) return;
                    break;
                case 0x0B:
                    Instructions.PHD(this, memory);
                    if (PC != oldPC) return;
                    break;
                case 0x4B:
                    Instructions.PHK(this, memory);
                    if (PC != oldPC) return;
                    break;
                case 0x08:
                    Instructions.PHP(this, memory);
                    if (PC != oldPC) return;
                    break;
                case 0xDA:
                    Instructions.PHX(this, memory);
                    if (PC != oldPC) return;
                    break;
                case 0x5A:
                    Instructions.PHY(this, memory);
                    if (PC != oldPC) return;
                    break;
                case 0x68:
                    Instructions.PLA(this, memory);
                    if (PC != oldPC) return;
                    break;
                case 0xAB:
                    Instructions.PLB(this, memory);
                    if (PC != oldPC) return;
                    break;
                case 0x2B:
                    Instructions.PLD(this, memory);
                    if (PC != oldPC) return;
                    break;
                case 0x28:
                    Instructions.PLP(this, memory);
                    if (PC != oldPC) return;
                    break;
                case 0xFA:
                    Instructions.PLX(this, memory);
                    if (PC != oldPC) return;
                    break;
                case 0x7A:
                    Instructions.PLY(this, memory);
                    if (PC != oldPC) return;
                    break;
                case 0xC2:
                    Instructions.REP(this, memory);
                    if (PC != oldPC) return;
                    break;
                case 0x40:
                    Instructions.RTI(this, memory);
                    if (PC != oldPC) return;
                    break;
                case 0x6B:
                    Instructions.RTL(this, memory);
                    if (PC != oldPC) return;
                    break;
                case 0x60:
                    Instructions.RTS(this, memory);
                    if (PC != oldPC) return;
                    break;
                case 0x38:
                    Instructions.SEC(this, memory);
                    if (PC != oldPC) return;
                    break;
                case 0xF8:
                    Instructions.SED(this, memory);
                    if (PC != oldPC) return;
                    break;
                case 0x78:
                    Instructions.SEI(this, memory);
                    if (PC != oldPC) return;
                    break;
                case 0xE2:
                    Instructions.SEP(this, memory);
                    if (PC != oldPC) return;
                    break;
                case 0xDB:
                    Instructions.STP(this, memory);
                    if (PC != oldPC) return;
                    break;
                case 0xAA:
                    Instructions.TAX(this);
                    if (PC != oldPC) return;
                    break;
                case 0xA8:
                    Instructions.TAY(this);
                    if (PC != oldPC) return;
                    break;
                case 0x5B:
                    Instructions.TCD(this);
                    if (PC != oldPC) return;
                    break;
                case 0x1B:
                    Instructions.TCS(this);
                    if (PC != oldPC) return;
                    break;
                case 0x7B:
                    Instructions.TDC(this);
                    if (PC != oldPC) return;
                    break;
                case 0x3B:
                    Instructions.TSC(this);
                    if (PC != oldPC) return;
                    break;
                case 0xBA:
                    Instructions.TSX(this);
                    if (PC != oldPC) return;
                    break;
                case 0x8A:
                    Instructions.TXA(this);
                    if (PC != oldPC) return;
                    break;
                case 0x9A:
                    Instructions.TXS(this);
                    if (PC != oldPC) return;
                    break;
                case 0x9B:
                    Instructions.TXY(this);
                    if (PC != oldPC) return;
                    break;
                case 0x98:
                    Instructions.TYA(this);
                    if (PC != oldPC) return;
                    break;
                case 0xBB:
                    Instructions.TYX(this);
                    if (PC != oldPC) return;
                    break;
                case 0xCB:
                    Instructions.WAI();
                    if (PC != oldPC) return;
                    break;
                case 0xEB:
                    Instructions.XBA(this);
                    if (PC != oldPC) return;
                    break;
                case 0xFB:
                    Instructions.XCE(this);
                    if (PC != oldPC) return;
                    break;
            }

            Instructions.BITByOpcode(opcode, this, memory);
            if (PC != oldPC) return;
            Instructions.STZByOpcode(opcode, this, memory);
            if (PC != oldPC) return;
            Instructions.JMPByOpcode(opcode, this, memory);
            if (PC != oldPC) return;
            Instructions.JSRByOpcode(opcode, this, memory);
            if (PC != oldPC) return;


            /*Boolean isGroup1Op = false;
            if (opcode != 0x89) {
                byte lastTwo = (byte)(opcode & 3);
                byte lastFive = (byte)(opcode & 0x1F);
                if (lastFive == 0x12) {
                    isGroup1Op = true;
                } else if (lastTwo == 1) {
                    isGroup1Op = true;
                } else if (lastTwo == 3) {
                    byte bit2n3 = (byte)((opcode >> 2) & 3);
                    isGroup1Op = bit2n3 != 2;
                }
            }
            if (isGroup1Op) {
                byte firstThree = (byte)((opcode >> 5) & 7);
                switch (firstThree) {
                    case 0:
                        Instructions.ORAByOpcode(opcode, this, memory);
                        break;
                    case 1:
                        Instructions.ANDByOpcode(opcode, this, memory);
                        break;
                    case 2:
                        Instructions.EORByOpcode(opcode, this, memory);
                        break;
                    case 3:
                        Instructions.ADCByOpcode(opcode, this, memory);
                        break;
                    case 4:
                        Instructions.STAByOpcode(opcode, this, memory);
                        break;
                    case 5:
                        Instructions.LDAByOpcode(opcode, this, memory);
                        break;
                    case 6:
                        Instructions.CMPByOpcode(opcode, this, memory);
                        break;
                    case 7:
                        Instructions.SBCByOpcode(opcode, this, memory);
                        break;
                }
                return;
            }

            Boolean isGroup2Op = false;
            byte addressMode = (byte)((opcode >> 2) & 7);
            byte op = (byte)(opcode & ~0x1C);
            if ((opcode & 1) == 0) {
                if((opcode & 2) == 0) {
                    


                } else {

                }



    
            }*/







        }

        public void setIRS() {
            if(!emulation)
                setFlag(4, true);
            X &= 0xFF;
            Y &= 0xFF;
        }
        public void resetIRS() {
            if(!emulation)
                setFlag(4, false);
        }

        public void setEmulation() {
            S &= 0xFF;
            S |= 0x100;
            IRS = true;
            ARS = true;
            emulation = true;
        }
        public void resetEmulation() {
            emulation = false;
        }

        public void pushByte(byte data, Memory memory) {
            memory[S] = data;
            S--;
        }
        public byte pullByte(Memory memory) {
            S++;
            return memory[S];
        }





        //flags
        public Boolean carry {
            set {
                setFlag(0, value);
            }
            get {
                return getFlag(0);
            }
        }
        public Boolean zero {
            set {
                setFlag(1, value);
            }
            get {
                return getFlag(1);
            }
        }
        public Boolean IRQMask {
            set {
                setFlag(2, value);
            }
            get {
                return getFlag(2);
            }
        }
        public Boolean decimalMode {
            set {
                setFlag(3, value);
            }
            get {
                return getFlag(3);
            }
        }
        public Boolean IRS {
            set {
                if (value) {
                    setIRS();
                } else {
                    resetIRS();
                }
            }
            get {
                return emulation ? true : getFlag(4);
            }
        }
        public Boolean ARS {
            set {
                if (!emulation) {
                    setFlag(5, value);
                }
            }
            get {
                return emulation ? true : getFlag(5);
            }
        }
        public Boolean overflow {
            set {
                setFlag(6, value);
            }
            get {
                return getFlag(6);
            }
        }
        public Boolean negative {
            set {
                setFlag(7, value);
            }
            get {
                return getFlag(7);
            }
        }

        public Boolean getFlag(byte bitNumber) {
            return (P & (1 << bitNumber)) == 1 << bitNumber;
        }
        public void setFlag(byte bitNumber, Boolean value) {
            //clear flag
            P = (byte)(P & ~(1 << bitNumber));
            //set flag
            if (value) {
                P = (byte)(P | (1 << bitNumber));
            }
        }

    }
}
