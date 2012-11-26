using System;
using System.Collections.Generic;
using Parameters;
using Units;
using Equations; 
using Value;
using fsNumericalMethods;
using StepCalculators;
using CalculatorModules;
using ParametersIdentifiers;
using ParametersIdentifiers.Ranges;
using CalculatorModules.Machine_Ranges;

namespace CalculatorModules.Hydrocyclone.Feeds
{
    public class fsFeedFunctionsData
    {
        #region Parameter Identifiers

        public static fsParameterIdentifier x_id = new fsParameterIdentifier("x", fsCharacteristic.ParticleSize);

        public static fsParameterIdentifier F_id = new fsParameterIdentifier("F", fsCharacteristic.Concentration);
        public static fsParameterIdentifier Fu_id = new fsParameterIdentifier("Fu", fsCharacteristic.Concentration);
        public static fsParameterIdentifier Fo_id = new fsParameterIdentifier("Fo", fsCharacteristic.Concentration);

        public static fsParameterIdentifier f_id = new fsParameterIdentifier("f", fsCharacteristic.FeedDerivative);
        public static fsParameterIdentifier fu_id = new fsParameterIdentifier("fu", fsCharacteristic.FeedDerivative);
        public static fsParameterIdentifier fo_id = new fsParameterIdentifier("fo", fsCharacteristic.FeedDerivative);

        public static fsParameterIdentifier G_id = new fsParameterIdentifier("G", fsCharacteristic.Concentration);
        public static fsParameterIdentifier GRed_id = new fsParameterIdentifier("GRed", fsCharacteristic.Concentration);

        private static fsParameterIdentifier[] parameterList = new fsParameterIdentifier[] 
                                                               {
                                                                   x_id, 
                                                                   F_id, Fo_id, Fu_id,
                                                                   f_id, fo_id, fu_id,
                                                                   G_id, GRed_id
                                                               };  


        #endregion

        // TODO: определить глобальное "непоявление" кривых в случае плохих sigma_s и пр.
        //if (!lnSigmaG2.Defined || lnSigmaG2 == fsValue.Zero)
        //    return new fsValue();
        //if (!lnXG.Defined)
        //    return new fsValue();

        #region Feed Functions as static ones
             
        public static fsValue F_func(fsValue x, fsValue lnSigmaG2, fsValue lnXG)
        {           
            return 0.5 * (1 + fsSpecialFunctions.Erf((fsValue.Log(x) - lnXG) / lnSigmaG2));
        }

        public static fsValue Fo_func(fsValue x, fsValue alpha, fsValue beta, fsValue zRed50, fsValue lnSigmaG2, fsValue lnXG)
        {
            return 0.5 * fsSpecialFunctions.ErfcExpInt(beta, zRed50, (fsValue.Log(x) - lnXG) / lnSigmaG2) /
                         fsSpecialFunctions.Erfc(alpha * zRed50);
        }

        public static fsValue Fu_func(fsValue x, fsValue ET, fsValue alpha, fsValue beta, fsValue zRed50, fsValue lnSigmaG2, fsValue lnXG)
        {
            return 1 / ET * (F_func(x, lnSigmaG2, lnXG) - (1 - ET) * Fo_func(x, alpha, beta, zRed50, lnSigmaG2, lnXG));
        }

        public static fsValue GRed_func(fsValue x, fsValue lnSigmaS2, fsValue lnXred50)
        {
            return 0.5 * (1 + fsSpecialFunctions.Erf((fsValue.Log(x) - lnXred50) / lnSigmaS2));
        }

        public static fsValue G_func(fsValue x, fsValue lnSigmaS2, fsValue lnXred50, fsValue rf)
        {
            return (1 - rf) * GRed_func(x, lnSigmaS2, lnXred50) + rf;
        }

        public static fsValue f_func(fsValue x, fsValue lnSigmaG2, fsValue lnXG, fsValue lnSigmaG2PiSqrt)
        {
            return lnSigmaG2PiSqrt / x * fsValue.Exp(-fsValue.Sqr((fsValue.Log(x) - lnXG) / lnSigmaG2));
        }

        public static fsValue fo_func(fsValue x, fsValue lnSigmaG2, fsValue lnXG, fsValue lnSigmaG2PiSqrt,
                           fsValue lnSigmaS2, fsValue lnXred50, fsValue rf, fsValue ET)
        {
            return 1 / (1 - ET) * (1 - G_func(x, lnSigmaS2, lnXred50, rf)) *
                   f_func(x, lnSigmaG2, lnXG, lnSigmaG2PiSqrt);
        }

