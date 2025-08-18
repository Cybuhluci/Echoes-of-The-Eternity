using Autodesk.Fbx;
using Luci.TARDIS;
using Luci.TARDIS.ConsoleSystems;
using Luci.TARDIS.ConsoleSystems.Demat;
using Luci.TARDIS.EngineSystems;
using System;
using System.Collections;
using System.Drawing;
using System.IO;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;
using static UnityEngine.Rendering.DebugUI;

namespace Luci.TARDIS.EngineSystems
{
    /// <summary>
    /// DematerialisationCircuit is a subsystem controller for the TARDIS console's dematerialisation circuit.
    /// It manages dematerialisation and rematerialisation processes.
    /// </summary>

    public class DematerialisationCircuit : TARDISEngineController
    {
        // --- Takeoff Control Sequences ---

        //Waypoint/Programmed Flight
        //Select a waypoint using the Astronav Computer.
        //flip the plotter switch to input coords into navcom.
        //Disengage the Time-Rotor Handbrake.
        //Crank up the Space-Time Throttle.

        //Coordinate Flight
        //Input w, x, y, z coordinates into the Coordinate Plotter whenever.
        //Flip the Plotter Switch to input coords in navcom.
        //Disengage the Time-Rotor Handbrake.
        //Crank up the Space-Time Throttle.

        //Fast Return
        //Enable the Fast Return Switch.
        //Disengage the Time-Rotor Handbrake.
        //Crank up the Space-Time Throttle.

        //Drift Flight
        //Press the Epsilon Circuit Button.
        //Disengage the Time-Rotor Handbrake.
        //Set the Space-Time Throttle to a low speed.
        //Select a desired drift path using the Coordinate Plotters (resets coordinates for a short bit so player can use it to make a 3d-vector line).

        //Emergency No-Destination Flight
        //Disengage the Time-Rotor Handbrake.
        //Crank up the Space-Time Throttle.
        // -- to then enter destination flight, start another flight as normal

        public void TryTakeoff() // this method is called by the SpaceTimeThrottle
        {
            // Fail immediately if the TARDIS isn't in a takeoff state.
            if (tardisMain.currentTARDISState != TARDISMain.TARDIFlightState.GroundLanded &&
                tardisMain.currentTARDISState != TARDISMain.TARDIFlightState.SpaceLanded)
            {
                Debug.LogWarning("TARDIS is not in a state to take off.");
                FailDematerialisation();
                return;
            }

            // Fail immediately if the universal takeoff conditions aren't met.
            if (consoleManager.timeRotorHandbrake.IsCircuitActive || consoleManager.doorControl.IsDoorOpen)
            {
                Debug.LogWarning("TARDIS console or engine not ready for takeoff.");
                FailDematerialisation();
                return;
            }

            // All conditions have been met, now handle state-specific logic.
            switch (tardisMain.currentTARDISState)
            {
                case TARDISMain.TARDIFlightState.GroundLanded:
                    Debug.Log("TARDIS is ready for takeoff.");
                    StartDematerialization();
                    break;

                case TARDISMain.TARDIFlightState.SpaceLanded:
                    if (consoleManager.epsilonCircuit.IsCircuitActive)
                    {
                        // Start Drift Flight
                        Debug.Log("Starting Drift Flight.");
                    }
                    else
                    {
                        // Begin normal Dematerialization Sequence
                        Debug.Log("TARDIS is ready for takeoff from space.");
                        StartDematerialization();
                    }
                    break;
            }
        }

        public void TryLanding() // this method is called by the TimeRotorHandbrake
        {
            // Fail immediately if the TARDIS isn't in a flight state.
            if (tardisMain.currentTARDISState != TARDISMain.TARDIFlightState.VortexFlying &&
                tardisMain.currentTARDISState != TARDISMain.TARDIFlightState.SpatialFlying &&
                tardisMain.currentTARDISState != TARDISMain.TARDIFlightState.DriftFlying)
            {
                Debug.LogWarning("TARDIS is not in a state to land.");
                return;
            }

            if (consoleManager.spaceTimeThrottle.CurrentThrottleValue != 0 || consoleManager.doorControl.IsDoorOpen)
            {
                Debug.LogWarning("TARDIS console or engine not ready for landing.");
                FailMaterialization();
                return;
            }

            // All conditions have been met, now handle state-specific logic.
            StartMaterialization();
        }

        public void StartDematerialization()
        {
            StartCoroutine(DematerializeCoroutine());
        }

        private IEnumerator DematerializeCoroutine()
        {
            engineManager.soundSystem.PlaySound(TARDISSoundSystem.TARDISAudioSourceKey.TakeoffWin);
            tardisMain.currentTARDISState = TARDISMain.TARDIFlightState.Dematerializing;

            // Wait for 18 seconds
            yield return new WaitForSeconds(18f);

            // This code will only run after the 18-second wait
            engineManager.soundSystem.PlaySound(TARDISSoundSystem.TARDISAudioSourceKey.FlightLoopNormal);
            tardisMain.currentTARDISState = TARDISMain.TARDIFlightState.SpatialFlying;
        }

        public void FailDematerialisation()
        {
            engineManager.soundSystem.PlaySound(TARDISSoundSystem.TARDISAudioSourceKey.TakeoffFail);
            // No wait is needed here, so a coroutine isn't necessary.
        }

        public void StartMaterialization()
        {
            StartCoroutine(MaterializeCoroutine());
        }

        private IEnumerator MaterializeCoroutine()
        {
            //engineManager.soundSystem.PlayLandingWin();
            engineManager.soundSystem.PlaySound(TARDISSoundSystem.TARDISAudioSourceKey.LandingWin);
            tardisMain.currentTARDISState = TARDISMain.TARDIFlightState.Materializing;

            // Wait for 18 seconds
            yield return new WaitForSeconds(18f);

            // This code will only run after the 18-second wait
            engineManager.soundSystem.StopSound(TARDISSoundSystem.TARDISAudioSourceKey.FlightLoopNormal);
            tardisMain.currentTARDISState = TARDISMain.TARDIFlightState.GroundLanded;
        }

        public void FailMaterialization()
        {
            // This fails the land, but the TARDIS is still materializing.
            // We need to wait for the animation to end before the TARDIS flies away again.
            StartCoroutine(FailMaterializationCoroutine());
        }

        private IEnumerator FailMaterializationCoroutine()
        {
            engineManager.soundSystem.PlaySound(TARDISSoundSystem.TARDISAudioSourceKey.LandingFail);
            tardisMain.currentTARDISState = TARDISMain.TARDIFlightState.Materializing;

            // Wait for 18 seconds
            yield return new WaitForSeconds(18f);

            // This code runs after the 18-second wait, so the TARDIS flies away
            tardisMain.currentTARDISState = TARDISMain.TARDIFlightState.GroundLanded;
        }
    }
}