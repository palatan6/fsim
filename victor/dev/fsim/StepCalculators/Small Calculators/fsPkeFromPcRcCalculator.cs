using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Parameters;
using Equations;

namespace StepCalculators
{
    public class fsPkeFromPcRcCalculator : fsCalculator
    {
        readonly fsCalculatorVariable m_pc;
        readonly fsCalculatorVariable m_rc;
        readonly fsCalculatorVariable m_alpha;
        readonly fsCalculatorConstant m_rhos;
        readonly fsCalculatorConstant m_eps;
        readonly fsCalculatorConstant m_rhosBulk;
        readonly fsCalculatorConstant m_sigma;
        readonly fsCalculatorConstant m_pkeSt;
        readonly fsCalculatorVariable m_pke;
        readonly fsCalculatorVariable m_localPc;
        readonly fsCalculatorVariable m_localBulk;
        readonly fsCalculatorVariable m_localOneMinusEps;

        public fsPkeFromPcRcCalculator()
        {
            #region Parameters Initialization

            m_pc = AddVariable(fsParameterIdentifier.CakePermeability);
            m_rc = AddVariable(fsParameterIdentifier.CakeResistance);
            m_alpha = AddVariable(fsParameterIdentifier.CakeResistanceAlpha);
            m_rhos = AddConstant(fsParameterIdentifier.SolidsDensity);
            m_eps = AddConstant(fsParameterIdentifier.CakePorosity);
            m_rhosBulk = AddConstant(fsParameterIdentifier.DryCakeDensity);
            m_sigma = AddConstant(fsParameterIdentifier.SurfaceTensionLiquidInCake);
            m_pkeSt = AddConstant(fsParameterIdentifier.StandardCapillaryPressure);
            m_pke = AddVariable(fsParameterIdentifier.CapillaryPressure);
            m_localPc = AddVariable(new fsParameterIdentifier("local Pc"));
            m_localBulk = AddVariable(new fsParameterIdentifier("local Bulk"));
            m_localOneMinusEps = AddVariable(new fsParameterIdentifier("1 - eps"));
        
            #endregion

            Equations = null;
        }

        public fsCalculationOptions.fsCakeInputOption CakeInputOption;

        public fsCalculationOptions.fsEnterSolidsDensity EnterSolidsOption;

        public void RebuildEquationsList()
        {
            Equations = new List<fsCalculatorEquation>();

            switch(CakeInputOption)
            {
                case fsCalculationOptions.fsCakeInputOption.PermeabilityPc:
                    Equations.Add(new fsAssignEquation(m_localPc, m_pc));
                    break;
                case fsCalculationOptions.fsCakeInputOption.ResistanceRc:
                    Equations.Add(new fsDivisionInverseEquation(m_localPc, m_rc));
                    break;
                case fsCalculationOptions.fsCakeInputOption.ResistanceAlpha:
                    switch (EnterSolidsOption)
                    {
                        case fsCalculationOptions.fsEnterSolidsDensity.BulkDensityDrySolids:
                            Equations.Add(new fsAssignEquation(m_localBulk, m_rhosBulk));
                            break;
                        case fsCalculationOptions.fsEnterSolidsDensity.SolidsDensityAndCakePorosity:
                            Equations.Add(new fsConstantSumEquation(1, m_localOneMinusEps, m_eps));
                            Equations.Add(new fsProductEquation(m_localBulk, m_localOneMinusEps, m_rhos));
                            break;
                    }
                    Equations.Add(new fsConstantProductEquation(1, m_alpha, m_localPc, m_localBulk));
                    break;
            }
            Equations.Add(new fsPkeFromStandartEquation(m_pke, m_pkeSt, m_sigma, m_localPc));
        }
    }
}
