using System;
using System.Collections.Generic;
using Parameters;
using Units;
using Equations; 
using Value;
using fsNumericalMethods;
using StepCalculators;
using CalculatorModules;

namespace CalculatorModules.Hydrocyclone.Feeds
{
    public class fsFeedFunctionsData
    {
        #region Parameter Identifiers

        public static fsParameterIdentifier x_id = new fsParameterIdentifier("x", fsCharacteristic.ParticleSize);
        public static fsParameterIdentifier xLog_id = new fsParameterIdentifier("xLog", fsCharacteristic.ParticleSize);

        public static fsParameterIdentifier F_id = new fsParameterIdentifier("F", fsCharacteristic.Concentration);
        public static fsParameterIdentifier Fu_id = new fsParameterIdentifier("Fu", fsCharacteristic.Concentration);
        public static fsParameterIdentifier Fo_id = new fsParameterIdentifier("Fo", fsCharacteristic.Concentration);

        public static fsParameterIdentifier f_id = new fsParameterIdentifier("f", fsCharacteristic.Concentration);
        public static fsParameterIdentifier fu_id = new fsParameterIdentifier("fu", fsCharacteristic.Concentration);
        public static fsParameterIdentifier fo_id = new fsParameterIdentifier("fo", fsCharacteristic.Concentration);

        public static fsParameterIdentifier G_id = new fsParameterIdentifier("G", fsCharacteristic.Concentration);
        public static fsParameterIdentifier GRed_id = new fsParameterIdentifier("GRed", fsCharacteristic.Concentration);


        #endregion

        #region Feed Functions as static ones

        public static fsValue F_func(fsValue xPar, fsValue lnSigmaG2, fsValue lnXG)
        {
            // TODO: определить глобальное "непоявление" кривых в случае плохих sigma_s и пр.
            //if (!lnSigmaG2.Defined || lnSigmaG2 == fsValue.Zero)
            //    return new fsValue();
            //if (!lnXG.Defined)
            //    return new fsValue();
            return 0.5 * (1 + fsSpecialFunctions.Erf((xPar - lnXG) / lnSigmaG2));
        }

        public static fsValue Fo_func(fsValue xPar, fsValue alpha, fsValue beta, fsValue zRed50, fsValue lnSigmaG2, fsValue lnXG)
        {
            return 0.5 * fsSpecialFunctions.ErfcExpInt(beta, zRed50, (xPar - lnXG) / lnSigmaG2) /
                         fsSpecialFunctions.Erfc(alpha * zRed50);
        }

        public static fsValue Fu_func(fsValue xPar, fsValue ET, fsValue alpha, fsValue beta, fsValue zRed50, fsValue lnSigmaG2, fsValue lnXG)
        {
            return 1 / ET * (F_func(xPar, lnSigmaG2, lnXG) - (1 - ET) * Fo_func(xPar, alpha, beta, zRed50, lnSigmaG2, lnXG));
        }

        public static fsValue GRed_func(fsValue xPar, fsValue lnSigmaS2, fsValue lnXred50)
        {
            return 0.5 * (1 + fsSpecialFunctions.Erf((xPar - lnXred50) / lnSigmaS2));
        }

        public static fsValue G_func(fsValue xPar, fsValue lnSigmaS2, fsValue lnXred50, fsValue rf)
        {
            return (1 - rf) * GRed_func(xPar, lnSigmaS2, lnXred50) + rf;
        }

        public static fsValue f_func(fsValue xPar, fsValue lnSigmaG2, fsValue lnXG, fsValue lnSigmaG2PiSqrt)
        {
            return lnSigmaG2PiSqrt * fsValue.Exp(-fsValue.Sqr((xPar - lnXG) / lnSigmaG2) - xPar);
        }

        public static fsValue fo_func(fsValue xPar, fsValue lnSigmaG2, fsValue lnXG, fsValue lnSigmaG2PiSqrt,
                           fsValue lnSigmaS2, fsValue lnXred50, fsValue rf, fsValue ET)
        {
            return 1 / (1 - ET) * (1 - G_func(xPar, lnSigmaS2, lnXred50, rf)) *
                   f_func(xPar, lnSigmaG2, lnXG, lnSigmaG2PiSqrt);
        }

