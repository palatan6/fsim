using Parameters;
using Value;

namespace Equations.Material
{
    public class fsMoistureContentFromCakeSaturationEquation : fsCalculatorEquation
    {
        #region Parameters

        private readonly IEquationParameter m_cakePorosity;
        private readonly IEquationParameter m_cakeSaturation;
        private readonly IEquationParameter m_liquidDensity;
        private readonly IEquationParameter m_moistureContent;
        private readonly IEquationParameter m_solidsDensity;

        #endregion

        public fsMoistureContentFromCakeSaturationEquation(
            IEquationParameter liquidDensity,
            IEquationParameter solidsDensity,
            IEquationParameter cakePorosity,
            IEquationParameter moistureContent,
            IEquationParameter cakeSaturation)
            : base(liquidDensity, solidsDensity, cakePorosity, moistureContent, cakeSaturation)
        {
            m_liquidDensity = liquidDensity;
            m_solidsDensity = solidsDensity;
            m_cakePorosity = cakePorosity;
            m_moistureContent = moistureContent;
            m_cakeSaturation = cakeSaturation;
        }

        protected override void InitFormulas()
        {
            AddFormula(m_moistureContent, MoistureContentFormula);
            AddFormula(m_cakePorosity, CakePororsityFormula);
            AddFormula(m_cakeSaturation, CakeSaturationFormula);
        }

        #region Formulas

        private void MoistureContentFormula()
        {
            m_moistureContent.Value = 1 /
                                      ((1 - m_cakePorosity.Value) * m_solidsDensity.Value /
                                       (m_cakePorosity.Value * m_liquidDensity.Value * m_cakeSaturation.Value) + 1);
        }

        private void CakeSaturationFormula()
        {
            fsValue nom = m_moistureContent.Value * m_solidsDensity.Value * (m_cakePorosity.Value - 1);
            fsValue den = m_cakePorosity.Value * m_liquidDensity.Value * (m_moistureContent.Value - 1);
            m_cakeSaturation.Value = nom / den;
        }

        private void CakePororsityFormula()
        {
            fsValue nom = m_moistureContent.Value * m_solidsDensity.Value;
            fsValue den = -nom + m_liquidDensity.Value * m_cakeSaturation.Value * (m_moistureContent.Value - 1);
            m_cakePorosity.Value = -nom / den;
        }

        #endregion
    }
}