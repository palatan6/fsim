using Equations;
using Parameters;
using Value;
using Equations.Belt_Filters_with_Reversible_Trays;
using Equations.CakeWashing;

namespace StepCalculators.Simulation_Calculators
{
    public class fsCakeWashingCalculator : fsCalculatorEquationsList
    {
        public override void AddToCalculator(fsCalculator calculator)
        {
            #region Parameters Initialization

            #region Material Parameters

            IEquationParameter eta = calculator.AddVariable(fsParameterIdentifier.MotherLiquidViscosity);

            IEquationParameter rho = calculator.AddVariable(fsParameterIdentifier.MotherLiquidDensity);

            IEquationParameter rhos = calculator.AddVariable(fsParameterIdentifier.SolidsDensity);

            IEquationParameter eps0   = calculator.AddVariable(fsParameterIdentifier.CakePorosity0);
            IEquationParameter rhocd0 = calculator.AddVariable(fsParameterIdentifier.DryCakeDensity0);
            IEquationParameter Rf0    = calculator.AddVariable(fsParameterIdentifier.CakeMoistureContentRf0);
            IEquationParameter eps    = calculator.AddVariable(fsParameterIdentifier.CakePorosity);
            IEquationParameter rhocd  = calculator.AddVariable(fsParameterIdentifier.DryCakeDensity);
            IEquationParameter Rf     = calculator.AddVariable(fsParameterIdentifier.CakeMoistureContentRf);

            IEquationParameter ne = calculator.AddVariable(fsParameterIdentifier.Ne);

            IEquationParameter pc0    = calculator.AddVariable(fsParameterIdentifier.CakePermeability0);
            IEquationParameter rc0    = calculator.AddVariable(fsParameterIdentifier.CakeResistance0);
            IEquationParameter alpha0 = calculator.AddVariable(fsParameterIdentifier.CakeResistanceAlpha0);
            IEquationParameter pc     = calculator.AddVariable(fsParameterIdentifier.CakePermeability);
            IEquationParameter rc     = calculator.AddVariable(fsParameterIdentifier.CakeResistance);
            IEquationParameter alpha  = calculator.AddVariable(fsParameterIdentifier.CakeResistanceAlpha);
            IEquationParameter K      = calculator.AddVariable(fsParameterIdentifier.PracticalCakePermeability);

            IEquationParameter nc  = calculator.AddVariable(fsParameterIdentifier.CakeCompressibility);

            IEquationParameter hce = calculator.AddVariable(fsParameterIdentifier.FilterMediumResistanceHce);
            IEquationParameter Rm = calculator.AddVariable(fsParameterIdentifier.FilterMediumResistanceRm);

            IEquationParameter Sw0  = calculator.AddVariable(fsParameterIdentifier.CakeSaturationSw0);
            IEquationParameter Rfw0 = calculator.AddVariable(fsParameterIdentifier.CakeMoistureContentRfw0);

            IEquationParameter fq = calculator.AddVariable(fsParameterIdentifier.PredeliquorFlowRate);

            IEquationParameter C0p = calculator.AddVariable(fsParameterIdentifier.CakeWashOutConcentration);
            IEquationParameter X0p = calculator.AddVariable(fsParameterIdentifier.CakeWashOutContentX0p);

            IEquationParameter xr = calculator.AddVariable(fsParameterIdentifier.RemanentWashOutContent);

            IEquationParameter rhow = calculator.AddVariable(fsParameterIdentifier.WashLiquidDensity);

            IEquationParameter etaw = calculator.AddVariable(fsParameterIdentifier.WashLiquidViscosity);

            IEquationParameter cw = calculator.AddVariable(fsParameterIdentifier.LiquidWashOutConcentration);

            IEquationParameter Dn0 = calculator.AddVariable(fsParameterIdentifier.WashIndexFor0);
            IEquationParameter Dn  = calculator.AddVariable(fsParameterIdentifier.WashIndexFor);

            IEquationParameter aw1 = calculator.AddVariable(fsParameterIdentifier.AdaptationPar1);
            IEquationParameter aw2 = calculator.AddVariable(fsParameterIdentifier.AdaptationPar2);
       
            #endregion

            #region Machine Setting Parameters

              IEquationParameter A = calculator.AddVariable(fsParameterIdentifier.FilterArea);
              IEquationParameter b = calculator.AddVariable(fsParameterIdentifier.MachineWidth);

              IEquationParameter ns = calculator.AddVariable(fsParameterIdentifier.ns);

              IEquationParameter ls  = calculator.AddVariable(fsParameterIdentifier.ls);
              IEquationParameter lsb = calculator.AddVariable(fsParameterIdentifier.ls_over_b);
              IEquationParameter l   = calculator.AddVariable(fsParameterIdentifier.FilterLength);
              IEquationParameter lb  = calculator.AddVariable(fsParameterIdentifier.l_over_b);
              IEquationParameter As  = calculator.AddVariable(fsParameterIdentifier.As);

              IEquationParameter ttech0 = calculator.AddVariable(fsParameterIdentifier.StandardTechnicalTime);
              IEquationParameter ttech  = calculator.AddVariable(fsParameterIdentifier.TechnicalTime);

              IEquationParameter lambda = calculator.AddVariable(fsParameterIdentifier.lambda);

              IEquationParameter u  = calculator.AddVariable(fsParameterIdentifier.u);
              IEquationParameter n  = calculator.AddVariable(fsParameterIdentifier.RotationalSpeed );
              IEquationParameter tc = calculator.AddVariable(fsParameterIdentifier.CycleTime);

              IEquationParameter hc  = calculator.AddVariable(fsParameterIdentifier.CakeHeight);
              IEquationParameter Ms  = calculator.AddVariable(fsParameterIdentifier.SolidsMass);
              IEquationParameter Qms = calculator.AddVariable(fsParameterIdentifier.Qms);

               IEquationParameter Dp = calculator.AddVariable(fsParameterIdentifier.PressureDifference);

                IEquationParameter w      = calculator.AddVariable(fsParameterIdentifier.WashingRatioW);
                IEquationParameter wv     = calculator.AddVariable(fsParameterIdentifier.WashingRatioWv);
                IEquationParameter wm     = calculator.AddVariable(fsParameterIdentifier.WashingRatioWm);
                IEquationParameter Vw     = calculator.AddVariable(fsParameterIdentifier.WashLiquidVolume);
                IEquationParameter Mw     = calculator.AddVariable(fsParameterIdentifier.WashLiquidMass);
                IEquationParameter nsw    = calculator.AddVariable(fsParameterIdentifier.NumberOfWashingSegments);
                IEquationParameter sw     = calculator.AddVariable(fsParameterIdentifier.SpecificWashArea);
                IEquationParameter tw     = calculator.AddVariable(fsParameterIdentifier.WashTime);
                IEquationParameter Qw     = calculator.AddVariable(fsParameterIdentifier.WashLiquidVolFlowRate);
                IEquationParameter Qmw    = calculator.AddVariable(fsParameterIdentifier.WashLiquidMassFlowRate);
                IEquationParameter cStar  = calculator.AddVariable(fsParameterIdentifier.SpecificWashOutConcentration);
                IEquationParameter caStar = calculator.AddVariable(fsParameterIdentifier.SpecificAverageWashOut);
                IEquationParameter ccStar = calculator.AddVariable(fsParameterIdentifier.SpecificWashOutConcentrationInCake);
                IEquationParameter c      = calculator.AddVariable(fsParameterIdentifier.WashOutConcentrationInWashfiltrate);
                IEquationParameter ca     = calculator.AddVariable(fsParameterIdentifier.AverageWashOutConcentration);
                IEquationParameter cc     = calculator.AddVariable(fsParameterIdentifier.WashOutConcentrationInCake);
                IEquationParameter xStar  = calculator.AddVariable(fsParameterIdentifier.SpecificWashOutXStar);
                IEquationParameter x      = calculator.AddVariable(fsParameterIdentifier.SpecificWashOutX);
                IEquationParameter X      = calculator.AddVariable(fsParameterIdentifier.CakeWashOutContent);

                IEquationParameter wf  = calculator.AddVariable(fsParameterIdentifier.SpecificWashfiltrate);
                IEquationParameter Vwf = calculator.AddVariable(fsParameterIdentifier.VolumeOfWashfiltrate);
                IEquationParameter Mwf = calculator.AddVariable(fsParameterIdentifier.MassOfWashfiltrate);
                IEquationParameter Vc  = calculator.AddVariable(fsParameterIdentifier.CakeVolume);
                IEquationParameter Mc  = calculator.AddVariable(fsParameterIdentifier.CakeMass);
                IEquationParameter Vlc = calculator.AddVariable(fsParameterIdentifier.LiquidVolumeInCake);
                IEquationParameter Mlc = calculator.AddVariable(fsParameterIdentifier.LiquidMassInCake);
                IEquationParameter Rfw = calculator.AddVariable(fsParameterIdentifier.CakeMoistureContentRfw);
                IEquationParameter Q   = calculator.AddVariable(fsParameterIdentifier.FeedVolumeFlowRate );
                IEquationParameter Qm  = calculator.AddVariable(fsParameterIdentifier.FeedSolidsMassFlowRate );
                IEquationParameter Qa  = calculator.AddVariable(fsParameterIdentifier.AverageVolumeFlowRate);
                IEquationParameter Qma = calculator.AddVariable(fsParameterIdentifier.AverageMassFlowRate);
                IEquationParameter q   = calculator.AddVariable(fsParameterIdentifier.SpecificVolumeFlowRate);
                IEquationParameter qm  = calculator.AddVariable(fsParameterIdentifier.SpecificMassFlowRate);
                IEquationParameter qa  = calculator.AddVariable(fsParameterIdentifier.SpecificAverageVolumeFlowRate);
                IEquationParameter qma = calculator.AddVariable(fsParameterIdentifier.SpecificAverageMassFlowRate); 

            #endregion 

            #region Auxiliary Parameters and Constants

            var constantZero = new fsCalculatorConstant(new fsParameterIdentifier("0")) { Value = fsValue.Zero };
            var constantOne  = new fsCalculatorConstant(new fsParameterIdentifier("1")) { Value = fsValue.One };

            IEquationParameter hcAddHce = calculator.AddVariable(new fsParameterIdentifier("hc + hce"));
            calculator.AddEquation(new fsSumEquation(hcAddHce, hc, hce));

            IEquationParameter oneMinusEps0 = calculator.AddVariable(new fsParameterIdentifier("1 - eps0"));
            calculator.AddEquation(new fsSumEquation(constantOne, eps0, oneMinusEps0));

            IEquationParameter oneMinusEps = calculator.AddVariable(new fsParameterIdentifier("1 - eps"));
            calculator.AddEquation(new fsSumEquation(constantOne, eps, oneMinusEps));

            IEquationParameter eps0Rho = calculator.AddVariable(new fsParameterIdentifier("eps0*rho"));
            calculator.AddEquation(new fsProductEquation(eps0Rho, eps0, rho));

            IEquationParameter epsRho = calculator.AddVariable(new fsParameterIdentifier("eps*rho"));
            calculator.AddEquation(new fsProductEquation(epsRho, eps, rho));

            IEquationParameter rhocd0AddEps0Rho = calculator.AddVariable(new fsParameterIdentifier("rhocd0 + eps0*rho"));
            calculator.AddEquation(new fsSumEquation(rhocd0AddEps0Rho, rhocd0, eps0Rho));

            IEquationParameter rhocdAddEpsRho = calculator.AddVariable(new fsParameterIdentifier("rhocd + eps*rho"));
            calculator.AddEquation(new fsSumEquation(rhocdAddEpsRho, rhocd, epsRho));

            IEquationParameter oneMinusXr = calculator.AddVariable(new fsParameterIdentifier("1 - xr"));
            calculator.AddEquation(new fsSumEquation(constantOne, xr, oneMinusXr));
            IEquationParameter oneMinusXrSw0 = calculator.AddVariable(new fsParameterIdentifier("(1 - xr)*Sw0"));
            calculator.AddEquation(new fsProductEquation(oneMinusXrSw0, oneMinusXr, Sw0));
            IEquationParameter xrAddoneMinusXrSw0 = calculator.AddVariable(new fsParameterIdentifier("xr + (1 - xr)*Sw0"));
            calculator.AddEquation(new fsSumEquation(xrAddoneMinusXrSw0, xr, oneMinusXrSw0));
            IEquationParameter invRfw0 = calculator.AddVariable(new fsParameterIdentifier("1/Rfw0"));
            calculator.AddEquation(new fsDivisionInverseEquation(invRfw0, Rfw0));
            IEquationParameter fractRfw0 = calculator.AddVariable(new fsParameterIdentifier("(1 - Rfw0)/Rfw0"));
            calculator.AddEquation(new fsSumEquation(invRfw0, fractRfw0, constantOne));

            IEquationParameter epsVc = calculator.AddVariable(new fsParameterIdentifier("eps*Vc"));
            calculator.AddEquation(new fsProductEquation(epsVc, eps, Vc));

            IEquationParameter nsTtech = calculator.AddVariable(new fsParameterIdentifier("ns*ttech"));
            calculator.AddEquation(new fsProductEquation(nsTtech, ns, ttech));
            IEquationParameter tcMinusNsTtech = calculator.AddVariable(new fsParameterIdentifier("tc - ns*ttech"));
            calculator.AddEquation(new fsSumEquation(tc, nsTtech, tcMinusNsTtech));

            IEquationParameter c0 = calculator.AddVariable(new fsParameterIdentifier("c0"));
            calculator.AddEquation(new fsProductEquation(c0, Sw0, C0p));
            IEquationParameter c0MinusCw = calculator.AddVariable(new fsParameterIdentifier("c0 - cw"));
            calculator.AddEquation(new fsSumEquation(c0, c0MinusCw, cw));
            IEquationParameter cStarC0MinusCw = calculator.AddVariable(new fsParameterIdentifier("(c0 - cw) * c* "));
            calculator.AddEquation(new fsProductEquation(cStarC0MinusCw, cStar, c0MinusCw));

            IEquationParameter xStarOneMinusXr = calculator.AddVariable(new fsParameterIdentifier("(1 - xr) * x* "));
            calculator.AddEquation(new fsProductEquation(xStarOneMinusXr, xStar, oneMinusXr));

            IEquationParameter xrX0p = calculator.AddVariable(new fsParameterIdentifier("xr*X0p"));
            calculator.AddEquation(new fsProductEquation(xrX0p, xr, X0p));
            IEquationParameter XMinusXrX0p = calculator.AddVariable(new fsParameterIdentifier("X - xr*X0p"));
            calculator.AddEquation(new fsSumEquation(X, xrX0p, XMinusXrX0p));
            
            IEquationParameter ccStarC0MinusCw = calculator.AddVariable(new fsParameterIdentifier("(c0 - cw) * cc*"));
            calculator.AddEquation(new fsProductEquation(ccStarC0MinusCw, ccStar, c0MinusCw));
            
            IEquationParameter caStarC0MinusCw = calculator.AddVariable(new fsParameterIdentifier("(c0 - cw) * c*"));
            calculator.AddEquation(new fsProductEquation(caStarC0MinusCw, caStar, c0MinusCw));

            IEquationParameter rhowf = calculator.AddVariable(new fsParameterIdentifier("rhowf"));
            calculator.AddEquation(new fsIfMoreOrLessThenOneEquation(rhowf, wf, rhow, rho, rho, constantZero, rhow));

            IEquationParameter etawf = calculator.AddVariable(new fsParameterIdentifier("etawf"));
            calculator.AddEquation(new fsIfMoreOrLessThenOneEquation(etawf, wf, etaw, eta, eta, constantZero, etaw));

            IEquationParameter sw0PowFq = calculator.AddVariable(new fsParameterIdentifier("Sw0^fq")); 
            calculator.AddEquation(new fsTechnicalTimeFrom0Equation(sw0PowFq, constantOne, Sw0, fq)); // It's a dirty trick, of course!

            IEquationParameter c3 = calculator.AddVariable(new fsParameterIdentifier("c3"));
            calculator.AddEquation(new fsProductsEquation(
                new[] { c3, eps, hc, hcAddHce },
                new[] { sw0PowFq, pc, Dp }));
           
            IEquationParameter rhowfEps = calculator.AddVariable(new fsParameterIdentifier("rhowf*eps"));
            calculator.AddEquation(new fsProductEquation(rhowfEps, rhowf, eps));
            IEquationParameter rhocdAddRhowfEps = calculator.AddVariable(new fsParameterIdentifier("rhocd + rhowf*eps"));
            calculator.AddEquation(new fsSumEquation(rhocdAddRhowfEps, rhocd, rhowfEps));

            #endregion

            #endregion

            #region Equations Initialization

            #region Material Parameters Equations

            calculator.AddEquation(new fsProductsEquation(
                new[] { rhocd0 },
                new[] { oneMinusEps0, rhos }));
            calculator.AddEquation(new fsProductsEquation(
                new[] { rhocd },
                new[] { oneMinusEps, rhos }));
            calculator.AddEquation(new fsProductEquation(eps0Rho, Rf0, rhocd0AddEps0Rho));
            calculator.AddEquation(new fsProductEquation(epsRho, Rf, rhocdAddEpsRho));
            calculator.AddEquation(new fsFrom0AndDpEquation(eps, eps0, Dp, ne));

            calculator.AddEquation(new fsDivisionInverseEquation(rc0, pc0));
            calculator.AddEquation(new fsDivisionInverseEquation(rc, pc));
            calculator.AddEquation(new fsFrom0AndDpEquation(pc, pc0, Dp, nc));
            calculator.AddEquation(new fsProductsEquation(
                new[] { alpha0, rhos, oneMinusEps0, pc0 },
                new[] { constantOne }));
            calculator.AddEquation(new fsProductsEquation(
                new[] { alpha, rhos, oneMinusEps, pc },
                new[] { constantOne }));
            calculator.AddEquation(new fsProductsEquation(
                new[] { K, eta, hcAddHce },
                new[] { hc, pc }));

            calculator.AddEquation(new fsProductEquation(hce, Rm, pc));

            calculator.AddEquation(new fsProductsEquation(
                new[] { oneMinusEps, rhos },
                new[] { Sw0, eps, rho, fractRfw0 }));

            calculator.AddEquation(new fsProductsEquation(
                new[] { X0p, rhocd, oneMinusXr },
                new[] { eps, xrAddoneMinusXrSw0, C0p }));

            /**
             * Modelling of the equation Dn = Dn0 * (u/u0)^(-aw1) * (hc/hc0)^aw2,
             * where u0 = 0.0001 m/s and hc0 = 0.01 m/s,
             * by means of the equation x = x0 * (p/10^5)^(-degree) settled in the class fsFrom0AndDpEquation 
             **/
            var const1e7 = new fsCalculatorConstant(new fsParameterIdentifier("1e7")) { Value = new fsValue(1e7) };
            var const1e9 = new fsCalculatorConstant(new fsParameterIdentifier("1e9")) { Value = new fsValue(1e9) };
            IEquationParameter v1 = calculator.AddVariable(new fsParameterIdentifier("u*10^9"));
            calculator.AddEquation(new fsProductEquation(v1, u, const1e9));
            IEquationParameter v2 = calculator.AddVariable(new fsParameterIdentifier("Dn0*(u/u0)^(-aw1)"));
            calculator.AddEquation(new fsFrom0AndDpEquation(v2, Dn0, v1, aw1));
            IEquationParameter v3 = calculator.AddVariable(new fsParameterIdentifier("hc*10^7"));
            calculator.AddEquation(new fsProductEquation(v3, hc, const1e7));
            IEquationParameter v4 = calculator.AddVariable(new fsParameterIdentifier("-aw2"));
            calculator.AddEquation(new fsSumEquation(constantZero, v4, aw2));
            calculator.AddEquation(new fsFrom0AndDpEquation(Dn, v2, v3, v4));
           
            calculator.AddEquation(new fsProductsEquation( // This equation needs to be discussed as to the possible status "calculated" for u and Dp (only input parameter!)
                new[] { u, eps, etaw, hcAddHce },
                new[] { pc, Dp }));

            #endregion

            #region Machine Setting Parameters Equations 

            calculator.AddEquation(new fsProductsEquation(
                new[] { b, ns, ls },
                new[] { A }));
            calculator.AddEquation(new fsProductEquation(ls, lsb, b));
            calculator.AddEquation(new fsProductEquation(l, lb, b));
            calculator.AddEquation(new fsProductEquation(l, ns, ls));
            calculator.AddEquation(new fsProductEquation(As, ls, b));
            
            calculator.AddEquation(new fsTechnicalTimeFrom0Equation(ttech, ttech0, As, lambda));

            calculator.AddEquation(new fsProductEquation(u, l, n));
            calculator.AddEquation(new fsDivisionInverseEquation(tc, n));

            calculator.AddEquation(new fsProductsEquation(
                new[] { A, rhos, oneMinusEps, hc },
                new[] { Ms }));
            calculator.AddEquation(new fsProductEquation(Ms, Qms, tc));

            calculator.AddEquation(new fsProductsEquation(
                new[] { w, eps },
                new[] { oneMinusEps, rhos, wv }));
            calculator.AddEquation(new fsProductsEquation(
                new[] { w, rhow, eps },
                new[] { rhos, oneMinusEps, wm }));
            calculator.AddEquation(new fsProductsEquation(
                new[] { eps, A, hc, w },
                new[] { Vw }));
            calculator.AddEquation(new fsProductEquation(Mw, rhow, Vw));
            calculator.AddEquation(new fsProductEquation(nsw, ns, sw));
            calculator.AddEquation(new fsProductEquation(tw, sw, tcMinusNsTtech));
            calculator.AddEquation(new fsSumsEquation(
                new[] { wf, constantOne },
                new[] { w, Sw0 }));
            calculator.AddEquation(new fsProductEquation(Vw, Qw, tc));
            calculator.AddEquation(new fsProductEquation(Qmw, rhow, Qw));
            calculator.AddEquation(new fsSumEquation(c, cStarC0MinusCw, cw));
            calculator.AddEquation(new fsSumEquation(x, xr, xStarOneMinusXr));
            calculator.AddEquation(new fsProductEquation(X, X0p, x));
            calculator.AddEquation(new fsProductsEquation(
                new[] { eps, cc },
                new[] { rhocd, XMinusXrX0p }));
            calculator.AddEquation(new fsSumEquation(cc, ccStarC0MinusCw, cw));
            calculator.AddEquation(new fsSumEquation(ca, caStarC0MinusCw, cw));
            calculator.AddEquation(new fsCstarDnWfEquation(cStar, Dn, wf));
            calculator.AddEquation(new fsXstarDnWfEquation(xStar, Dn, wf));
            calculator.AddEquation(new fsCaDnCwWfEquation(ca, cw, c0, Dn, wf));           

            calculator.AddEquation(new fsWfTwEquation(wf, tw, c3, etaw, eta));
            calculator.AddEquation(new fsCaWfXstarEquation(ca, xStar, cw, c0, wf));
            calculator.AddEquation(new fsIfMoreOrLessThenOneEquation(Vwf, wf, epsVc, constantZero, constantZero, epsVc, constantZero));
            calculator.AddEquation(new fsProductsEquation(
                new[] { eps, c3, Vc },
                new[] { Q, etawf }));
            calculator.AddEquation(new fsProductsEquation(
                new[] { rhowf, eps, Vc },
                new[] { Mlc }));
            calculator.AddEquation(new fsMwfVcWfEquation(Mwf, eps, Vc, rhow, rho, wf));
            calculator.AddEquation(new fsProductEquation(Qm, rhowf, Q));
            calculator.AddEquation(new fsProductEquation(Mc, rhocdAddRhowfEps, Vc));
            calculator.AddEquation(new fsProductEquation(Vc, A, hc));
            calculator.AddEquation(new fsProductEquation(Vlc, eps, Vc));
            calculator.AddEquation(new fsProductEquation(Mlc, Rfw, Mc));
            calculator.AddEquation(new fsProductEquation(Vwf, Qa, tc));
            calculator.AddEquation(new fsProductEquation(Mwf, Qma, tc));
            calculator.AddEquation(new fsProductEquation(Q, q, A));
            calculator.AddEquation(new fsProductEquation(Qm, qm, A));
            calculator.AddEquation(new fsProductEquation(Qa, qa, A));
            calculator.AddEquation(new fsProductEquation(Qma, qma, A));

            #endregion 

            #endregion
        }
    }
}