        public static fsValue fu_func(fsValue xPar, fsValue lnSigmaG2, fsValue lnXG, fsValue lnSigmaG2PiSqrt,
                           fsValue lnSigmaS2, fsValue lnXred50, fsValue rf, fsValue ET)
        {
            return 1 / ET * G_func(xPar, lnSigmaS2, lnXred50, rf) *
                   f_func(xPar, lnSigmaG2, lnXG, lnSigmaG2PiSqrt);
        }

        #endregion

        #region Groups

        public static List<fsParametersGroup> Groups;

        public static Dictionary<fsParameterIdentifier, fsParametersGroup> ParameterToGroup;

        public static void getGroups()
        {
            fsParameterIdentifier[] parameters = new fsParameterIdentifier[] 
                                                 {
                                                     xLog_id, 
                                                     F_id, Fo_id, Fu_id,
                                                     f_id, fo_id, fu_id,
                                                     G_id, G_id,
                                                     GRed_id, GRed_id
                                                 };
            List<fsParametersGroup> Groups = new List<fsParametersGroup>();
            fsParametersGroup group;
            foreach (var parameter in parameters)
	        {
                group = new fsParametersGroup(true);
        		group.Parameters.Add(parameter);
                ParameterToGroup[parameter] = group;
                group.Representator = parameter;
                Groups.Add(group);
	        }
            group = new fsParametersGroup(false);
            group.Parameters.Add(x_id);
            ParameterToGroup[x_id] = group;
            group.Representator = x_id;
            Groups.Add(group);
        }

        #endregion
    }

    #region Equations

    public class BigFLogEquation : fsCalculatorEquation
    {
        #region Parameters

        private readonly IEquationParameter m_F;
        private readonly IEquationParameter m_x;
        private readonly IEquationParameter m_lnSigmaG2;
        private readonly IEquationParameter m_lnXG;

        #endregion

        public BigFLogEquation(
           IEquationParameter F,
           IEquationParameter x,
           IEquationParameter lnSigmaG2,
           IEquationParameter lnXG)
            : base(F, x, lnSigmaG2, lnXG)
        {
            m_F = F;
            m_x = x;
            m_lnSigmaG2 = lnSigmaG2;
            m_lnXG = lnXG;
        }

        protected override void InitFormulas()
        {
            AddFormula(m_F, BigFFormula);
        }

        private void BigFFormula()
        {
            m_F.Value = fsFeedFunctionsData.F_func(m_x.Value, m_lnSigmaG2.Value, m_lnXG.Value);
        }
    }

    public class BigFoLogEquation : fsCalculatorEquation
    {
        #region Parameters

        private readonly IEquationParameter m_Fo;
        private readonly IEquationParameter m_x;
        private readonly IEquationParameter m_alpha;
        private readonly IEquationParameter m_beta;
        private readonly IEquationParameter m_zRed50;
        private readonly IEquationParameter m_lnSigmaG2;
        private readonly IEquationParameter m_lnXG;

        #endregion

        public BigFoLogEquation(
           IEquationParameter Fo,
           IEquationParameter x,
           IEquationParameter alpha,
           IEquationParameter beta,
           IEquationParameter zRed50,
           IEquationParameter lnSigmaG2,
           IEquationParameter lnXG)
            : base(Fo, x, alpha, beta, zRed50, lnSigmaG2, lnXG)
        {
            m_Fo = Fo;
            m_x = x;
            m_alpha = alpha;
            m_beta = beta;
            m_zRed50 = zRed50;
            m_lnSigmaG2 = lnSigmaG2;
            m_lnXG = lnXG;
        }

        protected override void InitFormulas()
        {
            AddFormula(m_Fo, BigFoFormula);
        }

        private void BigFoFormula()
        {
            m_Fo.Value = fsFeedFunctionsData.Fo_func(m_x.Value, m_alpha.Value, m_beta.Value, 
                                                     m_zRed50.Value, m_lnSigmaG2.Value, m_lnXG.Value);
        }
    }

    public class BigFuLogEquation : fsCalculatorEquation
    {
        #region Parameters

        private readonly IEquationParameter m_Fu;
        private readonly IEquationParameter m_x;
        private readonly IEquationParameter m_ET;
        private readonly IEquationParameter m_alpha;
        private readonly IEquationParameter m_beta;
        private readonly IEquationParameter m_zRed50;
        private readonly IEquationParameter m_lnSigmaG2;
        private readonly IEquationParameter m_lnXG;

