﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Value;

namespace Parameters
{
    public class fsSimulationParameter : fsCalculatorConstant
    {
        private bool m_isInput;
        public bool isInput
        {
            get { return m_isInput; }
            set { m_isInput = value; }
        }

        public fsSimulationParameter(fsParameterIdentifier identifier)
            : base(identifier)
        {
            m_isInput = false;
        }

        public fsSimulationParameter(fsParameterIdentifier identifier, bool isInput)
            : base(identifier)
        {
            m_isInput = isInput;
        }

        public fsSimulationParameter(fsParameterIdentifier identifier, fsValue value)
            : base(identifier, value)
        {
            m_isInput = false;
        }

        public fsSimulationParameter(fsParameterIdentifier identifier, bool isInput, fsValue value)
            : base(identifier, value)
        {
            m_isInput = isInput;
        }

        public override string ToString()
        {
            return Identifier.Name + "(" + (m_isInput ? "Inputed" : "Calculated") + ")" + " = " + Value.ToString();
        }
    }
}