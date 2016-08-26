using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SNES_Emulator.Emulator {
    public class Memory {

        public ROM rom = new ROM();
        public MemoryLocation WRAM = new MemoryLocation(131072);
        public MemoryLocation SRAM;

        public Memory() {

        }

        public void loadRom(FileStream stream) {
            rom.data = new byte[stream.Length];
            stream.Read(rom.data, 0, (int)(stream.Length));
            stream.Dispose();
            Boolean headered = rom.data.Length % 1024 == 512;

            //remove header
            if (headered) {
                byte[] temp = new byte[stream.Length - 512];
                for (int i = 0; i < temp.Length; i++) {
                    temp[i] = rom.data[i + 512];
                }
                rom.data = temp;
            }

            //detect SNES header.
            rom.hiRom = false;
            Boolean detected = true;
            for (int i = 0x7FC0; i <= 0x7FD4; i++) {
                if (rom.data[i] < 0x20 || rom.data[i] > 0x7E) {
                    detected = false;
                }
            }
            if (!detected) {
                detected = true;
                for (int i = 0xFFC0; i <= 0xFFD4; i++) {
                    if (rom.data[i] < 0x20 || rom.data[i] > 0x7E) {
                        detected = false;
                    }
                }
                if (detected) { rom.hiRom = true; } else {
                    throw new Exception("Invalid Rom");
                }

            }
            int SRAMsize = 0x400 << this[0xFFD8];
            SRAM = new MemoryLocation(SRAMsize);
        }

        public MemoryMappingResult mapAddress(int address) {
            byte bank = (byte)((address & 0xFF0000) >> 16);
            ushort rest = (ushort)(address & 0xFFFF);
            Boolean even = (bank % 2) == 1 ? false : true;
            int bankover2 = (bank / 2) << 16;


            //LoRom
            if (!rom.hiRom) {
                if (bank >= 0 && bank <= 0x3F) {
                    if (rest <= 0x1FFF) {
                        return new MemoryMappingResult(WRAM,rest);
                    } else if (rest >= 0x8000) {
                        if (even) {
                            return new MemoryMappingResult(rom,bankover2 + rest - 0x8000);
                        } else {
                            return new MemoryMappingResult(rom,bankover2 + rest);
                        }
                    }
                }
                else if (bank >= 0x40 && bank <= 0x6F) {
                    if (rest <= 0x7FFF) {
                        if (even) {
                            return new MemoryMappingResult(rom,bankover2 + rest);
                        } else {
                            return new MemoryMappingResult(rom,bankover2 + rest + 0x8000);
                        }
                    } else {
                        if (even) {
                            return new MemoryMappingResult(rom,bankover2 + rest - 0x8000);
                        } else {
                            return new MemoryMappingResult(rom,bankover2 + rest);
                        }
                    }
                }
                else if(bank >= 0x70 && bank <= 0x7D) {
                    if(rest <= 0x7FFF && SRAM is MemoryLocation && SRAM.data.Length > 0) {
                        int sramaddress;
                        if (even) {
                            sramaddress = (((bank-0x70)/2)<<16) + rest;
                        } else {
                            sramaddress = (((bank - 0x70) / 2) << 16) + rest + 0x8000;
                        }
                        if (sramaddress < SRAM.data.Length) {
                            return new MemoryMappingResult(SRAM, sramaddress);
                        }
                    }
                    if (rest >= 0x8000 && rest <= 0xFFFF) {
                        if (even) {
                            return new MemoryMappingResult(rom, bankover2 + rest - 0x8000);
                        } else {
                            return new MemoryMappingResult(rom, bankover2 + rest);
                        }
                    }
                    
                }
                else if(bank == 0x7E) {
                    return new MemoryMappingResult(WRAM, rest);
                }
                else if (bank == 0x7F) {
                    return new MemoryMappingResult(WRAM, (1 << 16) + rest);
                }
                else if (bank >= 0x80 && bank <= 0xFD) {
                    return mapAddress(address - 0x800000);
                }
                else if(bank >= 0xFE && bank <= 0xFF) {
                    if(rest <= 0x7FFF) {
                        int sramaddress;
                        if (even) {
                            sramaddress = ((bank / 2 - 0x78) << 16) + rest;
                        } else {
                            sramaddress = ((bank / 2 - 0x78) << 16) + rest + 0x8000;
                        }
                        if(sramaddress < SRAM.data.Length) {
                            return new MemoryMappingResult(SRAM, sramaddress);
                        }

                    } else {
                        if (even) {
                            return new MemoryMappingResult(rom, bankover2 + rest - 0x8000);
                        } else {
                            return new MemoryMappingResult(rom, bankover2 + rest);
                        }
                    }
                }


                return null;
                //HiRom
            } else {

                
                return null;
            }
        }
        
        public byte this[int address] {
            get {
                MemoryMappingResult mapped = mapAddress(address);
                if (mapped != null) { 
                    return mapped.location[mapped.address];
                }
                return 0;
            }
            set {
                try {
                    
                    MemoryMappingResult mapped = mapAddress(address);
                    if (mapped != null)
                        mapped.location[mapped.address] = value;
                } catch (Exception ex) {
                    
                }
            }
        }
        
        public class ROM : MemoryLocation{
            public Boolean hiRom = false;
        }



    }
}
