using Parameters;
using Equations;

namespace StepCalculators
{
    public class fsDensityConcentrationCalculator : fsCalculator
    {
        readonly fsCalculatorVariable m_filtrateDensity;
        readonly fsCalculatorVariable m_solidsDensity;
        readonly fsCalculatorVariable m_suspensionDensity;
        readonly fsCalculatorVariable m_solidsMassFraction;
        readonly fsCalculatorVariable m_solidsVolumeFraction;
        readonly fsCalculatorVariable m_solidsConcentration;

        public fsDensityConcentrationCalculator()
        {
            #region Parameters Initialization

            m_filtrateDensity = AddVariable(fsParameterIdentifier.MotherLiquidDensity);
            m_solidsDensity = AddVariable(fsParameterIdentifier.SolidsDensity);
            m_suspensionDensity = AddVariable(fsParameterIdentifier.SuspensionDensity);
            m_solidsMassFraction = AddVariable(fsParameterIdentifier.SuspensionSolidsMassFraction);
            m_solidsVolumeFraction = AddVariable(fsParameterIdentifier.SuspensionSolidsVolumeFraction);
            m_solidsConcentration = AddVariable(fsParameterIdentifier.SuspensionSolidsConcentration);

            #endregion

            #region Equations Initialization

            AddEquation(new fsMassConcentrationEquation(m_solidsMassFraction, m_filtrateDensity, m_solidsDensity, m_suspensionDensity));
            AddEquation(new fsVolumeConcentrationEquation(m_solidsVolumeFraction, m_filtrateDensity, m_solidsDensity, m_suspensionDensity));
            AddEquation(new fsConcentrationEquation(m_solidsConcentration, m_filtrateDensity, m_solidsDensity, m_suspensionDensity));

            #endregion
        }
    }
}
