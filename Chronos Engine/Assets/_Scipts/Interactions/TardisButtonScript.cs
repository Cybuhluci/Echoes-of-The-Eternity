using UnityEngine;
using UnityEngine.Events;
using Luci.TARDIS.EngineSystems;

namespace Luci.Interactions
{
    public class TardisButtonScript : MonoBehaviour, IInteractable
    {
        public TARDISEngineManager engineManager;

        [Header("Button Events")]
        public UnityEvent LeftInteraction;
        public UnityEvent RightInteraction;

        private void Start()
        {
            if (engineManager == null)
            {
                // Find the TARDIS Engine Manager in the scene if not assigned in Inspector
                engineManager = FindAnyObjectByType<TARDISEngineManager>();
            }
        }

        public void LeftInteract()
        {
            //Debug.Log("Button pressed!");
            LeftInteraction.Invoke();  // Calls whatever methods are assigned in the Inspector
            NotifyButtonPressed();
        }

        public void RightInteract()
        {
            //Debug.Log("RightInteract");
            RightInteraction.Invoke(); // Calls whatever methods are assigned in the Inspector
            NotifyButtonPressed();
        }

        public EInteractionType GetInteractionType()
        {
            return EInteractionType.InteractShort;
        }

        public void NotifyButtonPressed()
        {
            if (engineManager != null && engineManager.dematCircuit != null)
            {
                //engineManager.dematCircuit.OnConsoleButtonPressed(this);
            }
        }
    }
}
