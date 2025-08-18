using UnityEngine;
using Luci.TARDIS.ConsoleSystems;
using Luci.TARDIS.EngineSystems;

namespace Luci.TARDIS
{
    // script to manage TETO's mood and emotional state
    // this will be used to control engine groaning, console lights, and other mood-related effects
    public class TETOMood : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField] private TARDISMain tardisMain;
        [SerializeField] private TARDISConsoleManager consoleManager;
        [SerializeField] private TARDISEngineManager engineManager;

        [Header("TARDIS Mood/Reputation")]
        [Tooltip("Current TARDIS emotional state, ranging from -512 (very negative) to 512 (very positive).")]
        [Range(-512f, 512f)] // Add a Range attribute for easier tuning in the Inspector
        [SerializeField] private float _tardisReputation = 0f; // Using float for more granular control

        // You can define thresholds for different mood states
        private const float HAPPY_THRESHOLD = 200f;
        private const float SAD_THRESHOLD = -200f;
        private const float ANGRY_THRESHOLD = -400f; // Example for an "Angry" state
        private const float ENRAGED_THRESHOLD = -500f; // Example for "Enraged"
        private const float CONTENT_THRESHOLD = 50f; // Example for slightly positive
        private const float JOYFUL_THRESHOLD = 400f; // Example for very positive

        // Broad mood states derived from the reputation
        public enum MoodState { Enraged, Angry, Sad, Neutral, Content, Happy, Joyful }
        public MoodState currentMood { get; private set; } // Read-only property

        // Optional: Events for when the mood changes significantly
        public event System.Action<MoodState> OnMoodChange;

        private void Awake()
        {
            // Find dependencies if not set in Inspector (good for robust initialization)
            if (tardisMain == null) tardisMain = FindAnyObjectByType<TARDISMain>();
            if (consoleManager == null && tardisMain != null) consoleManager = tardisMain.consoleManager;
            if (engineManager == null && tardisMain != null) engineManager = tardisMain.engineManager;

            // Initialize mood based on starting reputation
            UpdateCurrentMoodState();
        }

        // --- Public methods to interact with TARDIS reputation ---

        /// <summary>
        /// Adjusts the TARDIS's reputation by a given amount.
        /// </summary>
        /// <param name="amount">The amount to add or subtract from reputation. Positive for good actions, negative for bad.</param>
        public void AdjustReputation(float amount)
        {
            float oldReputation = _tardisReputation;
            _tardisReputation = Mathf.Clamp(_tardisReputation + amount, -512f, 512f);

            // Only update effects if the mood state actually changes
            UpdateCurrentMoodState();

            if (oldReputation != _tardisReputation)
            {
                Debug.Log($"TARDIS Mood: Reputation changed by {amount}. New Reputation: {_tardisReputation}. Current Mood: {currentMood}");
            }
        }

        /// <summary>
        /// Sets the TARDIS's reputation to a specific value.
        /// </summary>
        /// <param name="newValue">The exact reputation value to set.</param>
        public void SetReputation(float newValue)
        {
            float oldReputation = _tardisReputation;
            _tardisReputation = Mathf.Clamp(newValue, -512f, 512f);

            // Only update effects if the mood state actually changes
            UpdateCurrentMoodState();

            if (oldReputation != _tardisReputation)
            {
                Debug.Log($"TARDIS Mood: Reputation set to {newValue}. Current Mood: {currentMood}");
            }
        }

        // --- Internal methods for managing mood effects ---

        /// <summary>
        /// Determines the current MoodState based on _tardisReputation and updates effects.
        /// </summary>
        private void UpdateCurrentMoodState()
        {
            MoodState newMood = DetermineMoodState(_tardisReputation);

            if (newMood != currentMood)
            {
                currentMood = newMood;
                ApplyMoodEffects();
                OnMoodChange?.Invoke(currentMood); // Invoke event if mood truly changed
            }
            // Even if mood state didn't change, subtle effects might still vary with reputation within a state.
            // Consider calling ApplyMoodEffects() every frame if you want continuous nuanced changes,
            // or if specific effects depend directly on the _tardisReputation value itself.
            // For now, it only updates when the *state* changes.
        }

        /// <summary>
        /// Translates the numerical reputation into a categorical MoodState.
        /// </summary>
        /// <param name="reputation">The current numerical reputation value.</param>
        /// <returns>The corresponding MoodState.</returns>
        private MoodState DetermineMoodState(float reputation)
        {
            if (reputation >= JOYFUL_THRESHOLD) return MoodState.Joyful;
            if (reputation >= HAPPY_THRESHOLD) return MoodState.Happy;
            if (reputation >= CONTENT_THRESHOLD) return MoodState.Content;
            if (reputation > SAD_THRESHOLD) return MoodState.Neutral; // Neutral is between SAD_THRESHOLD and CONTENT_THRESHOLD
            if (reputation > ANGRY_THRESHOLD) return MoodState.Sad;
            if (reputation > ENRAGED_THRESHOLD) return MoodState.Angry;
            return MoodState.Enraged;
        }

        /// <summary>
        /// Applies visual and auditory effects based on the current MoodState.
        /// This is where you'd integrate your fantastic sound library!
        /// </summary>
        private void ApplyMoodEffects()
        {
            // Reset any default states here if necessary before applying new ones
            // For example, stop all previous mood-specific hums/groans

            switch (currentMood)
            {
                case MoodState.Joyful:
                    Debug.Log("TARDIS is feeling joyful! (Rep: " + _tardisReputation + ")");
                    // Example: Play a very light, happy hum; console lights shimmer positively.
                    // You'd use your audio manager here to play the appropriate hum.
                    // engineManager.PlayTARDISHum(TARDISHumType.Joyful);
                    break;
                case MoodState.Happy:
                    Debug.Log("TARDIS is feeling happy! (Rep: " + _tardisReputation + ")");
                    // Example: Play a pleasant hum; console lights are bright.
                    // engineManager.PlayTARDISHum(TARDISHumType.Happy);
                    break;
                case MoodState.Content:
                    Debug.Log("TARDIS is feeling content. (Rep: " + _tardisReputation + ")");
                    // Example: Play a standard, slightly positive hum.
                    // engineManager.PlayTARDISHum(TARDISHumType.Content);
                    break;
                case MoodState.Neutral:
                    Debug.Log("TARDIS is neutral. (Rep: " + _tardisReputation + ")");
                    // Example: Play default console hum.
                    // engineManager.PlayTARDISHum(TARDISHumType.Neutral);
                    break;
                case MoodState.Sad:
                    Debug.Log("TARDIS is feeling sad. (Rep: " + _tardisReputation + ")");
                    // Example: Dim lights, play "displeased engine groans and cranks".
                    // engineManager.PlayTARDISHum(TARDISHumType.Sad);
                    // engineManager.PlayOneShot(TARDISSound.DispleasedGroan);
                    break;
                case MoodState.Angry:
                    Debug.Log("TARDIS is angry! (Rep: " + _tardisReputation + ")");
                    // Example: Redder lights, more aggressive groans/rumbles.
                    // engineManager.PlayTARDISHum(TARDISHumType.Angry);
                    break;
                case MoodState.Enraged:
                    Debug.Log("TARDIS is enraged! (Rep: " + _tardisReputation + ")");
                    // Example: Flickering lights, loud, distressed groans, maybe even a specific alarm.
                    // Consider triggering the "ejection sound" or forcing a perilous flight if reputation gets this low in gameplay.
                    // engineManager.PlayTARDISHum(TARDISHumType.Enraged);
                    // engineManager.PlayOneShot(TARDISSound.EjectionWarning);
                    break;
            }
            // This is where you would call methods on consoleManager or engineManager
            // to change lights, play specific ambient sounds (like the hums), or trigger one-shot effects.
        }

        // Example events/triggers that could adjust TARDIS reputation:
        // You would call AdjustReputation from other scripts.
        /*
        // Call this from a player interaction script:
        public void PlayerPatsConsole()
        {
            AdjustReputation(10f); // Positive interaction
        }

        // Call this from a power management script:
        public void PowerFluctuation()
        {
            AdjustReputation(-5f); // Minor negative event
        }

        // Call this when landing in a dangerous zone
        public void LandedInHazard()
        {
            AdjustReputation(-20f); // More significant negative event
        }
        */
    }
}