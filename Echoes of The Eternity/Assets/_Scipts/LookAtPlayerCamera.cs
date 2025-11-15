using UnityEngine;

public class LookAtPlayerCamera : MonoBehaviour
{
    private Transform mainCameraTransform;

    void Start()
    {
        // Find the main camera and get its transform
        Camera mainCamera = Camera.main;
        if (mainCamera != null)
        {
            mainCameraTransform = mainCamera.transform;
        }
        else
        {
            Debug.LogError("No main camera found in the scene. Please tag a camera with 'MainCamera'.");
        }
    }

    void Update()
    {
        // Check if a camera transform has been found
        if (mainCameraTransform != null)
        {
            // Rotate the object to look at the camera's position
            transform.LookAt(mainCameraTransform);
        }
    }
}