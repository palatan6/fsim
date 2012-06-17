using Parameters;

namespace Equations
{
    public class fsEpsFromMsAndQfEquation : fsCalculatorEquation
    {
        #region Parameters

        private readonly IEquationParameter eps;
        private readonly IEquationParameter cv;
        private readonly IEquationParameter ms;
        private readonly IEquationParameter A;
        private readonly IEquationParameter rhos;
        private readonly IEquationParameter tf;
        private readonly IEquationParameter qft;

        //eps = 1 - cv * (1 + 1 / (ms / (A * rhos * cv * tf * qft) - 1));

        #endregion

        public fsEpsFromMsAndQfEquation(
            IEquationParameter porosity,
            IEquationParameter suspension_solids_volume_fraction,
            IEquationParameter solids_mass,
            IEquationParameter filtration_area,
            IEquationParameter solids_density,
            IEquationParameter fitration_time,
            IEquationParameter specific_volume_flowrate)
            : base(
                porosity,
                suspension_solids_volume_fraction,
                solids_mass,
                filtration_area,
                solids_density,
                fitration_time,
                specific_volume_flowrate)
        {
            eps = porosity;
            cv = suspension_solids_volume_fraction;
            ms = solids_mass;
            A = filtration_area;
            rhos = solids_density;
            tf = fitration_time;
            qft = specific_volume_flowrate;
        }

        protected override void InitFormulas()
        {
            AddFormula(eps, PorosityFormula);
        }

        #region Formulas

        private void PorosityFormula()
        {
            eps.Value = 1 - cv.Value * (1 + 1 / (ms.Value / (A.Value * rhos.Value * cv.Value * tf.Value * qft.Value) - 1));            
        }

        #endregion
    }
}