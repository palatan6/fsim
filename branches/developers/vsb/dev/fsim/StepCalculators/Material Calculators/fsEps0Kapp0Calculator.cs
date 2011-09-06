﻿using System;
using System.Collections.Generic;

using System.Text;
using Parameters;
using Equations;

namespace StepCalculators
{
    public class fsEps0Kappa0Calculator : fsCalculator
    {
        private fsCalculatorVariable Porosity0;
        private fsCalculatorVariable Kappa0;
        private fsCalculatorConstant VolumeConcentration;

        protected override void InitParameters()
        {
            Porosity0 = InitVariable(fsParameterIdentifier.Porosity0);
            Kappa0 = InitVariable(fsParameterIdentifier.kappa0);
            VolumeConcentration = InitConstant(fsParameterIdentifier.VolumeConcentration);
        }

        protected override void InitEquations()
        {
            AddEquation(new fsEpsKappaCvEquation(Porosity0, Kappa0, VolumeConcentration));
        }
    }
}