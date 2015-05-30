using Parameters;

namespace Equations
{
    public class fsEpsFromVsusAndHcEquation : fsCalculatorEquation
    {
        #region Parameters

        private readonly IEquationParameter eps;
        private readonly IEquationParameter vsus;
        private readonly IEquationParameter c;
        private readonly IEquationParameter A;
        private readonly IEquationParameter rhos;
        private readonly IEquationParameter hc;

        //eps  = 1 - (c * vsus) / (A * rhos * hc);

        #endregion

        public fsEpsFromVsusAndHcEquation(
            IEquationParameter porosity,
            IEquationParameter susp_volume,
            IEquationParameter suspension_solids_concentration,
            IEquationParameter filtration_area,
            IEquationParameter solids_density,
            IEquationParameter cake_hight)
            : base(
                porosity,
                susp_volume,
                suspension_solids_concentration,
                filtration_area,
                solids_density,
                cake_hight)
        {
            eps = porosity;
            vsus = susp_volume;
            c = suspension_solids_concentration;
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
            eps.Value = 1 - c.Value*vsus.Value/(A.Value*rhos.Value*hc.Value);
        }

        #endregion
    }
}