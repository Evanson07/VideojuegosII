using UnityEngine;
using UnityEngine.SceneManagement; 
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("UI Settings")]
    [SerializeField] private TextMeshProUGUI scoreText; 
    [SerializeField] private TextMeshProUGUI ammoText;  
    [SerializeField] private GameObject menuInicioPanel; 

    [Header("UI Game Over")]
    [SerializeField] private GameObject canvasGameOver; 
    // 1. NUEVA VARIABLE: Para el texto de puntuación de la pantalla final
    [SerializeField] private TextMeshProUGUI finalScoreText; 

    [Header("Gameplay Settings")]
    [SerializeField] private GameObject pistolaVR; 

    private int score = 0;
    private int bullets = 0;
    private bool juegoIniciado = false;

    void Awake()
    {
        if (Instance == null) { Instance = this; }
        else { Destroy(gameObject); }
    }

    void Start()
    {
        if (menuInicioPanel != null) menuInicioPanel.SetActive(true);
        if (pistolaVR != null) pistolaVR.SetActive(false);
        if (canvasGameOver != null) canvasGameOver.SetActive(false); // Nos aseguramos de que empiece apagado

        ActualizarTextoUI();
    }

    public void ComenzarJuego()
    {
        juegoIniciado = true;
        bullets = 7; 

        if (menuInicioPanel != null) menuInicioPanel.SetActive(false);
        if (pistolaVR != null) pistolaVR.SetActive(true);

        ActualizarTextoUI();
    }

    public void AddPoints(int pointsToAdd)
    {
        if (!juegoIniciado) return; 

        score += pointsToAdd;
        ActualizarTextoUI();
    }

    public void RestarBala()
    {
        if (!juegoIniciado) return;

        if (bullets > 0)
        {
            bullets--;
            ActualizarTextoUI();
        }

        if (bullets <= 0)
        {
            // 2. CUANDO SE ACABAN LAS BALAS: Mandamos la puntuación al texto final antes de mostrar la pantalla
            if (finalScoreText != null)
            {
                finalScoreText.text = "FIN DEL JUEGO. PULSA REINICIAR\n PARA VOLVER A ECHAR PLOMO\n Puntuación: " + score;
            }

            if (canvasGameOver != null) 
            {
                canvasGameOver.SetActive(true);
            }
        }
    }

    public void ReiniciarJuego()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public bool CanShoot()
    {
        return juegoIniciado && bullets > 0;
    }

    private void ActualizarTextoUI()
    {
        if (scoreText != null) scoreText.text = "Puntos: " + score;
        if (ammoText != null) ammoText.text = "Balas: " + (juegoIniciado ? bullets.ToString() : "7");
    }
}