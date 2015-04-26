using System;
using System.Collections.Generic;
using Parameters;

namespace CalculatorModules
{
    public class fsParametersGroup
    {
        #region Constructors

        public fsParametersGroup(bool isOnlyCalcuated)
        {
            Parameters = new List<fsParameterIdentifier>();
            Representator = null;
            IsInput = false;
            IsOnlyCalculated = isOnlyCalcuated;
            Kind = fsParametersGroupKind.MachiningSettingsParameters;
        }

        public fsParametersGroup(bool isOnlyCalcuated, string name)
        {
            Parameters = new List<fsParameterIdentifier>();
            Representator = null;
            IsInput = false;
            IsOnlyCalculated = isOnlyCalcuated;
            Kind = fsParametersGroupKind.MachiningSettingsParameters;
            Name = name;
        }

        public fsParametersGroup(fsParametersGroup other)
        {
            Parameters = new List<fsParameterIdentifier>(other.Parameters);
            Representator = other.Representator;
            IsInput = other.IsInput;
            IsOnlyCalculated = other.IsOnlyCalculated;
            Kind = other.Kind;
        }

        #endregion

        public List<fsParameterIdentifier> Parameters { get; private set; }
        public fsParameterIdentifier Representator { get; set; }
        private bool IsInput { get; set; }
        private bool IsOnlyCalculated { get; set; }

        public void SetIsInputFlag(bool isInput)
        {
            if (IsOnlyCalculated)
                throw new Exception("Attempt to change input flag of only calculated group.");

            IsInput = isInput;
        }

        public bool GetIsInputFlag()
        {
            return IsOnlyCalculated ? false : IsInput;
        }

        public bool GetIsOnlyCalculatedFlag()
        {
            return IsOnlyCalculated;
        }

        public enum fsParametersGroupKind
        {
            MaterialParameters,
            MachiningSettingsParameters
        }

        public fsParametersGroupKind Kind { get; set; }

        public string Name { get; private set; }
    }
}