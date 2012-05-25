using Parameters;
using Value;

namespace Equations
{
    public class fsConstantProductEquation : fsCalculatorEquation
    {
        #region Parameters

        private readonly double m_const;
        private readonly IEquationParameter[] m_elements;

        #endregion

        // const = a * b * c * d * ... (any amount of parameters)

        public fsConstantProductEquation(
            double constValue,
            params IEquationParameter[] elements)
            : base(elements)
        {
            m_const = constValue;
            m_elements = elements;
        }

        protected override void InitFormulas()
        {
            // no specific formulas in this class
        }

        protected override bool Calculate(IEquationParameter result)
        {
            fsValue product = fsValue.One;
            foreach (IEquationParameter equationParameter in m_elements)
            {
                if (equationParameter != result)
                {
                    product = product * equationParameter.Value;
                }
            }
            result.Value = m_const / product;
            return true;
        }
    }
}