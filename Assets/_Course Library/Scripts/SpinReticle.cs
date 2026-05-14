using UnityEngine;

public class SpinReticle : MonoBehaviour
{
    [Header("Configuración del Giro")]
    public float rotationSpeed = 90f; 
    private float anguloActual = 0f; // El ángulo ahora solo crecerá al ver interactuables

    [Header("Configuración del Láser")]
    public float distanciaMaxima = 5f; 
    [Tooltip("Selecciona AQUÍ la capa 'Interactuable' que creaste en Unity.")]
    public LayerMask capaInteractiva; 

    [Header("Configuración Visual (Colores)")]
    public Color colorNormal = Color.red;       
    public Color colorInteractuable = Color.white; 

    private Transform camaraPrincipal;
    private Renderer miRenderer; 

    void Start()
    {
        camaraPrincipal = Camera.main.transform;
        miRenderer = GetComponent<Renderer>();

        if (miRenderer != null) miRenderer.material.color = colorNormal;
    }

    void Update()
    {
        Ray rayo = new Ray(camaraPrincipal.position, camaraPrincipal.forward);
        RaycastHit golpe;

        if (Physics.Raycast(rayo, out golpe, distanciaMaxima))
        {
            transform.position = golpe.point + (golpe.normal * 0.01f);

            // Verificamos si el objeto golpeado tiene la capa interactiva
            if ((capaInteractiva.value & (1 << golpe.collider.gameObject.layer)) > 0)
            {
                // ¡ES INTERACTUABLE!
                // 1. Cambiamos a color blanco
                if (miRenderer != null) miRenderer.material.color = colorInteractuable;
                
                // 2. Le sumamos velocidad para que GIRE
                anguloActual += rotationSpeed * Time.deltaTime; 
            }
            else
            {
                // ES UNA PARED NORMAL
                // 1. Regresamos al color rojo
                if (miRenderer != null) miRenderer.material.color = colorNormal;
                
                // (Ojo: Aquí NO sumamos a anguloActual, por lo que se queda quieto)
            }

            // Aplicamos la rotación (se moverá o se quedará quieto dependiendo del anguloActual)
            transform.rotation = Quaternion.FromToRotation(Vector3.up, golpe.normal) * Quaternion.Euler(0f, anguloActual, 0f);
        }
        else
        {
            // NO VEMOS NADA
            transform.position = camaraPrincipal.position + (camaraPrincipal.forward * distanciaMaxima);
            
            if (miRenderer != null) miRenderer.material.color = colorNormal;

            transform.rotation = Quaternion.FromToRotation(Vector3.up, -camaraPrincipal.forward) * Quaternion.Euler(0f, anguloActual, 0f);
        }
    }
}