        #endregion

        public BigFuLogEquation(
           IEquationParameter Fu,
           IEquationParameter x,
           IEquationParameter ET,
           IEquationParameter alpha,
           IEquationParameter beta,
           IEquationParameter zRed50,
           IEquationParameter lnSigmaG2,
           IEquationParameter lnXG)
            : base(Fu, x, ET, alpha, beta, zRed50, lnSigmaG2, lnXG)
        {
            m_Fu = Fu;
            m_x = x;
            m_ET = ET;
            m_alpha = alpha;
            m_beta = beta;
            m_zRed50 = zRed50;
            m_lnSigmaG2 = lnSigmaG2;
            m_lnXG = lnXG;
        }

        protected override void InitFormulas()
        {
            AddFormula(m_Fu, BigFuFormula);
        }

        private void BigFuFormula()
        {
            m_Fu.Value = fsFeedFunctionsData.Fu_func(m_x.Value, m_ET.Value, m_alpha.Value, m_beta.Value, m_zRed50.Value, m_lnSigmaG2.Value, m_lnXG.Value);
        }
    }

    public class SmallFLogEquation : fsCalculatorEquation
    {
        #region Parameters

        private readonly IEquationParameter m_f;
        private readonly IEquationParameter m_x;
        private readonly IEquationParameter m_lnSigmaG2;
        private readonly IEquationParameter m_lnXG;
        private readonly IEquationParameter m_lnSigmaG2PiSqrt;

        #endregion

        public SmallFLogEquation(
           IEquationParameter f,
           IEquationParameter x,
           IEquationParameter lnSigmaG2,
           IEquationParameter lnXG,
           IEquationParameter lnSigmaG2PiSqrt)
            : base(f, x, lnSigmaG2, lnXG, lnSigmaG2PiSqrt)
        {
            m_f = f;
            m_x = x;
            m_lnSigmaG2 = lnSigmaG2;
            m_lnXG = lnXG;
            m_lnSigmaG2PiSqrt = lnSigmaG2PiSqrt;
        }

        protected override void InitFormulas()
        {
            AddFormula(m_f, SmallFFormula);
        }

        private void SmallFFormula()
        {
            m_f.Value = fsFeedFunctionsData.f_func(m_x.Value, m_lnSigmaG2.Value, 
                                                   m_lnXG.Value, m_lnSigmaG2PiSqrt.Value);
        }
    }

    public class SmallFoLogEquation : fsCalculatorEquation
    {
        #region Parameters

        private readonly IEquationParameter m_fo;
        private readonly IEquationParameter m_x;
        private readonly IEquationParameter m_lnSigmaG2;
        private readonly IEquationParameter m_lnXG;
        private readonly IEquationParameter m_lnSigmaG2PiSqrt;
        private readonly IEquationParameter m_lnSigmaS2;
        private readonly IEquationParameter m_lnXred50;
        private readonly IEquationParameter m_rf;
        private readonly IEquationParameter m_ET;

        #endregion

        public SmallFoLogEquation(
           IEquationParameter fo,
           IEquationParameter x,
           IEquationParameter lnSigmaG2,
           IEquationParameter lnXG,
           IEquationParameter lnSigmaG2PiSqrt,
           IEquationParameter lnSigmaS2,
           IEquationParameter lnXred50,
           IEquationParameter rf,
           IEquationParameter ET)
            : base(fo, x, lnSigmaG2, lnXG, lnSigmaG2PiSqrt, lnSigmaS2, lnXred50, rf, ET)
        {
            m_fo = fo;
            m_x = x;
            m_lnSigmaG2 = lnSigmaG2;
            m_lnXG = lnXG;
            m_lnSigmaG2PiSqrt = lnSigmaG2PiSqrt;
            m_lnSigmaS2 = lnSigmaS2;
            m_lnXred50 = lnXred50;
            m_rf = rf;
            m_ET = ET;
        }

        protected override void InitFormulas()
        {
            AddFormula(m_fo, SmallFoFormula);
        }

        private void SmallFoFormula()
        {
            m_fo.Value = fsFeedFunctionsData.fo_func(m_x.Value, m_lnSigmaG2.Value, m_lnXG.Value, 
                                                     m_lnSigmaG2PiSqrt.Value, m_lnSigmaS2.Value, 
                                                     m_lnXred50.Value, m_rf.Value, m_ET.Value);
        }
    }

