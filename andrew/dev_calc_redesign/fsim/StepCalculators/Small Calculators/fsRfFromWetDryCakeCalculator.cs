using System.Collections.Generic;
using System.ComponentModel;
using Equations;
using Parameters;
using Value;

namespace StepCalculators
{
//     public class fsRfFromWetDryCakeCalculator : fsCalculatorEquationsList
//     {
//         readonly fsCalculatorConstant m_wetMass;
//         readonly fsCalculatorConstant m_dryMass;
//         readonly fsCalculatorConstant m_solidsMassFraction;
//         readonly fsCalculatorConstant m_solidsConcentration;
//         readonly fsCalculatorConstant m_liquidDensity;
//         readonly fsCalculatorVariable m_cakeMoistureContent;
//         readonly fsCalculatorVariable m_internalC;
// 
//         public fsRfFromWetDryCakeCalculator()
//         {
//             #region Parameters Initialization
// 
//             m_wetMass = calculator.AddVariable(fsParameterIdentifier.WetCakeMass);
//             m_dryMass = calculator.AddVariable(fsParameterIdentifier.DryCakeMass);
//             m_solidsMassFraction = calculator.AddVariable(fsParameterIdentifier.SolutesMassFractionInLiquid);
//             m_solidsConcentration = calculator.AddVariable(fsParameterIdentifier.SolutesConcentrationInCakeLiquid);
//             m_liquidDensity = calculator.AddVariable(fsParameterIdentifier.LiquidDensity);
//             m_cakeMoistureContent = calculator.AddVariable(fsParameterIdentifier.CakeMoistureContentRf);
//             m_internalC = calculator.AddVariable(new fsParameterIdentifier("internalC"));
// 
//             #endregion
// 
//             Equations = null;
//         }
// 
//         public fsCalculationOptions.fsSaltContentOption SaltContentOption;
//         public fsCalculationOptions.fsConcentrationOption ConcentrationOption;
// 
//         public void RebuildEquationsList()
//         {
//             Equations = new List<fsCalculatorEquation>();
// 
//             if (SaltContentOption == fsCalculationOptions.fsSaltContentOption.Neglected)
//             {
//                 m_internalC.Value = fsValue.Zero;
//                 m_internalC.IsInput = true;
//                 m_internalC.IsProcessed = true;
//                 calculator.AddEquation(new fsMoistureContentEquation(m_cakeMoistureContent, m_dryMass, m_wetMass, m_internalC));
//             }
//             else
//             {
//                 if (ConcentrationOption == fsCalculationOptions.fsConcentrationOption.SolidsMassFraction)
//                 {
//                     calculator.AddEquation(new fsMoistureContentEquation(m_cakeMoistureContent, m_dryMass, m_wetMass, m_solidsMassFraction));
//                 }
//                 else
//                 {
//                     m_internalC.IsInput = false;
//                     calculator.AddEquation(new fsProductEquation(m_solidsConcentration, m_internalC, m_liquidDensity));
//                     calculator.AddEquation(new fsMoistureContentEquation(m_cakeMoistureContent, m_dryMass, m_wetMass, m_internalC));
//                 }
//             }
//         }
//     }
}
