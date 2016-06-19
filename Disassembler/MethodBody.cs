using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Reflection;
using System.Reflection.Emit;

namespace Disassembler
{
    public class MethodBody
    {
        private MethodBase mb;
        private List<Instruction> instructions;
        private byte[] iarr;

        public MethodBody(MethodBase mb)
        {
            this.mb = mb;
            BuildInstructionList();
        }

        public List<Instruction> GetInstructions()
        {
            return instructions;
        }

        private void BuildInstructionList()
        {
            instructions = new List<Instruction>();
            if (mb.GetMethodBody() != null)
            {
                iarr = mb.GetMethodBody().GetILAsByteArray();
                BuildInstructions(mb.Module);
            }
        }

        private void BuildInstructions(Module module)
        {
            byte[] il = this.iarr;
            int position = 0;

            while (position < il.Length)
            {
                Instruction instruction = new Instruction();

                // get the operation code of the current instruction
                OpCode code = OpCodes.Nop;
                ushort value = il[position++];
                if (value != 0xfe)
                {
                    code = OpCodeList.GetSingleByteOpCode((ushort)value);
                }
                else
                {
                    value = il[position++];
                    code = OpCodeList.GetMultiByteOpCode((ushort)value);
                    value = (ushort)(value | 0xfe00);
                }
                instruction.Code = code;
                instruction.Offset = position - 1;
                int metadataToken = 0;
                // get the operand of the current operation
                switch (code.OperandType)
                {
                    case OperandType.InlineBrTarget:
                        metadataToken = ReadInt32(il, ref position);
                        metadataToken += position;
                        instruction.Operand = metadataToken;
                        break;
                    case OperandType.InlineField:
                        metadataToken = ReadInt32(il, ref position);
                        instruction.Operand = module.ResolveField(metadataToken);
                        break;
                    case OperandType.InlineMethod:
                        metadataToken = ReadInt32(il, ref position);
                        instruction.Operand = module.ResolveMethod(metadataToken);
                        break;
                    case OperandType.InlineSig:
                        metadataToken = ReadInt32(il, ref position);
                        instruction.Operand = module.ResolveSignature(metadataToken);
                        break;
                    case OperandType.InlineTok:
                        metadataToken = ReadInt32(il, ref position);
                        try
                        {
                            instruction.Operand = module.ResolveType(metadataToken);
                        }
                        catch
                        {
                            try
                            {
                                instruction.Operand = module.ResolveField(metadataToken);
                            }
                            catch
                            {
                                throw new Exception("token cant be resolved");
                            }
                        }
                        break;
                    case OperandType.InlineType:
                        metadataToken = ReadInt32(il, ref position);

                        instruction.Operand = module.ResolveType(metadataToken, this.mb.DeclaringType.GetGenericArguments(), this.mb.GetGenericArguments());
                        break;
                    case OperandType.InlineI:
                        {
                            instruction.Operand = ReadInt32(il, ref position);
                            break;
                        }
                    case OperandType.InlineI8:
                        {
                            instruction.Operand = ReadInt64(il, ref position);
                            break;
                        }
                    case OperandType.InlineNone:
                        {
                            instruction.Operand = null;
                            break;
                        }
                    case OperandType.InlineR:
                        {
                            instruction.Operand = ReadDouble(il, ref position);
                            break;
                        }
                    case OperandType.InlineString:
                        {
                            metadataToken = ReadInt32(il, ref position);
                            instruction.Operand = module.ResolveString(metadataToken);
                            break;
                        }
                    case OperandType.InlineSwitch:
                        {
                            int count = ReadInt32(il, ref position);
                            int[] casesAddresses = new int[count + 1];
                            for (int i = 1; i <= count; i++)
                            {
                                casesAddresses[i] = ReadInt32(il, ref position);
                            }
                            int[] cases = new int[count + 1];
                            cases[0] = count;
                            for (int i = 1; i <= count; i++)
                            {
                                cases[i] = position + casesAddresses[i];
                            }
                            instruction.Operand = cases;
                            break;
                        }
                    case OperandType.InlineVar:
                        {
                            instruction.Operand = ReadUInt16(il, ref position);
                            break;
                        }
                    case OperandType.ShortInlineBrTarget:
                        {
                            instruction.Operand = ReadSByte(il, ref position) + position;
                            break;
                        }
                    case OperandType.ShortInlineI:
                        {
                            instruction.Operand = ReadSByte(il, ref position);
                            break;
                        }
                    case OperandType.ShortInlineR:
                        {
                            instruction.Operand = ReadSingle(il, ref position);
                            break;
                        }
                    case OperandType.ShortInlineVar:
                        {
                            instruction.Operand = ReadByte(il, ref position);
                            break;
                        }
                    default:
                        {
                            throw new Exception("Unknown operand type.");
                        }
                }
                instructions.Add(instruction);
            }
        }


        private int ReadInt16(byte[] _il, ref int position)
        {
            Int16 v = BitConverter.ToInt16(_il, position);
            position += 2;
            return v;
        }

        private ushort ReadUInt16(byte[] _il, ref int position)
        {
            UInt16 v = BitConverter.ToUInt16(_il, position);
            position += 2;
            return v;
        }

        private int ReadInt32(byte[] _il, ref int position)
        {
            Int32 v = BitConverter.ToInt32(_il, position);
            position += 4;
            return v;
        }

        private ulong ReadInt64(byte[] _il, ref int position)
        {
            Int64 v = BitConverter.ToInt64(_il, position);
            position += 8;
            return (ulong)v;
        }

        private double ReadDouble(byte[] _il, ref int position)
        {
            double v = BitConverter.ToDouble(_il, position);
            position += 8;
            return v;
        }

        
        private sbyte ReadSByte(byte[] _il, ref int position)
        {
            return (sbyte)_il[position++];
        }

        private byte ReadByte(byte[] _il, ref int position)
        {
            return (byte)_il[position++];
        }

        private Single ReadSingle(byte[] _il, ref int position)
        {
            Single v = BitConverter.ToSingle(_il, position);
            position += 4;
            return v;
        }


    }
}