    public class SmallFuLogEquation : fsCalculatorEquation
    {
        #region Parameters

        private readonly IEquationParameter m_fu;
        private readonly IEquationParameter m_x;
        private readonly IEquationParameter m_lnSigmaG2;
        private readonly IEquationParameter m_lnXG;
        private readonly IEquationParameter m_lnSigmaG2PiSqrt;
        private readonly IEquationParameter m_lnSigmaS2;
        private readonly IEquationParameter m_lnXred50;
        private readonly IEquationParameter m_rf;
        private readonly IEquationParameter m_ET;

        #endregion

        public SmallFuLogEquation(
           IEquationParameter fu,
           IEquationParameter x,
           IEquationParameter lnSigmaG2,
           IEquationParameter lnXG,
           IEquationParameter lnSigmaG2PiSqrt,
           IEquationParameter lnSigmaS2,
           IEquationParameter lnXred50,
           IEquationParameter rf,
           IEquationParameter ET)
            : base(fu, x, lnSigmaG2, lnXG, lnSigmaG2PiSqrt, lnSigmaS2, lnXred50, rf, ET)
        {
            m_fu = fu;
            m_x = x;
            m_lnSigmaG2 = lnSigmaG2;
            m_lnXG = lnXG;
            m_lnSigmaG2PiSqrt = lnSigmaG2PiSqrt;
            m_lnSigmaS2 = lnSigmaS2;
            m_lnXred50 = lnXred50;
            m_rf = rf;
            m_ET = ET;
        }

        protected override void InitFormulas()
        {
            AddFormula(m_fu, SmallFuFormula);
        }

        private void SmallFuFormula()
        {
            m_fu.Value = fsFeedFunctionsData.fu_func(m_x.Value, m_lnSigmaG2.Value, m_lnXG.Value, 
                                                     m_lnSigmaG2PiSqrt.Value, m_lnSigmaS2.Value, 
                                                     m_lnXred50.Value, m_rf.Value, m_ET.Value);
        }
    }

    public class GLogEquation : fsCalculatorEquation
    {
        #region Parameters

        private readonly IEquationParameter m_G;
        private readonly IEquationParameter m_x;
        private readonly IEquationParameter m_lnSigmaS2;
        private readonly IEquationParameter m_lnXred50;
        private readonly IEquationParameter m_rf;

        #endregion

        public GLogEquation(
           IEquationParameter G,
           IEquationParameter x,
           IEquationParameter lnSigmaS2,
           IEquationParameter lnXred50,
           IEquationParameter rf)
            : base(G, x, lnSigmaS2, lnXred50, rf)
        {
            m_G = G;
            m_x = x;
            m_lnSigmaS2 = lnSigmaS2;
            m_lnXred50 = lnXred50;
            m_rf = rf;
        }

        protected override void InitFormulas()
        {
            AddFormula(m_G, GFormula);
        }

        private void GFormula()
        {
            m_G.Value = fsFeedFunctionsData.G_func(m_x.Value, m_lnSigmaS2.Value, m_lnXred50.Value, m_rf.Value);
        }
    }

    public class GRedLogEquation : fsCalculatorEquation
    {
        #region Parameters

        private readonly IEquationParameter m_GRed;
        private readonly IEquationParameter m_x;
        private readonly IEquationParameter m_lnSigmaS2;
        private readonly IEquationParameter m_lnXred50;
        private readonly IEquationParameter m_rf;

        #endregion

        public GRedLogEquation(
           IEquationParameter GRed,
           IEquationParameter x,
           IEquationParameter lnSigmaS2,
           IEquationParameter lnXred50)
            : base(GRed, x, lnSigmaS2, lnXred50)
        {
            m_GRed = GRed;
            m_x = x;
            m_lnSigmaS2 = lnSigmaS2;
            m_lnXred50 = lnXred50;
        }

        protected override void InitFormulas()
        {
            AddFormula(m_GRed, GRedFormula);
        }

        private void GRedFormula()
        {
            m_GRed.Value = fsFeedFunctionsData.GRed_func(m_x.Value, m_lnSigmaS2.Value, m_lnXred50.Value);
        }
    }

    #region Help Equations

    public class LogEquation : fsCalculatorEquation
    {
        #region Parameters

        private readonly IEquationParameter m_x;
        private readonly IEquationParameter m_lnX;

