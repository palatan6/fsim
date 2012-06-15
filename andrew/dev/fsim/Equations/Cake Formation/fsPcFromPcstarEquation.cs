using Parameters;

namespace Equations
{
    public class fsPcFromPcstarEquation : fsCalculatorEquation
    {
        #region Parameters

        private readonly IEquationParameter pc;
        private readonly IEquationParameter hc;
        private readonly IEquationParameter hce0;
        private readonly IEquationParameter pcstar;

        #endregion

        public fsPcFromPcstarEquation(
            IEquationParameter Pc,
            IEquationParameter Hc,
            IEquationParameter Hce0,
            IEquationParameter Pcstar)
            : base(
                Pc,
                Hc,
                Hce0,
                Pcstar)
        {
            pc = Pc;
            hc = Hc;
            hce0 = Hce0;
            pcstar = Pcstar;
        }

        protected override void InitFormulas()
        {
            AddFormula(pc, PcFormula);
            AddFormula(pcstar, StarFormula);
        }

        #region Formulas

        private void StarFormula()
        {
            pcstar.Value = hc.Value * pc.Value / (hc.Value + 2 * hce0.Value);
        }

        private void PcFormula()
        {
            pc.Value = ((hc.Value + 2 * hce0.Value) * pcstar.Value) / hc.Value;
        }

        #endregion
    }
}
