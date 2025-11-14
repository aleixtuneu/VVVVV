using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    [SerializeField] private Camera mainCamera; // Referencia a la cámara principal
    [SerializeField] private Vector2 parallaxEffectMultiplier; // Multiplicadores de efecto para X e Y
    [SerializeField] private Transform[] backgroundLayers;

    private Vector3 lastCameraPosition; // La posición de la cámara en el frame anterior

    void Start()
    {
        lastCameraPosition = mainCamera.transform.position;
    }

    void LateUpdate()
    {
        Vector3 deltaMovement = mainCamera.transform.position - lastCameraPosition;

        for (int i = 0; i < backgroundLayers.Length; i++)
        {
            if (backgroundLayers[i] == null) continue; 

            float parallaxX = deltaMovement.x * (parallaxEffectMultiplier.x * (i + 1)); 
            float parallaxY = deltaMovement.y * (parallaxEffectMultiplier.y * (i + 1));

            backgroundLayers[i].position += new Vector3(parallaxX, parallaxY, 0);
        }

        lastCameraPosition = mainCamera.transform.position;
    }
}
