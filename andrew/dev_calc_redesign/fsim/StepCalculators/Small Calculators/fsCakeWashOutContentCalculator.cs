using System.Collections.Generic;
using System.ComponentModel;
using Equations;
using Parameters;
using Value;

namespace StepCalculators
{
//     public class fsCakeWashOutContentCalculator : fsCalculatorEquationsList
//     {
//         readonly fsCalculatorConstant m_wetMass;
//         readonly fsCalculatorConstant m_dryMass;
//         readonly fsCalculatorConstant m_liquidMassForResuspension;
//         readonly fsCalculatorConstant m_solidsMassFraction;
//         readonly fsCalculatorConstant m_solidsConcentration;
//         readonly fsCalculatorVariable m_internalC;
//         readonly fsCalculatorConstant m_liquidDensity;
//         readonly fsCalculatorVariable m_cakeMoistureContent;
//         readonly fsCalculatorConstant m_pH;
//         readonly fsCalculatorVariable m_pHcake;
//         readonly fsCalculatorVariable m_cakeWashOutContent;
//         
//         public fsCakeWashOutContentCalculator()
//         {
//             #region Parameters Initialization
// 
//             m_wetMass = calculator.AddVariable(fsParameterIdentifier.WetCakeMass);
//             m_dryMass = calculator.AddVariable(fsParameterIdentifier.DryCakeMass);
//             m_liquidMassForResuspension = calculator.AddVariable(fsParameterIdentifier.LiquidMassForResuspension);
//             m_solidsMassFraction = calculator.AddVariable(fsParameterIdentifier.LiquidWashOutMassFraction);
//             m_solidsConcentration = calculator.AddVariable(fsParameterIdentifier.LiquidWashOutConcentration);
//             m_internalC = calculator.AddVariable(new fsParameterIdentifier("internalC"));
//             m_liquidDensity = calculator.AddVariable(fsParameterIdentifier.LiquidDensity);
//             m_cakeMoistureContent = calculator.AddVariable(fsParameterIdentifier.CakeMoistureContentRf);
//             m_pH = calculator.AddVariable(fsParameterIdentifier.Ph);
//             m_pHcake = calculator.AddVariable(fsParameterIdentifier.PHcake);
//             m_cakeWashOutContent = calculator.AddVariable(fsParameterIdentifier.CakeWashOutContent);
// 
//             #endregion
// 
//             Equations = null;
//         }
// 
//         public fsCalculationOptions.fsFromCalculationOption FromCalculationOption;
// 
//         public fsCalculationOptions.fsWashOutContentOption WashOutContentOption;
// 
//         public void RebuildEquationsList()
//         {
//             Equations = new List<fsCalculatorEquation>();
// 
//             var zero = new fsCalculatorConstant(new fsParameterIdentifier("zero"))
//             {
//                 IsInput = true,
//                 Value = fsValue.Zero
//             };
//             calculator.AddEquation(new fsMoistureContentEquation(m_cakeMoistureContent, m_dryMass, m_wetMass, zero));
// 
//             if (FromCalculationOption == fsCalculationOptions.fsFromCalculationOption.WashOutContent)
//             {
//                 if (WashOutContentOption == fsCalculationOptions.fsWashOutContentOption.AsMassFraction)
//                 {
//                     calculator.AddEquation(new fsCakeWashOutContentEquation(m_cakeWashOutContent, m_dryMass, m_wetMass, m_liquidMassForResuspension, m_solidsMassFraction));
//                 }
//                 else
//                 {
//                     m_internalC.IsInput = false;
//                     calculator.AddEquation(new fsProductEquation(m_solidsConcentration, m_internalC, m_liquidDensity));
//                     calculator.AddEquation(new fsCakeWashOutContentEquation(m_cakeWashOutContent, m_dryMass, m_wetMass, m_liquidMassForResuspension, m_internalC));
//                 }
//             }
//             else
//             {
//                 m_internalC.IsInput = false;
//                 calculator.AddEquation(new fsPhCakeEquation(m_pHcake, m_pH, m_wetMass, m_dryMass, m_liquidMassForResuspension));
//                 calculator.AddEquation(new fsConcentrationFromPhEquation(m_internalC, m_pH, m_liquidDensity));
//                 calculator.AddEquation(new fsCakeWashOutContentEquation(m_cakeWashOutContent, m_dryMass, m_wetMass, m_liquidMassForResuspension, m_internalC));
//             }
//         }
//     }
}
