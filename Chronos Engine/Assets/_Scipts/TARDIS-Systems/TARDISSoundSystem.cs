using Luci.TARDIS;
using Luci.TARDIS.ConsoleSystems;
using Luci.TARDIS.EngineSystems;
using System.Collections.Generic;
using UnityEngine;

namespace Luci.TARDIS.EngineSystems
{
    /// <summary>
    /// TARDISSoundSystem is a subsystem controller for the TARDIS console's sound system.
    /// It manages the sound effects and audio feedback for the TARDIS operations.
    /// </summary>

    public class TARDISSoundSystem : TARDISEngineController
    {
        public enum TARDISAudioSourceKey
        {
            ButtonClick,
            TakeoffWin,
            TakeoffFail,
            FlightLoopNormal,
            FlightLoopBroken,
            LandingWin,
            LandingFail,
            TardisHum,
            Warning,
            Alarm,
            TimeVortex,
            VortexLightning,
            TakeoffAbort,
            // Button sounds
            Button1, Button2, Button3, Button4, Button5, Button6, Button7, Button8, Button9, Button10,
            Button11, Button12, Button13, Button14, Button15, Button16, Button17, Button18, Button19, Button20,
            DoorOpen, DoorClose,
            DoorLock, DoorUnlock,
            MonitorBeep,
            MonitorMove,
            PhoneRing,
            Keyboard1, Keyboard2, Keyboard3, Keyboard4, Keyboard5,
            // Dematerialisation Circuit Subsystems
            TimeRotorHandbrakeOn, TimeRotorHandbrakeOff,
            SpaceTimeThrottleOn, SpaceTimeThrottleOff,
            FastReturnOn, FastReturnOff,
            TranswarpResonatorOn, TranswarpResonatorOff,
            ZetaCircuitOn, ZetaCircuitOff,
            // Navigational Computer Subsystems
            TelepathicCircuitOn, TelepathicCircuitOff,
            HelmicRegulatorOn, HelmicRegulatorOff,
            LandingTypeControllerOn, LandingTypeControllerOff,
            MonitorOn, MonitorOff,
            EpsilonCircuitOn, EpsilonCircuitOff,
            DeltaCircuitOn, DeltaCircuitOff,
            // Fluid Link Subsystems
            RefuellerOn, RefuellerOff,
            ExotronicCircuitOn, ExotronicCircuitOff,
            FluxRegulatorOn, FluxRegulatorOff,
            ArtronFluidLinkOn, ArtronFluidLinkOff,
            DataFluidLinkOn, DataFluidLinkOff,
            VortexFluidLinkOn, VortexFluidLinkOff,
            ArtronMercurialLinkOn, ArtronMercurialLinkOff,
            DataMercurialLinkOn, DataMercurialLinkOff,
            VortexMercurialLinkOn, VortexMercurialLinkOff,
            OrthogonalEngineFilterOn, OrthogonalEngineFilterOff,
            // Interstitial Antennae Subsystems
            CommunicatorOn, CommunicatorOff,
            DistressSignalInterceptorOn, DistressSignalInterceptorOff,
            UpsilonCircuitOn, UpsilonCircuitOff,
            HyperionCoreShaftOn, HyperionCoreShaftOff,
            // Chameleon Circuit Subsystems
            ChameleonCircuitOn, ChameleonCircuitOff,
            // Shield Generator Subsystems
            ExteriorBulkheadLockOn, ExteriorBulkheadLockOff,
            PhysicalLockOn, PhysicalLockOff,
            ShieldsOn, ShieldsOff,
            HostileActionDisplacementSystemOn, HostileActionDisplacementSystemOff,
            // Stabiliser Subsystems
            StabilisersOn, StabilisersOff,
            AntiGravsOn, AntiGravsOff,
            SpatialAnchorOn, SpatialAnchorOff,
            InertialDampenersOn, InertialDampenersOff,
            GammaCircuitOn, GammaCircuitOff,
            // Temporal Grace Unit Subsystems
            TemporalGraceUnitOn, TemporalGraceUnitOff,
            WibblyLeverOn, WibblyLeverOff,
            ChronalFieldEmitterOn, ChronalFieldEmitterOff,
            TemporalAnomalyAlarmOn, TemporalAnomalyAlarmOff,
            // Backup Generator Subsystems
            BackupGeneratorOn, BackupGeneratorOff,
            EmergencyPowerOn, EmergencyPowerOff,
            PhotonAcceleratorOn, PhotonAcceleratorOff,
            // Gravitational Circuit Subsystems
            GravitationalCircuitOn, GravitationalCircuitOff,
            GravitationalFieldStabiliserOn, GravitationalFieldStabiliserOff,
            // Desperation Module Subsystems (AKA names)
            SiegeModeOn, SiegeModeOff,
            IsomorphicSecurityOn, IsomorphicSecurityOff,
            CloakModeOn, CloakModeOff,
            HailMaryOn, HailMaryOff,
            EmergencyLandOn, EmergencyLandOff,
            ElectricDischargeOn, ElectricDischargeOff,
            SelfDestructOn, SelfDestructOff,
            SelfRepairOn, SelfRepairOff,
            // Life Support Systems
            LifeSupportSystemsOn, LifeSupportSystemsOff,
            OxygenReplenisherOn, OxygenReplenisherOff,
            // Cloister and Damage
            Cloister1, Cloister2, Cloister3,
            Damage1, Damage2, Damage3, Damage4,
            // Phase
            PhaseEnabled, PhaseDisabled
        }

        [System.Serializable]
        public struct AudioSourceMapping
        {
            public TARDISAudioSourceKey key;
            public AudioSource source;
        }

        [Header("Audio Sources")]
        [SerializeField] private List<AudioSourceMapping> audioSourceMappings;

        // The dictionary to access your AudioSources by their key
        private Dictionary<TARDISAudioSourceKey, AudioSource> audioSources;

        private void Awake()
        {
            audioSources = new Dictionary<TARDISAudioSourceKey, AudioSource>();
            foreach (var mapping in audioSourceMappings)
            {
                audioSources[mapping.key] = mapping.source;
            }
        }

        public void PlaySound(TARDISAudioSourceKey soundKey)
        {
            if (audioSources.ContainsKey(soundKey))
            {
                audioSources[soundKey].Play();
            }
        }

        public void StopSound(TARDISAudioSourceKey soundKey)
        {
            if (audioSources.ContainsKey(soundKey))
            {
                audioSources[soundKey].Stop();
            }
        }
    }
}