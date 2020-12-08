using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class SoundMeter : MonoBehaviour
{
    public TMP_Text decibelios, average;
    public Button start, stop, subirOffset, bajarOffset;
    public int offset = 0;

    private float dBValue;
    private float rmsValue;
    private AudioSource audioInput;
    private float[] samples;
    private float refValue = 20 * Mathf.Pow(10, -6);
    private int qSamples = 1024;
    private List<float> valores;
    private float delay = 1;
    private float time;

    public Image barraSonido1;
    public Image barraSonido2;

    public Sprite stopSprite;
    public Sprite startSprite;

    public Button encendidoapagado;

    public bool microEncendido = false;

    public TMP_Text textoComparativo;

    private void Awake()
    {
        audioInput = GetComponent<AudioSource>();
    }

    void Start()
    {
        samples = new float[qSamples];
        valores = new List<float>();
    }

    public void ActivarMicrofono()
    {
        if (microEncendido == true)
        {
            Microphone.End(Microphone.devices[0]);
            audioInput.Stop();
            float suma = 0;
            foreach (float valor in valores)
            {
                suma += valor;
                Debug.Log(valor);
            }
            float promedio = suma / valores.Count;
            average.text = "Average: " + promedio.ToString("#.#") + " dB";
            microEncendido = false;
            Debug.Log("Micro apagado");


        }
        else
        {
            valores.Clear();
            audioInput.clip = Microphone.Start(Microphone.devices[0], true, 10, 44100);
            while (!(Microphone.GetPosition(null) > 0)) { }
            audioInput.Play();
            audioInput.volume = 0.001f;
            //audioInput.mute = true;
            microEncendido = true;
            Debug.Log("Micro encendido");

        }
    }


    public void Update()
    {
        //Barras gráficas que representan el sonido
        barraSonido1.fillAmount = dBValue / 120;
        barraSonido2.fillAmount = dBValue / 120;


        //https://forum.unity.com/threads/why-no-timer-class.221139/
        if (time > 0)
        {
            time -= Time.deltaTime;

            if (time < 0) time = 0;
        }

        if (time == 0)
        {
            CalcularDecibelios(audioInput);
            time = delay;
        }

        //Comparativa con decibelios
        if (dBValue >= 0 && dBValue <= 20)
        {
            textoComparativo.text = "Pisada";
        }
        if (dBValue >= 21 && dBValue <= 40)
        {
            textoComparativo.text = "Viento";
        }
        if (dBValue >= 41 && dBValue <= 50)
        {
            textoComparativo.text = "Biblioteca";
        }
        if (dBValue >= 51 && dBValue <= 60)
        {
            textoComparativo.text = "Conversación";
        }
        if (dBValue >= 61 && dBValue <= 80)
        {
            textoComparativo.text = "Tráfico";
        }
        if (dBValue >= 81 && dBValue <= 100)
        {
            textoComparativo.text = "Sonido de motor";
        }
        if (dBValue >= 101 && dBValue <= 120)
        {
            textoComparativo.text = "Concierto";
        }
        if (dBValue >= 121 && dBValue <= 140)
        {
            textoComparativo.text = "Martillo neumático";
        }


        if (microEncendido == true)
        {
            encendidoapagado.gameObject.GetComponent<Image>().sprite = stopSprite;
        }
        else
        {
            encendidoapagado.gameObject.GetComponent<Image>().sprite = startSprite;
        }
        
    }

    public void CalcularDecibelios(AudioSource audio)
    {
        //https://dosits.org/science/advanced-topics/introduction-to-signal-levels/
        audio.GetOutputData(samples, 0);
        float sum = 0;
        for (int j = 0; j < qSamples; j++)
        {
            sum += Mathf.Pow(samples[j], 2);
        }
        rmsValue = Mathf.Sqrt(sum / qSamples);
        dBValue = 20 * Mathf.Log10(rmsValue / refValue) + offset;
        if (dBValue <= 0) dBValue = 0;
        if (dBValue > 0) valores.Add(dBValue);
        if (microEncendido == true)
        {
            decibelios.text = dBValue.ToString("#.#") + " dB";
        }
        else
        {
            decibelios.text = dBValue.ToString("#.#") + "0 dB";
        }

        
    }

    /*public void DesactivarMicrofono()
    {
        Microphone.End(Microphone.devices[0]);
        audioInput.Stop();
        float suma = 0;
        foreach (float valor in valores)
        {
            suma += valor;
            Debug.Log(valor);
        }
        float promedio = suma / valores.Count;
        average.text = "Average: " + promedio.ToString("#.#") + " dB";
    }*/

    public void SubirNivelOffset()
    {
        offset++;
    }

    public void BajarNivelOffset()
    {
        offset--;
    }
}
