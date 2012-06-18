using System.Collections.Generic;
using Parameters;
using Value;

namespace Equations
{
    public class fsProductsEquation : fsCalculatorEquation
    {
        // This class can calculate any parameter from equation of the next kind:
        //
        // L1 * L2 * ... * Ln  ==  R1 * R2 * ... * Rm;
        // 

        #region Parameters

        private readonly IEnumerable<IEquationParameter> m_leftElements;
        private readonly IEnumerable<IEquationParameter> m_rightElements;

        #endregion

        public fsProductsEquation(
            IEnumerable<IEquationParameter> leftElements,
            IEnumerable<IEquationParameter> rightElements)
            : base(leftElements, rightElements)
        {
            m_leftElements = leftElements;
            m_rightElements = rightElements;
        }

        protected override void InitFormulas()
        {
            // no specific formulas in this class
        }

        protected override bool Calculate(IEquationParameter result)
        {
            fsValue leftProduct = GetElementsProduct(m_leftElements, result);
            fsValue rightProduct = GetElementsProduct(m_rightElements, result);
            int resultDegreeCount = Count(m_leftElements, result) - Count(m_rightElements, result);
            if (resultDegreeCount == 0)
            {
                return false;
            }

            // Let's make r^d * a = b, where d > 0
            int d = resultDegreeCount;
            fsValue a = leftProduct;
            fsValue b = rightProduct;
            if (resultDegreeCount < 0)
            {
                a = rightProduct;
                b = leftProduct;
                d = -resultDegreeCount;
            }
            fsValue bOverA = b / a;
            int sign = 1;
            if (bOverA.Value < 0 && d % 2 == 1)
            {
                bOverA = -bOverA;
                sign = -1;
            }
            result.Value = bOverA == fsValue.Zero
                ? fsValue.Zero
                : sign * fsValue.Pow(bOverA, 1.0 / d);
            return true;
        }

        private static int Count(IEnumerable<IEquationParameter> parametersList, IEquationParameter parameter)
        {
            int result = 0;
            foreach (IEquationParameter p in parametersList)
            {
                result += (p == parameter ? 1 : 0);
            }
            return result;
        }

        private static fsValue GetElementsProduct(
            IEnumerable<IEquationParameter> elements,
            IEquationParameter result)
        {
            fsValue product = fsValue.One;
            foreach (IEquationParameter equationParameter in elements)
            {
                if (equationParameter != result)
                {
                    product = product * equationParameter.Value;
                }
            }
            return product;
        }
    }
}
