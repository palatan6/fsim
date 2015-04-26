using System.Collections.Generic;
using System.Windows.Forms;
using CalculatorModules;
using Parameters;
using ParametersIdentifiers.Ranges;
using Units;
using CalculatorModules.User_Controls;

namespace Calculator
{
    public class fsModule
    {
        private readonly fsCalculatorControl m_calculatorControl;

        public fsModule(string name, fsCalculatorControl calculatorControl)
        {
            Name = name;
            m_calculatorControl = calculatorControl;
            if (m_calculatorControl != null)
            {
                Form = new Form
                        {
                            Text = Name,
                            Width = m_calculatorControl.Width + 10,
                            Height = m_calculatorControl.Height + 10
                        };
                m_calculatorControl.Parent = Form;
                m_calculatorControl.ControlToResizeForExpanding = Form;
                m_calculatorControl.Dock = DockStyle.Fill;
                Form.Show();
            }
        }

        public string Name { get; private set; }
        public Form Form { get; private set; }

        internal void SetUnits(Dictionary<fsCharacteristic, fsUnit> dictionary)
        {
            m_calculatorControl.SetUnits(dictionary);
            m_calculatorControl.RecalculateAndRedraw();
        }

        internal void SetRanges(Dictionary<fsParameterIdentifier, fsRange> dictionary)
        {
            m_calculatorControl.SetRanges(dictionary);
        }

        internal void RecalculateAndRedraw()
        {
            m_calculatorControl.RecalculateAndRedraw();
        }

        #region Show/Hide parameters

        internal void ShowAndHideParameters(Dictionary<fsParameterIdentifier, bool> parametersToShowAndHide)
        {
            m_calculatorControl.ShowAndHideParameters(parametersToShowAndHide);
        }

        internal Dictionary<fsParameterIdentifier, bool> GetInvolvedParametersWithVisibleStatus()
        {
            return m_calculatorControl.GetInvolvedParametersWithVisibleStatus();
        }

        internal List<fsParametersGroup> GetGroups()
        {
            return m_calculatorControl.GetGroups();
        }

        #endregion
    }
}