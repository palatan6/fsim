using Units;

namespace Parameters
{
    public class fsParameterIdentifier
    {
        #region fsParameterIdentifier

        #region Constructors

        public fsParameterIdentifier(string name)
        {
            Name = name;
            FullName = name;
            MeasurementCharacteristic = fsCharacteristic.NoUnits;
        }

        public fsParameterIdentifier(string name, fsCharacteristic units)
        {
            Name = name;
            FullName = name;
            MeasurementCharacteristic = units;
        }

        public fsParameterIdentifier(string name, string fullName, fsCharacteristic units)
        {
            Name = name;
            FullName = fullName;
            MeasurementCharacteristic = units;
        }

        public fsParameterIdentifier(string name, string fullName)
        {
            Name = name;
            FullName = fullName;
            MeasurementCharacteristic = fsCharacteristic.NoUnits;
        }

        #endregion

        public string Name { get; private set; }

        public string FullName { get; private set; }

        public fsCharacteristic MeasurementCharacteristic { get; private set; }

        public override string ToString()
        {
            return FullName == Name ? Name : FullName + " (" + Name + ")";
        }

        #endregion

        #region Parameters Collection

        public static fsParameterIdentifier CakePorosity = new fsParameterIdentifier("eps", "Cake Porosity",
                                                                                     fsCharacteristic.Concentration);

        public static fsParameterIdentifier Kappa = new fsParameterIdentifier("kappa");

        public static fsParameterIdentifier CakePermeability = new fsParameterIdentifier("Pc", "Cake Permeability",
                                                                                         fsCharacteristic.
                                                                                             CakePermeability);

        public static fsParameterIdentifier CakeResistance = new fsParameterIdentifier("rc", "Cake Resistance",
                                                                                       fsCharacteristic.CakeResistance);

        public static fsParameterIdentifier CakeResistanceAlpha = new fsParameterIdentifier("alpha", "Cake Resistance",
                                                                                            fsCharacteristic.
                                                                                                CakeResistanceAlpha);

        public static fsParameterIdentifier PracticalCakePermeability =
            new fsParameterIdentifier("K", "Practical cake permeability", fsCharacteristic.PracticalCakePermeability);
        
        public static fsParameterIdentifier FilterArea = 
            new fsParameterIdentifier("A", "Filter Area", fsCharacteristic.Area);

        public static fsParameterIdentifier FilterLength =
            new fsParameterIdentifier("l", "Filter Length", fsCharacteristic.MachineGeometryLength);

        public static fsParameterIdentifier As = new fsParameterIdentifier("As", fsCharacteristic.Area);

        public static fsParameterIdentifier MachineDiameter = new fsParameterIdentifier("D", "Machine Diameter",
                                                                                        fsCharacteristic.MachineGeometryLength);

        public static fsParameterIdentifier FilterElementDiameter = new fsParameterIdentifier("d",
                                                                                              "Filter Element Diameter",
                                                                                              fsCharacteristic.MachineGeometryLength);

        public static fsParameterIdentifier MachineWidth = new fsParameterIdentifier("b", "Machine Width",
                                                                                     fsCharacteristic.MachineGeometryLength);

        public static fsParameterIdentifier WidthOverDiameterRatio = new fsParameterIdentifier("b/D",
                                                                                               "Width/Diameter Ratio",
                                                                                               fsCharacteristic.NoUnits);

        public static fsParameterIdentifier PressureDifference = new fsParameterIdentifier("Dp", "Pressure Difference",
                                                                                           fsCharacteristic.Pressure);

        public static fsParameterIdentifier CycleTime = new fsParameterIdentifier("tc", fsCharacteristic.Time);
        public static fsParameterIdentifier RotationalSpeed = new fsParameterIdentifier("n", fsCharacteristic.Frequency);

        public static fsParameterIdentifier SpecificFiltrationTime = new fsParameterIdentifier("sf",
                                                                                               "Specific Filtration Time",
                                                                                               fsCharacteristic.
                                                                                                   Concentration);

