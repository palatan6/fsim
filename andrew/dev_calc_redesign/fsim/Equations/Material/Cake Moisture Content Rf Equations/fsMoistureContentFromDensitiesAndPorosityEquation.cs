using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Parameters;

namespace Equations.Material.Cake_Moisture_Content_Rf_Equations
{
    public class fsMoistureContentFromDensitiesAndPorosityEquation : fsCalculatorEquation
    {
        #region Parameters

        private readonly IEquationParameter m_moistureContent;
        private readonly IEquationParameter m_cakePorosity;
        private readonly IEquationParameter m_filtrateDensity;
        private readonly IEquationParameter m_solidsDensity;
        
        #endregion

        public fsMoistureContentFromDensitiesAndPorosityEquation(
            IEquationParameter moistureContent,
            IEquationParameter cakePorosity,
            IEquationParameter filtrateDensity,
            IEquationParameter solidsDensity)
            : base(moistureContent, cakePorosity, filtrateDensity, solidsDensity)
        {
            m_moistureContent = moistureContent;
            m_cakePorosity = cakePorosity;
            m_filtrateDensity = filtrateDensity;
            m_solidsDensity = solidsDensity;
        }

        protected override void InitFormulas()
        {
            AddFormula(m_moistureContent, MoistureContentFormula);
            AddFormula(m_cakePorosity, CakePorosityFormula);
        }

        #region Formulas

        private void MoistureContentFormula()
        {
            m_moistureContent.Value = m_cakePorosity.Value * m_filtrateDensity.Value
                / ((1 - m_cakePorosity.Value) * m_solidsDensity.Value + m_cakePorosity.Value * m_filtrateDensity.Value);
        }

        private void CakePorosityFormula()
        {
            m_cakePorosity.Value = m_moistureContent.Value * m_solidsDensity.Value
                / (m_filtrateDensity.Value + m_moistureContent.Value * (m_solidsDensity.Value - m_filtrateDensity.Value));
        }

        #endregion
    }
}
