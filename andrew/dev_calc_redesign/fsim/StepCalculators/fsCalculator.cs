using System.Collections.Generic;
using System.Linq;
using Parameters;
using Equations;
using UpdateHandler;
using Value;
using System;

namespace StepCalculators
{
    /*
     * fsStepCalculator is a base abstract class for all calculators
     * it consists of three main components
     *     m_constants:     values that calculated before and used in current calculator like constants
     *     m_parameters:    input by user/calculated or just calculated parameters
     *     Equations:     set of equations that can be used for calculating values from m_parameters
     *     
     * In derivative classes user must override 
     *     InitParametersAndConstants():    here he must initialize all constants and parameters
     *     InitEquations():                 here he must add all corresponding equations
     *     
     * Then calculator can be used with public methods
     * 
     * */
    public class fsCalculator
    {
        private readonly Dictionary<fsParameterIdentifier, fsCalculatorVariable> m_variables = new Dictionary<fsParameterIdentifier, fsCalculatorVariable>();
        private readonly Dictionary<fsParameterIdentifier, fsCalculatorConstant> m_constants = new Dictionary<fsParameterIdentifier, fsCalculatorConstant>();
        public List<fsCalculatorEquation> Equations = new List<fsCalculatorEquation>();
        private fsCalculatorUpdateHandler m_updateHandler;

        public void Calculate()
        {
            foreach (var p in m_variables.Values)
            {
                p.IsProcessed = p.IsInput;
                if (p.IsProcessed == false)
                {
                    p.Value = new fsValue();
                }
            }

            #region Update Handler

            int startCount = m_variables.Values.Sum(p => p.IsProcessed ? 1 : 0);
            int currentCount = startCount;
            if (m_updateHandler != null)
            {
                m_updateHandler.SetProgress(0);
            }

            #endregion

            bool somethingChanged = true;
            while (somethingChanged)
            {
                somethingChanged = false;
                foreach (var equation in Equations)
                    if (equation.Calculate())
                    {
                        somethingChanged = true;

                        #region Update Handler

                        ++currentCount;
                        if (m_updateHandler != null)
                        {
                            m_updateHandler.SetProgress((double)(currentCount - startCount) / (m_variables.Count - startCount));
                        }

                        #endregion
                    }
            }

            #region Update Handler

            if (m_updateHandler != null)
            {
                m_updateHandler.SetProgress(1);
            }

            #endregion
        }

        public void AddEquations(fsCalculatorEquationsList equationsList)
        {
            equationsList.AddToCalculator(this);
        }

        #region Service Methods For Adding Parameters And Equations

        public fsCalculatorVariable AddVariable(fsParameterIdentifier identifier)
        {
            if (!m_variables.ContainsKey(identifier))
            {
                m_variables.Add(identifier, new fsCalculatorVariable(identifier));
            }
            return m_variables[identifier];
        }
      
        public void AddEquation(fsCalculatorEquation equation)
        {
            Equations.Add(equation);
        }

        #endregion

        #region Update Handler Issues

        public void SetUpdateHandler(fsCalculatorUpdateHandler updateHandler)
        {
            m_updateHandler = updateHandler;
        }

        public int GetToCalculateAmount()
        {
            return m_variables.Values.Sum(v => v.IsInput ? 0 : 1);
        }

        #endregion

        public string GetStatusMessage()
        {
            string message = m_variables.Values.Where(p => !p.IsInput && !p.IsProcessed).
                Aggregate("", (current, p) => current + ("     - Unable to calculate " + p.Identifier.Name + "\n"));
            if (message.Length == 0)
            {
                message = " + All parameters was calculated successfully.\n";
            }
            else
            {
                message = " - Some parameters impossible to calculate:\n" + message;
            }
            return message;
        }

        #region Get/Set Values Data

        public void CopyValuesToStorage(Dictionary<fsParameterIdentifier, fsCalculatorParameter> target)
        {
            foreach (fsParameterIdentifier p in target.Keys)
            {
                if (m_variables.ContainsKey(p))
                {
                    target[p].Value = m_variables[p].Value;
                }
            }
        }

        public void ReadDataFromStorage(Dictionary<fsParameterIdentifier, fsCalculatorParameter> source)
        {
            foreach (fsParameterIdentifier p in source.Keys)
            {
                if (m_variables.ContainsKey(p))
                {
                    m_variables[p].Value = source[p].Value;
                    m_variables[p].IsInput = source[p].IsInput;
                }
                if (m_constants.ContainsKey(p))
                {
                    m_constants[p].Value = source[p].Value;
                }
            }
        }

        #endregion
    }
}