        #endregion

        public LogEquation(
           IEquationParameter x,
           IEquationParameter lnX)
            : base(x, lnX)
        {
            m_x = x;
            m_lnX = lnX;
        }

        protected override void InitFormulas()
        {
            AddFormula(m_lnX, LogFormula);
        }

        private void LogFormula()
        {
            m_lnX.Value = fsValue.Log(m_x.Value);
        }
    }

    public class NormEquation : fsCalculatorEquation
    {
        #region Parameters

        private readonly IEquationParameter m_norm;
        private readonly IEquationParameter m_x;
        private readonly IEquationParameter m_y;

        #endregion

        public NormEquation(
           IEquationParameter norm,
           IEquationParameter x,
           IEquationParameter y)
            : base(norm, x, y)
        {
            m_norm = norm;
            m_x = x;
            m_y = y;
        }

        protected override void InitFormulas()
        {
            AddFormula(m_norm, NormFormula);
        }

        private void NormFormula()
        {
            m_norm.Value = fsValue.Sqrt(fsValue.Sqr(m_x.Value) + fsValue.Sqr(m_y.Value));
        }
    }

    #endregion

    #endregion         
    
    #region Calculators

    public class LinearCalculator : fsCalculator
    {
        public LinearCalculator()
        {
            #region Parameters Initialization

            IEquationParameter x = AddVariable(fsFeedFunctionsData.x_id);
            IEquationParameter xLog = AddVariable(fsFeedFunctionsData.xLog_id);

            IEquationParameter F = AddVariable(fsFeedFunctionsData.F_id);
            IEquationParameter Fo = AddVariable(fsFeedFunctionsData.Fo_id);
            IEquationParameter Fu = AddVariable(fsFeedFunctionsData.Fu_id);

            IEquationParameter f = AddVariable(fsFeedFunctionsData.f_id);
            IEquationParameter fo = AddVariable(fsFeedFunctionsData.fo_id);
            IEquationParameter fu = AddVariable(fsFeedFunctionsData.fu_id);

            IEquationParameter G = AddVariable(fsFeedFunctionsData.G_id);
            IEquationParameter GRed = AddVariable(fsFeedFunctionsData.GRed_id);

            #region Help Parameters


            var constantOne = new fsCalculatorConstant(new fsParameterIdentifier("1")) { Value = fsValue.One };
            var constantTwoSqrt = new fsCalculatorConstant(new fsParameterIdentifier("2^(1/2)")) { Value = new fsValue(Math.Sqrt(2)) };
            var constantPiSqrt = new fsCalculatorConstant(new fsParameterIdentifier("pi^(1/2)")) { Value = new fsValue(Math.Sqrt(Math.PI)) };

            // lnXG
            IEquationParameter xG = AddConstant(fsParameterIdentifier.xg);
            IEquationParameter lnXG = AddConstant(new fsParameterIdentifier("lnXG"));
            Equations.Add(new LogEquation(xG, lnXG));

            // lnSigmaG2
            IEquationParameter sigmaG = AddConstant(fsParameterIdentifier.sigma_g);
            IEquationParameter lnSigmaG = AddConstant(new fsParameterIdentifier("lnSigmaG"));
            Equations.Add(new LogEquation(sigmaG, lnSigmaG));
            IEquationParameter lnSigmaG2 = AddConstant(new fsParameterIdentifier("lnSigmaG2"));
            Equations.Add(new fsProductEquation(lnSigmaG2, lnSigmaG, constantTwoSqrt));

            // lnSigmaG2PiSqrt
            IEquationParameter lnSigmaG2PiSqrt = AddConstant(new fsParameterIdentifier("lnSigmaG2PiSqrt"));
            Equations.Add(new fsProductsEquation(
                new[] { lnSigmaG2PiSqrt, constantPiSqrt, lnSigmaG2 },
                new[] { constantOne }));

            // lnSigmaS2
            IEquationParameter sigmaS = AddConstant(fsParameterIdentifier.sigma_s);
            IEquationParameter lnSigmaS = AddConstant(new fsParameterIdentifier("lnSigmaS"));
            Equations.Add(new LogEquation(sigmaS, lnSigmaS));
            IEquationParameter lnSigmaS2 = AddConstant(new fsParameterIdentifier("lnSigmaS2"));
            Equations.Add(new fsProductEquation(lnSigmaS2, lnSigmaS, constantTwoSqrt));

            //lnXred50
            IEquationParameter xRed50 = AddConstant(fsParameterIdentifier.ReducedCutSize);
            IEquationParameter lnXred50 = AddConstant(new fsParameterIdentifier("lnXred50"));
            Equations.Add(new LogEquation(xRed50, lnXred50));

            IEquationParameter rf = AddConstant(fsParameterIdentifier.rf);

            IEquationParameter ET = AddConstant(fsParameterIdentifier.TotalEfficiency);

            //zRed50
            IEquationParameter lnXGminusLnXred50 = AddConstant(new fsParameterIdentifier("lnXG - lnXred50"));
            Equations.Add(new fsSumEquation(lnXG, lnXGminusLnXred50, lnXred50));
            IEquationParameter zRed50 = AddConstant(new fsParameterIdentifier("zRed50"));
            Equations.Add(new fsProductEquation(lnXGminusLnXred50, zRed50, lnSigmaS2));

            //alpha
            IEquationParameter normOfLns = AddConstant(new fsParameterIdentifier("normOfLns"));
            Equations.Add(new NormEquation(normOfLns, lnSigmaG, lnSigmaS));
            IEquationParameter alpha = AddConstant(new fsParameterIdentifier("alpha"));
            Equations.Add(new fsProductEquation(lnSigmaS, alpha, normOfLns));

            //beta
            IEquationParameter beta = AddConstant(new fsParameterIdentifier("beta"));
            Equations.Add(new fsProductEquation(lnSigmaG, beta, lnSigmaS));


            #endregion

            #endregion

            #region Equations Initialization

            Equations.Add(new LogEquation(x, xLog));

            Equations.Add(new BigFLogEquation(F, xLog, lnSigmaG2, lnXG));
            Equations.Add(new BigFoLogEquation(Fo, xLog, alpha, beta, zRed50, lnSigmaG2, lnXG));
            Equations.Add(new BigFuLogEquation(Fu, xLog, ET, alpha, beta, zRed50, lnSigmaG2, lnXG));

            Equations.Add(new SmallFLogEquation(f, xLog, lnSigmaG2, lnXG, lnSigmaG2PiSqrt));
            Equations.Add(new SmallFoLogEquation(fo, xLog, lnSigmaG2, lnXG, lnSigmaG2PiSqrt, lnSigmaS2, lnXred50, rf, ET));
            Equations.Add(new SmallFuLogEquation(fu, xLog, lnSigmaG2, lnXG, lnSigmaG2PiSqrt, lnSigmaS2, lnXred50, rf, ET));

            Equations.Add(new GLogEquation(G, xLog, lnSigmaS2, lnXred50, rf));
            Equations.Add(new GRedLogEquation(GRed, xLog, lnSigmaS2, lnXred50));

            #endregion
        }
    }

