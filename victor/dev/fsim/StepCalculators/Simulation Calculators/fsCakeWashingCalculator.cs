using Equations;
using Parameters;
using Value;
using Equations.Belt_Filters_with_Reversible_Trays;
using Equations.CakeWashing;

namespace StepCalculators.Simulation_Calculators
{
    public class fsCakeWashingCalculator : fsCalculator
    {
        public fsCakeWashingCalculator()
        {
            #region Parameters Initialization

            #region Material Parameters

            IEquationParameter eta = AddVariable(fsParameterIdentifier.MotherLiquidViscosity);

            IEquationParameter rho = AddVariable(fsParameterIdentifier.MotherLiquidDensity);

            IEquationParameter rhos = AddVariable(fsParameterIdentifier.SolidsDensity);

            IEquationParameter eps0   = AddVariable(fsParameterIdentifier.CakePorosity0);
            IEquationParameter rhocd0 = AddVariable(fsParameterIdentifier.DryCakeDensity0);
            IEquationParameter Rf0    = AddVariable(fsParameterIdentifier.CakeMoistureContentRf0);
            IEquationParameter eps    = AddVariable(fsParameterIdentifier.CakePorosity);
            IEquationParameter rhocd  = AddVariable(fsParameterIdentifier.DryCakeDensity);
            IEquationParameter Rf     = AddVariable(fsParameterIdentifier.CakeMoistureContentRf);

            IEquationParameter ne = AddVariable(fsParameterIdentifier.Ne);

            IEquationParameter pc0    = AddVariable(fsParameterIdentifier.CakePermeability0);
            IEquationParameter rc0    = AddVariable(fsParameterIdentifier.CakeResistance0);
            IEquationParameter alpha0 = AddVariable(fsParameterIdentifier.CakeResistanceAlpha0);
            IEquationParameter pc     = AddVariable(fsParameterIdentifier.CakePermeability);
            IEquationParameter rc     = AddVariable(fsParameterIdentifier.CakeResistance);
            IEquationParameter alpha  = AddVariable(fsParameterIdentifier.CakeResistanceAlpha);
            IEquationParameter K      = AddVariable(fsParameterIdentifier.PracticalCakePermeability);

            IEquationParameter nc  = AddVariable(fsParameterIdentifier.CakeCompressibility);

            IEquationParameter hce = AddVariable(fsParameterIdentifier.FilterMediumResistanceHce);
            IEquationParameter Rm = AddVariable(fsParameterIdentifier.FilterMediumResistanceRm);

            IEquationParameter Sw0  = AddVariable(fsParameterIdentifier.CakeSaturationSw0);
            IEquationParameter Rfw0 = AddVariable(fsParameterIdentifier.CakeMoistureContentRfw0);

            IEquationParameter fq = AddVariable(fsParameterIdentifier.PredeliquorFlowRate);

            IEquationParameter C0p = AddVariable(fsParameterIdentifier.CakeWashOutConcentration);
            IEquationParameter X0p = AddVariable(fsParameterIdentifier.CakeWashOutContentX0p);

            IEquationParameter xr = AddVariable(fsParameterIdentifier.RemanentWashOutContent);

            IEquationParameter rhow = AddVariable(fsParameterIdentifier.WashLiquidDensity);

            IEquationParameter etaw = AddVariable(fsParameterIdentifier.WashLiquidViscosity);

            IEquationParameter cw = AddVariable(fsParameterIdentifier.LiquidWashOutConcentration);

            IEquationParameter Dn0 = AddVariable(fsParameterIdentifier.WashIndexFor0);
            IEquationParameter Dn  = AddVariable(fsParameterIdentifier.WashIndexFor);

            IEquationParameter aw1 = AddVariable(fsParameterIdentifier.AdaptationPar1);
            IEquationParameter aw2 = AddVariable(fsParameterIdentifier.AdaptationPar2);
       
            #endregion

            #region Machine Setting Parameters

              IEquationParameter A = AddVariable(fsParameterIdentifier.FilterArea);
              IEquationParameter b = AddVariable(fsParameterIdentifier.MachineWidth);

              IEquationParameter ns = AddVariable(fsParameterIdentifier.ns);

              IEquationParameter ls  = AddVariable(fsParameterIdentifier.ls);
              IEquationParameter lsb = AddVariable(fsParameterIdentifier.ls_over_b);
              IEquationParameter l   = AddVariable(fsParameterIdentifier.FilterLength);
              IEquationParameter lb  = AddVariable(fsParameterIdentifier.l_over_b);
              IEquationParameter As  = AddVariable(fsParameterIdentifier.As);

              IEquationParameter ttech0 = AddVariable(fsParameterIdentifier.StandardTechnicalTime);
              IEquationParameter ttech  = AddVariable(fsParameterIdentifier.TechnicalTime);

              IEquationParameter lambda = AddVariable(fsParameterIdentifier.lambda);

              IEquationParameter u  = AddVariable(fsParameterIdentifier.u);
              IEquationParameter n  = AddVariable(fsParameterIdentifier.RotationalSpeed );
              IEquationParameter tc = AddVariable(fsParameterIdentifier.CycleTime);

              IEquationParameter hc  = AddVariable(fsParameterIdentifier.CakeHeight);
              IEquationParameter Ms  = AddVariable(fsParameterIdentifier.SolidsMass);
              IEquationParameter Qms = AddVariable(fsParameterIdentifier.Qms);

               IEquationParameter Dp = AddVariable(fsParameterIdentifier.PressureDifference);

                IEquationParameter w      = AddVariable(fsParameterIdentifier.WashingRatioW);
                IEquationParameter wv     = AddVariable(fsParameterIdentifier.WashingRatioWv);
                IEquationParameter wm     = AddVariable(fsParameterIdentifier.WashingRatioWm);
                IEquationParameter Vw     = AddVariable(fsParameterIdentifier.WashLiquidVolume);
                IEquationParameter Mw     = AddVariable(fsParameterIdentifier.WashLiquidMass);
                IEquationParameter nsw    = AddVariable(fsParameterIdentifier.NumberOfWashingSegments);
                IEquationParameter sw     = AddVariable(fsParameterIdentifier.SpecificWashArea);
                IEquationParameter tw     = AddVariable(fsParameterIdentifier.WashTime);
                IEquationParameter Qw     = AddVariable(fsParameterIdentifier.WashLiquidVolFlowRate);
                IEquationParameter Qmw    = AddVariable(fsParameterIdentifier.WashLiquidMassFlowRate);
                IEquationParameter cStar  = AddVariable(fsParameterIdentifier.SpecificWashOutConcentration);
                IEquationParameter caStar = AddVariable(fsParameterIdentifier.SpecificAverageWashOut);
                IEquationParameter ccStar = AddVariable(fsParameterIdentifier.SpecificWashOutConcentrationInCake);
                IEquationParameter c      = AddVariable(fsParameterIdentifier.WashOutConcentrationInWashfiltrate);
                IEquationParameter ca     = AddVariable(fsParameterIdentifier.AverageWashOutConcentration);
                IEquationParameter cc     = AddVariable(fsParameterIdentifier.WashOutConcentrationInCake);
                IEquationParameter xStar  = AddVariable(fsParameterIdentifier.SpecificWashOutXStar);
                IEquationParameter x      = AddVariable(fsParameterIdentifier.SpecificWashOutX);
                IEquationParameter X      = AddVariable(fsParameterIdentifier.CakeWashOutContent);

                IEquationParameter wf  = AddVariable(fsParameterIdentifier.SpecificWashfiltrate);
                IEquationParameter Vwf = AddVariable(fsParameterIdentifier.VolumeOfWashfiltrate);
                IEquationParameter Mwf = AddVariable(fsParameterIdentifier.MassOfWashfiltrate);
                IEquationParameter Vc  = AddVariable(fsParameterIdentifier.CakeVolume);
                IEquationParameter Mc  = AddVariable(fsParameterIdentifier.CakeMass);
                IEquationParameter Vlc = AddVariable(fsParameterIdentifier.LiquidVolumeInCake);
                IEquationParameter Mlc = AddVariable(fsParameterIdentifier.LiquidMassInCake);
                IEquationParameter Rfw = AddVariable(fsParameterIdentifier.CakeMoistureContentRfw);
                IEquationParameter Q   = AddVariable(fsParameterIdentifier.FeedVolumeFlowRate );
                IEquationParameter Qm  = AddVariable(fsParameterIdentifier.FeedSolidsMassFlowRate );
                IEquationParameter Qa  = AddVariable(fsParameterIdentifier.AverageVolumeFlowRate);
                IEquationParameter Qma = AddVariable(fsParameterIdentifier.AverageMassFlowRate);
                IEquationParameter q   = AddVariable(fsParameterIdentifier.SpecificVolumeFlowRate);
                IEquationParameter qm  = AddVariable(fsParameterIdentifier.SpecificMassFlowRate);
                IEquationParameter qa  = AddVariable(fsParameterIdentifier.SpecificAverageVolumeFlowRate);
                IEquationParameter qma = AddVariable(fsParameterIdentifier.SpecificAverageMassFlowRate); 

            #endregion 

            #region Auxiliary Parameters and Constants

            var constantZero = new fsCalculatorConstant(new fsParameterIdentifier("0")) { Value = fsValue.Zero };
            var constantOne  = new fsCalculatorConstant(new fsParameterIdentifier("1")) { Value = fsValue.One };

            IEquationParameter hcAddHce = AddVariable(new fsParameterIdentifier("hc + hce"));
            Equations.Add(new fsSumEquation(hcAddHce, hc, hce));

            IEquationParameter oneMinusEps0 = AddVariable(new fsParameterIdentifier("1 - eps0"));
            Equations.Add(new fsSumEquation(constantOne, eps0, oneMinusEps0));

            IEquationParameter oneMinusEps = AddVariable(new fsParameterIdentifier("1 - eps"));
            Equations.Add(new fsSumEquation(constantOne, eps, oneMinusEps));

            IEquationParameter eps0Rho = AddVariable(new fsParameterIdentifier("eps0*rho"));
            Equations.Add(new fsProductEquation(eps0Rho, eps0, rho));

            IEquationParameter epsRho = AddVariable(new fsParameterIdentifier("eps*rho"));
            Equations.Add(new fsProductEquation(epsRho, eps, rho));

            IEquationParameter rhocd0AddEps0Rho = AddVariable(new fsParameterIdentifier("rhocd0 + eps0*rho"));
            Equations.Add(new fsSumEquation(rhocd0AddEps0Rho, rhocd0, eps0Rho));

            IEquationParameter rhocdAddEpsRho = AddVariable(new fsParameterIdentifier("rhocd + eps*rho"));
            Equations.Add(new fsSumEquation(rhocdAddEpsRho, rhocd, epsRho));

            IEquationParameter oneMinusXr = AddVariable(new fsParameterIdentifier("1 - xr"));
            Equations.Add(new fsSumEquation(constantOne, xr, oneMinusXr));
            IEquationParameter oneMinusXrSw0 = AddVariable(new fsParameterIdentifier("(1 - xr)*Sw0"));
            Equations.Add(new fsProductEquation(oneMinusXrSw0, oneMinusXr, Sw0));
            IEquationParameter xrAddoneMinusXrSw0 = AddVariable(new fsParameterIdentifier("xr + (1 - xr)*Sw0"));
            Equations.Add(new fsSumEquation(xrAddoneMinusXrSw0, xr, oneMinusXrSw0));
            IEquationParameter invRfw0 = AddVariable(new fsParameterIdentifier("1/Rfw0"));
            Equations.Add(new fsDivisionInverseEquation(invRfw0, Rfw0));
            IEquationParameter fractRfw0 = AddVariable(new fsParameterIdentifier("(1 - Rfw0)/Rfw0"));
            Equations.Add(new fsSumEquation(invRfw0, fractRfw0, constantOne));

            IEquationParameter epsVc = AddVariable(new fsParameterIdentifier("eps*Vc"));
            Equations.Add(new fsProductEquation(epsVc, eps, Vc));

            IEquationParameter nsTtech = AddVariable(new fsParameterIdentifier("ns*ttech"));
            Equations.Add(new fsProductEquation(nsTtech, ns, ttech));
            IEquationParameter tcMinusNsTtech = AddVariable(new fsParameterIdentifier("tc - ns*ttech"));
            Equations.Add(new fsSumEquation(tc, nsTtech, tcMinusNsTtech));

            IEquationParameter c0 = AddVariable(new fsParameterIdentifier("c0"));
            Equations.Add(new fsProductEquation(c0, Sw0, C0p));
            IEquationParameter c0MinusCw = AddVariable(new fsParameterIdentifier("c0 - cw"));
            Equations.Add(new fsSumEquation(c0, c0MinusCw, cw));
            IEquationParameter cStarC0MinusCw = AddVariable(new fsParameterIdentifier("(c0 - cw) * c* "));
            Equations.Add(new fsProductEquation(cStarC0MinusCw, cStar, c0MinusCw));

            IEquationParameter xStarOneMinusXr = AddVariable(new fsParameterIdentifier("(1 - xr) * x* "));
            Equations.Add(new fsProductEquation(xStarOneMinusXr, xStar, oneMinusXr));

            IEquationParameter xrX0p = AddVariable(new fsParameterIdentifier("xr*X0p"));
            Equations.Add(new fsProductEquation(xrX0p, xr, X0p));
            IEquationParameter XMinusXrX0p = AddVariable(new fsParameterIdentifier("X - xr*X0p"));
            Equations.Add(new fsSumEquation(X, xrX0p, XMinusXrX0p));
            
            IEquationParameter ccStarC0MinusCw = AddVariable(new fsParameterIdentifier("(c0 - cw) * cc*"));
            Equations.Add(new fsProductEquation(ccStarC0MinusCw, ccStar, c0MinusCw));
            
            IEquationParameter caStarC0MinusCw = AddVariable(new fsParameterIdentifier("(c0 - cw) * c*"));
            Equations.Add(new fsProductEquation(caStarC0MinusCw, caStar, c0MinusCw));

            IEquationParameter rhowf = AddVariable(new fsParameterIdentifier("rhowf"));
            Equations.Add(new fsIfMoreOrLessThenOneEquation(rhowf, wf, rhow, rho, rho, constantZero, rhow));

            IEquationParameter etawf = AddVariable(new fsParameterIdentifier("etawf"));
            Equations.Add(new fsIfMoreOrLessThenOneEquation(etawf, wf, etaw, eta, eta, constantZero, etaw));

            IEquationParameter sw0PowFq = AddVariable(new fsParameterIdentifier("Sw0^fq")); 
            Equations.Add(new fsTechnicalTimeFrom0Equation(sw0PowFq, constantOne, Sw0, fq)); // It's a dirty trick, of course!

            IEquationParameter c3 = AddVariable(new fsParameterIdentifier("c3"));
            Equations.Add(new fsProductsEquation(
                new[] { c3, eps, hc, hcAddHce },
                new[] { sw0PowFq, pc, Dp }));
           
            IEquationParameter rhowfEps = AddVariable(new fsParameterIdentifier("rhowf*eps"));
            Equations.Add(new fsProductEquation(rhowfEps, rhowf, eps));
            IEquationParameter rhocdAddRhowfEps = AddVariable(new fsParameterIdentifier("rhocd + rhowf*eps"));
            Equations.Add(new fsSumEquation(rhocdAddRhowfEps, rhocd, rhowfEps));

            #endregion

            #endregion

            #region Equations Initialization

            #region Material Parameters Equations

            Equations.Add(new fsProductsEquation(
                new[] { rhocd0 },
                new[] { oneMinusEps0, rhos }));
            Equations.Add(new fsProductsEquation(
                new[] { rhocd },
                new[] { oneMinusEps, rhos }));
            Equations.Add(new fsProductEquation(eps0Rho, Rf0, rhocd0AddEps0Rho));
            Equations.Add(new fsProductEquation(epsRho, Rf, rhocdAddEpsRho));
            Equations.Add(new fsFrom0AndDpEquation(eps, eps0, Dp, ne));

            Equations.Add(new fsDivisionInverseEquation(rc0, pc0));
            Equations.Add(new fsDivisionInverseEquation(rc, pc));
            Equations.Add(new fsFrom0AndDpEquation(pc, pc0, Dp, nc));
            Equations.Add(new fsProductsEquation(
                new[] { alpha0, rhos, oneMinusEps0, pc0 },
                new[] { constantOne }));
            Equations.Add(new fsProductsEquation(
                new[] { alpha, rhos, oneMinusEps, pc },
                new[] { constantOne }));
            Equations.Add(new fsProductsEquation(
                new[] { K, eta, hcAddHce },
                new[] { hc, pc }));

            Equations.Add(new fsProductEquation(hce, Rm, pc));

            Equations.Add(new fsProductsEquation(
                new[] { oneMinusEps, rhos },
                new[] { Sw0, eps, rho, fractRfw0 }));

            Equations.Add(new fsProductsEquation(
                new[] { X0p, rhocd, oneMinusXr },
                new[] { eps, xrAddoneMinusXrSw0, C0p }));

            /**
             * Modelling of the equation Dn = Dn0 * (u/u0)^(-aw1) * (hc/hc0)^aw2,
             * where u0 = 0.0001 m/s and hc0 = 0.01 m/s,
             * by means of the equation x = x0 * (p/10^5)^(-degree) settled in the class fsFrom0AndDpEquation 
             **/
            var const1e7 = new fsCalculatorConstant(new fsParameterIdentifier("1e7")) { Value = new fsValue(1e7) };
            var const1e9 = new fsCalculatorConstant(new fsParameterIdentifier("1e9")) { Value = new fsValue(1e9) };
            IEquationParameter v1 = AddVariable(new fsParameterIdentifier("u*10^9"));
            Equations.Add(new fsProductEquation(v1, u, const1e9));
            IEquationParameter v2 = AddVariable(new fsParameterIdentifier("Dn0*(u/u0)^(-aw1)"));
            Equations.Add(new fsFrom0AndDpEquation(v2, Dn0, v1, aw1));
            IEquationParameter v3 = AddVariable(new fsParameterIdentifier("hc*10^7"));
            Equations.Add(new fsProductEquation(v3, hc, const1e7));
            IEquationParameter v4 = AddVariable(new fsParameterIdentifier("-aw2"));
            Equations.Add(new fsSumEquation(constantZero, v4, aw2));
            Equations.Add(new fsFrom0AndDpEquation(Dn, v2, v3, v4));
           
            Equations.Add(new fsProductsEquation( // This equation needs to be discussed as to the possible status "calculated" for u and Dp (only input parameter!)
                new[] { u, eps, etaw, hcAddHce },
                new[] { pc, Dp }));

            #endregion

            #region Machine Setting Parameters Equations 

            Equations.Add(new fsProductsEquation(
                new[] { b, ns, ls },
                new[] { A }));
            Equations.Add(new fsProductEquation(ls, lsb, b));
            Equations.Add(new fsProductEquation(l, lb, b));
            Equations.Add(new fsProductEquation(l, ns, ls));
            Equations.Add(new fsProductEquation(As, ls, b));
            
            Equations.Add(new fsTechnicalTimeFrom0Equation(ttech, ttech0, As, lambda));

            Equations.Add(new fsProductEquation(u, l, n));
            Equations.Add(new fsDivisionInverseEquation(tc, n));

            Equations.Add(new fsProductsEquation(
                new[] { A, rhos, oneMinusEps, hc },
                new[] { Ms }));
            Equations.Add(new fsProductEquation(Ms, Qms, tc));

            Equations.Add(new fsProductsEquation(
                new[] { w, eps },
                new[] { oneMinusEps, rhos, wv }));
            Equations.Add(new fsProductsEquation(
                new[] { w, rhow, eps },
                new[] { rhos, oneMinusEps, wm }));
            Equations.Add(new fsProductsEquation(
                new[] { eps, A, hc, w },
                new[] { Vw }));
            Equations.Add(new fsProductEquation(Mw, rhow, Vw));
            Equations.Add(new fsProductEquation(nsw, ns, sw));
            Equations.Add(new fsProductEquation(tw, sw, tcMinusNsTtech));
            Equations.Add(new fsSumsEquation(
                new[] { wf, constantOne },
                new[] { w, Sw0 }));
            Equations.Add(new fsProductEquation(Vw, Qw, tc));
            Equations.Add(new fsProductEquation(Qmw, rhow, Qw));
            Equations.Add(new fsSumEquation(c, cStarC0MinusCw, cw));
            Equations.Add(new fsSumEquation(x, xr, xStarOneMinusXr));
            Equations.Add(new fsProductEquation(X, X0p, x));
            Equations.Add(new fsProductsEquation(
                new[] { eps, cc },
                new[] { rhocd, XMinusXrX0p }));
            Equations.Add(new fsSumEquation(cc, ccStarC0MinusCw, cw));
            Equations.Add(new fsSumEquation(ca, caStarC0MinusCw, cw));
            Equations.Add(new fsCstarDnWfEquation(cStar, Dn, wf));
            Equations.Add(new fsXstarDnWfEquation(xStar, Dn, wf));
            Equations.Add(new fsCaDnCwWfEquation(ca, cw, c0, Dn, wf));           

            Equations.Add(new fsWfTwEquation(wf, tw, c3, etaw, eta));
            Equations.Add(new fsCaWfXstarEquation(ca, xStar, cw, c0, wf));
            Equations.Add(new fsIfMoreOrLessThenOneEquation(Vwf, wf, epsVc, constantZero, constantZero, epsVc, constantZero));
            Equations.Add(new fsProductsEquation(
                new[] { eps, c3, Vc },
                new[] { Q, etawf }));
            Equations.Add(new fsProductsEquation(
                new[] { rhowf, eps, Vc },
                new[] { Mlc }));
            Equations.Add(new fsMwfVcWfEquation(Mwf, eps, Vc, rhow, rho, wf));
            Equations.Add(new fsProductEquation(Qm, rhowf, Q));
            Equations.Add(new fsProductEquation(Mc, rhocdAddRhowfEps, Vc));
            Equations.Add(new fsProductEquation(Vc, A, hc));
            Equations.Add(new fsProductEquation(Vlc, eps, Vc));
            Equations.Add(new fsProductEquation(Mlc, Rfw, Mc));
            Equations.Add(new fsProductEquation(Vwf, Qa, tc));
            Equations.Add(new fsProductEquation(Mwf, Qma, tc));
            Equations.Add(new fsProductEquation(Q, q, A));
            Equations.Add(new fsProductEquation(Qm, qm, A));
            Equations.Add(new fsProductEquation(Qa, qa, A));
            Equations.Add(new fsProductEquation(Qma, qma, A));

            #endregion 

            #endregion
        }
    }
}
