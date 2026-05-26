using UnityEngine;

public class FloorTrigger : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("El Tag que deben tener las latas en el Inspector")]
    [SerializeField] private string targetTag = "Lata";

    // Este método de Unity detecta cuando un objeto entra en el área del cubo invisible (Trigger)
    private void OnTriggerEnter(Collider other)
    {
        // 1. Verificamos si el objeto que cayó tiene la etiqueta correcta
        if (other.CompareTag(targetTag))
        {
            // 2. Buscamos el script 'TargetCan' en la lata que cayó
            TargetCan can = other.GetComponent<TargetCan>();
            
            // 3. Si la lata tiene el script, le ordenamos que ejecute su función de dar puntos
            if (can != null)
            {
                can.ScorePoints();
            }
        }
    }
}