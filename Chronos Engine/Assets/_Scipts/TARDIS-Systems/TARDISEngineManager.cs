using UnityEngine;
using Luci.TARDIS;
using Luci.TARDIS.ConsoleSystems;

namespace Luci.TARDIS.EngineSystems
{
    public class TARDISEngineManager : MonoBehaviour
    {
        [Header("Essential Engine Subsystems")]
        public DematerialisationCircuit dematCircuit;
        public FluidLinks fluidlinks;

        [Header("Optional Engine Subsytems")] 
        public NavigationalComputer navigationcom;
        public ChameleonCircuit chameleon;
        public InterstitialAntennae antennae;
        public TemporalGraceUnit temporalgrace;
        public ShieldGenerator shieldgenerator;
        public Stabilisers stabilisers;

        [Header("Newbie Engine Subsystems")]
        public DesperationCircuit desperation; 
        public LifeSupport lifeSupport;
        public BackupGenerator backupGenerator; 
        public GravitationalCircuit gravitational;

        [Header("TARDIS Sound System")]
        public TARDISSoundSystem soundSystem;

        void Awake()
        {

        }
    }
}