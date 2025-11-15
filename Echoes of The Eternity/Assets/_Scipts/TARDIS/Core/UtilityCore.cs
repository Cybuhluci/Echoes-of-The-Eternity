using UnityEngine;

namespace TARDIS.Core
{
    public class UtilityCore : MonoBehaviour
    {
        public float currentFuel;
        public float maxFuel;

        public void ConsumeFuel(float amount)
        {
            currentFuel = Mathf.Max(0, currentFuel - amount);
            Debug.Log($"Fuel consumed: {amount}. Remaining fuel: {currentFuel}");
        }

        public void Refuel(float amount)
        {
            currentFuel = Mathf.Min(maxFuel, currentFuel + amount);
            Debug.Log($"Fuel added: {amount}. Current fuel: {currentFuel}");
        }
    }
}