using Parameters;
using Equations;

namespace StepCalculators
{
    public class fsCakeFormationDpConstCalculator : fsCalculator
    {
        readonly fsCalculatorConstant m_suspensionDensity;
        readonly fsCalculatorConstant m_solidsDensity;
        readonly fsCalculatorConstant m_etaf;
        readonly fsCalculatorConstant m_hce0;
        readonly fsCalculatorConstant m_porosity0;
        readonly fsCalculatorConstant m_pc0;
        readonly fsCalculatorConstant m_ne;
        readonly fsCalculatorConstant m_nc;
        readonly fsCalculatorConstant m_volumeConcentration;

        readonly fsCalculatorVariable m_filterArea;

        readonly fsCalculatorVariable m_pressure;

        readonly fsCalculatorVariable m_cycleTime;
        readonly fsCalculatorVariable m_rotationalSpeed;

        readonly fsCalculatorVariable m_formationRelativeTime;
        readonly fsCalculatorVariable m_formationTime;
        readonly fsCalculatorVariable m_cakeHeight;
        readonly fsCalculatorVariable m_suspensionMass;
        readonly fsCalculatorVariable m_suspensionVolume;
        readonly fsCalculatorVariable m_suspensionMassFlowrate;

        readonly fsCalculatorVariable m_porosity;
        readonly fsCalculatorVariable m_pc;
        readonly fsCalculatorVariable m_rc;
        readonly fsCalculatorVariable m_alpha;
        readonly fsCalculatorVariable m_kappa;

        public fsCakeFormationDpConstCalculator()
        {
            #region Parameters Initialization

            m_suspensionDensity = AddConstant(fsParameterIdentifier.SuspensionDensity);
            m_solidsDensity = AddConstant(fsParameterIdentifier.SolidsDensity);
            m_etaf = AddConstant(fsParameterIdentifier.MotherLiquidViscosity);
            m_hce0 = AddConstant(fsParameterIdentifier.FilterMediumResistanceHce0);
            m_porosity0 = AddConstant(fsParameterIdentifier.CakePorosity0);
            AddConstant(fsParameterIdentifier.Kappa0);
            m_pc0 = AddConstant(fsParameterIdentifier.CakePermeability0);
            m_ne = AddConstant(fsParameterIdentifier.Ne);
            m_nc = AddConstant(fsParameterIdentifier.CakeCompressibility);
            m_volumeConcentration = AddConstant(fsParameterIdentifier.SuspensionSolidsVolumeFraction);

            m_filterArea = AddVariable(fsParameterIdentifier.FilterArea);

            m_pressure = AddVariable(fsParameterIdentifier.PressureDifference);

            m_cycleTime = AddVariable(fsParameterIdentifier.CycleTime);
            m_rotationalSpeed = AddVariable(fsParameterIdentifier.RotationalSpeed);

            m_formationRelativeTime = AddVariable(fsParameterIdentifier.SpecificFiltrationTime);
            m_formationTime = AddVariable(fsParameterIdentifier.FiltrationTime);
            m_cakeHeight = AddVariable(fsParameterIdentifier.CakeHeight);
            m_suspensionMass = AddVariable(fsParameterIdentifier.SuspensionMass);
            m_suspensionVolume = AddVariable(fsParameterIdentifier.SuspensionVolume);
            m_suspensionMassFlowrate = AddVariable(fsParameterIdentifier.SuspensionMassFlowrate);

            m_porosity = AddVariable(fsParameterIdentifier.CakePorosity);
            m_pc = AddVariable(fsParameterIdentifier.CakePermeability);
            m_rc = AddVariable(fsParameterIdentifier.CakeResistance);
            m_alpha = AddVariable(fsParameterIdentifier.CakeResistanceAlpha);
            m_kappa = AddVariable(fsParameterIdentifier.Kappa);

            #endregion

            #region Equations Initialization

            AddEquation(new fsDivisionInverseEquation(m_cycleTime, m_rotationalSpeed));
            AddEquation(new fsDivisionInverseEquation(m_rotationalSpeed, m_cycleTime));
            AddEquation(new fsProductEquation(m_formationTime, m_formationRelativeTime, m_cycleTime));
            AddEquation(new fsCakeHeightFromDpTf(m_cakeHeight, m_hce0, m_pc, m_kappa, m_pressure, m_formationTime, m_etaf));
            AddEquation(new fsVsusFromAreaAndCakeHeightEquation(m_suspensionVolume, m_filterArea, m_cakeHeight, m_kappa));
            AddEquation(new fsProductEquation(m_suspensionMass, m_suspensionDensity, m_suspensionVolume));
            AddEquation(new fsFrom0AndDpEquation(m_porosity, m_porosity0, m_pressure, m_ne));
            AddEquation(new fsFrom0AndDpEquation(m_pc, m_pc0, m_pressure, m_nc));
            AddEquation(new fsEpsKappaCvEquation(m_porosity, m_kappa, m_volumeConcentration));
            AddEquation(new fsAlphaPcEquation(m_alpha, m_pc, m_porosity, m_solidsDensity));
            AddEquation(new fsDivisionInverseEquation(m_rc, m_pc));
            AddEquation(new fsProductEquation(m_suspensionMass, m_suspensionMassFlowrate, m_cycleTime));

            #endregion
        }
    }
}
