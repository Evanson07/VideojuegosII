using UnityEngine;

public class TargetCan : MonoBehaviour
{
    [Header("Audio Settings")]
    [SerializeField] private AudioClip hitSound; // El audio del golpe
    private AudioSource audioSource;
    private bool pointsGiven = false;

    void Start()
    {
        // Agregamos o conseguimos el componente de Audio de la lata
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        
        // Configuración básica para que se escuche bien
        audioSource.playOnAwake = false;
    }

    // Se ejecuta cuando ALGO choca físicamente con la lata
    private void OnCollisionEnter(Collision collision)
    {
        // Verificamos si lo que le pegó tiene el script de la bala (VRBullet)
        if (collision.gameObject.GetComponent<VRBullet>() != null)
        {
            if (hitSound != null && audioSource != null)
            {
                // Reproduce el sonido sin interrumpir si ya se estaba ejecutando otro
                audioSource.PlayOneShot(hitSound);
            }
        }

        //Debug.Log("PEGO PEGO");
    }

    // Esta función la llamaremos desde el suelo cuando la lata caiga
    public void ScorePoints()
    {
        // Evitamos que dé puntos más de una vez si rebota en el suelo
        if (!pointsGiven)
        {
            pointsGiven = true;
            GameManager.Instance.AddPoints(5);
            
            // Opcional: destruir la lata después de 2 segundos de caer para limpiar la escena
            Destroy(gameObject, 2f);
        }
    }
}