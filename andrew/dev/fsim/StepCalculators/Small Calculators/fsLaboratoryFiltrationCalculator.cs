using Equations;
using Parameters;

namespace StepCalculators
{
    public class fsLaboratoryFiltrationCalculator : fsCalculator
    {
        #region Parameters Declaration

        readonly fsCalculatorVariable m_hce0;
        readonly fsCalculatorConstant m_viscosity;
        readonly fsCalculatorConstant m_pc0;
        readonly fsCalculatorConstant m_kappa0;
        readonly fsCalculatorConstant m_pressure;
        readonly fsCalculatorConstant m_area;
        readonly fsCalculatorConstant m_nc;
        readonly fsCalculatorConstant m_eps0;
        readonly fsCalculatorConstant m_solidsDensity;
        readonly fsCalculatorConstant m_cm;
        readonly fsCalculatorVariable m_pc;
        readonly fsCalculatorVariable m_hc;
        readonly fsCalculatorVariable m_formationTime;
        readonly fsCalculatorVariable m_suspensionMass;
        readonly fsCalculatorVariable m_solidsMass;
        readonly fsCalculatorVariable m_liquidMass;
        readonly fsCalculatorVariable m_Rm0;
        readonly fsCalculatorVariable m_mf;
        readonly fsCalculatorVariable m_ms;
        readonly fsCalculatorVariable m_mc;
        readonly fsCalculatorConstant m_rhosus;
        readonly fsCalculatorConstant m_kappa;
        readonly fsCalculatorConstant m_rho;
        readonly fsCalculatorVariable m_rhos;
        readonly fsCalculatorConstant m_eps;
        readonly fsCalculatorVariable m_qmft;
        readonly fsCalculatorVariable m_Dp;
        readonly fsCalculatorVariable m_vsus;
        readonly fsCalculatorVariable m_vf;
        readonly fsCalculatorVariable m_vc;
        readonly fsCalculatorVariable m_vs;
        readonly fsCalculatorVariable m_qft;

        #endregion

        public fsLaboratoryFiltrationCalculator()
        {
            #region Parameters Initialization

            m_rhos = AddVariable(fsParameterIdentifier.SolidsDensity);
            m_rho = AddConstant(fsParameterIdentifier.MotherLiquidDensity);
            m_eps = AddConstant(fsParameterIdentifier.CakePorosity);
            m_hce0 = AddVariable(fsParameterIdentifier.FilterMediumResistanceHce0);
            m_viscosity = AddConstant(fsParameterIdentifier.MotherLiquidViscosity);
            m_pc0 = AddConstant(fsParameterIdentifier.CakePermeability0);
            m_kappa0 = AddConstant(fsParameterIdentifier.Kappa0);
            m_pressure = AddConstant(fsParameterIdentifier.PressureDifference);
            m_area = AddConstant(fsParameterIdentifier.FilterArea);
            m_nc = AddConstant(fsParameterIdentifier.CakeCompressibility);
            m_rhosus = AddConstant(fsParameterIdentifier.SuspensionDensity);
            m_eps0 = AddConstant(fsParameterIdentifier.CakePorosity0);
            m_solidsDensity = AddConstant(fsParameterIdentifier.SolidsDensity);
            m_cm = AddConstant(fsParameterIdentifier.SuspensionSolidsMassFraction);
            m_kappa = AddConstant(fsParameterIdentifier.Kappa);
            m_pc = AddVariable(fsParameterIdentifier.CakePermeability);
            m_hc = AddVariable(fsParameterIdentifier.CakeHeight);
            m_formationTime = AddVariable(fsParameterIdentifier.FiltrationTime);
            m_solidsMass = AddVariable(fsParameterIdentifier.SolidsMassInSuspension);
            m_liquidMass = AddVariable(fsParameterIdentifier.LiquidMassInSuspension);
            m_suspensionMass = AddVariable(fsParameterIdentifier.SuspensionMass);
            m_Rm0 = AddVariable(fsParameterIdentifier.FilterMediumResistanceRm0);
            m_mf = AddVariable(fsParameterIdentifier.FiltrateMass);
            m_mc = AddVariable(fsParameterIdentifier.CakeMass);
            m_ms = AddVariable(fsParameterIdentifier.SolidsMass);
            m_qmft = AddVariable(fsParameterIdentifier.qmft);
            m_Dp = AddVariable(fsParameterIdentifier.PressureDifference);
            m_vsus = AddVariable(fsParameterIdentifier.SuspensionVolume);
            m_vf = AddVariable(fsParameterIdentifier.FiltrateVolume);
            m_vc = AddVariable(fsParameterIdentifier.CakeVolume);
            m_vs = AddVariable(fsParameterIdentifier.SolidsVolume);
            m_qft = AddVariable(fsParameterIdentifier.qft);

            #endregion

            #region Equations Initialization

            AddEquation(new fsFrom0AndDpEquation(m_pc, m_pc0, m_pressure, m_nc));
            AddEquation(new fsCakeHeightFromDpTf(m_hc, m_hce0, m_pc, m_kappa0, m_pressure, m_formationTime, m_viscosity));
            AddEquation(new fsSuspensionMassFromHcEpsPlainAreaEquation(m_suspensionMass, m_eps0, m_solidsDensity, m_area, m_hc, m_cm));
            AddEquation(new fsProductEquation(m_solidsMass, m_suspensionMass, m_cm));
            AddEquation(new fsSumEquation(m_suspensionMass, m_solidsMass, m_liquidMass));
            AddEquation(new fsRm0FromHcePc0(m_Rm0, m_hce0, m_pc0));
            AddEquation(new fsMsusFromHcRhosusAKappa(m_suspensionMass, m_rhosus, m_area, m_hc, m_kappa));
            AddEquation(new fsMfFromHc(m_mf, m_rho, m_area, m_hc, m_kappa));
            AddEquation(new fsMcFromHc(m_mc, m_area, m_hc, m_rhos, m_eps, m_rho));
            AddEquation(new fsMsFromHc(m_ms, m_area, m_hc, m_rhos, m_eps));
            AddEquation(new fsQmftFromHcRhoEtaDp(m_qmft, m_rho, m_pc, m_Dp, m_viscosity, m_hc, m_hce0));
            AddEquation(new fsVsusFromHcAKappa(m_vsus, m_area, m_hc, m_kappa));
            AddEquation(new fsVfFromHc(m_vf, m_area, m_hc, m_kappa));
            AddEquation(new fsVcFromHc(m_vc, m_area , m_hc));
            AddEquation(new fsVsFromHc(m_vs, m_area, m_hc, m_eps));
            AddEquation(new fsQftFromHcEtaDp(m_qft, m_pc, m_Dp, m_viscosity, m_hc, m_hce0));

            #endregion
        }
    }
}

