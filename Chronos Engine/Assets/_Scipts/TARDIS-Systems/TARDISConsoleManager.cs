using UnityEngine;
using Luci.TARDIS;
using Luci.TARDIS.EngineSystems;
using Luci.TARDIS.ConsoleSystems.Demat;
using Luci.TARDIS.ConsoleSystems.Navcom;
using Luci.TARDIS.ConsoleSystems.Antennae;
using Luci.TARDIS.ConsoleSystems.Chameleon;
using Luci.TARDIS.ConsoleSystems.Shield;
using Luci.TARDIS.ConsoleSystems.Stabilisers;
using Luci.TARDIS.ConsoleSystems.Desperation;
using Luci.TARDIS.ConsoleSystems.FluidLink;

namespace Luci.TARDIS.ConsoleSystems
{
    public class TARDISConsoleManager : MonoBehaviour
    {
        [Header("Dematerialisation Circuit Subsystems")]
        public TimeRotorHandbrake timeRotorHandbrake; // time rotor handbrake
        public SpaceTimeThrottle spaceTimeThrottle; // space time throttle
        public FastReturn fastReturn; // fast return switch
        public ZetaCircuit zetaCircuit; // zeta circuit

        [Header("Navigational Computer Subsystems")]
        public VortexFlight vortexFlight; // vortex flight switch
        public DeltaCircuit deltaCircuit; // coordinate control switch
        public ExteriorFacing exteriorFacing; // exterior facing switch
        public EpsilonCircuit epsilonCircuit; // epsilon circuit

        [Header("Fluid Link Subsystems")]
        public Refueller refueller;
        public ExotronicCircuit exotronicCircuit; // exotronic circuit

        [Header("Interstitial Antennae Subsystems")]
        public Communicator communicator;

        [Header("Chameleon Circuit Subsytems")]
        public ChameleonSwitch chameleonCircuit;

        [Header("Shield Generator Subsystems")]
        public ShieldSystem shieldSystem;
        public ExatronicCircuit exatronicCircuit; // exatronic circuit
        public ExteriorBulkhead exteriorBulkhead; // exterior bulkhead
        public PhysicalLocking physicalLocking; // physical locking

        [Header("Stabiliser Subsystems")]
        public BlueStabilisers stabilisers;
        public GammaCircuit gammaCircuit; // gamma circuit

        [Header("Temporal Grace Unit Subsystems")]
        public GameObject temporalAnomalyAlarm;
        public UpsilonCircuit upsilonCircuit; // upsilon circuit

        [Header("Desperation Circuit Subsystems")]
        public ArtronDump artronDump;
        public HailMary hailMary;
        public IsomorphicSecurity isomorphicSecurity;
        public SelfDestructionSwitch selfDestructionSwitch;
        public ActiveCloaking activeCloaking;
        public SelfRepair selfRepair;

        [Header("Backup Generator Subsystems")]
        public GameObject emergencyPower;

        [Header("Gravitational Circuit Subsystems")]
        public GameObject antiGravs;

        [Header("Life Support Subsystems")]
        public GameObject Oxygenator;

        [Header("Extra Subsystems")]
        public TardisMonitor tardisMonitor;
        public TardisPower TardisPower;
        public ConsolePower consolePower;
        public DoorControl doorControl;

        void Awake()
        {

        }
    }
}