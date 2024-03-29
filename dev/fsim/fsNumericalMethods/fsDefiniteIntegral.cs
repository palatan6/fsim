﻿using System;
using System.Collections.Generic;
using System.Linq;
using Value;

namespace fsNumericalMethods
{
    public class fsDefiniteIntegral
    {
        public static fsValue Calculate(fsFunction function, fsValue beginArg, fsValue endArg, fsValue tolerance)
        {
            if (beginArg > endArg)
            {
                throw new Exception("fsDefiniteIntegral: provided limits are: beginAng > endArg");
            }

            if (beginArg == endArg)
            {
                return fsValue.Zero;
            }

            fsValue kronrodValue, gaussValue;

            EvalIntegrals(function, beginArg, endArg, out kronrodValue, out gaussValue);
            fsValue kronrodResult = kronrodValue;
            fsValue gaussResult = gaussValue;

            var queue = new SortedDictionary<fsIntegrationSegment, int>
                        {
                            {new fsIntegrationSegment(kronrodValue, gaussValue, beginArg, endArg), 0}
                        };
            int iterationsCount = 0;
            while (fsValue.Abs(kronrodResult - gaussResult) > tolerance
                || fsValue.Abs(kronrodResult / gaussResult - 1) > tolerance)
            {
                if (++iterationsCount >= 1000)  // If we made too many iterations
                {                               // and still don't have proper precision
                    break;                      // then just return not precise answer.
                }

                fsIntegrationSegment currentSegment = queue.Last().Key;
                queue.Remove(currentSegment);

                beginArg = currentSegment.BeginArg;
                endArg = currentSegment.EndArg;
                if (IsNeighbouringNumbers(beginArg, endArg))
                {
                    continue;
                }

                kronrodResult -= currentSegment.KronrodIntergralValue;
                gaussResult -= currentSegment.GaussIntergralValue;

                EvalIntegrals(function, beginArg, 0.5 * (beginArg + endArg), out kronrodValue, out gaussValue);
                kronrodResult += kronrodValue;
                gaussResult += gaussValue;
                queue.Add(new fsIntegrationSegment(kronrodValue, gaussValue, beginArg, 0.5 * (beginArg + endArg)), 0);

                EvalIntegrals(function, 0.5 * (beginArg + endArg), endArg, out kronrodValue, out gaussValue);
                kronrodResult += kronrodValue;
                gaussResult += gaussValue;
                queue.Add(new fsIntegrationSegment(kronrodValue, gaussValue, 0.5 * (beginArg + endArg), endArg), 0);
            }

            return (kronrodResult + gaussResult) / 2;
        }

        private static bool IsNeighbouringNumbers(fsValue a, fsValue b)
        {
            double mid = (a.Value + b.Value) / 2;
            return a.Value == mid || b.Value == mid;
        }

        private struct fsIntegrationSegment : IComparable
        {
            public readonly fsValue KronrodIntergralValue;
            public readonly fsValue GaussIntergralValue;
            public readonly fsValue BeginArg;
            public readonly fsValue EndArg;

		    public fsIntegrationSegment(fsValue kronrodIntergralValue, fsValue gaussIntergralValue, fsValue beginArg, fsValue endArg)
		    {
			    KronrodIntergralValue = kronrodIntergralValue;
                GaussIntergralValue = gaussIntergralValue;
                BeginArg = beginArg;
                EndArg = endArg;
		    }

            private static Int32 Compare(fsIntegrationSegment a, fsIntegrationSegment b)
            {
                int compareResult = Compare(a.GetPriority(), b.GetPriority());
                if (compareResult != 0)
                {
                    return compareResult;
                }

                compareResult = Compare(a.KronrodIntergralValue, b.KronrodIntergralValue);
                if (compareResult != 0)
                {
                    return compareResult;
                }

                compareResult = Compare(a.GaussIntergralValue, b.GaussIntergralValue);
                if (compareResult != 0)
                {
                    return compareResult;
                }

                compareResult = Compare(a.BeginArg, b.BeginArg);
                if (compareResult != 0)
                {
                    return compareResult;
                }

                compareResult = Compare(a.EndArg, b.EndArg);
                if (compareResult != 0)
                {
                    return compareResult;
                }

                return 0;
            }

            private static int Compare(fsValue first, fsValue second)
            {
                return first.Value < second.Value ? -1 : first.Value > second.Value ? 1 : 0;
            }

