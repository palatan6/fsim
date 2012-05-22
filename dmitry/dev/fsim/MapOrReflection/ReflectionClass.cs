using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Parameters;

namespace MapOrReflection
{
    public class ReflectionClass : reflectionBase
    {
        public fsCalculatorVariable m_rhoS { get; set; }
        public fsCalculatorVariable m_eps { get; set; }
        public fsCalculatorVariable m_pc0 { get; set; }
        public fsCalculatorVariable m_rc0 { get; set; }
        public fsCalculatorVariable m_alpha0 { get; set; }
        public fsCalculatorVariable m_nc { get; set; }
        public fsCalculatorVariable m_pressure { get; set; }
        public fsCalculatorVariable m_pc { get; set; }
        public fsCalculatorVariable m_rc { get; set; }
        public fsCalculatorVariable m_alpha { get; set; }

        public ReflectionClass()
        {
           
        }
    }
}
