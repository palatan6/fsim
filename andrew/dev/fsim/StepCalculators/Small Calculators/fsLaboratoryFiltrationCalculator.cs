using Equations;
using Parameters;

namespace StepCalculators
{
    public class fsLaboratoryFiltrationCalculator : fsCalculator
    {
        public fsLaboratoryFiltrationCalculator()
        {
            #region Parameters Initialization

            fsCalculatorConstant m_rho = AddConstant(fsParameterIdentifier.MotherLiquidDensity);
            fsCalculatorConstant m_eps = AddConstant(fsParameterIdentifier.CakePorosity);
            fsCalculatorConstant m_viscosity = AddConstant(fsParameterIdentifier.MotherLiquidViscosity);
            fsCalculatorConstant m_pc0 = AddConstant(fsParameterIdentifier.CakePermeability0);
            fsCalculatorConstant m_kappa0 = AddConstant(fsParameterIdentifier.Kappa0);
            fsCalculatorConstant m_pressure = AddConstant(fsParameterIdentifier.PressureDifference);
            fsCalculatorConstant m_area = AddConstant(fsParameterIdentifier.FilterArea);
            fsCalculatorConstant m_nc = AddConstant(fsParameterIdentifier.CakeCompressibility);
            fsCalculatorConstant m_rhosus = AddConstant(fsParameterIdentifier.SuspensionDensity);
            fsCalculatorConstant m_eps0 = AddConstant(fsParameterIdentifier.CakePorosity0);
            fsCalculatorConstant m_solidsDensity = AddConstant(fsParameterIdentifier.SolidsDensity);
            fsCalculatorConstant m_cm = AddConstant(fsParameterIdentifier.SuspensionSolidsMassFraction);
            fsCalculatorConstant m_kappa = AddConstant(fsParameterIdentifier.Kappa);
            fsCalculatorVariable m_pc = AddVariable(fsParameterIdentifier.CakePermeability);
            fsCalculatorVariable m_hc = AddVariable(fsParameterIdentifier.CakeHeight);
            fsCalculatorVariable m_formationTime = AddVariable(fsParameterIdentifier.FiltrationTime);
            fsCalculatorVariable m_solidsMass = AddVariable(fsParameterIdentifier.SolidsMassInSuspension);
            fsCalculatorVariable m_liquidMass = AddVariable(fsParameterIdentifier.LiquidMassInSuspension);
            fsCalculatorVariable m_suspensionMass = AddVariable(fsParameterIdentifier.SuspensionMass);
            fsCalculatorVariable m_Rm0 = AddVariable(fsParameterIdentifier.FilterMediumResistanceRm0);
            fsCalculatorVariable m_mf = AddVariable(fsParameterIdentifier.FiltrateMass);
            fsCalculatorVariable m_mc = AddVariable(fsParameterIdentifier.CakeMass);
            fsCalculatorVariable m_ms = AddVariable(fsParameterIdentifier.SolidsMass);
            fsCalculatorVariable m_qmft = AddVariable(fsParameterIdentifier.qmft);
            fsCalculatorVariable m_Dp = AddVariable(fsParameterIdentifier.PressureDifference);
            fsCalculatorVariable m_vsus = AddVariable(fsParameterIdentifier.SuspensionVolume);
            fsCalculatorVariable m_vf = AddVariable(fsParameterIdentifier.FiltrateVolume);
            fsCalculatorVariable m_vc = AddVariable(fsParameterIdentifier.CakeVolume);
            fsCalculatorVariable m_vs = AddVariable(fsParameterIdentifier.SolidsVolume);
            fsCalculatorVariable m_qft = AddVariable(fsParameterIdentifier.qft);
            fsCalculatorVariable m_rhos = AddVariable(fsParameterIdentifier.SolidsDensity);
            fsCalculatorVariable m_hce0 = AddVariable(fsParameterIdentifier.FilterMediumResistanceHce0);
            
            #endregion

            #region Equations Initialization

            AddEquation(new fsFrom0AndDpEquation(m_pc, m_pc0, m_pressure, m_nc));
            AddEquation(new fsCakeHeightFromDpTf(m_hc, m_hce0, m_pc, m_kappa0, m_pressure, m_formationTime, m_viscosity));
            AddEquation(new fsSuspensionMassFromHcEpsPlainAreaEquation(m_suspensionMass, m_eps0, m_solidsDensity, m_area, m_hc, m_cm));
            AddEquation(new fsProductEquation(m_solidsMass, m_suspensionMass, m_cm));
            AddEquation(new fsSumEquation(m_suspensionMass, m_solidsMass, m_liquidMass));
            AddEquation(new fsRm0FromHcePc0Equation(m_Rm0, m_hce0, m_pc0));
            AddEquation(new fsMsusFromHcRhosusAKappaEquation(m_suspensionMass, m_rhosus, m_area, m_hc, m_kappa));
            AddEquation(new fsMfFromHcEquation(m_mf, m_rho, m_area, m_hc, m_kappa));
            AddEquation(new fsMcFromHcEquation(m_mc, m_area, m_hc, m_rhos, m_eps, m_rho));
            AddEquation(new fsMsFromHcEquation(m_ms, m_area, m_hc, m_rhos, m_eps));
            AddEquation(new fsQmftFromHcRhoEtaDpEquation(m_qmft, m_rho, m_pc, m_Dp, m_viscosity, m_hc, m_hce0));
            AddEquation(new fsVsusFromHcAKappaEquation(m_vsus, m_area, m_hc, m_kappa));
            AddEquation(new fsVfFromHcEquation(m_vf, m_area, m_hc, m_kappa));
            AddEquation(new fsVcFromHcEquation(m_vc, m_area, m_hc));
            AddEquation(new fsVsFromHcEquation(m_vs, m_area, m_hc, m_eps));
            AddEquation(new fsQftFromHcEtaDpEquation(m_qft, m_pc, m_Dp, m_viscosity, m_hc, m_hce0));

            #endregion
        }
    }
}