        public static fsParameterIdentifier SpecificResidualTime = new fsParameterIdentifier("sr",
                                                                                             "Specific Residual Time",
                                                                                             fsCharacteristic.
                                                                                                 Concentration);

        public static fsParameterIdentifier FiltrationTime = new fsParameterIdentifier("tf", "Filtration Time",
                                                                                       fsCharacteristic.Time);

        public static fsParameterIdentifier ResidualTime = new fsParameterIdentifier("tr", "Residual Time",
                                                                                     fsCharacteristic.Time);

        public static fsParameterIdentifier CakeHeight = new fsParameterIdentifier("hc", "Cake Height",
                                                                                   fsCharacteristic.CakeHeight);

        public static fsParameterIdentifier WetCakeMass = new fsParameterIdentifier("Mcw", "Wet Cake Mass",
                                                                                    fsCharacteristic.Mass);

        public static fsParameterIdentifier DryCakeMass =
            new fsParameterIdentifier("Mcd", "Dry Cake Mass", fsCharacteristic.Mass);

        public static fsParameterIdentifier SolidsMass =
            new fsParameterIdentifier("Ms", "Solids Mass", fsCharacteristic.Mass);

        public static fsParameterIdentifier SuspensionMass =
            new fsParameterIdentifier("Msus", "Suspension Mass", fsCharacteristic.Mass);

        public static fsParameterIdentifier SpecificSuspensionMass =
            new fsParameterIdentifier("msus", "Specific susp mass", fsCharacteristic.SpecificMass);

        public static fsParameterIdentifier FiltrateMass =
            new fsParameterIdentifier("Mf", "Fitrate Mass", fsCharacteristic.Mass);

        public static fsParameterIdentifier LiquidMassInSuspension =
            new fsParameterIdentifier("Ml", "Liquid Mass in Suspension", fsCharacteristic.Mass);

        public static fsParameterIdentifier SolidsMassInSuspension =
            new fsParameterIdentifier("Ms", "Solids Mass in Suspension", fsCharacteristic.Mass);

        public static fsParameterIdentifier CakeMass =
            new fsParameterIdentifier("Mc", "Cake Mass", fsCharacteristic.Mass);

        public static fsParameterIdentifier SolidsVolume =
            new fsParameterIdentifier("Vs", "Solids Volume", fsCharacteristic.Volume);

        public static fsParameterIdentifier SuspensionVolume = 
            new fsParameterIdentifier("Vsus", "Suspension Volume", fsCharacteristic.Volume);

        public static fsParameterIdentifier SpecificSuspensionVolume =
            new fsParameterIdentifier("vsus", "Specific susp volume", fsCharacteristic.SpecificVolume);

        public static fsParameterIdentifier FiltrateVolume =
            new fsParameterIdentifier("Vf", "Fitrate Volume", fsCharacteristic.Volume);

        public static fsParameterIdentifier CakeVolume =
            new fsParameterIdentifier("Vc", "Cake Volume", fsCharacteristic.Volume);

        public static fsParameterIdentifier SuspensionMassFlowrate = 
            new fsParameterIdentifier("Qmsus", fsCharacteristic.MassFlowrate);

        public static fsParameterIdentifier Qms =
            new fsParameterIdentifier("Qms", fsCharacteristic.MassFlowrate);

        public static fsParameterIdentifier Qsus =
            new fsParameterIdentifier("Qsus", fsCharacteristic.VolumeFlowrate);

        public static fsParameterIdentifier Qs =
            new fsParameterIdentifier("Qs", fsCharacteristic.VolumeFlowrate);

        public static fsParameterIdentifier Qmsust =
            new fsParameterIdentifier("Qmsus,t", "Susp mass rate rel to t", fsCharacteristic.MassFlowrate);

        public static fsParameterIdentifier Qmsusd =
            new fsParameterIdentifier("Qmsus,d", "Diff susp mass rate", fsCharacteristic.MassFlowrate);

