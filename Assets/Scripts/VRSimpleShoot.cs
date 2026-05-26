using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class VRSimpleShoot : MonoBehaviour
{
    [Header("Prefab References")]
    public GameObject bulletPrefab;

    [Header("Location References")]
    [SerializeField] private Transform barrelLocation;

    [Header("Settings")]
    [Tooltip("Fuerza de salida de la bala")] [SerializeField] private float shotPower = 500f;

    private XRGrabInteractable grabInteractable;

    void Start()
    {
        if (barrelLocation == null)
            barrelLocation = transform;

        // Obtenemos el componente XR Grab Interactable del objeto
        grabInteractable = GetComponent<XRGrabInteractable>();

        // Nos suscribimos al evento del gatillo (Activate)
        grabInteractable.activated.AddListener(OnTriggerPulled);
    }

    void OnDestroy()
    {
        // Limpieza del evento para evitar fugas de memoria
        if (grabInteractable != null)
            grabInteractable.activated.RemoveListener(OnTriggerPulled);
    }

    // Se ejecuta al pulsar el gatillo del control VR mientras sostienes el arma
    private void OnTriggerPulled(ActivateEventArgs args)
    {
        Shoot();
    }

    void Shoot()
{
    if (bulletPrefab)
    {
        // CONFIGURACIÓN: Ajusta este número. 
        // 0.1f son 10 centímetros adelante del cañón. Si sigue chocando, súbelo a 0.15f o 0.2f
        float forwardOffset = 0.12f; 
        
        // Calculamos la nueva posición sumando un empuje hacia adelante (barrelLocation.forward)
        Vector3 spawnPosition = barrelLocation.position + (barrelLocation.forward * forwardOffset);

        // Spawn de la bala en la posición con offset, pero manteniendo la rotación original
        GameObject spawnableBullet = Instantiate(bulletPrefab, spawnPosition, barrelLocation.rotation);
        
        // Impulso físico hacia adelante
        Rigidbody rb = spawnableBullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(barrelLocation.forward * shotPower);
        }
    }
}
}