using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            result.Value = fsValue.Pow(rightProduct / leftProduct, 1.0 / resultDegreeCount);
            return true;
        }

        private int Count(IEnumerable<IEquationParameter> parametersList, IEquationParameter parameter)
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
