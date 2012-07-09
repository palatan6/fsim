using Parameters;

namespace Equations
{
    public class fsProductEquation : fsProductsEquation
    {
        // product = firstFactor * secondFactor

        public fsProductEquation(
            IEquationParameter product,
            IEquationParameter firstFactor,
            IEquationParameter secondFactor)
            : base(new IEquationParameter[] {product}, new IEquationParameter[] {firstFactor, secondFactor})
        {
        }
    }
}