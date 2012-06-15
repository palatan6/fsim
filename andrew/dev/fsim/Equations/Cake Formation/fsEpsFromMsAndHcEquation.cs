using Parameters;

namespace Equations
{
    public class fsEpsFromMsAndHcEquation : fsCalculatorEquation
    {
        #region Parameters

        private readonly IEquationParameter eps;
        private readonly IEquationParameter ms;
        private readonly IEquationParameter A;
        private readonly IEquationParameter rhos;
        private readonly IEquationParameter hc;

        //eps  = 1 - ms / (A * rhos * hc);

        #endregion

        public fsEpsFromMsAndHcEquation(
            IEquationParameter porosity,
            IEquationParameter solids_mass,
            IEquationParameter filtration_area,
            IEquationParameter solids_density,
            IEquationParameter cake_hight)
            : base(
                porosity,
                solids_mass,
                filtration_area,
                solids_density,
                cake_hight)
        {
            eps = porosity;
            ms = solids_mass;
            A = filtration_area;
            rhos = solids_density;
            hc = cake_hight;
        }

        protected override void InitFormulas()
        {
            AddFormula(eps, PorosityFormula);
        }

        #region Formulas

        private void PorosityFormula()
        {
            eps.Value = 1 - ms.Value / (A.Value * rhos.Value * hc.Value);
        }

        #endregion
    }
}