            private fsValue GetPriority()
            {
                return fsValue.Abs(KronrodIntergralValue - GaussIntergralValue);
            }

            public int CompareTo(object other)
            {
                return Compare(this, (fsIntegrationSegment)other);
            }
	    };

        static private void EvalIntegrals(fsFunction function, fsValue beginArg, fsValue endArg, out fsValue kronrodResult, out fsValue gaussResult)
	    {
		    fsValue c0 = 0.5 * (endArg + beginArg);
		    fsValue c1 = 0.5 * (endArg - beginArg);
            kronrodResult = fsValue.Zero;
            gaussResult = fsValue.Zero;

            for (int i = 0; i <= 60; ++i)
            {
			    fsValue fValue = function.Eval(c0 + c1 * X[i]);
			    kronrodResult += WKronrod[i] * fValue;
			    gaussResult += WGauss[i] * fValue;
		    }
            kronrodResult *= c1;
            gaussResult *= c1;
	    }

        private static readonly double[] X = new[]
                                        {
                                            0.999484410050490637571325895705811,
                                            0.996893484074649540271630050918695,
                                            0.991630996870404594858628366109486,
                                            0.983668123279747209970032581605663,
                                            0.973116322501126268374693868423707,
                                            0.960021864968307512216871025581798,
                                            0.944374444748559979415831324037439,
                                            0.926200047429274325879324277080474,
                                            0.905573307699907798546522558925958,
                                            0.882560535792052681543116462530226,
                                            0.857205233546061098958658510658944,
                                            0.829565762382768397442898119732502,
                                            0.799727835821839083013668942322683,
                                            0.767777432104826194917977340974503,
                                            0.733790062453226804726171131369528,
                                            0.697850494793315796932292388026640,
                                            0.660061064126626961370053668149271,
                                            0.620526182989242861140477556431189,
                                            0.579345235826361691756024932172540,
                                            0.536624148142019899264169793311073,
                                            0.492480467861778574993693061207709,
                                            0.447033769538089176780609900322854,
                                            0.400401254830394392535476211542661,
                                            0.352704725530878113471037207089374,
                                            0.304073202273625077372677107199257,
                                            0.254636926167889846439805129817805,
                                            0.204525116682309891438957671002025,
                                            0.153869913608583546963794672743256,
                                            0.102806937966737030147096751318001,
                                            0.051471842555317695833025213166723,
                                            0.000000000000000000000000000000000,
                                            -0.051471842555317695833025213166723,
                                            -0.102806937966737030147096751318001,
                                            -0.153869913608583546963794672743256,
                                            -0.204525116682309891438957671002025,
                                            -0.254636926167889846439805129817805,
                                            -0.304073202273625077372677107199257,
                                            -0.352704725530878113471037207089374,
                                            -0.400401254830394392535476211542661,
                                            -0.447033769538089176780609900322854,
                                            -0.492480467861778574993693061207709,
                                            -0.536624148142019899264169793311073,
                                            -0.579345235826361691756024932172540,
                                            -0.620526182989242861140477556431189,
                                            -0.660061064126626961370053668149271,
                                            -0.697850494793315796932292388026640,
                                            -0.733790062453226804726171131369528,
                                            -0.767777432104826194917977340974503,
                                            -0.799727835821839083013668942322683,
                                            -0.829565762382768397442898119732502,
                                            -0.857205233546061098958658510658944,
                                            -0.882560535792052681543116462530226,
                                            -0.905573307699907798546522558925958,
                                            -0.926200047429274325879324277080474,
                                            -0.944374444748559979415831324037439,
                                            -0.960021864968307512216871025581798,
                                            -0.973116322501126268374693868423707,
                                            -0.983668123279747209970032581605663,
                                            -0.991630996870404594858628366109486,
                                            -0.996893484074649540271630050918695,
                                            -0.999484410050490637571325895705811
                                        };