        public static fsValue fu_func(fsValue x, fsValue lnSigmaG2, fsValue lnXG, fsValue lnSigmaG2PiSqrt,
                           fsValue lnSigmaS2, fsValue lnXred50, fsValue rf, fsValue ET)
        {
            return 1 / ET * G_func(x, lnSigmaS2, lnXred50, rf) *
                   f_func(x, lnSigmaG2, lnXG, lnSigmaG2PiSqrt);
        }

        #endregion

        #region Groups

        public static List<fsParametersGroup> Groups = new List<fsParametersGroup>();

        public static Dictionary<fsParameterIdentifier, fsParametersGroup> ParameterToGroup = new Dictionary<fsParameterIdentifier, fsParametersGroup>();

        public static void getGroups()
        {
            fsParametersGroup group;
            group = new fsParametersGroup(false);
            group.Parameters.Add(x_id);
            ParameterToGroup.Add(x_id, group);
            group.Representator = x_id;
            group.SetIsInputFlag(true);
            Groups.Add(group);
            for (int i = 1; i < parameterList.Length; i++)
            {
                group = new fsParametersGroup(true);
                group.Parameters.Add(parameterList[i]);
                ParameterToGroup.Add(parameterList[i], group);
                group.Representator = parameterList[i];
                Groups.Add(group); 
            }           
        }

        #endregion

        #region Values

        public static Dictionary<fsParameterIdentifier, fsSimulationModuleParameter> Values = new  Dictionary<fsParameterIdentifier, fsSimulationModuleParameter>();

        public static void getValues()
        {
            fsRange machr = fsMachineRanges.DefaultMachineRanges.Ranges[fsParameterIdentifier.ReducedCutSize].Range;
            fsValue from = machr.From;
            fsValue to = machr.To;
            Values.Add(x_id, new fsSimulationModuleParameter(x_id, new fsValue(), new fsRange(from, to)));
            //Values.Add(xLog_id, new fsSimulationModuleParameter(xLog_id, new fsValue(), new fsRange(fsValue.Log(from), fsValue.Log(to))));
            //for (int i = 2; i < parameterList.Length; i++)
            for (int i = 1; i < parameterList.Length; i++)
            {
                Values.Add(parameterList[i], new fsSimulationModuleParameter(parameterList[i], new fsValue())); 
            }
        }

        #endregion

        #region Calculators

        public static List<fsCalculator> Calculators =  new List<fsCalculator>();

        public static void getCalculators(fsHydrocycloneNewControl hcControl) 
        {
            Calculators.Add(new FeedCurvesCalculator(hcControl));
        }

        #endregion
    }

    #region Equations

    public class BigFEquation : fsCalculatorEquation
    {
        #region Parameters

        private readonly IEquationParameter m_F;
        private readonly IEquationParameter m_x;
        private readonly IEquationParameter m_lnSigmaG2;
        private readonly IEquationParameter m_lnXG;

        #endregion

