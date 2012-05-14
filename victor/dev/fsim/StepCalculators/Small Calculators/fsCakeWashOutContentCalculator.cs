using System.Collections.Generic;
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
        readonly fsCalculatorConstant m_liquidMassForResuspension;
        readonly fsCalculatorConstant m_solidsMassFraction;
        readonly fsCalculatorConstant m_solidsConcentration;
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
            m_liquidMassForResuspension = AddConstant(fsParameterIdentifier.LiquidMassForResuspension);
            m_solidsMassFraction = AddConstant(fsParameterIdentifier.LiquidWashOutMassFraction);
            m_solidsConcentration = AddConstant(fsParameterIdentifier.LiquidWashOutConcentration);
            m_internalC = AddVariable(new fsParameterIdentifier("internalC"));
            m_liquidDensity = AddConstant(fsParameterIdentifier.LiquidDensity);
            m_cakeMoistureContent = AddVariable(fsParameterIdentifier.CakeMoistureContent);
            m_pH = AddConstant(fsParameterIdentifier.Ph);
            m_pHcake = AddVariable(fsParameterIdentifier.PHcake);
            m_cakeWashOutContent = AddVariable(fsParameterIdentifier.CakeWashOutContent);

            #endregion

            Equations = null;
        }

        public fsCalculationOptions.fsFromCalculationOption FromCalculationOption;

        public fsCalculationOptions.fsWashOutContentOption WashOutContentOption;

        public void RebuildEquationsList()
        {
            Equations = new List<fsCalculatorEquation>();

            var zero = new fsCalculatorConstant(new fsParameterIdentifier("zero"))
            {
                IsInput = true,
                Value = fsValue.Zero
            };
            AddEquation(new fsMoistureContentEquation(m_cakeMoistureContent, m_dryMass, m_wetMass, zero));

            if (FromCalculationOption == fsCalculationOptions.fsFromCalculationOption.WashOutContent)
            {
                if (WashOutContentOption == fsCalculationOptions.fsWashOutContentOption.AsMassFraction)
                {
                    AddEquation(new fsCakeWashOutContentEquation(m_cakeWashOutContent, m_dryMass, m_wetMass, m_liquidMassForResuspension, m_solidsMassFraction));
                }
                else
                {
                    m_internalC.IsInput = false;
                    AddEquation(new fsProductEquation(m_solidsConcentration, m_internalC, m_liquidDensity));
                    AddEquation(new fsCakeWashOutContentEquation(m_cakeWashOutContent, m_dryMass, m_wetMass, m_liquidMassForResuspension, m_internalC));
                }
            }
            else
            {
                m_internalC.IsInput = false;
                AddEquation(new fsPhCakeEquation(m_pHcake, m_pH, m_wetMass, m_dryMass, m_liquidMassForResuspension));
                AddEquation(new fsConcentrationFromPhEquation(m_internalC, m_pH, m_liquidDensity));
                AddEquation(new fsCakeWashOutContentEquation(m_cakeWashOutContent, m_dryMass, m_wetMass, m_liquidMassForResuspension, m_internalC));
            }
        }
    }
}
