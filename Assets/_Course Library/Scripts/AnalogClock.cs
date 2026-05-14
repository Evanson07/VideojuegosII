using System;
using UnityEngine;

public class AnalogClock : MonoBehaviour
{
    public Transform HoursPivot;
    public Transform MinutesPivot;
    public Transform SecondsPivot;

    private const float hoursToDegrees = 360f / 12f;  
    private const float minutesToDegrees = 360f / 60f; 
    private const float secondsToDegrees = 360f / 60f; 

    void Update()
    {
        // Obtener la hora actual del sistema
        DateTime time = DateTime.Now;
        // Calcular los ángulos exactos
        float currentHour = time.Hour % 12;
        float hourAngle = (currentHour * hoursToDegrees) + (time.Minute * 0.5f); 
        
        float minuteAngle = time.Minute * minutesToDegrees;
        float secondAngle = time.Second * secondsToDegrees;

        // Aplicar tu rotación corregida a los PIVOTES
        if (HoursPivot != null)
            HoursPivot.localRotation = Quaternion.Euler(hourAngle, 0f, 0f);
            
        if (MinutesPivot != null)
            MinutesPivot.localRotation = Quaternion.Euler(minuteAngle, 0f, 0f);
            
        if (SecondsPivot != null)
            SecondsPivot.localRotation = Quaternion.Euler(secondAngle, 0f, 0f);
    }
}