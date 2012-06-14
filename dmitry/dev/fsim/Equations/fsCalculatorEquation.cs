using System.Collections.Generic;
using Parameters;

namespace Equations
{
    public abstract class fsCalculatorEquation
    {
        #region Parameters

        private readonly List<IEquationParameter> m_parameters = new List<IEquationParameter>();

        #endregion

        #region Formulas

        private List<KeyValuePair<IEquationParameter, fsFormula>> m_formulas;

        protected void AddFormula(IEquationParameter result, fsFormula formula)
        {
            m_formulas.Add(new KeyValuePair<IEquationParameter, fsFormula>(result, formula));
        }

        protected abstract void InitFormulas();

        protected delegate void fsFormula();

        #endregion

        #region Constructors

        protected fsCalculatorEquation(
            params IEquationParameter[] parameters)
        {
            foreach (IEquationParameter p in parameters)
            {
                m_parameters.Add(p);
            }
        }

        protected fsCalculatorEquation(
            IEnumerable<IEquationParameter> leftPart,
            IEnumerable<IEquationParameter> rightPart)
        {
            foreach (IEquationParameter p in leftPart)
            {
                m_parameters.Add(p);
            }
            foreach (IEquationParameter p in rightPart)
            {
                m_parameters.Add(p);
            }
        }

        #endregion

        #region Calculate

        public bool Calculate()
        {
            IEquationParameter result = null;
            foreach (IEquationParameter p in m_parameters)
            {
                if (p == result)  // Sometimes m_parameters may have duplications.
                {                 // For example in product equations like x * x = a
                    continue;
                }
                if (p.IsProcessed == false)
                {
                    if (result == null)
                    {
                        result = p;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            if (result == null)
                return false;
            if (Calculate(result))
            {
                result.IsProcessed = true;
                return true;
            }
            return false;
        }

        protected virtual bool Calculate(IEquationParameter result)
        {
            if (m_formulas == null)
            {
                m_formulas = new List<KeyValuePair<IEquationParameter, fsFormula>>();
                InitFormulas();
            }
            foreach (var f in m_formulas)
            {
                if (f.Key == result)
                {
                    f.Value();  // run formula that calculates key == result
                    return true;
                }
            }
            return false;
        }

        #endregion
    }
}