    public class LogarithmCalculator : fsCalculator
    {
        public LogarithmCalculator()
        {
            #region Parameters Initialization

            IEquationParameter x = AddVariable(fsFeedFunctionsData.x_id);

            IEquationParameter F = AddVariable(fsFeedFunctionsData.F_id);
            IEquationParameter Fo = AddVariable(fsFeedFunctionsData.Fo_id);
            IEquationParameter Fu = AddVariable(fsFeedFunctionsData.Fu_id);

            IEquationParameter f = AddVariable(fsFeedFunctionsData.f_id);
            IEquationParameter fo = AddVariable(fsFeedFunctionsData.fo_id);
            IEquationParameter fu = AddVariable(fsFeedFunctionsData.fu_id);

            IEquationParameter G = AddVariable(fsFeedFunctionsData.G_id);
            IEquationParameter GRed = AddVariable(fsFeedFunctionsData.GRed_id);

            #region Help Parameters


            var constantOne = new fsCalculatorConstant(new fsParameterIdentifier("1")) { Value = fsValue.One };
            var constantTwoSqrt = new fsCalculatorConstant(new fsParameterIdentifier("2^(1/2)")) { Value = new fsValue(Math.Sqrt(2)) };
            var constantPiSqrt = new fsCalculatorConstant(new fsParameterIdentifier("pi^(1/2)")) { Value = new fsValue(Math.Sqrt(Math.PI)) };

            // lnXG
            IEquationParameter xG = AddConstant(fsParameterIdentifier.xg);
            IEquationParameter lnXG = AddConstant(new fsParameterIdentifier("lnXG"));
            Equations.Add(new LogEquation(xG, lnXG));

            // lnSigmaG2
            IEquationParameter sigmaG = AddConstant(fsParameterIdentifier.sigma_g);
            IEquationParameter lnSigmaG = AddConstant(new fsParameterIdentifier("lnSigmaG"));
            Equations.Add(new LogEquation(sigmaG, lnSigmaG));
            IEquationParameter lnSigmaG2 = AddConstant(new fsParameterIdentifier("lnSigmaG2"));
            Equations.Add(new fsProductEquation(lnSigmaG2, lnSigmaG, constantTwoSqrt));

            // lnSigmaG2PiSqrt
            IEquationParameter lnSigmaG2PiSqrt = AddConstant(new fsParameterIdentifier("lnSigmaG2PiSqrt"));
            Equations.Add(new fsProductsEquation(
                new[] { lnSigmaG2PiSqrt, constantPiSqrt, lnSigmaG2 },
                new[] { constantOne }));

            // lnSigmaS2
            IEquationParameter sigmaS = AddConstant(fsParameterIdentifier.sigma_s);
            IEquationParameter lnSigmaS = AddConstant(new fsParameterIdentifier("lnSigmaS"));
            Equations.Add(new LogEquation(sigmaS, lnSigmaS));
            IEquationParameter lnSigmaS2 = AddConstant(new fsParameterIdentifier("lnSigmaS2"));
            Equations.Add(new fsProductEquation(lnSigmaS2, lnSigmaS, constantTwoSqrt));

            //lnXred50
            IEquationParameter xRed50 = AddConstant(fsParameterIdentifier.ReducedCutSize);
            IEquationParameter lnXred50 = AddConstant(new fsParameterIdentifier("lnXred50"));
            Equations.Add(new LogEquation(xRed50, lnXred50));

            IEquationParameter rf = AddConstant(fsParameterIdentifier.rf);

            IEquationParameter ET = AddConstant(fsParameterIdentifier.TotalEfficiency);

            //zRed50
            IEquationParameter lnXGminusLnXred50 = AddConstant(new fsParameterIdentifier("lnXG - lnXred50"));
            Equations.Add(new fsSumEquation(lnXG, lnXGminusLnXred50, lnXred50));
            IEquationParameter zRed50 = AddConstant(new fsParameterIdentifier("zRed50"));
            Equations.Add(new fsProductEquation(lnXGminusLnXred50, zRed50, lnSigmaS2));

            //alpha
            IEquationParameter normOfLns = AddConstant(new fsParameterIdentifier("normOfLns"));
            Equations.Add(new NormEquation(normOfLns, lnSigmaG, lnSigmaS));
            IEquationParameter alpha = AddConstant(new fsParameterIdentifier("alpha"));
            Equations.Add(new fsProductEquation(lnSigmaS, alpha, normOfLns));

            //beta
            IEquationParameter beta = AddConstant(new fsParameterIdentifier("beta"));
            Equations.Add(new fsProductEquation(lnSigmaG, beta, lnSigmaS));


            #endregion


            #endregion

            #region Equations Initialization

            Equations.Add(new BigFLogEquation(F, x, lnSigmaG2, lnXG));
            Equations.Add(new BigFoLogEquation(Fo, x, alpha, beta, zRed50, lnSigmaG2, lnXG));
            Equations.Add(new BigFuLogEquation(Fu, x, ET, alpha, beta, zRed50, lnSigmaG2, lnXG));

            Equations.Add(new SmallFLogEquation(f, x, lnSigmaG2, lnXG, lnSigmaG2PiSqrt));
            Equations.Add(new SmallFoLogEquation(fo, x, lnSigmaG2, lnXG, lnSigmaG2PiSqrt, lnSigmaS2, lnXred50, rf, ET));
            Equations.Add(new SmallFuLogEquation(fu, x, lnSigmaG2, lnXG, lnSigmaG2PiSqrt, lnSigmaS2, lnXred50, rf, ET));

            Equations.Add(new GLogEquation(G, x, lnSigmaS2, lnXred50, rf));
            Equations.Add(new GRedLogEquation(GRed, x, lnSigmaS2, lnXred50));

            #endregion
        }
    }

    #endregion
 
}