using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Parameters;
using Equations;
using Value;

namespace StepCalculators
{
//     public class fsSuspensionSolidsMassFractionCalculator : fsCalculatorEquationsList
//     {
//         readonly fsCalculatorConstant m_suspensionMass;
//         readonly fsCalculatorConstant m_dryMass;
//         readonly fsCalculatorConstant m_Cfm;
//         readonly fsCalculatorConstant m_Cf;
//         readonly fsCalculatorConstant m_motherLiquidDensity;
//         readonly fsCalculatorVariable m_Cm;
//         readonly fsCalculatorVariable m_internalC;
// 
//         public fsSuspensionSolidsMassFractionCalculator()
//         {
//             #region Parameters Initialization
// 
//             m_suspensionMass = calculator.AddVariable(fsParameterIdentifier.SuspensionMass);
//             m_dryMass = calculator.AddVariable(fsParameterIdentifier.DryCakeMass);
//             m_Cfm = calculator.AddVariable(fsParameterIdentifier.SolutesMassFractionInMotherLiquid);
//             m_Cf = calculator.AddVariable(fsParameterIdentifier.SolutesConcentrationInMotherLiquid);
//             m_motherLiquidDensity = calculator.AddVariable(fsParameterIdentifier.MotherLiquidDensity);
//             m_Cm = calculator.AddVariable(fsParameterIdentifier.SuspensionSolidsMassFraction);
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
//                 calculator.AddEquation(new fsSuspensionSolidsMassFractionEquation(m_Cm, m_dryMass, m_suspensionMass, m_internalC));
//             }
//             else
//             {
//                 if (ConcentrationOption == fsCalculationOptions.fsConcentrationOption.SolidsMassFraction)
//                 {
//                     calculator.AddEquation(new fsSuspensionSolidsMassFractionEquation(m_Cm, m_dryMass, m_suspensionMass, m_Cfm));
//                 }
//                 else
//                 {
//                     m_internalC.IsInput = false;
//                     calculator.AddEquation(new fsProductEquation(m_Cf, m_internalC, m_motherLiquidDensity));
//                     calculator.AddEquation(new fsSuspensionSolidsMassFractionEquation(m_Cm, m_dryMass, m_suspensionMass, m_internalC));
//                 }
//             }
//         }
//     }
}
