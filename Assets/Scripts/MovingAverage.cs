using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingAverage : MonoBehaviour
{
    private int muestras = 1000;
    private float media;
    private float[] buffer;
    private float average;

    public void Start()
    {
        buffer = new float[muestras];
    }

    void Update()
    {
                
    }

    public float MostrarMedia(float [] valores)
    {
        for (int i = 0; i < buffer.Length; i++)
        {
            if (i >= muestras - 1)
            {
                float total = 0;
                for (int x = i; x > (i - muestras); x--)
                    total += buffer[x];
                average = total / muestras;
            }
        }
        return average; 
    }
}