        public static fsParameterIdentifier Qsust =
            new fsParameterIdentifier("Qsus,t", "Susp volume rate rel to t", fsCharacteristic.VolumeFlowrate);

        public static fsParameterIdentifier Qsusd =
            new fsParameterIdentifier("Qsus,d", "Diff susp volume rate", fsCharacteristic.VolumeFlowrate);

        public static fsParameterIdentifier ns =
            new fsParameterIdentifier("ns", "Number of segments", fsCharacteristic.NoUnits);

        public static fsParameterIdentifier ls_over_b =
            new fsParameterIdentifier("ls/b", fsCharacteristic.NoUnits);

        public static fsParameterIdentifier l_over_b =
            new fsParameterIdentifier("l/b", fsCharacteristic.NoUnits);

        public static fsParameterIdentifier ls =
            new fsParameterIdentifier("ls", "Length of segments", fsCharacteristic.MachineGeometryLength);

        public static fsParameterIdentifier StandardTechnicalTime =
            new fsParameterIdentifier("ttech0", "Standard technical time", fsCharacteristic.Time);

        public static fsParameterIdentifier lambda =
            new fsParameterIdentifier("lambda", fsCharacteristic.NoUnits);

        public static fsParameterIdentifier TechnicalTime =
            new fsParameterIdentifier("ttech", "Technical Time", fsCharacteristic.Time);

        public static fsParameterIdentifier u =
            new fsParameterIdentifier("u", "Belt speed", fsCharacteristic.Speed);

        public static fsParameterIdentifier nsf =
            new fsParameterIdentifier("nsf", "Segment number for cake formation", fsCharacteristic.NoUnits);

        public static fsParameterIdentifier nsr =
            new fsParameterIdentifier("nsr", "Segment number for residual steps", fsCharacteristic.NoUnits);

        public static fsParameterIdentifier qsust =
            new fsParameterIdentifier("qsus,t", fsCharacteristic.SpecificVolumeFlowrate);

        public static fsParameterIdentifier qsusd =
            new fsParameterIdentifier("qsus,d", fsCharacteristic.SpecificVolumeFlowrate);

        public static fsParameterIdentifier qmsust =
            new fsParameterIdentifier("qmsus,t", fsCharacteristic.SpecificMassFlowrate);

        public static fsParameterIdentifier qmsusd =
            new fsParameterIdentifier("qmsus,d", fsCharacteristic.SpecificMassFlowrate);

        public static fsParameterIdentifier qft =
            new fsParameterIdentifier("qft", "Spec filtrate volume rel to time", fsCharacteristic.SpecificVolumeFlowrate);

        public static fsParameterIdentifier qmft =
            new fsParameterIdentifier("qmft", fsCharacteristic.SpecificMassFlowrate);

        public static fsParameterIdentifier MotherLiquidViscosity =
            new fsParameterIdentifier("eta", "Mother Liquid Viscosity", fsCharacteristic.Viscosity);

        public static fsParameterIdentifier SurfaceTensionLiquidInCake = 
            new fsParameterIdentifier("sigma", "Surface Tension Liquid in Cake", fsCharacteristic.SurfaceTension);

        public static fsParameterIdentifier StandardCapillaryPressure = 
            new fsParameterIdentifier("pke_st", "Standard Capillary Pressure", fsCharacteristic.Pressure);

        public static fsParameterIdentifier CapillaryPressure = 
            new fsParameterIdentifier("pke", "Capillary Pressure", fsCharacteristic.Pressure);

        public static fsParameterIdentifier DryCakeDensity0 =
            new fsParameterIdentifier("rho_cd0", "Dry cake density (Dp=1 bar)", fsCharacteristic.Density);

        public static fsParameterIdentifier DryCakeDensity =
            new fsParameterIdentifier("rho_cd", "Dry cake density", fsCharacteristic.Density);

