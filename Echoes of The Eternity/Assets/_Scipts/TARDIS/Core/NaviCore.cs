using System.Collections;
using Unity.Mathematics;
using UnityEngine;

namespace TARDIS.Core
{
    public class NaviCore : MonoBehaviour
    {
        public CoreManager coreManager;
        public UtilityCore utilityCore;

        private void Start()
        {
            utilityCore = coreManager.utilityCore;
        }

        public int4 targetCoords; // gets updated by delta circuit
        public int4 currentCoords; // changes in this script - used for display and the int4 that moves from lastcoords to targetcoords
        public int4 startingCoords; // this is for lerp start
        public int4 lastCoords; // this is both for fast return

        public void UpdateTargetCoords(int4 target)
        {
            targetCoords = target;
        }

        public void BeginFlightNavigation()
        {
            // Logic to start navigating from startingCoords to targetCoords
            startingCoords = currentCoords;
            lastCoords = currentCoords;
            StartCoroutine(TravelToTarget());
        }

        private enum TempSpeedModeEnum { Drift = 1, Normal = 10, Medium = 15, Maximum = 25 }

        private IEnumerator TravelToTarget() 
        {
            // Calculate the total distance between startingCoords and targetCoords
            float totalDistance = math.distance((float4)startingCoords, (float4)targetCoords);

            // Loop until the currentCoords match the targetCoords
            while (!math.all(currentCoords == targetCoords))
            {
                // Calculate the direction to move in each axis
                int4 direction = new int4(
                    targetCoords.x > currentCoords.x ?1 : (targetCoords.x < currentCoords.x ? -1 :0),
                    targetCoords.y > currentCoords.y ?1 : (targetCoords.y < currentCoords.y ? -1 :0),
                    targetCoords.z > currentCoords.z ?1 : (targetCoords.z < currentCoords.z ? -1 :0),
                    targetCoords.w > currentCoords.w ?1 : (targetCoords.w < currentCoords.w ? -1 :0)
                );

                // Update currentCoords by moving one step in the calculated direction
                currentCoords += direction;

                // Calculate the remaining distance to the target
                float remainingDistance = math.distance((float4)currentCoords, (float4)targetCoords);

                // Log progress for debugging
                Debug.Log($"Traveling: CurrentCoords = {currentCoords}, RemainingDistance = {remainingDistance}");

                // Simulate fuel consumption based on distance and speed
                float fuelConsumption = GetFuelConsumptionRate(TempSpeedModeEnum.Normal) * math.distance((float4)direction, float4.zero);
                utilityCore.ConsumeFuel(fuelConsumption);

                // Check if there is enough fuel to continue
                if (utilityCore.currentFuel <=0)
                {
                    Debug.LogWarning("Out of fuel! Navigation halted.");
                    yield break;
                }

                // Wait for the next step based on the speed mode
                yield return new WaitForSeconds(1f / (float)TempSpeedModeEnum.Normal);
            }

            Debug.Log("Navigation complete. Target reached.");
        }

        private float GetFuelConsumptionRate(TempSpeedModeEnum speedMode)
        {
            switch (speedMode)
            {
                case TempSpeedModeEnum.Drift: return 0.1f; // 1 fuel per 10 distance units
                case TempSpeedModeEnum.Normal: return 0.2f; // 1 fuel per 5 distance units
                case TempSpeedModeEnum.Medium: return 0.33f; // 1 fuel per 3 distance units
                case TempSpeedModeEnum.Maximum: return 1f; // 1 fuel per 1 distance unit
                default: return 1f;
            }
        }
    }
}