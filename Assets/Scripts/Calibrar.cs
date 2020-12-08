using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Calibrar : MonoBehaviour
{
    private AudioSource pureTone;
    public Button calibrar;
    public bool calibrando = false;

    public Sprite calibrandoSprite;
    public Sprite nocalibrandoSprite;

    public Button calibrarBoton;

    private void Awake()
    {
        pureTone = GetComponent<AudioSource>();
    }

    private void Update()
    {

        if (calibrando == true)
        {
            calibrarBoton.gameObject.GetComponent<Image>().sprite = calibrandoSprite;
        }
        else
        {
            calibrarBoton.gameObject.GetComponent<Image>().sprite = nocalibrandoSprite;
        }
    }

    public void Calibrado()
    {
        if (calibrando == true)
        {
            pureTone.Stop();
            Debug.Log("Fin calibrado");
            calibrando = false;
        }
        else
        {
            pureTone.playOnAwake = true;
            pureTone.Play();
            Debug.Log("Emitiendo pitido...");
            calibrando = true;
        }

        /*public void FinCalibrado()
    {
        pureTone.Stop();
    }*/
    }
}