        public static fsParameterIdentifier CakeWetDensity0 =
            new fsParameterIdentifier("rho_cw0", "Cake wet density 0", fsCharacteristic.Density);

        public static fsParameterIdentifier CakeWetDensity =
            new fsParameterIdentifier("rho_cw", "Cake wet density", fsCharacteristic.Density);

        public static fsParameterIdentifier MotherLiquidDensity = 
            new fsParameterIdentifier("rho", "Density of Mother Liquid", fsCharacteristic.Density);

        public static fsParameterIdentifier LiquidDensity = 
            new fsParameterIdentifier("rho_l", "Liquid Density", fsCharacteristic.Density);

        public static fsParameterIdentifier SolidsDensity = 
            new fsParameterIdentifier("rho_s", "Solids Density", fsCharacteristic.Density);

        public static fsParameterIdentifier SuspensionDensity = new fsParameterIdentifier("rho_sus",
                                                                                          "Suspension Density",
                                                                                          fsCharacteristic.Density);

        public static fsParameterIdentifier SuspensionSolidsMassFraction = new fsParameterIdentifier("Cm",
                                                                                                     "Suspension Solids Mass Fraction",
                                                                                                     fsCharacteristic.
                                                                                                         Concentration);

        public static fsParameterIdentifier SuspensionSolidsVolumeFraction = new fsParameterIdentifier("Cv",
                                                                                                       "Suspension Solids Volume Fraction",
                                                                                                       fsCharacteristic.
                                                                                                           Concentration);

        public static fsParameterIdentifier SuspensionSolidsConcentration = new fsParameterIdentifier("C",
                                                                                                      "Suspension Solids Concentration",
                                                                                                      fsCharacteristic.
                                                                                                          SolidsConcentration);

        public static fsParameterIdentifier CakePorosity0 = new fsParameterIdentifier("eps0", "Cake Porosity 0",
                                                                                      fsCharacteristic.Concentration);

        public static fsParameterIdentifier Kappa0 = new fsParameterIdentifier("kappa0");
        public static fsParameterIdentifier Ne = new fsParameterIdentifier("ne");

        public static fsParameterIdentifier CakePermeability0 = new fsParameterIdentifier("Pc0", "Cake Permeability0",
                                                                                          fsCharacteristic.
                                                                                              CakePermeability);

        public static fsParameterIdentifier CakeResistance0 = new fsParameterIdentifier("rc0", "Cake Resistance0",
                                                                                        fsCharacteristic.CakeResistance);

        public static fsParameterIdentifier CakeCompressibility = new fsParameterIdentifier("nc", "Cake Compressibility");

        public static fsParameterIdentifier CakeResistanceAlpha0 = 
            new fsParameterIdentifier("alpha0", "Cake Resistance0", fsCharacteristic.CakeResistanceAlpha);

        public static fsParameterIdentifier CakePlusMediumPermeability =
           new fsParameterIdentifier("Pc*", "'Cake + Medium' Permeability", fsCharacteristic.CakePermeability);

        public static fsParameterIdentifier CakePlusMediumResistance =
            new fsParameterIdentifier("rc*", "'Cake + Medium' Resistance", fsCharacteristic.CakeResistance);

        public static fsParameterIdentifier CakePlusMediumResistanceAlpha =
            new fsParameterIdentifier("alpha*", "'Cake + Medium' Resistance", fsCharacteristic.CakeResistanceAlpha);

        public static fsParameterIdentifier FilterMediumResistanceHce0 = 
            new fsParameterIdentifier("hce0", "Filter Medium Resistance hce0", fsCharacteristic.CakeHeight);

        public static fsParameterIdentifier FilterMediumResistanceRm0 = 
            new fsParameterIdentifier("Rm0", fsCharacteristic.FilterMediumResistance);

        public static fsParameterIdentifier FilterMediumResistanceHce =
            new fsParameterIdentifier("hce", "Filter Medium Resistance hce", fsCharacteristic.CakeHeight);

