using System.Collections.Generic;

namespace Calculator
{
    public class fsSection
    {
        private readonly List<fsModule> m_modules = new List<fsModule>();

        public void AddModule(fsModule module)
        {
            m_modules.Add(module);
        }

        public void RemoveModule(fsModule module)
        {
            m_modules.Remove(module);
        }
    }
}