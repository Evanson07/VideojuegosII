using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("UI Settings")]
    [SerializeField] private TextMeshProUGUI scoreText; // Texto de puntos en la pared
    [SerializeField] private TextMeshProUGUI ammoText;  // Texto de balas en la pared
    [SerializeField] private GameObject menuInicioPanel; // El panel gris que se va a ocultar

    [Header("Gameplay Settings")]
    [SerializeField] private GameObject pistolaVR; // El objeto de la pistola para activarlo

    private int score = 0;
    private int bullets = 0;
    private bool juegoIniciado = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Al arrancar, nos aseguramos de que el menú se vea y la pistola esté apagada
        if (menuInicioPanel != null) menuInicioPanel.SetActive(true);
        if (pistolaVR != null) pistolaVR.SetActive(false);

        ActualizarTextoUI();
    }

    // ESTA FUNCIÓN LA LLAMARÁ EL BOTÓN "INICIO"
    public void ComenzarJuego()
    {
        juegoIniciado = true;
        bullets = 7; // Cargamos las 7 balas iniciales

        // ¡AQUÍ ESTÁ EL TRUCO! 
        // En lugar de apagar solo el panel de adentro, apagamos TODO el Canvas del menú
        if (menuInicioPanel != null) 
        {
            menuInicioPanel.SetActive(false); 
        }
        
        if (pistolaVR != null) 
        {
            pistolaVR.SetActive(true);
        }

        ActualizarTextoUI();
    }

    public void AddPoints(int pointsToAdd)
    {
        if (!juegoIniciado) return; // Si no ha iniciado el juego, no suma puntos

        score += pointsToAdd;
        Debug.Log("¡Puntos totales: " + score + "!");
        
        ActualizarTextoUI();
    }

    // LLAMA A ESTA FUNCIÓN DESDE TU SCRIPT DE DISPARO CUANDO GASTES UNA BALA
    public void RestarBala()
    {
        if (!juegoIniciado) return;

        if (bullets >= 1)
        {
            bullets--;
            ActualizarTextoUI();
        }

        if (bullets <= 1)
        {
            Debug.Log("¡Te quedaste sin plomo!");
            // Aquí en el futuro puedes poner que se acabe la partida
        }
    }

    private void ActualizarTextoUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Puntos: " + score;
        }

        if (ammoText != null)
        {
            // Si el juego no ha iniciado, puedes mostrar "Balas: --" o "Balas: 7"
            ammoText.text = "Balas: " + (juegoIniciado ? bullets.ToString() : "7") + " de 7";
        }
    }
    // Agrega esto al final de tu GameManager.cs
    public bool CanShoot()
    {
        // Regresa true solo si el juego ya empezó y todavía quedan balas
        return juegoIniciado && bullets > 0;
    }
    
}