        public static fsParameterIdentifier FilterMediumResistanceRm =
            new fsParameterIdentifier("Rm", fsCharacteristic.FilterMediumResistance);

        public static fsParameterIdentifier SolutesConcentrationInMotherLiquid = 
            new fsParameterIdentifier("C_sol", "Solutes Concentration in Mother Liquid", fsCharacteristic.SolidsConcentration);

        public static fsParameterIdentifier SolutesMassFractionInMotherLiquid = new fsParameterIdentifier("Cm_sol",
                                                                                                          "Solutes Mass Fraction in Mother Liquid",
                                                                                                          fsCharacteristic
                                                                                                              .
                                                                                                              Concentration);

        public static fsParameterIdentifier SolutesConcentrationInCakeLiquid = new fsParameterIdentifier("C_sol",
                                                                                                         "Solutes Concentration in Cake Liquid",
                                                                                                         fsCharacteristic
                                                                                                             .
                                                                                                             SolidsConcentration);

        public static fsParameterIdentifier SolutesMassFractionInLiquid = new fsParameterIdentifier("Cm_sol",
                                                                                                    "Solutes Mass Fraction in Liquid",
                                                                                                    fsCharacteristic.
                                                                                                        Concentration);

        public static fsParameterIdentifier LiquidWashOutMassFraction = new fsParameterIdentifier("Cwm",
                                                                                                  "Liquid Wash Out Mass Fraction",
                                                                                                  fsCharacteristic.
                                                                                                      Concentration);

        public static fsParameterIdentifier LiquidWashOutConcentration = new fsParameterIdentifier("Cw",
                                                                                                   "Liquid Wash Out Concentration",
                                                                                                   fsCharacteristic.
                                                                                                       SolidsConcentration);

        public static fsParameterIdentifier CakeSaturation = new fsParameterIdentifier("S", "Cake Saturation",
                                                                                       fsCharacteristic.Concentration);

        public static fsParameterIdentifier CakeMoistureContent =
            new fsParameterIdentifier("RF", "Cake Moisture Content", fsCharacteristic.Concentration);

        public static fsParameterIdentifier CakeMoistureContentRf0 =
            new fsParameterIdentifier("Rf0", "Cake Moisture Content 0", fsCharacteristic.Concentration);

        public static fsParameterIdentifier CakeMoistureContentRf =
            new fsParameterIdentifier("Rf", "Cake Moisture Content", fsCharacteristic.Concentration);

        public static fsParameterIdentifier CakeWetMassSolidsFractionRs0 =
            new fsParameterIdentifier("Rs0", "Cake wet mass solids fraction 0", fsCharacteristic.Concentration);

        public static fsParameterIdentifier CakeWetMassSolidsFractionRs =
            new fsParameterIdentifier("Rs", "Cake wet mass solids fraction", fsCharacteristic.Concentration);

        public static fsParameterIdentifier CakeWashOutContent = 
            new fsParameterIdentifier("X", "Cake Wash Out Content", fsCharacteristic.CakeWashOutContent);

        public static fsParameterIdentifier LiquidMassForResuspension
            = new fsParameterIdentifier("Ml", "Liquid Mass for Resuspension", fsCharacteristic.Mass);

        public static fsParameterIdentifier Ph
            = new fsParameterIdentifier("pH", "pH of liquid after resuspension", fsCharacteristic.NoUnits);

        public static fsParameterIdentifier PHcake
            = new fsParameterIdentifier("pH_c", "pH of liquid in the cake", fsCharacteristic.NoUnits);

        public static fsParameterIdentifier MeanHeightRate
            = new fsParameterIdentifier("hc/tf", "Mean height rate", fsCharacteristic.Speed);

        public static fsParameterIdentifier HcOverTc
            = new fsParameterIdentifier("hc/tc", "Height rate rel to tc", fsCharacteristic.Speed);

        public static fsParameterIdentifier DiffHeightRate
            = new fsParameterIdentifier("dhc/dt", "Diff. height rate", fsCharacteristic.Speed);

