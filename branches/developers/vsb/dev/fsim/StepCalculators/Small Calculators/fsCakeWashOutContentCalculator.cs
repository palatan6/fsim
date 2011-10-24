﻿using System.Collections.Generic;
using System.ComponentModel;
using Equations;
using Parameters;
using Value;

namespace StepCalculators
{
    public class fsCakeWashOutContentCalculator : fsCalculator
    {
        readonly fsCalculatorConstant m_wetMass;
        readonly fsCalculatorConstant m_dryMass;
        readonly fsCalculatorConstant m_liquidMass;
        readonly fsCalculatorConstant m_solidsMassFraction;
        readonly fsCalculatorConstant m_solidsMassConcentration;
        readonly fsCalculatorVariable m_internalC;
        readonly fsCalculatorConstant m_liquidDensity;
        readonly fsCalculatorVariable m_cakeMoistureContent;
        readonly fsCalculatorConstant m_pH;
        readonly fsCalculatorVariable m_pHcake;
        readonly fsCalculatorVariable m_cakeWashOutContent;
        
        public fsCakeWashOutContentCalculator()
        {
            #region Parameters Initialization

            m_wetMass = AddConstant(fsParameterIdentifier.WetCakeMass);
            m_dryMass = AddConstant(fsParameterIdentifier.DryCakeMass);
            m_liquidMass = AddConstant(fsParameterIdentifier.LiquidMass);
            m_solidsMassFraction = AddConstant(fsParameterIdentifier.SolidsMassFraction);
            m_solidsMassConcentration = AddConstant(fsParameterIdentifier.SolidsConcentration);
            m_internalC = AddVariable(new fsParameterIdentifier("internalC"));
            m_liquidDensity = AddConstant(fsParameterIdentifier.LiquidDensity);
            m_cakeMoistureContent = AddVariable(fsParameterIdentifier.CakeMoistureContent);
            m_pH = AddConstant(fsParameterIdentifier.pH);
            m_pHcake = AddVariable(fsParameterIdentifier.pHcake);
            m_cakeWashOutContent = AddVariable(fsParameterIdentifier.CakeWashOutContent);

            #endregion

            Equations = null;
        }

        public enum fsFromCalculationOption
        {
            [Description("Wash Out Concentration")]
            WashOutConcentration,
            [Description("pH")]
            Ph
        }
        public fsFromCalculationOption FromCalculationOption;

        public enum fsWashOutContentOption
        {
            [Description("As Mass Fraction")]
            AsMassFraction,
            [Description("As Concentration")]
            AsConcentration
        }
        public fsWashOutContentOption WashOutContentOption;

        public void RebuildEquationsList()
        {
            Equations = new List<fsCalculatorEquation>();

            var zero = new fsCalculatorConstant(new fsParameterIdentifier("zero"))
            {
                IsInput = true,
                Value = fsValue.Zero
            };
            AddEquation(new fsMoistureContentEquation(m_cakeMoistureContent, m_dryMass, m_wetMass, zero));

            if (FromCalculationOption == fsFromCalculationOption.WashOutConcentration)
            {
                if (WashOutContentOption == fsWashOutContentOption.AsMassFraction)
                {
                    AddEquation(new fsCakeWashOutContentEquation(m_cakeWashOutContent, m_dryMass, m_wetMass, m_liquidMass, m_solidsMassFraction));
                }
                else
                {
                    m_internalC.IsInput = false;
                    AddEquation(new fsProductEquation(m_solidsMassConcentration, m_internalC, m_liquidDensity));
                    AddEquation(new fsCakeWashOutContentEquation(m_cakeWashOutContent, m_dryMass, m_wetMass, m_liquidMass, m_internalC));
                }
            }
            else
            {
                m_internalC.IsInput = false;
                AddEquation(new fsPhCakeEquation(m_pHcake, m_pH, m_wetMass, m_dryMass, m_liquidMass));
                AddEquation(new fsConcentrationFromPhEquation(m_internalC, m_pH, m_liquidDensity));
                AddEquation(new fsCakeWashOutContentEquation(m_cakeWashOutContent, m_dryMass, m_wetMass, m_liquidMass, m_internalC));
            }
        }
    }
}
