using System.Collections;
using TARDIS.Main;
using TMPro;
using UnityEngine;
using static TARDIS.Main._42Main;

namespace TARDIS.Core
{
    public class FlightCore : MonoBehaviour
    {
        public CoreManager coreManager;

        bool throttleActive = false;
        bool handbrakeActive = false;

        public _42Audio audioManager;

        // to use the instance of _42Main, use "_42Main.Instance"
        // to get the flightstate, use "_42Main.Instance.currentFlightState"

        public void ThrottleUpdate(bool bole)
        {
            throttleActive = bole;

            switch (Instance.currentFlightState)
            {
                case ShipFlightState.InFlight:
                    // Throttling up or down does nothing while in flight
                    Debug.Log("Throttle input ignored: TARDIS is already in flight.");
                    break;

                case ShipFlightState.Dematerialising:
                    if (!throttleActive)
                    {
                        // If throttle is deactivated during dematerialisation, nothing much happens
                    }
                    break;

                case ShipFlightState.Rematerialising:
                    if (throttleActive)
                    {
                        // if throttle is activated during rematerialisation, we try to do a hop back to flight
                        Debug.Log("Throttle activated during rematerialisation: Aborting rematerialisation and returning to flight.");
                    }
                    break;

                case ShipFlightState.Parked:
                    if (throttleActive)
                    {
                        if (handbrakeActive)
                        {
                            Debug.Log("Throttle activated with handbrake on: Deploying demat alarm.");
                            audioManager.PlayDematAlarm();
                        }
                        else
                        {
                            Debug.Log("Throttle activated with handbrake off: Starting dematerialisation.");
                            Instance.currentFlightState = ShipFlightState.Dematerialising;
                            StartCoroutine(Dematerialise());
                        }
                    }
                    else
                    {
                        Debug.Log("Throttle deactivated: Halting flight progress and stopping alarms.");
                        audioManager.StopDematAlarm();
                    }
                    break;
            }
        }

        public void HandbrakeUpdate(bool bole)
        {
            handbrakeActive = bole;

            switch (Instance.currentFlightState)
            {
                case ShipFlightState.Parked:
                    if (handbrakeActive)
                    {
                        Debug.Log("Handbrake activated while parked.");
                        // Add logic for not much, since we're already parked
                    }
                    break;

                case ShipFlightState.Dematerialising:
                    if (handbrakeActive)
                    {
                        Debug.Log("Handbrake activated during dematerialisation: Aborting dematerialisation.");
                        Instance.currentFlightState = ShipFlightState.Parked;
                        // Add logic to abort dematerialisation
                    }
                    break;

                case ShipFlightState.Rematerialising:
                    if (handbrakeActive)
                    {
                        Debug.Log("Handbrake activated during rematerialisation: Aborting rematerialisation.");
                        Instance.currentFlightState = ShipFlightState.InFlight;
                        // Add logic to abort rematerialisation
                    }
                    break;

                case ShipFlightState.InFlight:
                    if (handbrakeActive)
                    {
                        if (!throttleActive)
                        {
                            Debug.Log("Handbrake activated while throttling: Initiating emergency landing sequence.");
                            Instance.currentFlightState = ShipFlightState.Rematerialising;
                            StartCoroutine(Rematerialise());
                        }
                        else
                        {
                            Debug.Log("Handbrake activated: Engaging hover mode.");
                            // Add logic for hover mode
                        }
                    }
                    else
                    {
                        Debug.Log("Handbrake deactivated: Resuming normal operations.");
                        // Add logic for resuming normal operations
                    }
                    break;
            }
        }

        public IEnumerator Dematerialise()
        {
            Debug.Log("Starting dematerialization sequence...");
            audioManager.PlayTakeoffSound();
            yield return new WaitForSeconds(11); // Simulate time taken to dematerialize
            Instance.currentFlightState = ShipFlightState.InFlight;
            audioManager.PlayFlightLoop();
            Debug.Log("Dematerialization complete. TARDIS is now in flight.");
            coreManager.naviCore.BeginFlightNavigation();
        }

        public IEnumerator Rematerialise()
        {
            Debug.Log("Starting rematerialization sequence...");
            audioManager.PlayLandingSound();
            yield return new WaitForSeconds(11); // Simulate time taken to rematerialize
            audioManager.PlayLandingNotify();
            Instance.currentFlightState = ShipFlightState.Parked;
            Debug.Log("Rematerialization complete. TARDIS is now grounded.");
        }

        public TMP_Text tempdebugtext;

        void Update()
        {
            if (tempdebugtext != null)
            {
                tempdebugtext.text = $"{Instance.currentFlightState}";
            }
        }
    }
}