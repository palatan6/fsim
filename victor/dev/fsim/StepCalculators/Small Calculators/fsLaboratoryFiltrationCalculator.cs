using Equations;
using Parameters;

namespace StepCalculators
{
    public class fsLaboratoryFiltrationCalculator : fsCalculator
    {

        readonly fsCalculatorConstant m_hce0;
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

        public fsLaboratoryFiltrationCalculator()
        {
            #region Parameters Initialization

            m_hce0 = AddConstant(fsParameterIdentifier.FilterMediumResistanceHce0);
            m_viscosity = AddConstant(fsParameterIdentifier.MotherLiquidViscosity);
            m_pc0 = AddConstant(fsParameterIdentifier.CakePermeability0);
            m_kappa0 = AddConstant(fsParameterIdentifier.Kappa0);
            m_pressure = AddConstant(fsParameterIdentifier.PressureDifference);
            m_area = AddConstant(fsParameterIdentifier.FilterArea);
            m_nc = AddConstant(fsParameterIdentifier.CakeCompressibility);
            AddConstant(fsParameterIdentifier.SuspensionDensity);
            m_eps0 = AddConstant(fsParameterIdentifier.CakePorosity0);
            m_solidsDensity = AddConstant(fsParameterIdentifier.SolidsDensity);
            m_cm = AddConstant(fsParameterIdentifier.SuspensionSolidsMassFraction);
            m_pc = AddVariable(fsParameterIdentifier.CakePermeability);
            m_hc = AddVariable(fsParameterIdentifier.CakeHeight);
            m_formationTime = AddVariable(fsParameterIdentifier.FiltrationTime);
            m_solidsMass = AddVariable(fsParameterIdentifier.SolidsMassInSuspension);
            m_liquidMass = AddVariable(fsParameterIdentifier.LiquidMassInSuspension);
            m_suspensionMass = AddVariable(fsParameterIdentifier.SuspensionMass);

            #endregion

            #region Equations Initialization

            AddEquation(new fsFrom0AndDpEquation(m_pc, m_pc0, m_pressure, m_nc));
            AddEquation(new fsCakeHeightFromDpTf(m_hc, m_hce0, m_pc, m_kappa0, m_pressure, m_formationTime, m_viscosity));
            AddEquation(new fsSuspensionMassFromHcEpsPlainAreaEquation(m_suspensionMass, m_eps0, m_solidsDensity, m_area, m_hc, m_cm));
            AddEquation(new fsProductEquation(m_solidsMass, m_suspensionMass, m_cm));
            AddEquation(new fsSumEquation(m_suspensionMass, m_solidsMass, m_liquidMass));

            #endregion
        }
    }
}

