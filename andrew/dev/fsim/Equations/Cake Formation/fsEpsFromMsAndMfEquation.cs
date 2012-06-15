using Parameters;

namespace Equations
{
    public class fsEpsFromMsAndMfEquation : fsCalculatorEquation
    {
        #region Parameters

        private readonly IEquationParameter eps;
        private readonly IEquationParameter cv;
        private readonly IEquationParameter ms;
        private readonly IEquationParameter rho;
        private readonly IEquationParameter rhos;
        private readonly IEquationParameter mf;

        #endregion

        public fsEpsFromMsAndMfEquation(
            IEquationParameter porosity,
            IEquationParameter suspension_solids_volume_fraction,
            IEquationParameter solids_mass,
            IEquationParameter mother_liquid_density,
            IEquationParameter solids_density,
            IEquationParameter fitration_mass)
            : base(
                porosity,
                suspension_solids_volume_fraction,
                solids_mass,
                mother_liquid_density,
                solids_density,
                fitration_mass)
        {
            eps = porosity;
            cv = suspension_solids_volume_fraction;
            ms = solids_mass;
            rho = mother_liquid_density;
            rhos = solids_density;
            mf = fitration_mass;
        }

        protected override void InitFormulas()
        {
            AddFormula(eps, PorosityFormula);
        }

        #region Formulas

        private void PorosityFormula()
        {
            eps.Value = 1 - cv.Value * (1 + 1 / ((ms.Value * rho.Value) / (rhos.Value * cv.Value * mf.Value) - 1));
        }

        #endregion
    }
}