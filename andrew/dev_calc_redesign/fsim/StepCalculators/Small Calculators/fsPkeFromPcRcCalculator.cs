using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Parameters;
using Equations;

namespace StepCalculators
{
//     public class fsPkeFromPcRcCalculator : fsCalculatorEquationsList
//     {
//         readonly fsCalculatorVariable m_pc;
//         readonly fsCalculatorVariable m_rc;
//         readonly fsCalculatorVariable m_alpha;
//         readonly fsCalculatorConstant m_rhos;
//         readonly fsCalculatorConstant m_eps;
//         readonly fsCalculatorConstant m_rhosBulk;
//         readonly fsCalculatorConstant m_sigma;
//         readonly fsCalculatorConstant m_pkeSt;
//         readonly fsCalculatorVariable m_pke;
//         readonly fsCalculatorVariable m_localPc;
//         readonly fsCalculatorVariable m_localBulk;
//         readonly fsCalculatorVariable m_localOneMinusEps;
// 
//         public fsPkeFromPcRcCalculator()
//         {
//             #region Parameters Initialization
// 
//             m_pc = calculator.AddVariable(fsParameterIdentifier.CakePermeability);
//             m_rc = calculator.AddVariable(fsParameterIdentifier.CakeResistance);
//             m_alpha = calculator.AddVariable(fsParameterIdentifier.CakeResistanceAlpha);
//             m_rhos = calculator.AddVariable(fsParameterIdentifier.SolidsDensity);
//             m_eps = calculator.AddVariable(fsParameterIdentifier.CakePorosity);
//             m_rhosBulk = calculator.AddVariable(fsParameterIdentifier.DryCakeDensity);
//             m_sigma = calculator.AddVariable(fsParameterIdentifier.SurfaceTensionLiquidInCake);
//             m_pkeSt = calculator.AddVariable(fsParameterIdentifier.StandardCapillaryPressure);
//             m_pke = calculator.AddVariable(fsParameterIdentifier.CapillaryPressure);
//             m_localPc = calculator.AddVariable(new fsParameterIdentifier("local Pc"));
//             m_localBulk = calculator.AddVariable(new fsParameterIdentifier("local Bulk"));
//             m_localOneMinusEps = calculator.AddVariable(new fsParameterIdentifier("1 - eps"));
//         
//             #endregion
// 
//             Equations = null;
//         }
// 
//         public fsCalculationOptions.fsCakeInputOption CakeInputOption;
// 
//         public fsCalculationOptions.fsEnterSolidsDensity EnterSolidsOption;
// 
//         public void RebuildEquationsList()
//         {
//             Equations = new List<fsCalculatorEquation>();
// 
//             switch(CakeInputOption)
//             {
//                 case fsCalculationOptions.fsCakeInputOption.PermeabilityPc:
//                     calculator.AddEquation(new fsAssignEquation(m_localPc, m_pc));
//                     break;
//                 case fsCalculationOptions.fsCakeInputOption.ResistanceRc:
//                     calculator.AddEquation(new fsDivisionInverseEquation(m_localPc, m_rc));
//                     break;
//                 case fsCalculationOptions.fsCakeInputOption.ResistanceAlpha:
//                     switch (EnterSolidsOption)
//                     {
//                         case fsCalculationOptions.fsEnterSolidsDensity.BulkDensityDrySolids:
//                             calculator.AddEquation(new fsAssignEquation(m_localBulk, m_rhosBulk));
//                             break;
//                         case fsCalculationOptions.fsEnterSolidsDensity.SolidsDensityAndCakePorosity:
//                             calculator.AddEquation(new fsConstantSumEquation(1, m_localOneMinusEps, m_eps));
//                             calculator.AddEquation(new fsProductEquation(m_localBulk, m_localOneMinusEps, m_rhos));
//                             break;
//                     }
//                     calculator.AddEquation(new fsConstantProductEquation(1, m_alpha, m_localPc, m_localBulk));
//                     break;
//             }
//             calculator.AddEquation(new fsPkeFromStandartEquation(m_pke, m_pkeSt, m_sigma, m_localPc));
//         }
//     }
}
