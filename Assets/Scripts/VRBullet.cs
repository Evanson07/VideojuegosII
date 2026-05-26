using UnityEngine;

public class VRBullet : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("Tiempo máximo de vida por si la bala vuela al infinito y no choca con nada")]
    [SerializeField] private float maxLifeTime = 5f;

    [Tooltip("Tiempo que tarda en borrarse la bala DESPUÉS de haber chocado")]
    [SerializeField] private float destroyDelayAfterCollision = 1f;

    void Start()
    {
        // Si la bala no choca con nada, se limpia sola tras X segundos
        Destroy(gameObject, maxLifeTime);
    }

    // Este método se ejecuta automáticamente en el impacto físico
    private void OnCollisionEnter(Collision collision)
    {
        // CAMBIO: En lugar de borrarse al instante, le metemos el retraso de 1 segundo.
        // Unity esperará ese tiempo antes de eliminar el GameObject de la escena.
        Destroy(gameObject, destroyDelayAfterCollision);
    }
}