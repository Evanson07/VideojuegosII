using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private int score = 0;

    void Awake()
    {
        // Aseguramos que solo exista un GameManager en la escena
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Función pública para sumar puntos desde otros scripts
    public void AddPoints(int pointsToAdd)
    {
        score += pointsToAdd;
        Debug.Log("¡Puntos totales: " + score + "!");
        
        // Aquí en el futuro puedes actualizar un texto en la interfaz (UI) de tu juego
    }
}