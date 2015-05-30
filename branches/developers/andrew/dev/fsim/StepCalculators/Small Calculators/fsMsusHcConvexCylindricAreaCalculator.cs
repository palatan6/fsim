using Equations;
using Equations.Material.Cake_Moisture_Content_Rf_Equations;
using Equations.Material.Eps_Kappa_Equations;
using Parameters;
using Value;

namespace StepCalculators
{
    public class fsMsusHcConvexCylindricAreaCalculator : fsCalculator
    {
        readonly fsCalculatorVariable m_filtrateDensity;
        readonly fsCalculatorVariable m_solidsDensity;
        readonly fsCalculatorVariable m_suspensionDensity;
        readonly fsCalculatorVariable m_solidsMassFraction;
        readonly fsCalculatorVariable m_solidsVolumeFraction;
        readonly fsCalculatorVariable m_solidsConcentration;

        readonly fsCalculatorVariable m_porosity;
        readonly fsCalculatorVariable m_kappa;
        readonly fsCalculatorVariable m_kappa_m;
        readonly fsCalculatorVariable m_rho_cd;
        readonly fsCalculatorVariable m_rho_cw;
        readonly fsCalculatorVariable m_Rf;
        readonly fsCalculatorVariable m_Cmc;
        
        readonly fsCalculatorVariable m_filterArea;
        readonly fsCalculatorVariable m_filterElementDiameter;

        readonly fsCalculatorVariable m_cakeHeight;

        readonly fsCalculatorVariable m_suspensionMass;
        readonly fsCalculatorVariable m_suspensionVolume;
        readonly fsCalculatorVariable m_Mc;
        readonly fsCalculatorVariable m_Mf;
        readonly fsCalculatorVariable m_Ms;
        readonly fsCalculatorVariable m_Mlc;
        readonly fsCalculatorVariable m_Vc;
        readonly fsCalculatorVariable m_Vf;
        readonly fsCalculatorVariable m_Vs;
        readonly fsCalculatorVariable m_Vlc;
        readonly fsCalculatorVariable m_msus;
        readonly fsCalculatorVariable m_mc;
        readonly fsCalculatorVariable m_mf;
        readonly fsCalculatorVariable m_ms;
        readonly fsCalculatorVariable m_mlc;
        readonly fsCalculatorVariable m_vsus;
        readonly fsCalculatorVariable m_vc;
        readonly fsCalculatorVariable m_vf;
        readonly fsCalculatorVariable m_vs;
        readonly fsCalculatorVariable m_vlc;

