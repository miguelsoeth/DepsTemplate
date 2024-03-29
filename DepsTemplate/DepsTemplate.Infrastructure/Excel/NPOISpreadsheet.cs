﻿using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using NPOI.SS.UserModel;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace DepsTemplate.Infrastructure.Excel
{
    public partial class NPOISpreadsheet
    {
        private ISheet Sheet { get; set; }
        private HSSFWorkbook Workbook { get; set; }
        private Dictionary<Font, IFont> Fonts { get; set; } = new Dictionary<Font, IFont>();
        private Dictionary<ExcelStyle.Style, ICellStyle> Styles { get; set; } = new Dictionary<ExcelStyle.Style, ICellStyle>();

        public NPOISpreadsheet(string nome, CultureInfo culture = null)
        {
            CreateWorkbook(nome);
            CreateCustomColors();
            CreateFonts();
            CreateStyles(culture);
            CreateDefaultSheet(nome);
        }

        private NPOI.SS.UserModel.CellType GetCellType(ExcelStyle.CellType cellType) => cellType switch
        {
            ExcelStyle.CellType.Numerico => NPOI.SS.UserModel.CellType.Numeric,
            ExcelStyle.CellType.Boolean => NPOI.SS.UserModel.CellType.Boolean,
            ExcelStyle.CellType.Formula => NPOI.SS.UserModel.CellType.Formula,
            _ => NPOI.SS.UserModel.CellType.String
        };

        public void SetCell(
            int row,
            Column column,
            string texto,
            ExcelStyle.Style style = ExcelStyle.Style.Padrao,
            ExcelStyle.CellType cellType = ExcelStyle.CellType.Texto)
        {
            row--;

            var columnInt = (int)--column;

            var cell = GetCellAt(row, columnInt);

            if (cellType == ExcelStyle.CellType.Formula)
            {
                cell.SetCellType(GetCellType(cellType));

                cell.CellFormula = texto;
            }
            else
            {
                WriteCellValue(cell, texto, cellType);
            }

            cell.CellStyle = Styles[style];
        }

        public void SetMergedCell(
            int rowStart,
            Column columnStart,
            int rowFinish,
            Column columnFinish,
            string texto,
            ExcelStyle.Style style = ExcelStyle.Style.Padrao,
            ExcelStyle.CellType cellType = ExcelStyle.CellType.Texto)
        {
            rowStart--;
            rowFinish--;

            var columnStartInt = (int)--columnStart;
            var columnFinishInt = (int)--columnFinish;

            MergeCells(rowStart, rowFinish, columnStartInt, columnFinishInt);

            var cell = GetCellAt(rowStart, (int)columnStart);

            WriteCellValue(cell, texto, cellType);

            for (var x = rowStart; x <= rowFinish; x++)
            {
                for (var y = columnStart; y <= columnFinish; y++)
                {
                    var mergedCell = GetCellAt(x, (int)y);

                    mergedCell.CellStyle = Styles[style];
                }
            }
        }

        public MemoryStream GetMemoryStream()
        {
            var file = new MemoryStream();

            Workbook.Write(file);

            file.Seek(0, SeekOrigin.Begin);

            return file;
        }

        private void WriteCellValue(ICell cell, string texto, ExcelStyle.CellType cellType)
        {
            cell.SetCellType(GetCellType(cellType));

            if (cellType == ExcelStyle.CellType.Numerico)
            {
                _ = double.TryParse(texto, out double value);

                cell.SetCellValue(value);
            }
            else
            {
                cell.SetCellValue(texto);
            }
        }

        private void MergeCells(int xStart, int xFinish, int yStart, int yFinish)
        {
            var cellRangeAddress = new NPOI.SS.Util.CellRangeAddress(xStart, xFinish, yStart, yFinish);

            Sheet.AddMergedRegion(cellRangeAddress);
        }

        private ICell GetCellAt(int x, int y)
        {
            var row = Sheet.GetRow(x);

            if (row == null)
            {
                row = Sheet.CreateRow(x);
            }

            var cell = row.GetCell(y);

            if (cell == null)
            {
                cell = row.CreateCell(y);
            }

            return cell;
        }

        private void CreateWorkbook(string nome)
        {
            Workbook = new HSSFWorkbook();
        }

        private void CreateDefaultSheet(string sheetName)
        {
            Sheet = Workbook.CreateSheet(sheetName);

            Sheet.DefaultRowHeightInPoints = 18;
            Sheet.DefaultColumnWidth = 35;
            Sheet.DisplayGridlines = false;

            for (var i = 0; i < 100; i++)
            {
                Sheet.SetDefaultColumnStyle(i, Styles[ExcelStyle.Style.Padrao]);
                Sheet.SetColumnWidth(i, 22 * 256);
            }
        }

        private void CreateFonts()
        {
            var fontCinza = CreateFont();
            var fontPadrao = CreateFont();
            var fontNegrito = CreateFont();

            fontCinza.Color = IndexedColors.Grey40Percent.Index;

            fontNegrito.IsBold = true;

            Fonts.Add(Font.Cinza, fontCinza);
            Fonts.Add(Font.Padrao, fontPadrao);
            Fonts.Add(Font.Negrito, fontNegrito);
        }

        private IFont CreateFont()
        {
            var font = Workbook.CreateFont();

            font.FontName = "Calibri";
            font.FontHeightInPoints = 12;
            font.Color = IndexedColors.Grey80Percent.Index;

            return font;
        }

        private void CreateCustomColors()
        {
            var palette = Workbook.GetCustomPalette();
            palette.SetColorAtIndex(HSSFColor.Grey25Percent.Index, (byte)220, (byte)220, (byte)220);
            palette.SetColorAtIndex(HSSFColor.Grey40Percent.Index, (byte)190, (byte)190, (byte)190);
        }

        private void CreateStyles(CultureInfo culture)
        {
            IDataFormat format = Workbook.CreateDataFormat();

            var styleNull = Workbook.CreateCellStyle();
            var stylePadrao = Workbook.CreateCellStyle();
            var styleTabelaData = Workbook.CreateCellStyle();
            var styleFiltroCampo = Workbook.CreateCellStyle();
            var styleFiltroLabel = Workbook.CreateCellStyle();
            var styleTabelaMoeda = Workbook.CreateCellStyle();
            var styleTabelaInteiro = Workbook.CreateCellStyle();
            var styleTabelaDecimal = Workbook.CreateCellStyle();
            var styleFiltroCabecalho = Workbook.CreateCellStyle();
            var styleTabelaCabecalhoLeft = Workbook.CreateCellStyle();
            var styleTabelaCabecalhoRight = Workbook.CreateCellStyle();

            styleFiltroCabecalho.SetFont(Fonts[Font.Negrito]);
            styleFiltroCabecalho.BorderTop = BorderStyle.Thin;
            styleFiltroCabecalho.BorderLeft = BorderStyle.Thin;
            styleFiltroCabecalho.BorderRight = BorderStyle.Thin;
            styleFiltroCabecalho.BorderBottom = BorderStyle.Thin;
            styleFiltroCabecalho.Alignment = HorizontalAlignment.Center;
            styleFiltroCabecalho.FillPattern = FillPattern.SolidForeground;
            styleFiltroCabecalho.FillForegroundColor = HSSFColor.Grey25Percent.Index;

            styleFiltroLabel.SetFont(Fonts[Font.Negrito]);
            styleFiltroLabel.BorderLeft = BorderStyle.Thin;
            styleFiltroLabel.BorderRight = BorderStyle.Thin;
            styleFiltroLabel.BorderBottom = BorderStyle.Thin;
            styleFiltroLabel.Alignment = HorizontalAlignment.Left;
            styleFiltroLabel.FillPattern = FillPattern.SolidForeground;
            styleFiltroLabel.FillForegroundColor = HSSFColor.Grey25Percent.Index;

            styleFiltroCampo.SetFont(Fonts[Font.Padrao]);
            styleFiltroCampo.BorderRight = BorderStyle.Thin;
            styleFiltroCampo.BorderBottom = BorderStyle.Thin;
            styleFiltroCampo.Alignment = HorizontalAlignment.Center;

            styleTabelaCabecalhoLeft.SetFont(Fonts[Font.Negrito]);
            styleTabelaCabecalhoLeft.BorderTop = BorderStyle.Thin;
            styleTabelaCabecalhoLeft.BorderLeft = BorderStyle.Thin;
            styleTabelaCabecalhoLeft.BorderRight = BorderStyle.Thin;
            styleTabelaCabecalhoLeft.BorderBottom = BorderStyle.Thin;
            styleTabelaCabecalhoLeft.Alignment = HorizontalAlignment.Left;
            styleTabelaCabecalhoLeft.FillPattern = FillPattern.SolidForeground;
            styleTabelaCabecalhoLeft.FillForegroundColor = HSSFColor.Grey25Percent.Index;

            styleTabelaCabecalhoRight.SetFont(Fonts[Font.Negrito]);
            styleTabelaCabecalhoRight.BorderTop = BorderStyle.Thin;
            styleTabelaCabecalhoRight.BorderLeft = BorderStyle.Thin;
            styleTabelaCabecalhoRight.BorderRight = BorderStyle.Thin;
            styleTabelaCabecalhoRight.BorderBottom = BorderStyle.Thin;
            styleTabelaCabecalhoRight.Alignment = HorizontalAlignment.Right;
            styleTabelaCabecalhoRight.FillPattern = FillPattern.SolidForeground;
            styleTabelaCabecalhoRight.FillForegroundColor = HSSFColor.Grey25Percent.Index;

            stylePadrao.SetFont(Fonts[Font.Padrao]);

            styleTabelaInteiro.SetFont(Fonts[Font.Padrao]);
            styleTabelaInteiro.Alignment = HorizontalAlignment.Right;

            styleTabelaDecimal.SetFont(Fonts[Font.Padrao]);
            styleTabelaDecimal.Alignment = HorizontalAlignment.Right;
            styleTabelaDecimal.DataFormat = HSSFDataFormat.GetBuiltinFormat("0.00");

            var moeda = culture?.NumberFormat.CurrencySymbol ?? "R$";

            styleTabelaMoeda.SetFont(Fonts[Font.Padrao]);
            styleTabelaMoeda.Alignment = HorizontalAlignment.Right;
            styleTabelaMoeda.DataFormat = format.GetFormat($@"""{moeda}""#,##0.00");

            styleTabelaData.SetFont(Fonts[Font.Padrao]);

            styleNull.SetFont(Fonts[Font.Cinza]);
            styleNull.Alignment = HorizontalAlignment.Right;

            Styles.Add(ExcelStyle.Style.Null, styleNull);
            Styles.Add(ExcelStyle.Style.Padrao, stylePadrao);
            Styles.Add(ExcelStyle.Style.TabelaData, styleTabelaData);
            Styles.Add(ExcelStyle.Style.TabelaMoeda, styleTabelaMoeda);
            Styles.Add(ExcelStyle.Style.FiltroCampo, styleFiltroCampo);
            Styles.Add(ExcelStyle.Style.FiltroLabel, styleFiltroLabel);
            Styles.Add(ExcelStyle.Style.TabelaDecimal, styleTabelaDecimal);
            Styles.Add(ExcelStyle.Style.TabelaInteiro, styleTabelaInteiro);
            Styles.Add(ExcelStyle.Style.FiltroCabecalho, styleFiltroCabecalho);
            Styles.Add(ExcelStyle.Style.TabelaCabecalhoLeft, styleTabelaCabecalhoLeft);
            Styles.Add(ExcelStyle.Style.TabelaCabecalhoRight, styleTabelaCabecalhoRight);

            foreach (var style in Styles.Select(x => x.Value))
            {
                style.BottomBorderColor = HSSFColor.Grey40Percent.Index;
                style.TopBorderColor = HSSFColor.Grey40Percent.Index;
                style.RightBorderColor = HSSFColor.Grey40Percent.Index;
                style.LeftBorderColor = HSSFColor.Grey40Percent.Index;
            }
        }

        private enum Font
        {
            Cinza,
            Padrao,
            Negrito
        }

        public enum Column
        {
            A = 1,
            B = 2,
            C = 3,
            D = 4,
            E = 5,
            F = 6,
            G = 7,
            H = 8,
            I = 9,
            J = 10,
            K = 11,
            L = 12,
            M = 13,
            N = 14,
            O = 15,
            P = 16,
            Q = 17,
            R = 18,
            S = 19,
            T = 20,
            U = 21,
            V = 22,
            W = 23,
            X = 24,
            Y = 25,
            Z = 26,
            AA = 27,
            AB = 28,
            AC = 29,
            AD = 30,
            AE = 31,
            AF = 32,
            AG = 33,
            AH = 34,
            AI = 35,
            AJ = 36,
            AK = 37,
            AL = 38,
            AM = 39,
            AN = 40,
            AO = 41,
            AP = 42,
            AQ = 43,
            AR = 44,
            AS = 45,
            AT = 46,
            AU = 47,
            AV = 48,
            AW = 49,
            AX = 50,
            AY = 51,
            AZ = 52,
            BA = 53,
            BB = 54,
            BC = 55,
            BD = 56,
            BE = 57,
            BF = 58,
            BG = 59,
            BH = 60,
            BI = 61,
            BJ = 62,
            BK = 63,
            BL = 64,
            BM = 65,
            BN = 66,
            BO = 67,
            BP = 68,
            BQ = 69,
            BR = 70,
            BS = 71,
            BT = 72,
            BU = 73,
            BV = 74,
            BW = 75,
            BX = 76,
            BY = 77,
            BZ = 78,
            CA = 79,
            CB = 80,
            CC = 81,
            CD = 82,
            CE = 83,
            CF = 84,
            CG = 85,
            CH = 86,
            CI = 87,
            CJ = 88,
            CK = 89,
            CL = 90,
            CM = 91,
            CN = 92,
            CO = 93,
            CP = 94,
            CQ = 95,
            CR = 96,
            CS = 97,
            CT = 98,
            CU = 99,
            CV = 100,
            CW = 101,
            CX = 102,
            CY = 103,
            CZ = 104,
            DA = 105,
            DB = 106,
            DC = 107,
            DD = 108,
            DE = 109,
            DF = 110,
            DG = 111,
            DH = 112,
            DI = 113,
            DJ = 114,
            DK = 115,
            DL = 116,
            DM = 117,
            DN = 118,
            DO = 119,
            DP = 120,
            DQ = 121,
            DR = 122,
            DS = 123,
            DT = 124,
            DU = 125,
            DV = 126,
            DW = 127,
            DX = 128,
            DY = 129,
            DZ = 130,
            EA = 131,
            EB = 132,
            EC = 133,
            ED = 134,
            EE = 135,
            EF = 136,
            EG = 137,
            EH = 138,
            EI = 139,
            EJ = 140,
            EK = 141,
            EL = 142,
            EM = 143,
            EN = 144,
            EO = 145,
            EP = 146,
            EQ = 147,
            ER = 148,
            ES = 149,
            ET = 150,
            EU = 151,
            EV = 152,
            EW = 153,
            EX = 154,
            EY = 155,
            EZ = 156,
            FA = 157,
            FB = 158,
            FC = 159,
            FD = 160,
            FE = 161,
            FF = 162,
            FG = 163,
            FH = 164,
            FI = 165,
            FJ = 166,
            FK = 167,
            FL = 168,
            FM = 169,
            FN = 170,
            FO = 171,
            FP = 172,
            FQ = 173,
            FR = 174,
            FS = 175,
            FT = 176,
            FU = 177,
            FV = 178,
            FW = 179,
            FX = 180,
            FY = 181,
            FZ = 182,
            GA = 183,
            GB = 184,
            GC = 185,
            GD = 186,
            GE = 187,
            GF = 188,
            GG = 189,
            GH = 190,
            GI = 191,
            GJ = 192,
            GK = 193,
            GL = 194,
            GM = 195,
            GN = 196,
            GO = 197,
            GP = 198,
            GQ = 199,
            GR = 200,
            GS = 201,
            GT = 202,
            GU = 203,
            GV = 204,
            GW = 205,
            GX = 206,
            GY = 207,
            GZ = 208,
            HA = 209,
            HB = 210,
            HC = 211,
            HD = 212,
            HE = 213,
            HF = 214,
            HG = 215,
            HH = 216,
            HI = 217,
            HJ = 218,
            HK = 219,
            HL = 220,
            HM = 221,
            HN = 222,
            HO = 223,
            HP = 224,
            HQ = 225,
            HR = 226,
            HS = 227,
            HT = 228,
            HU = 229,
            HV = 230,
            HW = 231,
            HX = 232,
            HY = 233,
            HZ = 234,
            IA = 235,
            IB = 236,
            IC = 237,
            ID = 238,
            IE = 239,
            IF = 240,
            IG = 241,
            IH = 242,
            II = 243,
            IJ = 244,
            IK = 245,
            IL = 246,
            IM = 247,
            IN = 248,
            IO = 249,
            IP = 250,
            IQ = 251,
            IR = 252,
            IS = 253,
            IT = 254,
            IU = 255,
            IV = 256,
            IW = 257,
            IX = 258,
            IY = 259,
            IZ = 260,
            JA = 261,
            JB = 262,
            JC = 263,
            JD = 264,
            JE = 265,
            JF = 266,
            JG = 267,
            JH = 268,
            JI = 269,
            JJ = 270,
            JK = 271,
            JL = 272,
            JM = 273,
            JN = 274,
            JO = 275,
            JP = 276,
            JQ = 277,
            JR = 278,
            JS = 279,
            JT = 280,
            JU = 281,
            JV = 282,
            JW = 283,
            JX = 284,
            JY = 285,
            JZ = 286,
            KA = 287,
            KB = 288,
            KC = 289,
            KD = 290,
            KE = 291,
            KF = 292,
            KG = 293,
            KH = 294,
            KI = 295,
            KJ = 296,
            KK = 297,
            KL = 298,
            KM = 299,
            KN = 300,
            KO = 301,
            KP = 302,
            KQ = 303,
            KR = 304,
            KS = 305,
            KT = 306,
            KU = 307,
            KV = 308,
            KW = 309,
            KX = 310,
            KY = 311,
            KZ = 312,
            LA = 313,
            LB = 314,
            LC = 315,
            LD = 316,
            LE = 317,
            LF = 318,
            LG = 319,
            LH = 320,
            LI = 321,
            LJ = 322,
            LK = 323,
            LL = 324,
            LM = 325,
            LN = 326,
            LO = 327,
            LP = 328,
            LQ = 329,
            LR = 330,
            LS = 331,
            LT = 332,
            LU = 333,
            LV = 334,
            LW = 335,
            LX = 336,
            LY = 337,
            LZ = 338,
            MA = 339,
            MB = 340,
            MC = 341,
            MD = 342,
            ME = 343,
            MF = 344,
            MG = 345,
            MH = 346,
            MI = 347,
            MJ = 348,
            MK = 349,
            ML = 350,
            MM = 351,
            MN = 352,
            MO = 353,
            MP = 354,
            MQ = 355,
            MR = 356,
            MS = 357,
            MT = 358,
            MU = 359,
            MV = 360,
            MW = 361,
            MX = 362,
            MY = 363,
            MZ = 364,
            NA = 365,
            NB = 366,
            NC = 367,
            ND = 368,
            NE = 369,
            NF = 370,
            NG = 371,
            NH = 372,
            NI = 373,
            NJ = 374,
            NK = 375,
            NL = 376,
            NM = 377,
            NN = 378,
            NO = 379,
            NP = 380,
            NQ = 381,
            NR = 382,
            NS = 383,
            NT = 384,
            NU = 385,
            NV = 386,
            NW = 387,
            NX = 388,
            NY = 389,
            NZ = 390,
            OA = 391,
            OB = 392,
            OC = 393,
            OD = 394,
            OE = 395,
            OF = 396,
            OG = 397,
            OH = 398,
            OI = 399,
            OJ = 400,
            OK = 401,
            OL = 402,
            OM = 403,
            ON = 404,
            OO = 405,
            OP = 406,
            OQ = 407,
            OR = 408,
            OS = 409,
            OT = 410,
            OU = 411,
            OV = 412,
            OW = 413,
            OX = 414,
            OY = 415,
            OZ = 416,
            PA = 417,
            PB = 418,
            PC = 419,
            PD = 420,
            PE = 421,
            PF = 422,
            PG = 423,
            PH = 424,
            PI = 425,
            PJ = 426,
            PK = 427,
            PL = 428,
            PM = 429,
            PN = 430,
            PO = 431,
            PP = 432,
            PQ = 433,
            PR = 434,
            PS = 435,
            PT = 436,
            PU = 437,
            PV = 438,
            PW = 439,
            PX = 440,
            PY = 441,
            PZ = 442,
            QA = 443,
            QB = 444,
            QC = 445,
            QD = 446,
            QE = 447,
            QF = 448,
            QG = 449,
            QH = 450,
            QI = 451,
            QJ = 452,
            QK = 453,
            QL = 454,
            QM = 455,
            QN = 456,
            QO = 457,
            QP = 458,
            QQ = 459,
            QR = 460,
            QS = 461,
            QT = 462,
            QU = 463,
            QV = 464,
            QW = 465,
            QX = 466,
            QY = 467,
            QZ = 468,
            RA = 469,
            RB = 470,
            RC = 471,
            RD = 472,
            RE = 473,
            RF = 474,
            RG = 475,
            RH = 476,
            RI = 477,
            RJ = 478,
            RK = 479,
            RL = 480,
            RM = 481,
            RN = 482,
            RO = 483,
            RP = 484,
            RQ = 485,
            RR = 486,
            RS = 487,
            RT = 488,
            RU = 489,
            RV = 490,
            RW = 491,
            RX = 492,
            RY = 493,
            RZ = 494,
            SA = 495,
            SB = 496,
            SC = 497,
            SD = 498,
            SE = 499,
            SF = 500,
            SG = 501,
            SH = 502,
            SI = 503,
            SJ = 504,
            SK = 505,
            SL = 506,
            SM = 507,
            SN = 508,
            SO = 509,
            SP = 510,
            SQ = 511,
            SR = 512,
            SS = 513,
            ST = 514,
            SU = 515,
            SV = 516,
            SW = 517,
            SX = 518,
            SY = 519,
            SZ = 520,
            TA = 521,
            TB = 522,
            TC = 523,
            TD = 524,
            TE = 525,
            TF = 526,
            TG = 527,
            TH = 528,
            TI = 529,
            TJ = 530,
            TK = 531,
            TL = 532,
            TM = 533,
            TN = 534,
            TO = 535,
            TP = 536,
            TQ = 537,
            TR = 538,
            TS = 539,
            TT = 540,
            TU = 541,
            TV = 542,
            TW = 543,
            TX = 544,
            TY = 545,
            TZ = 546,
            UA = 547,
            UB = 548,
            UC = 549,
            UD = 550,
            UE = 551,
            UF = 552,
            UG = 553,
            UH = 554,
            UI = 555,
            UJ = 556,
            UK = 557,
            UL = 558,
            UM = 559,
            UN = 560,
            UO = 561,
            UP = 562,
            UQ = 563,
            UR = 564,
            US = 565,
            UT = 566,
            UU = 567,
            UV = 568,
            UW = 569,
            UX = 570,
            UY = 571,
            UZ = 572,
            VA = 573,
            VB = 574,
            VC = 575,
            VD = 576,
            VE = 577,
            VF = 578,
            VG = 579,
            VH = 580,
            VI = 581,
            VJ = 582,
            VK = 583,
            VL = 584,
            VM = 585,
            VN = 586,
            VO = 587,
            VP = 588,
            VQ = 589,
            VR = 590,
            VS = 591,
            VT = 592,
            VU = 593,
            VV = 594,
            VW = 595,
            VX = 596,
            VY = 597,
            VZ = 598,
            WA = 599,
            WB = 600,
            WC = 601,
            WD = 602,
            WE = 603,
            WF = 604,
            WG = 605,
            WH = 606,
            WI = 607,
            WJ = 608,
            WK = 609,
            WL = 610,
            WM = 611,
            WN = 612,
            WO = 613,
            WP = 614,
            WQ = 615,
            WR = 616,
            WS = 617,
            WT = 618,
            WU = 619,
            WV = 620,
            WW = 621,
            WX = 622,
            WY = 623,
            WZ = 624,
            XA = 625,
            XB = 626,
            XC = 627,
            XD = 628,
            XE = 629,
            XF = 630,
            XG = 631,
            XH = 632,
            XI = 633,
            XJ = 634,
            XK = 635,
            XL = 636,
            XM = 637,
            XN = 638,
            XO = 639,
            XP = 640,
            XQ = 641,
            XR = 642,
            XS = 643,
            XT = 644,
            XU = 645,
            XV = 646,
            XW = 647,
            XX = 648,
            XY = 649,
            XZ = 650,
            YA = 651,
            YB = 652,
            YC = 653,
            YD = 654,
            YE = 655,
            YF = 656,
            YG = 657,
            YH = 658,
            YI = 659,
            YJ = 660,
            YK = 661,
            YL = 662,
            YM = 663,
            YN = 664,
            YO = 665,
            YP = 666,
            YQ = 667,
            YR = 668,
            YS = 669,
            YT = 670,
            YU = 671,
            YV = 672,
            YW = 673,
            YX = 674,
            YY = 675,
            YZ = 676,
            ZA = 677,
            ZB = 678,
            ZC = 679,
            ZD = 680,
            ZE = 681,
            ZF = 682,
            ZG = 683,
            ZH = 684,
            ZI = 685,
            ZJ = 686,
            ZK = 687,
            ZL = 688,
            ZM = 689,
            ZN = 690,
            ZO = 691,
            ZP = 692,
            ZQ = 693,
            ZR = 694,
            ZS = 695,
            ZT = 696,
            ZU = 697,
            ZV = 698,
            ZW = 699,
            ZX = 700,
            ZY = 701,
            ZZ = 702
        }
    }
}
