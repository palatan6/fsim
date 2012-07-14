using System.Collections.Generic;
using System.ComponentModel;
using Equations;
using Parameters;
using Value;

namespace StepCalculators
{
     public class fsCakeWashOutContentCalculator : fsCalculatorEquationsList
     {
         fsCalculatorVariable m_wetMass;
         fsCalculatorVariable m_dryMass;
         fsCalculatorVariable m_liquidMassForResuspension;
         fsCalculatorVariable m_solidsMassFraction;
         fsCalculatorVariable m_solidsConcentration;
         fsCalculatorVariable m_internalC;
         fsCalculatorVariable m_liquidDensity;
         fsCalculatorVariable m_cakeMoistureContent;
         fsCalculatorVariable m_pH;
         fsCalculatorVariable m_pHcake;
         fsCalculatorVariable m_cakeWashOutContent;

         public override void AddToCalculator(fsCalculator calculator)
         {
             #region Parameters Initialization
 
             m_wetMass = calculator.AddVariable(fsParameterIdentifier.WetCakeMass);
             m_dryMass = calculator.AddVariable(fsParameterIdentifier.DryCakeMass);
             m_liquidMassForResuspension = calculator.AddVariable(fsParameterIdentifier.LiquidMassForResuspension);
             m_solidsMassFraction = calculator.AddVariable(fsParameterIdentifier.LiquidWashOutMassFraction);
             m_solidsConcentration = calculator.AddVariable(fsParameterIdentifier.LiquidWashOutConcentration);
             m_internalC = calculator.AddVariable(new fsParameterIdentifier("internalC"));
             m_liquidDensity = calculator.AddVariable(fsParameterIdentifier.LiquidDensity);
             m_cakeMoistureContent = calculator.AddVariable(fsParameterIdentifier.CakeMoistureContentRf);
             m_pH = calculator.AddVariable(fsParameterIdentifier.Ph);
             m_pHcake = calculator.AddVariable(fsParameterIdentifier.PHcake);
             m_cakeWashOutContent = calculator.AddVariable(fsParameterIdentifier.CakeWashOutContent);
 
             #endregion
 
             calculator.Equations = null;
         }
 
         public fsCalculationOptions.fsFromCalculationOption FromCalculationOption;
 
         public fsCalculationOptions.fsWashOutContentOption WashOutContentOption;

         public void RebuildEquationsList(fsCalculator calculator)
         {
             calculator.Equations = new List<fsCalculatorEquation>();
 
             var zero = new fsCalculatorConstant(new fsParameterIdentifier("zero"))
             {
                 IsInput = true,
                 Value = fsValue.Zero
             };
             calculator.AddEquation(new fsMoistureContentEquation(m_cakeMoistureContent, m_dryMass, m_wetMass, zero));
 
             if (FromCalculationOption == fsCalculationOptions.fsFromCalculationOption.WashOutContent)
             {
                 if (WashOutContentOption == fsCalculationOptions.fsWashOutContentOption.AsMassFraction)
                 {
                     calculator.AddEquation(new fsCakeWashOutContentEquation(m_cakeWashOutContent, m_dryMass, m_wetMass, m_liquidMassForResuspension, m_solidsMassFraction));
                 }
                 else
                 {
                     m_internalC.IsInput = false;
                     calculator.AddEquation(new fsProductEquation(m_solidsConcentration, m_internalC, m_liquidDensity));
                     calculator.AddEquation(new fsCakeWashOutContentEquation(m_cakeWashOutContent, m_dryMass, m_wetMass, m_liquidMassForResuspension, m_internalC));
                 }
             }
             else
             {
                 m_internalC.IsInput = false;
                 calculator.AddEquation(new fsPhCakeEquation(m_pHcake, m_pH, m_wetMass, m_dryMass, m_liquidMassForResuspension));
                 calculator.AddEquation(new fsConcentrationFromPhEquation(m_internalC, m_pH, m_liquidDensity));
                 calculator.AddEquation(new fsCakeWashOutContentEquation(m_cakeWashOutContent, m_dryMass, m_wetMass, m_liquidMassForResuspension, m_internalC));
             }
         }
     }
}
