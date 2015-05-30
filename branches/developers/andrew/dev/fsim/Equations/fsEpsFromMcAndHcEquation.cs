using Parameters;

namespace Equations
{
    public class fsEpsFromMcAndHcEquation : fsCalculatorEquation
    {
        #region Parameters

        private readonly IEquationParameter eps;
        private readonly IEquationParameter mc;
        private readonly IEquationParameter rhof;
        private readonly IEquationParameter A;
        private readonly IEquationParameter rhos;
        private readonly IEquationParameter hc;

        //eps  = (rhos * A * hc  Mc ) / (A * hc * (phos - rhof));

        #endregion

        public fsEpsFromMcAndHcEquation(
            IEquationParameter porosity,
            IEquationParameter cake_mass,
            IEquationParameter mother_liquid_density,
            IEquationParameter filtration_area,
            IEquationParameter solids_density,
            IEquationParameter cake_hight)
            : base(
                porosity,
                cake_mass,
                mother_liquid_density,
                filtration_area,
                solids_density,
                cake_hight)
        {
            eps = porosity;
            mc = cake_mass;
            rhof = mother_liquid_density;
            A = filtration_area;
            rhos = solids_density;
            hc = cake_hight;
        }

        protected override void InitFormulas()
        {
            AddFormula(eps, PorosityFormula);
            AddFormula(hc, HcFormula);
        }

        #region Formulas

        private void PorosityFormula()
        {
            eps.Value = (rhos.Value * A.Value * hc.Value - mc.Value)/(A.Value*hc.Value*(rhos.Value - rhof.Value));
        }

        private void HcFormula()
        {
            hc.Value = mc.Value/(rhos.Value*A.Value - A.Value*(rhos.Value - rhof.Value)*eps.Value);
        }

        #endregion
    }
}
