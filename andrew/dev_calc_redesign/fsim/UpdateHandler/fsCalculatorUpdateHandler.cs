namespace UpdateHandler
{
    public class fsCalculatorUpdateHandler
    {
        private readonly fsCalculatorUpdateHandler m_parentHandler;
        private readonly double m_startValue;
        private readonly double m_endValue;
        private readonly IUpdatable m_statusControl;

        #region Constructors

        public fsCalculatorUpdateHandler(IUpdatable statusControl)
        {
            m_statusControl = statusControl;
        }

        private fsCalculatorUpdateHandler(
            fsCalculatorUpdateHandler parentHandler,
            double startValue,
            double endValue)
        {
            m_parentHandler = parentHandler;
            m_startValue = startValue;
            m_endValue = endValue;
        }

        #endregion

        #region Update Routines

        private delegate void fsCallUpdateProgressBar(IUpdatable statusControl, int progress);
        static private void UpdateProgressBar(IUpdatable statusControl, int progress)
        {
            statusControl.Value = progress;
            statusControl.Refresh();
        }

        public void SetProgress(double progress)
        {
            if (m_parentHandler == null)
            {
                var intProgress = (int)(progress * m_statusControl.Maximum + 0.5);
                m_statusControl.Invoke(
                    new fsCallUpdateProgressBar(UpdateProgressBar),
                    new object[] { m_statusControl, intProgress });
            }
            else
            {
                m_parentHandler.SetProgress(m_startValue + (m_endValue - m_startValue) * progress);
            }
        }

        #endregion

        public fsCalculatorUpdateHandler CreateSubHandler(double startValue, double endValue)
        {
            return new fsCalculatorUpdateHandler(this, startValue, endValue);
        }
    }

}
