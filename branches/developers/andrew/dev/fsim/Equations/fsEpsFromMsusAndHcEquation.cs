using Parameters;

namespace Equations
{
    public class fsEpsFromMsusAndHcEquation : fsCalculatorEquation
    {
        #region Parameters

        private readonly IEquationParameter eps;
        private readonly IEquationParameter msus;
        private readonly IEquationParameter cm;
        private readonly IEquationParameter A;
        private readonly IEquationParameter rhos;
        private readonly IEquationParameter hc;

        //eps  = 1 - (cm * msus) / (A * rhos * hc);

        #endregion

        public fsEpsFromMsusAndHcEquation(
            IEquationParameter porosity,
            IEquationParameter susp_mass,
            IEquationParameter suspension_solids_mass_fraction,
            IEquationParameter filtration_area,
            IEquationParameter solids_density,
            IEquationParameter cake_hight)
            : base(
                porosity,
                susp_mass,
                suspension_solids_mass_fraction,
                filtration_area,
                solids_density,
                cake_hight)
        {
            eps = porosity;
            msus = susp_mass;
            cm = suspension_solids_mass_fraction;
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
            eps.Value = 1 - cm.Value*msus.Value/(A.Value*rhos.Value*hc.Value);
        }

        #endregion
    }
}