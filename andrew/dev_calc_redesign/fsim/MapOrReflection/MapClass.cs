using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Parameters;

namespace MapOrReflection
{
    public class MapClass
    {
        Dictionary<fsParameterIdentifier, fsCalculatorVariable> m_variables = new Dictionary<fsParameterIdentifier, fsCalculatorVariable>();
        readonly fsCalculatorVariable m_rhoS;
        readonly fsCalculatorVariable m_eps;
        readonly fsCalculatorVariable m_pc0;
        readonly fsCalculatorVariable m_rc0;
        readonly fsCalculatorVariable m_alpha0;
        readonly fsCalculatorVariable m_nc;
        readonly fsCalculatorVariable m_pressure;
        readonly fsCalculatorVariable m_pc;
        readonly fsCalculatorVariable m_rc;
        readonly fsCalculatorVariable m_alpha;

        public MapClass()
        {
            m_rhoS = AddVariable(fsParameterIdentifier.SolidsDensity);
            m_eps = AddVariable(fsParameterIdentifier.CakePorosity);
            m_pc0 = AddVariable(fsParameterIdentifier.CakePermeability0);
            m_rc0 = AddVariable(fsParameterIdentifier.CakeResistance0);
            m_alpha0 = AddVariable(fsParameterIdentifier.CakeResistanceAlpha0);
            m_nc = AddVariable(fsParameterIdentifier.CakeCompressibility);
            m_pressure = AddVariable(fsParameterIdentifier.PressureDifference);
            m_pc = AddVariable(fsParameterIdentifier.CakePermeability);
            m_rc = AddVariable(fsParameterIdentifier.CakeResistance);
            m_alpha = AddVariable(fsParameterIdentifier.CakeResistanceAlpha);
        }

        protected fsCalculatorVariable AddVariable(fsParameterIdentifier identifier)
        {
            var p = new fsCalculatorVariable(identifier);
            m_variables[identifier] = p;
            return p;
        }

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
            }
        }
    }
}