        public fsMsusHcConvexCylindricAreaCalculator()
        {
            #region Parameters Initialization

            m_filtrateDensity = AddVariable(fsParameterIdentifier.MotherLiquidDensity);
            m_solidsDensity = AddVariable(fsParameterIdentifier.SolidsDensity);
            m_suspensionDensity = AddVariable(fsParameterIdentifier.SuspensionDensity);
            m_solidsMassFraction = AddVariable(fsParameterIdentifier.SuspensionSolidsMassFraction);
            m_solidsVolumeFraction = AddVariable(fsParameterIdentifier.SuspensionSolidsVolumeFraction);
            m_solidsConcentration = AddVariable(fsParameterIdentifier.SuspensionSolidsConcentration);

            m_porosity = AddVariable(fsParameterIdentifier.CakePorosity);
            m_kappa = AddVariable(fsParameterIdentifier.Kappa);
            m_kappa_m = AddVariable(fsParameterIdentifier.Kappam);
            m_rho_cd = AddVariable(fsParameterIdentifier.DryCakeDensity);
            m_rho_cw = AddVariable(fsParameterIdentifier.CakeWetDensity);
            m_Rf = AddVariable(fsParameterIdentifier.CakeMoistureContent);
            m_Cmc = AddVariable(fsParameterIdentifier.CakeSolidsContentCmc);
            
            m_filterArea = AddVariable(fsParameterIdentifier.FilterArea);
            m_filterElementDiameter = AddVariable(fsParameterIdentifier.FilterElementDiameter);

            m_cakeHeight = AddVariable(fsParameterIdentifier.CakeHeight);
            m_suspensionMass = AddVariable(fsParameterIdentifier.SuspensionMass);
            m_suspensionVolume = AddVariable(fsParameterIdentifier.SuspensionVolume);

            m_Vc = AddVariable(fsParameterIdentifier.CakeVolume);
            m_Vf = AddVariable(fsParameterIdentifier.FiltrateVolume);
            m_Vs = AddVariable(fsParameterIdentifier.SolidsVolume);
            m_Vlc = AddVariable(fsParameterIdentifier.LiquidVolumeInCake);
            m_Mc = AddVariable(fsParameterIdentifier.CakeMass);
            m_Mf = AddVariable(fsParameterIdentifier.FiltrateMass);
            m_Ms = AddVariable(fsParameterIdentifier.SolidsMass);
            m_Mlc = AddVariable(fsParameterIdentifier.LiquidMassInCake);

            m_msus = AddVariable(fsParameterIdentifier.SpecificSuspensionMass);
            m_mc = AddVariable(fsParameterIdentifier.SpecificCakeMass);
            m_mf = AddVariable(fsParameterIdentifier.SpecificFiltrateMass);
            m_ms = AddVariable(fsParameterIdentifier.SpecificSolidsMass);
            m_mlc = AddVariable(fsParameterIdentifier.SpecificLiquidMassInCake);
            m_vsus = AddVariable(fsParameterIdentifier.SpecificSuspensionVolume);
            m_vc = AddVariable(fsParameterIdentifier.SpecificCakeVolume);
            m_vf = AddVariable(fsParameterIdentifier.SpecificFiltrateVolume);
            m_vs = AddVariable(fsParameterIdentifier.SpecificSolidsVolume);
            m_vlc = AddVariable(fsParameterIdentifier.SpecificLiquidVolumeInCake);

            #endregion

            #region Equations Initialization

            var constantOne = new fsCalculatorConstant(new fsParameterIdentifier("1")) { Value = fsValue.One };

            IEquationParameter oneMinusEps = AddVariable(new fsParameterIdentifier("1 - eps"));
            Equations.Add(new fsSumEquation(constantOne, m_porosity, oneMinusEps));

            AddEquation(new fsvcConvexAreaEquation(m_vc, m_cakeHeight, m_filterElementDiameter));

            AddEquation(new fsMassConcentrationEquation(m_solidsMassFraction, m_filtrateDensity, m_solidsDensity, m_suspensionDensity));
            AddEquation(new fsVolumeConcentrationEquation(m_solidsVolumeFraction, m_filtrateDensity, m_solidsDensity, m_suspensionDensity));
            AddEquation(new fsConcentrationEquation(m_solidsConcentration, m_filtrateDensity, m_solidsDensity, m_suspensionDensity));

            AddEquation(new fsEpsKappaCvEquation(m_porosity, m_kappa, m_solidsVolumeFraction));

            AddEquation(new fsCakeDrySolidsDensityEquation(m_rho_cd, m_porosity, m_solidsDensity));
            AddEquation(new fsCakeWetDensityFromRhofRhosPorosityEquation(m_rho_cw, m_filtrateDensity, m_solidsDensity,
                m_porosity));
            AddEquation(new fsMoistureContentFromDensitiesAndPorosityEquation(m_Rf, m_porosity, m_filtrateDensity,
                m_solidsDensity));
            AddEquation(new fsSumEquation(constantOne, m_Cmc, m_Rf));
            AddEquation(new fsEpsKappaMCvEquation(m_porosity, m_kappa_m, m_solidsVolumeFraction, m_solidsDensity));

            AddEquation(new fsSuspensionMassFromHcEpsConvexCylindircAreaEquation(m_suspensionMass, m_porosity, m_solidsDensity, m_filterArea, m_filterElementDiameter, m_cakeHeight, m_solidsMassFraction));
            //AddEquation(new fsSuspensionVolumeFromHcEpsConvexCylindircAreaEquation(m_suspensionVolume, m_porosity, m_filterArea, m_cakeHeight, m_solidsVolumeFraction));
            //AddEquation(new fsSuspensionVolumeFromHcKappaConvexCylindircAreaEquation(m_suspensionVolume, m_kappa, m_filterArea, m_cakeHeight));

            AddEquation(new fsProductEquation(m_suspensionMass, m_suspensionVolume, m_suspensionDensity));

            AddEquation(new fsSumEquation(m_msus, m_mf, m_mc));
            AddEquation(new fsSumEquation(m_suspensionMass, m_Mf, m_Mc));

            AddEquation(new fsvcFromMsusAndOthers(m_vc, m_porosity, m_solidsVolumeFraction, m_filterArea, m_suspensionDensity, m_suspensionMass));

            AddEquation(new fsProductEquation(m_msus, m_suspensionDensity, m_vsus));
            AddEquation(new fsVSusFromEpsCvVc(m_vsus, m_porosity, m_solidsVolumeFraction, m_vc));
            AddEquation(new fsProductEquation(m_msus, m_suspensionDensity, m_vsus));
            AddEquation(new fsProductEquation(m_vc, m_vf, m_kappa));
            AddEquation(new fsProductEquation(m_mf, m_filtrateDensity, m_vf));
            AddEquation(new fsProductEquation(m_vs, oneMinusEps, m_vc));
            AddEquation(new fsProductEquation(m_ms, m_solidsDensity, m_vs));
            AddEquation(new fsProductEquation(m_vlc, m_porosity, m_vc));
            AddEquation(new fsProductEquation(m_mlc, m_vlc, m_filtrateDensity));
            AddEquation(new fsProductEquation(m_suspensionVolume, m_filterArea, m_vsus));
            AddEquation(new fsProductEquation(m_Vf, m_filterArea, m_vf));
            AddEquation(new fsProductEquation(m_Vc, m_filterArea, m_vc));
            AddEquation(new fsProductEquation(m_Vs, m_filterArea, m_vs));
            AddEquation(new fsProductEquation(m_Vlc, m_filterArea, m_vlc));
            AddEquation(new fsProductEquation(m_suspensionMass, m_filterArea, m_msus));
            AddEquation(new fsProductEquation(m_Mf, m_filterArea, m_mf));
            AddEquation(new fsProductEquation(m_Mc, m_filterArea, m_mc));
            AddEquation(new fsProductEquation(m_Ms, m_filterArea, m_ms));
            AddEquation(new fsProductEquation(m_Mlc, m_filterArea, m_mlc));

            AddEquation(new fsProductEquation(m_Mc, m_Vc, m_rho_cw));
            AddEquation(new fsProductEquation(m_mc, m_vc, m_rho_cw));
            
            #endregion
        }
    }
}
