using System.Collections.Generic;
using System.ComponentModel;
using Equations;
using Parameters;

namespace StepCalculators
{
     /*
      * 
      * This calculator used in some of small modules. It isn't a general solution.
      * And it isn't used somewhere else.
      * 
      * */
 
     public class fsCakePorosityCalculator : fsCalculatorEquationsList
     {
         /*readonly fsCalculatorVariable m_machineArea;
         readonly fsCalculatorVariable m_cakeArea;
         readonly fsCalculatorVariable m_machineDiameter;
         readonly fsCalculatorVariable m_filterElementDiameter;
         readonly fsCalculatorVariable m_b;
         readonly fsCalculatorVariable m_bOverD;
         readonly fsCalculatorConstant m_rhoL;
         readonly fsCalculatorConstant m_rhoS;
         readonly fsCalculatorConstant m_cakeHeight;
         readonly fsCalculatorConstant m_wetCakeMass;
         readonly fsCalculatorConstant m_dryCakeMass;
         readonly fsCalculatorConstant m_c;
         readonly fsCalculatorVariable m_eps;*/

         IEquationParameter m_machineArea;
          IEquationParameter m_cakeArea;
          IEquationParameter m_machineDiameter;
          IEquationParameter m_filterElementDiameter;
          IEquationParameter m_b;
          IEquationParameter m_bOverD;
          IEquationParameter m_rhoL;
          IEquationParameter m_rhoS;
          IEquationParameter m_cakeHeight;
          IEquationParameter m_wetCakeMass;
          IEquationParameter m_dryCakeMass;
          IEquationParameter m_c;
          IEquationParameter m_eps;

         public override void AddToCalculator(fsCalculator calculator)
         {
             #region Parameters Initialization

             m_machineArea = calculator.AddVariable(fsParameterIdentifier.FilterArea);
             m_machineDiameter = calculator.AddVariable(fsParameterIdentifier.MachineDiameter);
             m_filterElementDiameter = calculator.AddVariable(fsParameterIdentifier.FilterElementDiameter);
             m_cakeArea = calculator.AddVariable(new fsParameterIdentifier("cakeArea"));
             m_b = calculator.AddVariable(fsParameterIdentifier.MachineWidth);
             m_bOverD = calculator.AddVariable(fsParameterIdentifier.WidthOverDiameterRatio);
             m_rhoL = calculator.AddVariable(fsParameterIdentifier.LiquidDensity);
             m_rhoS = calculator.AddVariable(fsParameterIdentifier.SolidsDensity);
             m_cakeHeight = calculator.AddVariable(fsParameterIdentifier.CakeHeight);
             m_wetCakeMass = calculator.AddVariable(fsParameterIdentifier.WetCakeMass);
             m_dryCakeMass = calculator.AddVariable(fsParameterIdentifier.DryCakeMass);
             m_c = calculator.AddVariable(fsParameterIdentifier.SolutesConcentrationInCakeLiquid);
             m_eps = calculator.AddVariable(fsParameterIdentifier.CakePorosity);
 
             #endregion
 
             calculator.Equations = null;
         }
 
         public enum fsSaltContentOption
         {
             [Description("Neglected")]
             Neglected,
             [Description("Considered")]
             Considered
         }
         public fsSaltContentOption SaltContentOption;
 
         public enum fsSaturationOption
         {
             [Description("General Case")]
             NotSaturatedCake,
             [Description("Cake Saturated")]
             SaturatedCake
         }
         public fsSaturationOption SaturationOption;
 
         public enum fsMachineTypeOption
         {
             [Description("Plane Area ")]
             PlainArea,
             [Description("Convex Area (Candle Filters)")]
             ConvexCylindric,
             [Description("Concave Area (Centrifuges)")]
             ConcaveCylindric
         }
         public fsMachineTypeOption MachineTypeOption;
 
         public void RebuildEquationsList(fsCalculator calculator)
         {
             calculator.Equations = new List<fsCalculatorEquation>();
 
             switch (MachineTypeOption)
             {
                 case fsMachineTypeOption.PlainArea:
                     calculator.AddEquation(new fsAssignEquation(m_cakeArea, m_machineArea));
                     break;
                 case fsMachineTypeOption.ConvexCylindric:
                     calculator.AddEquation(new fsConvexCakeAreaEquation(m_cakeArea, m_machineArea, m_cakeHeight, m_filterElementDiameter));
                     break;
                 case fsMachineTypeOption.ConcaveCylindric:
                     calculator.AddEquation(new fsConcaveCakeAreaEquation(m_cakeArea, m_machineArea, m_cakeHeight, m_machineDiameter));
                     calculator.AddEquation(new fsCylinderAreaEquation(m_machineArea, m_machineDiameter, m_b));
                     calculator.AddEquation(new fsProductEquation(m_b, m_machineDiameter, m_bOverD));
                     break;
             }
 
             if (SaltContentOption == fsSaltContentOption.Neglected)
             {
                 if (SaturationOption == fsSaturationOption.SaturatedCake)
                 {
                     calculator.AddEquation(new fsPorositySaltNeglectedCakeSaturatedEquation(m_eps, m_dryCakeMass, m_wetCakeMass,
                                                                                  m_rhoS, m_rhoL));
                 }
                 else
                 {
                     calculator.AddEquation(new fsPorositySaltNeglectedCakeNotSaturatedEquation(m_eps, m_dryCakeMass, m_rhoS,
                                                                                     m_cakeArea, m_cakeHeight));
                 }
             }
             else
             {
                 if (SaturationOption == fsSaturationOption.SaturatedCake)
                 {
                     calculator.AddEquation(new fsPorositySaltConsideredCakeSaturatedEquation(m_eps, m_dryCakeMass, m_wetCakeMass,
                                                                                   m_c, m_rhoS, m_rhoL));
                 }
                 else
                 {
                     calculator.AddEquation(new fsPorositySaltConsideredCakeNotSaturatedEquation(m_eps, m_dryCakeMass, m_wetCakeMass,
                                                                                      m_c, m_rhoS, m_rhoL, m_cakeArea,
                                                                                      m_cakeHeight));
                 }
             }
  
          }
     }
}