        public BigFEquation(
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

    public class BigFoEquation : fsCalculatorEquation
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

        public BigFoEquation(
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

    public class BigFuEquation : fsCalculatorEquation
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

        public BigFuEquation(
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

    public class SmallFEquation : fsCalculatorEquation
    {
        #region Parameters

        private readonly IEquationParameter m_f;
        private readonly IEquationParameter m_x;
        private readonly IEquationParameter m_lnSigmaG2;
        private readonly IEquationParameter m_lnXG;
        private readonly IEquationParameter m_lnSigmaG2PiSqrt;

        #endregion

        public SmallFEquation(
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

    public class SmallFoEquation : fsCalculatorEquation
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

        public SmallFoEquation(
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

    public class SmallFuEquation : fsCalculatorEquation
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

        public SmallFuEquation(
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

    public class GEquation : fsCalculatorEquation
    {
        #region Parameters

        private readonly IEquationParameter m_G;
        private readonly IEquationParameter m_x;
        private readonly IEquationParameter m_lnSigmaS2;
        private readonly IEquationParameter m_lnXred50;
        private readonly IEquationParameter m_rf;

        #endregion

        public GEquation(
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

    public class GRedEquation : fsCalculatorEquation
    {
        #region Parameters

        private readonly IEquationParameter m_GRed;
        private readonly IEquationParameter m_x;
        private readonly IEquationParameter m_lnSigmaS2;
        private readonly IEquationParameter m_lnXred50;
        private readonly IEquationParameter m_rf;

        #endregion

        public GRedEquation(
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

    //#region Help Equations

    //public class LogEquation : fsCalculatorEquation
    //{
    //    #region Parameters

    //    private readonly IEquationParameter m_x;
    //    private readonly IEquationParameter m_lnX;

    //    #endregion

    //    public LogEquation(
    //       IEquationParameter x,
    //       IEquationParameter lnX)
    //        : base(x, lnX)
    //    {
    //        m_x = x;
    //        m_lnX = lnX;
    //    }

    //    protected override void InitFormulas()
    //    {
    //        AddFormula(m_lnX, LogFormula);
    //    }

    //    private void LogFormula()
    //    {
    //        m_lnX.Value = fsValue.Log(m_x.Value);
    //    }
    //}

    //public class NormEquation : fsCalculatorEquation
    //{
    //    #region Parameters

    //    private readonly IEquationParameter m_norm;
    //    private readonly IEquationParameter m_x;
    //    private readonly IEquationParameter m_y;

    //    #endregion

    //    public NormEquation(
    //       IEquationParameter norm,
    //       IEquationParameter x,
    //       IEquationParameter y)
    //        : base(norm, x, y)
    //    {
    //        m_norm = norm;
    //        m_x = x;
    //        m_y = y;
    //    }

    //    protected override void InitFormulas()
    //    {
    //        AddFormula(m_norm, NormFormula);
    //    }

    //    private void NormFormula()
    //    {
    //        m_norm.Value = fsValue.Sqrt(fsValue.Sqr(m_x.Value) + fsValue.Sqr(m_y.Value));
    //    }
    //}

    //#endregion

    #endregion

    public class FeedCurvesCalculator : fsCalculator
    {
        public FeedCurvesCalculator(fsHydrocycloneNewControl hcControl)
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

            var lnXG = new fsCalculatorConstant(new fsParameterIdentifier("lnXG")) 
                           { Value = fsValue.Log(hcControl.ValuesForFeeds[fsParameterIdentifier.xg]) };

            // lnSigmaG2
            fsValue lnSigmaG = fsValue.Log(hcControl.ValuesForFeeds[fsParameterIdentifier.sigma_g]);
            var lnSigmaG2 = new fsCalculatorConstant(new fsParameterIdentifier("lnSigmaG2")) 
                                { Value =  lnSigmaG * Math.Sqrt(2) };

            var lnSigmaG2PiSqrt = new fsCalculatorConstant(new fsParameterIdentifier("lnSigmaG2PiSqrt")) 
                                      { Value =  1 / (Math.Sqrt(Math.PI) * lnSigmaG2.Value)};

            // lnSigmaS2
            fsValue lnSigmaS = fsValue.Log(hcControl.ValuesForFeeds[fsParameterIdentifier.sigma_s]);
            var lnSigmaS2 = new fsCalculatorConstant(new fsParameterIdentifier("lnSigmaS2")) 
                                { Value = lnSigmaS * Math.Sqrt(2) };   

            var lnXred50 = new fsCalculatorConstant(new fsParameterIdentifier("lnXred50")) 
                               { Value = fsValue.Log(hcControl.ValuesForFeeds[fsParameterIdentifier.ReducedCutSize]) };
            
            var rf = new fsCalculatorConstant(new fsParameterIdentifier("rf")) 
                         { Value = fsValue.Log(hcControl.ValuesForFeeds[fsParameterIdentifier.rf]) };

            var ET = new fsCalculatorConstant(new fsParameterIdentifier("ET")) 
                         { Value = fsValue.Log(hcControl.ValuesForFeeds[fsParameterIdentifier.TotalEfficiency]) };
   
            var zRed50 = new fsCalculatorConstant(new fsParameterIdentifier("zRed50")) 
                             { Value =  (lnXG.Value - lnXred50.Value) / lnSigmaS2.Value };
            
            var alpha = new fsCalculatorConstant(new fsParameterIdentifier("alpha")) 
                            { Value =  lnSigmaS / fsValue.Sqrt(fsValue.Sqr(lnSigmaS) + fsValue.Sqr(lnSigmaG)) };

            var beta = new fsCalculatorConstant(new fsParameterIdentifier("beta")) 
                           { Value = lnSigmaG / lnSigmaS };

            #endregion

            #endregion

            #region Equations Initialization

            Equations.Add(new BigFEquation(F, x, lnSigmaG2, lnXG));
            Equations.Add(new BigFoEquation(Fo, x, alpha, beta, zRed50, lnSigmaG2, lnXG));
            Equations.Add(new BigFuEquation(Fu, x, ET, alpha, beta, zRed50, lnSigmaG2, lnXG));

            Equations.Add(new SmallFEquation(f, x, lnSigmaG2, lnXG, lnSigmaG2PiSqrt));
            Equations.Add(new SmallFoEquation(fo, x, lnSigmaG2, lnXG, lnSigmaG2PiSqrt, lnSigmaS2, lnXred50, rf, ET));
            Equations.Add(new SmallFuEquation(fu, x, lnSigmaG2, lnXG, lnSigmaG2PiSqrt, lnSigmaS2, lnXred50, rf, ET));

            Equations.Add(new GEquation(G, x, lnSigmaS2, lnXred50, rf));
            Equations.Add(new GRedEquation(GRed, x, lnSigmaS2, lnXred50));

            #endregion
        }
    }
 
}