using Parameters;
using Equations.Material;

namespace StepCalculators
{
    public class fsRfFromCakeSaturationCalculator : fsCalculator
    {
        readonly fsCalculatorConstant m_liquidDensity;
        readonly fsCalculatorConstant m_solidsDensity;
        readonly fsCalculatorVariable m_cakePorosity;
        readonly fsCalculatorVariable m_cakeSaturation;
        readonly fsCalculatorVariable m_cakeMoistureContent;

        public fsRfFromCakeSaturationCalculator()
        {
            #region Parameters Initialization

            m_liquidDensity = AddConstant(fsParameterIdentifier.LiquidDensity);
            m_solidsDensity = AddConstant(fsParameterIdentifier.SolidsDensity);
            m_cakePorosity = AddVariable(fsParameterIdentifier.CakePorosity);
            m_cakeSaturation = AddVariable(fsParameterIdentifier.CakeSaturation);
            m_cakeMoistureContent = AddVariable(fsParameterIdentifier.CakeMoistureContentRf);

            #endregion

            #region Equations Initialization

            Equations.Add(new fsMoistureContentFromCakeSaturationEquation(m_liquidDensity, m_solidsDensity, m_cakePorosity, m_cakeMoistureContent, m_cakeSaturation));

            #endregion
        }
    }
}
