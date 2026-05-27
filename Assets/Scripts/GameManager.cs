using UnityEngine;
using TMPro; // Para que reconozca TextMeshPro

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("UI Settings")]
    [SerializeField] private TextMeshProUGUI scoreText; // La ranura para el texto de puntos

    private int score = 0;

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
        ActualizarTextoUI();
    }

    public void AddPoints(int pointsToAdd)
    {
        score += pointsToAdd;
        Debug.Log("¡Puntos totales: " + score + "!");
        
        ActualizarTextoUI();
    }

    private void ActualizarTextoUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "PUNTOS: " + score.ToString("D2");
        }
    }
}