using Parameters;
using Value;

namespace Equations
{
    public class fsConstantSumEquation : fsCalculatorEquation
    {
        #region Parameters

        private readonly double m_const;
        private readonly IEquationParameter[] m_elements;

        #endregion

        // const = a + b + c + d + ... (any amount of parameters)

        public fsConstantSumEquation(
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
            fsValue sum = fsValue.Zero;
            foreach (IEquationParameter equationParameter in m_elements)
            {
                if (equationParameter != result)
                {
                    sum = sum + equationParameter.Value;
                }
            }
            result.Value = m_const - sum;
            return true;
        }
    }
}