        public static fsParameterIdentifier xg
            = new fsParameterIdentifier("xg", "Median particle size of the feed", fsCharacteristic.ParticleSize);

        public static fsParameterIdentifier sigma_g
            = new fsParameterIdentifier("sigma_g", "Standard deviation of the feed particle size distribution", fsCharacteristic.NoUnits);

        public static fsParameterIdentifier sigma_s
            = new fsParameterIdentifier("sigma_s", "Sdandard deviation of the Grade efficiency curve", fsCharacteristic.NoUnits);

        public static fsParameterIdentifier rf
            = new fsParameterIdentifier("rf", "Underflow volume rate to feed volume flow rate", fsCharacteristic.Concentration);

        public static fsParameterIdentifier UnderflowSolidsMassFraction
                   = new fsParameterIdentifier("cmu", "Underflow solids mass fraction", fsCharacteristic.Concentration);

        public static fsParameterIdentifier DuOverD
                   = new fsParameterIdentifier("Du/D", "Underflow diameter to cyclone diameter", fsCharacteristic.NoUnits);

        public static fsParameterIdentifier ReducedCutSize
                   = new fsParameterIdentifier("x’50", "Reduced cut size", fsCharacteristic.ParticleSize);

        public static fsParameterIdentifier OverflowParticleSize
                   = new fsParameterIdentifier("x0i", "Overflow particle size", fsCharacteristic.ParticleSize);

        public static fsParameterIdentifier OverflowSolidsMassFraction
                   = new fsParameterIdentifier("cmo", "Overflow solids mass fraction", fsCharacteristic.Concentration);

        public static fsParameterIdentifier NumberOfCyclones
            = new fsParameterIdentifier("n", "Number of Cyclones", fsCharacteristic.NoUnits);

        public static fsParameterIdentifier FeedVolumeFlowRate
            = new fsParameterIdentifier("Q", "Feed volume flow rate", fsCharacteristic.VolumeFlowrate);

        public static fsParameterIdentifier FeedSolidsMassFlowRate
            = new fsParameterIdentifier("Qm", "Feed solids mass flow rate", fsCharacteristic.MassFlowrate);

        public static fsParameterIdentifier Alpha1
            = new fsParameterIdentifier("alpha1", fsCharacteristic.NoUnits);

        public static fsParameterIdentifier Alpha2
            = new fsParameterIdentifier("alpha2",  fsCharacteristic.NoUnits);

        public static fsParameterIdentifier Alpha3
            = new fsParameterIdentifier("alpha3",  fsCharacteristic.NoUnits);

        public static fsParameterIdentifier Beta1
            = new fsParameterIdentifier("Beta1",  fsCharacteristic.NoUnits);

        public static fsParameterIdentifier Beta2
            = new fsParameterIdentifier("Beta2",  fsCharacteristic.NoUnits);

        public static fsParameterIdentifier Beta3
            = new fsParameterIdentifier("Beta3",  fsCharacteristic.NoUnits);

        public static fsParameterIdentifier Gamma1
            = new fsParameterIdentifier("Gamma1", fsCharacteristic.NoUnits);

        public static fsParameterIdentifier Gamma2
            = new fsParameterIdentifier("Gamma2", fsCharacteristic.NoUnits);

        public static fsParameterIdentifier Gamma3
            = new fsParameterIdentifier("Gamma3", fsCharacteristic.NoUnits);

        public static fsParameterIdentifier bigLOverD 
            = new fsParameterIdentifier("L/D", fsCharacteristic.NoUnits);

        public static fsParameterIdentifier smallLOverD  
            = new fsParameterIdentifier("l/D", fsCharacteristic.NoUnits);

        public static fsParameterIdentifier DiOverD
            = new fsParameterIdentifier("Di/D", fsCharacteristic.NoUnits);

        public static fsParameterIdentifier DoOverD
            = new fsParameterIdentifier("Do/D", fsCharacteristic.NoUnits);

