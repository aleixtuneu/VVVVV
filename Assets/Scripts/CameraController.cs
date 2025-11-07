using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float smoothSpeed = 0.125f;
    [SerializeField] private Vector3 offset;
    [SerializeField] private Vector2 minCameraBounds;
    [SerializeField] private Vector2 maxCameraBounds;
    [SerializeField] private bool useBounds = false;

    void LateUpdate()
    {
        // Calcular posició desde la càmera
        Vector3 desiredPosition = target.position + offset;

        // Suavitzar el moviment de la càmera
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Límits
        if (useBounds)
        {
            // Obtenir tamany de la vista de la càmera
            Camera cam = Camera.main;
            float camHalfHeight = cam.orthographicSize;
            float camHalfWidth = cam.aspect * camHalfHeight;

            float clampedX = Mathf.Clamp(smoothedPosition.x, minCameraBounds.x + camHalfWidth, maxCameraBounds.x - camHalfWidth);
            float clampedY = Mathf.Clamp(smoothedPosition.y, minCameraBounds.y + camHalfHeight, maxCameraBounds.y - camHalfHeight);

            smoothedPosition = new Vector3(clampedX, clampedY, smoothedPosition.z);
        }

        // Posició de la càmera mantenint Z
        transform.position = new Vector3(smoothedPosition.x, smoothedPosition.y, transform.position.z);
    }
}