        private static readonly double[] WGauss = new[]
                                              {
                                                  0,
                                                  0.007968192496166605615465883474674,
                                                  0,
                                                  0.018466468311090959142302131912047,
                                                  0,
                                                  0.028784707883323369349719179611292,
                                                  0,
                                                  0.038799192569627049596801936446348,
                                                  0,
                                                  0.048402672830594052902938140422808,
                                                  0,
                                                  0.057493156217619066481721689402056,
                                                  0,
                                                  0.065974229882180495128128515115962,
                                                  0,
                                                  0.073755974737705206268243850022191,
                                                  0,
                                                  0.080755895229420215354694938460530,
                                                  0,
                                                  0.086899787201082979802387530715126,
                                                  0,
                                                  0.092122522237786128717632707087619,
                                                  0,
                                                  0.096368737174644259639468626351810,
                                                  0,
                                                  0.099593420586795267062780282103569,
                                                  0,
                                                  0.101762389748405504596428952168554,
                                                  0,
                                                  0.102852652893558840341285636705415,
                                                  0,
                                                  0.102852652893558840341285636705415,
                                                  0,
                                                  0.101762389748405504596428952168554,
                                                  0,
                                                  0.099593420586795267062780282103569,
                                                  0,
                                                  0.096368737174644259639468626351810,
                                                  0,
                                                  0.092122522237786128717632707087619,
                                                  0,
                                                  0.086899787201082979802387530715126,
                                                  0,
                                                  0.080755895229420215354694938460530,
                                                  0,
                                                  0.073755974737705206268243850022191,
                                                  0,
                                                  0.065974229882180495128128515115962,
                                                  0,
                                                  0.057493156217619066481721689402056,
                                                  0,
                                                  0.048402672830594052902938140422808,
                                                  0,
                                                  0.038799192569627049596801936446348,
                                                  0,
                                                  0.028784707883323369349719179611292,
                                                  0,
                                                  0.018466468311090959142302131912047,
                                                  0,
                                                  0.007968192496166605615465883474674,
                                                  0
                                              };

        private static readonly double[] WKronrod = new[]
                                                {
                                                    0.001389013698677007624551591226760,
                                                    0.003890461127099884051267201844516,
                                                    0.006630703915931292173319826369750,
                                                    0.009273279659517763428441146892024,
                                                    0.011823015253496341742232898853251,
                                                    0.014369729507045804812451432443580,
                                                    0.016920889189053272627572289420322,
                                                    0.019414141193942381173408951050128,
                                                    0.021828035821609192297167485738339,
                                                    0.024191162078080601365686370725232,
                                                    0.026509954882333101610601709335075,
                                                    0.028754048765041292843978785354334,
                                                    0.030907257562387762472884252943092,
                                                    0.032981447057483726031814191016854,
                                                    0.034979338028060024137499670731468,
                                                    0.036882364651821229223911065617136,
                                                    0.038678945624727592950348651532281,
                                                    0.040374538951535959111995279752468,
                                                    0.041969810215164246147147541285970,
                                                    0.043452539701356069316831728117073,
                                                    0.044814800133162663192355551616723,
                                                    0.046059238271006988116271735559374,
                                                    0.047185546569299153945261478181099,
                                                    0.048185861757087129140779492298305,
                                                    0.049055434555029778887528165367238,
                                                    0.049795683427074206357811569379942,
                                                    0.050405921402782346840893085653585,
                                                    0.050881795898749606492297473049805,
                                                    0.051221547849258772170656282604944,
                                                    0.051426128537459025933862879215781,
                                                    0.051494729429451567558340433647099,
                                                    0.051426128537459025933862879215781,
                                                    0.051221547849258772170656282604944,
                                                    0.050881795898749606492297473049805,
                                                    0.050405921402782346840893085653585,
                                                    0.049795683427074206357811569379942,
                                                    0.049055434555029778887528165367238,
                                                    0.048185861757087129140779492298305,
                                                    0.047185546569299153945261478181099,
                                                    0.046059238271006988116271735559374,
                                                    0.044814800133162663192355551616723,
                                                    0.043452539701356069316831728117073,
                                                    0.041969810215164246147147541285970,
                                                    0.040374538951535959111995279752468,
                                                    0.038678945624727592950348651532281,
                                                    0.036882364651821229223911065617136,
                                                    0.034979338028060024137499670731468,
                                                    0.032981447057483726031814191016854,
                                                    0.030907257562387762472884252943092,
                                                    0.028754048765041292843978785354334,
                                                    0.026509954882333101610601709335075,
                                                    0.024191162078080601365686370725232,
                                                    0.021828035821609192297167485738339,
                                                    0.019414141193942381173408951050128,
                                                    0.016920889189053272627572289420322,
                                                    0.014369729507045804812451432443580,
                                                    0.011823015253496341742232898853251,
                                                    0.009273279659517763428441146892024,
                                                    0.006630703915931292173319826369750,
                                                    0.003890461127099884051267201844516,
                                                    0.001389013698677007624551591226760
                                                };
    }
}
