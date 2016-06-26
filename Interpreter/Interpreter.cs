using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter
{
    public class Interpreter
    {
        private Package package;

        public Interpreter(Package p)
        {
            this.package = p;
            InitCommands();
        }


        private TValue[] Execute(TStack stack, String name)
        {
            Procedure p = package.FindProcedure(name);

            Frame frame = new Frame(package, stack);

            frame.LoadArguments(p.GetArguments());

            ExecuteProcedure(frame, p);

            return frame.Arguments;
        }


        public void ExecuteProcedure(Frame frame, Procedure p)
        {
            Instruction[] arr = p.GetInstructions();
            int offset = 0;

            while (offset < arr.Length)
            {
                Instruction ili = arr[offset];
                ushort idx = (ushort)ili.Code;
                Func<Frame, Instruction, int> a = idx < 254 ? commands[idx] : commandsFE[idx & 0xff];
                int off = a.Invoke(frame, ili);
                switch (off)
                {
                    case -1:
                        return;
                    case 0:
                        offset++;
                        break;
                    default:
                        offset = off;
                        break;
                }
            }
        }

        private int DoINIT_ARRAY(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        private int DoCREATE_DELEGATE(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        private int DoCALL_LOC(Frame frame, Instruction ili)
        {
            bool isConstructor = ili.Operand.As<string>().IsConstructor();

            int argCount;
            TStack argsStack = new TStack();
            if (ili.Operand.As<string>().IsNoArgFunc())
                argCount = 0;
            else
            {
                Type[] pargs = TypeHelper.GetArguments(ili.Operand.As<string>().ExtractArguments());
                argCount = pargs.Length;
            }

            TValue[] argRefs = new TValue[argCount];
            TValue[] args = new TValue[argCount];

            if (isConstructor)
            {
                throw new NotImplementedException();
            }

            for (int i = argCount - 1; i >= (isConstructor ? 1 : 0); i--)
            {
                argRefs[i] = frame.Pop();
                args[i] = frame.ResolveRef(argRefs[i]);

                if (argRefs[i].IsRef)
                {
                    args[i].MakeValueRef();
                }
            }

            for (int i = 0; i < argCount; i++)
                argsStack.Push(args[i]);

            args = Execute(argsStack, ili.Operand.As<string>());
            
            while (argsStack.Count > 0)
            {
                TValue val = argsStack.Pop();
                if (val.IsArgRef)
                    frame.Push(args[val.Index]);
                else
                    frame.Push(val);
            }

            for (int i = argCount - 1; i >= 0; i--)
                frame.AssignRef(argRefs[i], args[i]);

            if (isConstructor)
                frame.Push(frame.Locals[255]);

            return 0;
        }


        private int NotImplemented(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        private int DoNOP(Frame frame, Instruction ili)
        {
            return 0;
        }

        private int DoBREAK(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        private int DoLDARG_0(Frame frame, Instruction ili)
        {
            frame.LdArg(0);
            return 0;
        }

        private int DoLDARG_1(Frame frame, Instruction ili)
        {
            frame.LdArg(1);
            return 0;
        }

        private int DoLDARG_2(Frame frame, Instruction ili)
        {
            frame.LdArg(2);
            return 0;
        }

        private int DoLDARG_3(Frame frame, Instruction ili)
        {
            frame.LdArg(3);
            return 0;
        }

        private int DoLDARG(Frame frame, Instruction ili)
        {
            throw new NotImplementedException(); // more than 256 arguments are not supported
        }

        private int DoLDARG_S(Frame frame, Instruction ili)
        {
            frame.Push(frame.Arguments[ili.Operand.As<int>()]);
            return 0;
        }

        private int DoLDARGA_S(Frame frame, Instruction ili)
        {
            frame.Push(TValue.CreateArgRef(ili.Operand.As<int>()));
            return 0;
        }

        private int DoLDARGA(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        private int DoLDLOC_0(Frame frame, Instruction ili)
        {
            frame.Push(frame.Locals[0]);
            return 0;
        }

        private int DoLDLOC_1(Frame frame, Instruction ili)
        {
            frame.Push(frame.Locals[1]);
            return 0;
        }

        private int DoLDLOC_2(Frame frame, Instruction ili)
        {
            frame.Push(frame.Locals[2]);
            return 0;
        }

        private int DoLDLOC_3(Frame frame, Instruction ili)
        {
            frame.Push(frame.Locals[3]);
            return 0;
        }

        private int DoLDLOC_S(Frame frame, Instruction ili)
        {
            frame.Push(frame.Locals[ili.Operand.As<int>()]);
            return 0;
        }

        private int DoLDLOCA(Frame frame, Instruction ili)
        {
            throw new NotImplementedException(); // more than 256 variables are not supported
        }

        private int DoLDLOC(Frame frame, Instruction ili)
        {
            throw new NotImplementedException(); // more than 256 variables are not supported
        }

        private int DoLDLOCA_S(Frame frame, Instruction ili)
        {
            frame.Push(TValue.CreateLocalRef(ili.Operand.As<int>()));
            return 0;
        }

        private int DoSTLOC_0(Frame frame, Instruction ili)
        {
            frame.Locals[0] = frame.Pop();
            return 0;
        }

        private int DoSTLOC_1(Frame frame, Instruction ili)
        {
            frame.Locals[1] = frame.Pop();
            return 0;
        }

        private int DoSTLOC_2(Frame frame, Instruction ili)
        {
            frame.Locals[2] = frame.Pop();
            return 0;
        }

        private int DoSTLOC_3(Frame frame, Instruction ili)
        {
            frame.Locals[3] = frame.Pop();
            return 0;
        }

        private int DoSTLOC_S(Frame frame, Instruction ili)
        {
            frame.Locals[ili.Operand.As<int>()] = frame.Pop();
            return 0;
        }

        private int DoSTLOC(Frame frame, Instruction ili)
        {
            throw new NotImplementedException(); // more than 256 variables are not supported
        }

        private int DoSTARG_S(Frame frame, Instruction ili)
        {
            bool argumentIsRef = frame.Arguments[(byte)ili.Operand.As<int>()].IsValueRef;
            if (!argumentIsRef) 
                frame.Arguments[(byte)ili.Operand.As<int>()] = frame.Pop();
            else
                frame.Pop();
            //ignore command in order not to spoil passed argument (CLR works this way). 
            //It may lead bug because local copy of argument should be modified by the operator cemantic
            //But c# compiler will no generate starg for ref argument
            //To write into ref argument stind will be used

            return 0;
        }

        private int DoSTARG(Frame frame, Instruction ili)
        {
            throw new NotImplementedException(); // more than 256 arguments are not supported
        }

        private int DoLDNULL(Frame frame, Instruction ili)
        {
            frame.Push(new TValue());
            return 0;
        }

        private int DoLDC_I4_M1(Frame frame, Instruction ili)
        {
            frame.Push(new TValue((System.Int32)(-1)));
            return 0;
        }

        private int DoLDC_I4_0(Frame frame, Instruction ili)
        {
            frame.Push(new TValue((System.Int32)0));
            return 0;
        }

        private int DoLDC_I4_1(Frame frame, Instruction ili)
        {
            frame.Push(new TValue((System.Int32)1));
            return 0;
        }

        private int DoLDC_I4_2(Frame frame, Instruction ili)
        {
            frame.Push(new TValue((System.Int32)2));
            return 0;
        }

        private int DoLDC_I4_3(Frame frame, Instruction ili)
        {
            frame.Push(new TValue((System.Int32)3));
            return 0;
        }

        private int DoLDC_I4_4(Frame frame, Instruction ili)
        {
            frame.Push(new TValue((System.Int32)4));
            return 0;
        }

        private int DoLDC_I4_5(Frame frame, Instruction ili)
        {
            frame.Push(new TValue((System.Int32)5));
            return 0;

        }

        private int DoLDC_I4_6(Frame frame, Instruction ili)
        {
            frame.Push(new TValue((System.Int32)6));
            return 0;

        }

        private int DoLDC_I4_7(Frame frame, Instruction ili)
        {
            frame.Push(new TValue((System.Int32)7));
            return 0;
        }

        private int DoLDC_I4_8(Frame frame, Instruction ili)
        {
            frame.Push(new TValue((System.Int32)8));
            return 0;
        }

        private int DoLDC_I4_S(Frame frame, Instruction ili)
        {
            frame.Push(ili.Operand);
            return 0;
        }

        private int DoLDC_I4(Frame frame, Instruction ili)
        {
            frame.Push(ili.Operand);
            return 0;
        }

        private int DoLDC_I8(Frame frame, Instruction ili)
        {
            frame.Push(ili.Operand);
            return 0;
        }

        private int DoLDC_R4(Frame frame, Instruction ili)
        {
            frame.Push(ili.Operand);
            return 0;
        }

        private int DoLDC_R8(Frame frame, Instruction ili)
        {
            frame.Push(ili.Operand);
            return 0;
        }

        private int DoDUP(Frame frame, Instruction ili)
        {
            frame.Push(frame.Peek());
            return 0;
        }

        private int DoPOP(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        private int DoJMP(Frame frame, Instruction ili)
        {
            throw new NotImplementedException(); //not used in managed code
        }

        private int DoCALL(Frame frame, Instruction ili)
        {
            MethodBase mi = (MethodBase)ili.Operand.Value;
            object[] args = new object[mi.GetParameters().Length];
            TValue[] argRefs = new TValue[mi.GetParameters().Length];
            for (int i = args.Length - 1; i >= 0; i--)
            {
                argRefs[i] = frame.Pop();
                TValue arg = frame.ResolveRef(argRefs[i]);
                args[i] = arg.Value;
            }
            TValue result;
            if (mi.IsStatic)
                result = new TValue(mi.Invoke(null, args));
            else if (mi.IsConstructor)
            {
                TValue thisRef = frame.Pop();
                frame.AssignRef(thisRef, new TValue(((ConstructorInfo)mi).Invoke(args)));
                result = new TValue();
            }
            else
            {
                TValue obj = frame.ResolveRef(frame.Pop());
                object instance = obj.Value;
                result = new TValue(mi.Invoke(instance, args));
            }
            if (mi is MethodInfo)
            {
                if (((MethodInfo)mi).ReturnType != typeof(void))
                {
                    frame.Push(result);
                }
            }
            for (int i = args.Length - 1; i >= 0; i--)
            {
                frame.AssignRef(argRefs[i], new TValue(args[i]));
            }

            return 0;
        }

        /*
        static TValue ResolveRef(Frame frame, TValue obj)
        {
            if (obj.IsLocalRef)
                obj = frame.Locals[obj.Index];
            if (obj.IsArgRef)
                obj = frame.Arguments[obj.Index];
            if (obj.IsArrayRef)
                obj = new TValue(((Array)obj.Value).GetValue(obj.Index));
            return obj;
        }

        static void AssignRef(Frame frame, TValue obj, TValue value)
        {
            if (obj.IsLocalRef)
                frame.Locals[obj.Index] = value;
            if (obj.IsArgRef)
            {
                bool isRefArgument = frame.Arguments[obj.Index].IsValueRef;
                frame.Arguments[obj.Index] = value;
                if (isRefArgument)
                    frame.Arguments[obj.Index].MakeValueRef();
                else
                    frame.Arguments[obj.Index].MakeSimple();
            }
            if (obj.IsArrayRef)
                ((Array)obj.Value).SetValue(value.Value, obj.Index);
        }
        */

         
        private int DoCALLI(Frame frame, Instruction ili)
        {
            throw new NotImplementedException(); //unmanaged code calls are not supported
        }

        private int DoRET(Frame frame, Instruction ili)
        {
            return -1;
        }

        private int DoBR_S(Frame frame, Instruction ili)
        {
            return ili.Operand.As<int>();
        }

        private int DoBR_FALSE_S(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        private int DoBR_TRUE_S(Frame frame, Instruction ili)
        {
            return frame.Pop().CheckIfFalseNullZero() ? 0 : ili.Operand.As<int>();
        }

        private int DoBEQ_S(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        private int DoBGE_S(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        private int DoBGT_S(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        private int DoBLE_S(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        private int DoBLT_S(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        private int DoBNE_UN_S(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        private int DoBGE_UN_S(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        private int DoBGT_UN_S(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        private int DoBLE_UN_S(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        private int DoBLT_UN_S(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        private int DoBR(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        private int DoBRFALSE(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        private int DoBRTRUE(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        private int DoBEQ(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        private int DoBGE(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        private int DoBGT(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        private int DoBLE(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        private int DoBLT(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        private int DoBNE_UN(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        private int DoBGE_UN(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        private int DoBGT_UN(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        private int DoBLE_UN(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        private int DoBLT_UN(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        private int DoSWITCH(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        private int DoLDIND_I1(Frame frame, Instruction ili)
        {
            return DoLDIND_I4(frame, ili);
        }

        private int DoLDIND_U1(Frame frame, Instruction ili)
        {
            return DoLDIND_I4(frame, ili);
        }

        private int DoLDIND_I2(Frame frame, Instruction ili)
        {
            return DoLDIND_I4(frame, ili);
        }

        private int DoLDIND_U2(Frame frame, Instruction ili)
        {
            return DoLDIND_I4(frame, ili);
        }

        private int DoLDIND_I4(Frame frame, Instruction ili)
        {
            frame.Push(frame.ResolveRef(frame.Pop()));
            return 0;
        }

        private int DoLDIND_U4(Frame frame, Instruction ili)
        {
            return DoLDIND_I4(frame, ili);
        }

        private int DoLDIND_I8(Frame frame, Instruction ili)
        {
            return DoLDIND_I4(frame, ili);
        }

        private int DoLDIND_I(Frame frame, Instruction ili)
        {
            return DoLDIND_I4(frame, ili);
        }

        private int DoLDIND_R4(Frame frame, Instruction ili)
        {
            return DoLDIND_I4(frame, ili);
        }

        private int DoLDIND_R8(Frame frame, Instruction ili)
        {
            return DoLDIND_I4(frame, ili);
        }

        private int DoLDIND_REF(Frame frame, Instruction ili)
        {
            return DoLDIND_I4(frame, ili);
        }

        private int DoSTIND_REF(Frame frame, Instruction ili)
        {
            return DoSTIND_I4(frame, ili);
        }

        private int DoSTIND_I1(Frame frame, Instruction ili)
        {
            return DoSTIND_I4(frame, ili);
        }

        private int DoSTIND_I2(Frame frame, Instruction ili)
        {
            return DoSTIND_I4(frame, ili);
        }

        private int DoSTIND_I4(Frame frame, Instruction ili)
        {
            TValue data = frame.Pop();
            frame.AssignRef(frame.Pop(), data);
            return 0;
        }

        private int DoSTIND_I8(Frame frame, Instruction ili)
        {
            return DoSTIND_I4(frame, ili);
        }

        private int DoSTIND_R4(Frame frame, Instruction ili)
        {
            return DoSTIND_I4(frame, ili);
        }

        private int DoSTIND_R8(Frame frame, Instruction ili)
        {
            return DoSTIND_I4(frame, ili);
        }

        private int DoADD(Frame frame, Instruction ili)
        {
            TValue v2 = frame.Pop();
            TValue v1 = frame.Pop();
            frame.Push(MathHelper.Add(frame.ResolveRef(v1), frame.ResolveRef(v2)));
            return 0;
        }

        private int DoSUB(Frame frame, Instruction ili)
        {
            TValue v2 = frame.Pop();
            TValue v1 = frame.Pop();
            frame.Push(MathHelper.Sub(frame.ResolveRef(v1), frame.ResolveRef(v2)));
            return 0;
        }

        private int DoMUL(Frame frame, Instruction ili)
        {
            TValue v2 = frame.Pop();
            TValue v1 = frame.Pop();
            frame.Push(MathHelper.Mul(frame.ResolveRef(v1), frame.ResolveRef(v2)));
            return 0;
        }

        private int DoDIV(Frame frame, Instruction ili)
        {
            TValue v2 = frame.Pop();
            TValue v1 = frame.Pop();
            frame.Push(MathHelper.Div(frame.ResolveRef(v1), frame.ResolveRef(v2)));
            return 0;
        }

        private int DoDIV_UN(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        private int DoREM(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        private int DoREM_UN(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        private int DoAND(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        private int DoOR(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        private int DoXOR(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        private int DoSHL(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        private int DoSHR(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        private int DoSHR_UN(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        private int DoNEG(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        private int DoNOT(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        private int DoCONV_I1(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        private int DoCONV_I2(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        private int DoCONV_I4(Frame frame, Instruction ili)
        {
            frame.Push(frame.Pop().ConvertTo(typeof(System.Int32)));
            return 0;
        }

        private int DoCONV_I8(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        private int DoCONV_R4(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        private int DoCONV_R8(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        private int DoCONV_U4(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        private int DoCONV_U8(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        private int DoCALLVIRT(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        private int DoCPOBJ(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        private int DoLDOBJ(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        private int DoLDSTR(Frame frame, Instruction ili)
        {
            frame.Push(ili.Operand);
            return 0;
        }

        private int DoNEWOBJ(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        private int DoCASTCLASS(Frame frame, Instruction ili)
        {
            return 0; //external call only
        }

        private int DoISINST(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        private int DoCONV_R_UN(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        private int DoUNBOX(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        private int DoTHROW(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        private int DoLDFLD(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        private int DoLDFLDA(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        private int DoSTFLD(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        private int DoLDSFLD(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        private int DoLDSFLDA(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        private int DoSTSFLD(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        private int DoSTOBJ(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        private int DoCONF_OVF_I1_UN(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        private int DoCONF_OVF_I2_UN(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        private int DoCONF_OVF_I4_UN(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        private int DoCONF_OVF_I8_UN(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        private int DoCONF_OVF_U1_UN(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        private int DoCONF_OVF_U2_UN(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        private int DoCONF_OVF_U4_UN(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        private int DoCONF_OVF_U8_UN(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        private int DoCONF_OVF_I_UN(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        private int DoCONF_OVF_U_UN(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        private int DoBOX(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        private int DoNEWARR(Frame frame, Instruction ili)
        {
            frame.Push(new TValue(ili.OperandT, frame.Pop().As<int>()));
            return 0;
        }

        private int DoLDLEN(Frame frame, Instruction ili)
        {
            TValue arr = frame.Pop();
            frame.Push(new TValue(arr.As<Array>().Length));
            return 0;
        }

        private int DoLDLEMA(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        private int DoLDLEM_I1(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        private int DoLDLEM_U1(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        private int DoLDLEM_I2(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        private int DoLDLEM_U2(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        private int DoLDLEM_I4(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        private int DoLDLEM_U4(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        private int DoLDLEM_I8(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        private int DoLDLEM_I(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        private int DoLDLEM_R4(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        private int DoLDLEM_R8(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        private int DoLDLEM_REF(Frame frame, Instruction ili)
        {
            TValue index = frame.Pop();
            TValue arr = frame.Pop();
            frame.Push(new TValue(((Array)arr.Value).GetValue(index.As<int>())));
            return 0;
        }

        private int DoSTELEM_I(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        private int DoSTELEM_I1(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        private int DoSTELEM_I2(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }


        private int DoSTELEM_I4(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        private int DoSTELEM_I8(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        private int DoSTELEM_R4(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        private int DoSTELEM_R8(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        private int DoSTELEM_REF(Frame frame, Instruction ili)
        {
            TValue value = frame.Pop();
            TValue index = frame.Pop();
            TValue arr = frame.Pop();
            arr.As<Array>().SetValue(value.Value, index.As<int>());
            return 0;
        }

        private int DoLDELEM(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        private int DoSTELEM(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        private int DoUNBOX_ANY(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        private int DoCONV_OVF_I1(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        private int DoCONV_OVF_U1(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        private int DoCONV_OVF_I2(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        private int DoCONV_OVF_U2(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        private int DoCONV_OVF_I4(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        private int DoCONV_OVF_U4(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        private int DoCONV_OVF_I8(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        private int DoCONV_OVF_U8(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        private int DoREFANYVAL(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        private int DoCKFINITE(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        private int DoMKREFANY(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        private int DoLDTOKEN(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        private int DoCONV_U2(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        private int DoCONV_U1(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        private int DoCONV_I(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        private int DoCONV_OVF_I(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        private int DoCONV_OVF_U(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        private int DoADD_OVF(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        private int DoADD_OVF_UN(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        private int DoMUL_OVF(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        private int DoMUL_OVF_UN(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        private int DoSUB_OVF(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        private int DoSUB_OVF_UN(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        private int DoENDFINALLY(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        private int DoLEAVE(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        private int DoLEAVE_S(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        private int DoSTIND_I(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        private int DoCONV_U(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        private int DoARGLIST(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        private int DoCEQ(Frame frame, Instruction ili)
        {
            TValue v2 = frame.Pop();
            TValue v1 = frame.Pop();
            frame.Push(new TValue(frame.ResolveRef(v1).CompareTo(frame.ResolveRef(v2)) == 0 ? 1 : 0));
            return 0;
        }

        private int DoCGT(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        private int DoCGT_UN(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        private int DoCLT(Frame frame, Instruction ili)
        {
            TValue v2 = frame.Pop();
            TValue v1 = frame.Pop();
            frame.Push(new TValue(frame.ResolveRef(v1).CompareTo(frame.ResolveRef(v2)) < 0 ? 1 : 0));
            return 0;

        }

        private int DoCLT_UN(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        private int DoLD_FTN(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        private int DoLDVIRT_FTN(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        private int DoLOCALLOC(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        private int DoENDFILTER(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        private int DoUNALIGNED(Frame frame, Instruction ili)
        {
            return 0; // prexif
        }

        private int DoVOLATILE(Frame frame, Instruction ili)
        {
            return 0; // prefix
        }

        private int DoTAIL(Frame frame, Instruction ili)
        {
            return 0; // prefix
        }

        private int DoINITOBJ(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        private int DoCONSTRAINED(Frame frame, Instruction ili)
        {
            return 0; // prefix
        }

        private int DoCPBLK(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        private int DoINITBLK(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        private int DoNO(Frame frame, Instruction ili)
        {
            return 0; // prefix
        }

        private int DoRETHROW(Frame frame, Instruction ili)
        {
            return 0;
        }

        private int DoSIZEOF(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        private int DoREFANYTYPE(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        private int DoREADONLY(Frame frame, Instruction ili)
        {
            return 0; // prefix
        }

        private Func<Frame, Instruction, int>[] commandsFE;
        private Func<Frame, Instruction, int>[] commands;

        private void InitCommands()
        {

            commandsFE = new Func<Frame, Instruction, int>[] {
                DoARGLIST,
                DoCEQ,
                DoCGT,
                DoCGT_UN,
                DoCLT,
                DoCLT_UN,
                DoLD_FTN,
                DoLDVIRT_FTN,
                DoNOP,
                DoLDARG,
                DoLDARGA,
                DoSTARG,
                DoLDLOC,
                DoLDLOCA,
                DoSTLOC,
                DoLOCALLOC,
                DoNOP,
                DoENDFILTER,
                DoUNALIGNED,
                DoVOLATILE,
                DoTAIL,
                DoINITOBJ,
                DoCONSTRAINED,
                DoCPBLK,
                DoINITBLK,
                DoNO,
                DoRETHROW,
                DoNOP,
                DoSIZEOF,
                DoREFANYTYPE,
                DoREADONLY
            };

            commands = new Func<Frame, Instruction, int>[] {
                DoNOP,    
                DoBREAK,
                DoLDARG_0,
                DoLDARG_1,
                DoLDARG_2,
                DoLDARG_3,
                DoLDLOC_0,
                DoLDLOC_1,
                DoLDLOC_2,
                DoLDLOC_3,
                DoSTLOC_0,
                DoSTLOC_1,
                DoSTLOC_2,
                DoSTLOC_3,
                DoLDARG_S,
                DoLDARGA_S,
                DoSTARG_S,
                DoLDLOC_S,
                DoLDLOCA_S,
                DoSTLOC_S,
                DoLDNULL,
                DoLDC_I4_M1,
                DoLDC_I4_0,
                DoLDC_I4_1,
                DoLDC_I4_2,
                DoLDC_I4_3,
                DoLDC_I4_4,
                DoLDC_I4_5,
                DoLDC_I4_6,
                DoLDC_I4_7,
                DoLDC_I4_8,
                DoLDC_I4_S,
                DoLDC_I4,
                DoLDC_I8,
                DoLDC_R4,
                DoLDC_R8,
                DoNOP,
                DoDUP,
                DoPOP,
                DoJMP,
                DoCALL,
                DoCALLI,
                DoRET,
                DoBR_S,
                DoBR_FALSE_S,
                DoBR_TRUE_S,
                DoBEQ_S,
                DoBGE_S,
                DoBGT_S,
                DoBLE_S,
                DoBLT_S,
                DoBNE_UN_S,
                DoBGE_UN_S,
                DoBGT_UN_S,
                DoBLE_UN_S,
                DoBLT_UN_S,
                DoBR,
                DoBRFALSE,
                DoBRTRUE,
                DoBEQ,
                DoBGE,
                DoBGT,
                DoBLE,
                DoBLT,
                DoBNE_UN,
                DoBGE_UN,
                DoBGT_UN,
                DoBLE_UN,
                DoBLT_UN,
                DoSWITCH,
                DoLDIND_I1,
                DoLDIND_U1,
                DoLDIND_I2,
                DoLDIND_U2,
                DoLDIND_I4,
                DoLDIND_U4,
                DoLDIND_I8,
                DoLDIND_I,
                DoLDIND_R4,
                DoLDIND_R8,
                DoLDIND_REF,
                DoSTIND_REF,
                DoSTIND_I1,
                DoSTIND_I2,
                DoSTIND_I4,
                DoSTIND_I8,
                DoSTIND_R4,
                DoSTIND_R8,
                DoADD,
                DoSUB,
                DoMUL,
                DoDIV,
                DoDIV_UN,
                DoREM,
                DoREM_UN,
                DoAND,
                DoOR,
                DoXOR,
                DoSHL,
                DoSHR,
                DoSHR_UN,
                DoNEG,
                DoNOT,
                DoCONV_I1,
                DoCONV_I2,
                DoCONV_I4,
                DoCONV_I8,
                DoCONV_R4,
                DoCONV_R8,
                DoCONV_U4,
                DoCONV_U8,
                DoCALLVIRT,
                DoCPOBJ,
                DoLDOBJ,
                DoLDSTR,
                DoNEWOBJ,
                DoCASTCLASS,
                DoISINST,
                DoCONV_R_UN,
                DoNOP,
                DoNOP,
                DoUNBOX,
                DoTHROW,
                DoLDFLD,
                DoLDFLDA,
                DoSTFLD,
                DoLDSFLD,
                DoLDSFLDA,
                DoSTSFLD,
                DoSTOBJ,
                DoCONF_OVF_I1_UN,
                DoCONF_OVF_I2_UN,
                DoCONF_OVF_I4_UN,
                DoCONF_OVF_I8_UN,
                DoCONF_OVF_U1_UN,
                DoCONF_OVF_U2_UN,
                DoCONF_OVF_U4_UN,
                DoCONF_OVF_U8_UN,
                DoCONF_OVF_I_UN,
                DoCONF_OVF_U_UN,
                DoBOX,
                DoNEWARR,
                DoLDLEN,
                DoLDLEMA,
                DoLDLEM_I1,
                DoLDLEM_U1,
                DoLDLEM_I2,
                DoLDLEM_U2,
                DoLDLEM_I4,
                DoLDLEM_U4,
                DoLDLEM_I8,
                DoLDLEM_I,
                DoLDLEM_R4,
                DoLDLEM_R8,
                DoLDLEM_REF,
                DoSTELEM_I,
                DoSTELEM_I1,
                DoSTELEM_I2,
                DoSTELEM_I4,
                DoSTELEM_I8,
                DoSTELEM_R4,
                DoSTELEM_R8,
                DoSTELEM_REF,
                DoLDELEM,
                DoSTELEM,
                DoUNBOX_ANY,
                DoNOP,
                DoNOP,
                DoNOP,
                DoNOP,
                DoNOP,
                DoNOP,
                DoNOP,
                DoNOP,
                DoNOP,
                DoNOP,
                DoNOP,
                DoNOP,
                DoNOP,
                DoCONV_OVF_I1,
                DoCONV_OVF_U1,
                DoCONV_OVF_I2,
                DoCONV_OVF_U2,
                DoCONV_OVF_I4,
                DoCONV_OVF_U4,
                DoCONV_OVF_I8,
                DoCONV_OVF_U8,
                DoNOP,
                DoNOP,
                DoNOP,
                DoNOP,
                DoNOP,
                DoNOP,
                DoNOP,
                DoREFANYVAL,
                DoCKFINITE,
                DoNOP,
                DoNOP,
                DoMKREFANY,
                DoNOP,
                DoNOP,
                DoNOP,
                DoNOP,
                DoNOP,
                DoNOP,
                DoNOP,
                DoNOP,
                DoNOP,
                DoLDTOKEN,
                DoCONV_U2,
                DoCONV_U1,
                DoCONV_I,
                DoCONV_OVF_I,
                DoCONV_OVF_U,
                DoADD_OVF,
                DoADD_OVF_UN,
                DoMUL_OVF,
                DoMUL_OVF_UN,
                DoSUB_OVF,
                DoSUB_OVF_UN,
                DoENDFINALLY,
                DoLEAVE,
                DoLEAVE_S,
                DoSTIND_I,
                DoCONV_U,
                DoNOP,
                DoNOP,
                DoNOP,
                DoNOP,
                DoNOP,
                DoNOP,
                DoNOP,
                DoNOP,
                DoNOP,
                DoNOP,
                DoNOP,
                DoNOP,
                DoNOP,
                DoNOP,
                DoNOP,
                DoCALL_LOC,
                DoCREATE_DELEGATE,
                DoINIT_ARRAY
            };
        }
    }
}
