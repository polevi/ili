using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Disassembler
{
    [XmlType("Instruction")]
    public class ILInstr
    {
        private Instruction instruction;
        private Type arrayType;
        private int arraySize;
        private object initType;

        public ushort Code { get; set; }

        public String Name { get; set; }

        public byte OpType { get; set; }

        public String ArgType { get; set; }

        public String ArgValue { get; set; }

        public ILInstr()
        {
        }

        public ILInstr(Instruction ili, Assembly scope)
        {
            this.instruction = ili;
            this.Code = (ushort)instruction.Code.Value;
            this.Name = instruction.Code.ToString();
            this.OpType = (byte)instruction.Code.OperandType;
            if (instruction.Operand != null)
            {
                this.ArgType = instruction.Operand.GetType().FullName;
                this.ArgValue = GetArgValue(scope);
            }
        }

        public int GetOffset()
        {
            return instruction.Offset;
        }

        public void Prepare(Dictionary<int, int> addrs)
        {
            switch (this.Code)
            {
                case 1:
                case 0x27:
                case 0x29:
                case 0xFE1D:
                case 0xC2:
                case 0xC6:
                case 0xFE00:
                case 0xFE0F:
                case 0xFE11:
                case 0xFE17:
                case 0xFE18:
                    throw new Exception(String.Format("generated unimplemented IL opcode {0}", this.Code));
                case 0xFE09:
                case 0xFE0A:
                case 0xFE0B:
                    throw new Exception("more than 256 args are not implemented");
                case 0xFE0C:
                case 0xFE0D:
                case 0xFE0E:
                    throw new Exception("more than 256 local vars are not implemented");
                case 0x75:
                    throw new Exception("'is' operation is not implemented");
                case 0x7F:
                    throw new Exception("pass static fields by reference is not implemented");
                case 0x82:
                case 0x83:
                case 0x84:
                case 0x85:
                case 0x86:
                case 0x87:
                case 0x88:
                case 0x89:
                case 0x8A:
                case 0x8B:
                    throw new Exception("checked unsigned conversions are not implemented");
            }

            if (this.Code == 0x8D) // newarr
            {
                arrayType = (Type)instruction.Operand;
            }

            if (this.Code >= 0x15 && this.Code <= 0x20) // ldc.i4  
            {
                if (this.Code < 0x1F)
                    arraySize = this.Code - 0x16;
                else
                    arraySize = Int32.Parse(ArgValue);
            }

            if (this.Code == 0xD0) // ldtoken
            {
                initType = instruction.Operand;
            }

            // change instruction opcode for local method calls to reserved 0xf0

            if (this.OpType == (byte)OperandType.InlineMethod)
            {
                String[] methodName = this.ArgValue.Split(new String[] { " " }, StringSplitOptions.None);

                if ((this.Code == 0xfe06 || this.Code == 0xfe07) && methodName[1].StartsWith("local."))
                {
                    this.OpType = (byte)OperandType.InlineString;
                }
                else
                    if (methodName[1].StartsWith("local."))
                    {
                        this.OpType = (byte)OperandType.InlineString;
                        this.Code = 0xF0;
                        this.Name = "callloc";
                    }
                    else
                        if (methodName[0].StartsWith("delegate"))
                        {
                            this.OpType = (byte)OperandType.InlineString;
                            this.Name = "delegate";
                            this.Code = 0xF1;
                        }
                        else
                            if (instruction.Operand is System.Reflection.MethodInfo)
                            {
                                System.Reflection.MethodInfo mOperand = (System.Reflection.MethodInfo)instruction.Operand;
                                if (mOperand.ReflectedType.Equals(typeof(System.Runtime.CompilerServices.RuntimeHelpers)) && mOperand.Name.Equals("InitializeArray"))
                                {
                                    this.OpType = (byte)OperandType.InlineString;
                                    this.Code = 0xF2; // initarray
                                    this.Name = "initarray";

                                    Array arr = Array.CreateInstance(arrayType, arraySize);
                                    System.Runtime.CompilerServices.RuntimeHelpers.InitializeArray(arr, ((System.Reflection.FieldInfo)initType).FieldHandle);
                                    this.ArgType = "System.String";
                                    StringBuilder sb = new StringBuilder();
                                    for (int idx = 0; idx < arraySize; idx++)
                                    {
                                        if (idx > 0)
                                            sb.Append("|");
                                        sb.Append(arr.GetValue(idx).ToString());
                                    }
                                    this.ArgValue = sb.ToString();
                                }
                            }
            }

            if (this.OpType == (byte)OperandType.InlineBrTarget || this.OpType == (byte)OperandType.ShortInlineBrTarget)
            {
                this.ArgValue = addrs[(int)instruction.Operand].ToString();
            }

            if (this.OpType == (byte)OperandType.InlineSwitch)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(((int[])instruction.Operand)[0]);
                for (int i = 1; i < ((int[])instruction.Operand).Length; i++)
                    sb.Append("," + addrs[((int[])instruction.Operand)[i]]);
                this.ArgValue = sb.ToString();
            }
        }

        private String GetArgValue(Assembly scope)
        {
            if (instruction.Operand == null)
                return "";
            else
            {
                bool isLocalCall;

                string s = "";
                if (instruction.Operand != null)
                {
                    switch (instruction.Code.OperandType)
                    {
                        case OperandType.InlineField:
                            System.Reflection.FieldInfo fOperand = ((System.Reflection.FieldInfo)instruction.Operand);
                            s = fOperand.FieldType.FullName + " " + Helper.ProcessSpecialTypes(fOperand.ReflectedType, scope) + "::" + fOperand.Name + "";
                            break;
                        case OperandType.InlineMethod:
                            try
                            {
                                System.Reflection.MethodInfo mOperand = (System.Reflection.MethodInfo)instruction.Operand;
                                String t = Helper.ProcessSpecialTypes(mOperand.ReflectedType, scope);
                                isLocalCall = t.StartsWith("local");
                                if (isLocalCall)
                                    s = mOperand.ReturnType.ToString() + " " + t + "::" + mOperand.Name + Helper.ParametersAsString(mOperand.GetParameters(), !mOperand.IsStatic);
                                else
                                    s = mOperand.ReturnType.ToString() + " " + t + "::" + mOperand.Name + Helper.ParametersAsString(mOperand.GetParameters());
                            }
                            catch
                            {
                                try
                                {
                                    System.Reflection.ConstructorInfo mOperand = (System.Reflection.ConstructorInfo)instruction.Operand;
                                    String t = Helper.ProcessSpecialTypes(mOperand.ReflectedType, scope);
                                    bool isDelegateCall = ((mOperand.GetMethodImplementationFlags() & System.Reflection.MethodImplAttributes.Runtime) != 0);
                                    if (isDelegateCall)
                                    {
                                        s = "delegate " + Helper.ProcessSpecialTypes(mOperand.ReflectedType, scope) +
                                        "::" + mOperand.Name + Helper.ParametersAsString(mOperand.GetParameters(), !mOperand.IsStatic);

                                    }
                                    else
                                    {
                                        isLocalCall = t.StartsWith("local");
                                        if (isLocalCall)
                                            s = "System.Void " + Helper.ProcessSpecialTypes(mOperand.ReflectedType, scope) +
                                            "::" + mOperand.Name + Helper.ParametersAsString(mOperand.GetParameters(), !mOperand.IsStatic);
                                        else
                                            s = "System.Void " + Helper.ProcessSpecialTypes(mOperand.ReflectedType, scope) +
                                            "::" + mOperand.Name + Helper.ParametersAsString(mOperand.GetParameters());
                                    }
                                }
                                catch
                                {
                                }
                            }
                            break;
                        case OperandType.ShortInlineBrTarget:
                        case OperandType.InlineBrTarget:
                            // can assign anything, not used
                            s = instruction.Offset.ToString();
                            break;
                        case OperandType.InlineType:
                            s = Helper.ProcessSpecialTypes((Type)instruction.Operand, scope);
                            break;
                        case OperandType.InlineString:
                            s = instruction.Operand.ToString();
                            break;
                        case OperandType.ShortInlineVar:
                            s = instruction.Operand.ToString();
                            break;
                        case OperandType.InlineI:
                        case OperandType.InlineI8:
                        case OperandType.InlineR:
                        case OperandType.ShortInlineI:
                        case OperandType.ShortInlineR:
                            s = instruction.Operand.ToString();
                            break;
                        case OperandType.InlineTok:
                            if (instruction.Operand is Type)
                                s = ((Type)instruction.Operand).FullName;
                            else
                                s = "not supported";
                            break;


                        default: s = "not supported"; break;
                    }
                }

                return s;
            }
        }

    }
}
