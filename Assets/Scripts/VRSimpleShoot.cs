using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class VRSimpleShoot : MonoBehaviour
{
    [Header("Prefab References")]
    public GameObject bulletPrefab;

    [Header("Location References")]
    [SerializeField] private Transform barrelLocation;

    [Header("Settings")]
    [Tooltip("Fuerza de salida de la bala")] [SerializeField] private float shotPower = 1500f;

    private XRGrabInteractable grabInteractable;

    void Start()
    {
        if (barrelLocation == null)
            barrelLocation = transform;

        grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.activated.AddListener(OnTriggerPulled);
    }

    void OnDestroy()
    {
        if (grabInteractable != null)
            grabInteractable.activated.RemoveListener(OnTriggerPulled);
    }

    private void OnTriggerPulled(ActivateEventArgs args)
    {
        Shoot();
    }

    void Shoot()
    {
        // Revisamos si el GameManager nos da permiso de disparar
        if (GameManager.Instance != null && GameManager.Instance.CanShoot())
        {
            if (bulletPrefab)
            {
                float forwardOffset = 0.15f; 
                Vector3 spawnPosition = barrelLocation.position + (barrelLocation.forward * forwardOffset);

                GameObject spawnableBullet = Instantiate(bulletPrefab, spawnPosition, barrelLocation.rotation);
                
                Rigidbody rb = spawnableBullet.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.AddForce(barrelLocation.forward * shotPower);
                }

                // Le avisamos al GameManager que gaste la bala
                GameManager.Instance.RestarBala();
            }
        }
        else
        {
            Debug.Log("¡Se acabaron las balas! Desapareciendo pistola...");
            
            // ¡EL TRUCO! La pistola se desactiva por completo en la escena
            gameObject.SetActive(false);
        }
    }
}