        public static fsParameterIdentifier StokesNumber
            = new fsParameterIdentifier("Stk’", "Stokes number", fsCharacteristic.NoUnits);

        public static fsParameterIdentifier EulerNumber
            = new fsParameterIdentifier("Eu", "Euler number", fsCharacteristic.NoUnits);

        public static fsParameterIdentifier ReynoldsNumber
            = new fsParameterIdentifier("Re", "Reynolds number", fsCharacteristic.NoUnits);

        public static fsParameterIdentifier AverageVelocity
            = new fsParameterIdentifier("v", "Average velocity", fsCharacteristic.Speed);

        public static fsParameterIdentifier TotalEfficiency
            = new fsParameterIdentifier("ET", "Total efficiency", fsCharacteristic.Concentration);

        public static fsParameterIdentifier ReducedTotalEfficiency
            = new fsParameterIdentifier("E’T", "Reduced total efficiency", fsCharacteristic.Concentration);

        public static fsParameterIdentifier CycloneLength
            = new fsParameterIdentifier("L", "Cyclone length", fsCharacteristic.Length);

        public static fsParameterIdentifier LengthOfCylindricalPart
            = new fsParameterIdentifier("l", "Length of cylindrical part", fsCharacteristic.Length);
       
        public static fsParameterIdentifier InletDiameter
            = new fsParameterIdentifier("Di", "Inlet diameter", fsCharacteristic.Length);

        public static fsParameterIdentifier OutletDiameter
            = new fsParameterIdentifier("Do", "Outlet diameter", fsCharacteristic.Length);

        public static fsParameterIdentifier UnderflowDiameter
            = new fsParameterIdentifier("Du", "Underflow diameter", fsCharacteristic.Length);
        
        public static fsParameterIdentifier OverflowVolumeFlowRate
            = new fsParameterIdentifier("Qo", "Overflow volume flow rate", fsCharacteristic.VolumeFlowrate);

        public static fsParameterIdentifier OverflowMassFlowRate
            = new fsParameterIdentifier("Qmo", "Overflow mass flow rate", fsCharacteristic.MassFlowrate);

        public static fsParameterIdentifier OverflowSolidsMassFlowRate
            = new fsParameterIdentifier("Qmso", "Overflow solids mass flow rate", fsCharacteristic.MassFlowrate);

        public static fsParameterIdentifier UnderflowSolidsMassFlowRate
            = new fsParameterIdentifier("Qmsu", "Underflow solids mass flow rate", fsCharacteristic.MassFlowrate);

        public static fsParameterIdentifier UnderflowVolumeFlowRate
            = new fsParameterIdentifier("Qu", "Underflow volume flow rate", fsCharacteristic.VolumeFlowrate);

        public static fsParameterIdentifier UnderflowMassFlowRate
            = new fsParameterIdentifier("Qmu", "Underflow mass flow rate", fsCharacteristic.MassFlowrate);

        public static fsParameterIdentifier OverflowMeanParticleSize
            = new fsParameterIdentifier("x50,o", "Overflow mean particle size", fsCharacteristic.ParticleSize);

        public static fsParameterIdentifier UnderflowMeanParticleSize
            = new fsParameterIdentifier("x50,u", "Underflow mean particle size", fsCharacteristic.ParticleSize);

        public static fsParameterIdentifier OverflowSolidsVolumeFraction
            = new fsParameterIdentifier("cvo", fsCharacteristic.Concentration);

        public static fsParameterIdentifier UnderflowSolidsVolumeFraction
            = new fsParameterIdentifier("cvu", fsCharacteristic.Concentration);

        public static fsParameterIdentifier OverflowSolidsConcentration
            = new fsParameterIdentifier("Co", fsCharacteristic.SolidsConcentration);

        public static fsParameterIdentifier UnderflowSolidsConcentration
            = new fsParameterIdentifier("Cu", fsCharacteristic.SolidsConcentration);

        #endregion
    }
}

