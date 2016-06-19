using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter
{
    public class Interpreter
    {
        public Interpreter()
        {
            InitCommands();
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

        public int DoINIT_ARRAY(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoCREATE_DELEGATE(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoCALL_LOC(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }


        public int NotImplemented(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoNOP(Frame frame, Instruction ili)
        {
            return 0;
        }

        public int DoBREAK(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoLDARG_0(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoLDARG_1(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoLDARG_2(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoLDARG_3(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoLDARG(Frame frame, Instruction ili)
        {
            throw new NotImplementedException(); // more than 256 arguments are not supported
        }

        public int DoLDARG_S(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoLDARGA_S(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoLDARGA(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoLDLOC_0(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoLDLOC_1(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoLDLOC_2(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoLDLOC_3(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoLDLOC_S(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoLDLOCA(Frame frame, Instruction ili)
        {
            throw new NotImplementedException(); // more than 256 variables are not supported
        }

        public int DoLDLOC(Frame frame, Instruction ili)
        {
            throw new NotImplementedException(); // more than 256 variables are not supported
        }

        public int DoLDLOCA_S(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoSTLOC_0(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoSTLOC_1(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoSTLOC_2(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoSTLOC_3(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoSTLOC_S(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoSTLOC(Frame frame, Instruction ili)
        {
            throw new NotImplementedException(); // more than 256 variables are not supported
        }

        public int DoSTARG_S(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoSTARG(Frame frame, Instruction ili)
        {
            throw new NotImplementedException(); // more than 256 arguments are not supported
        }

        public int DoLDNULL(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoLDC_I4_M1(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoLDC_I4_0(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoLDC_I4_1(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoLDC_I4_2(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoLDC_I4_3(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoLDC_I4_4(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoLDC_I4_5(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoLDC_I4_6(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoLDC_I4_7(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoLDC_I4_8(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoLDC_I4_S(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoLDC_I4(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoLDC_I8(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoLDC_R4(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoLDC_R8(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoDUP(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoPOP(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoJMP(Frame frame, Instruction ili)
        {
            throw new NotImplementedException(); //not used in managed code
        }

        public int DoCALL(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoCALLI(Frame frame, Instruction ili)
        {
            throw new NotImplementedException(); //unmanaged code calls are not supported
        }

        public int DoRET(Frame frame, Instruction ili)
        {
            return -1;
        }

        public int DoBR_S(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoBR_FALSE_S(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoBR_TRUE_S(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoBEQ_S(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoBGE_S(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoBGT_S(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoBLE_S(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoBLT_S(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoBNE_UN_S(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoBGE_UN_S(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoBGT_UN_S(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoBLE_UN_S(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoBLT_UN_S(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoBR(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoBRFALSE(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoBRTRUE(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoBEQ(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoBGE(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoBGT(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoBLE(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoBLT(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoBNE_UN(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoBGE_UN(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoBGT_UN(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoBLE_UN(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoBLT_UN(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoSWITCH(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoLDIND_I1(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoLDIND_U1(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoLDIND_I2(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoLDIND_U2(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoLDIND_I4(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoLDIND_U4(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoLDIND_I8(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoLDIND_I(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoLDIND_R4(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoLDIND_R8(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoLDIND_REF(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoSTIND_REF(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoSTIND_I1(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoSTIND_I2(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoSTIND_I4(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoSTIND_I8(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoSTIND_R4(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoSTIND_R8(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoADD(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoSUB(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoMUL(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoDIV(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoDIV_UN(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoREM(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoREM_UN(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoAND(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoOR(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoXOR(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoSHL(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoSHR(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoSHR_UN(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoNEG(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoNOT(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoCONV_I1(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoCONV_I2(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoCONV_I4(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoCONV_I8(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoCONV_R4(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoCONV_R8(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoCONV_U4(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoCONV_U8(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoCALLVIRT(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoCPOBJ(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoLDOBJ(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoLDSTR(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoNEWOBJ(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoCASTCLASS(Frame frame, Instruction ili)
        {
            return 0; //external call only
        }

        public int DoISINST(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoCONV_R_UN(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoUNBOX(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoTHROW(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoLDFLD(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoLDFLDA(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoSTFLD(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoLDSFLD(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoLDSFLDA(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoSTSFLD(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoSTOBJ(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoCONF_OVF_I1_UN(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoCONF_OVF_I2_UN(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoCONF_OVF_I4_UN(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoCONF_OVF_I8_UN(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoCONF_OVF_U1_UN(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoCONF_OVF_U2_UN(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoCONF_OVF_U4_UN(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoCONF_OVF_U8_UN(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoCONF_OVF_I_UN(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoCONF_OVF_U_UN(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoBOX(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoNEWARR(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoLDLEN(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoLDLEMA(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoLDLEM_I1(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoLDLEM_U1(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoLDLEM_I2(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoLDLEM_U2(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoLDLEM_I4(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoLDLEM_U4(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoLDLEM_I8(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoLDLEM_I(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoLDLEM_R4(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoLDLEM_R8(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoLDLEM_REF(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoSTELEM_I(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoSTELEM_I1(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoSTELEM_I2(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }


        public int DoSTELEM_I4(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoSTELEM_I8(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoSTELEM_R4(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoSTELEM_R8(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoSTELEM_REF(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoLDELEM(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoSTELEM(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoUNBOX_ANY(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoCONV_OVF_I1(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoCONV_OVF_U1(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoCONV_OVF_I2(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoCONV_OVF_U2(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoCONV_OVF_I4(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoCONV_OVF_U4(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoCONV_OVF_I8(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoCONV_OVF_U8(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoREFANYVAL(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoCKFINITE(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoMKREFANY(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoLDTOKEN(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoCONV_U2(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoCONV_U1(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoCONV_I(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoCONV_OVF_I(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoCONV_OVF_U(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoADD_OVF(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoADD_OVF_UN(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoMUL_OVF(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoMUL_OVF_UN(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoSUB_OVF(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoSUB_OVF_UN(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoENDFINALLY(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoLEAVE(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoLEAVE_S(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoSTIND_I(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoCONV_U(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoARGLIST(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoCEQ(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoCGT(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoCGT_UN(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoCLT(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoCLT_UN(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoLD_FTN(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoLDVIRT_FTN(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoLOCALLOC(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoENDFILTER(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoUNALIGNED(Frame frame, Instruction ili)
        {
            return 0; // prexif
        }

        public int DoVOLATILE(Frame frame, Instruction ili)
        {
            return 0; // prefix
        }

        public int DoTAIL(Frame frame, Instruction ili)
        {
            return 0; // prefix
        }

        public int DoINITOBJ(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoCONSTRAINED(Frame frame, Instruction ili)
        {
            return 0; // prefix
        }

        public int DoCPBLK(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoINITBLK(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoNO(Frame frame, Instruction ili)
        {
            return 0; // prefix
        }

        public int DoRETHROW(Frame frame, Instruction ili)
        {
            return 0;
        }

        public int DoSIZEOF(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoREFANYTYPE(Frame frame, Instruction ili)
        {
            throw new NotImplementedException();
        }

        public int DoREADONLY(Frame frame, Instruction ili)
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
