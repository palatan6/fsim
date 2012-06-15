using Parameters;

namespace Equations
{
    public class fsEpsFromMcAndQfEquation : fsCalculatorEquation
    {
        #region Parameters

        private readonly IEquationParameter eps;
        private readonly IEquationParameter cv;
        private readonly IEquationParameter mc;
        private readonly IEquationParameter A;
        private readonly IEquationParameter rho;
        private readonly IEquationParameter rhos;
        private readonly IEquationParameter tf;
        private readonly IEquationParameter qft;

        #endregion

        public fsEpsFromMcAndQfEquation(
            IEquationParameter porosity,
            IEquationParameter suspension_solids_volume_fraction,
            IEquationParameter cake_mass,
            IEquationParameter filter_area,
            IEquationParameter mother_liquid_density,
            IEquationParameter solids_density,
            IEquationParameter fitration_time,
            IEquationParameter Qft)
            : base(
                porosity,
                suspension_solids_volume_fraction,
                cake_mass,
                filter_area,
                mother_liquid_density,
                solids_density,
                fitration_time,
                Qft)
        {
            eps = porosity;
            cv = suspension_solids_volume_fraction;
            mc = cake_mass;
            A = filter_area;
            rho = mother_liquid_density;
            rhos = solids_density;
            tf = fitration_time;
            qft = Qft;
        }

        protected override void InitFormulas()
        {
            AddFormula(eps, PorosityFormula);
        }

        #region Formulas

        private void PorosityFormula()
        {
            eps.Value = ((1 - cv.Value) * mc.Value / (A.Value * rhos.Value * cv.Value * tf.Value * qft.Value) - 1) / (mc.Value / (A.Value * rhos.Value * cv.Value * tf.Value * qft.Value) + rho.Value / rhos.Value - 1);
        }

        #endregion
    }
}