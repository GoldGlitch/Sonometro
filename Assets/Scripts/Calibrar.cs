using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Calibrar : MonoBehaviour
{
    private AudioSource pureTone;
    public Button calibrar;

    private void Awake()
    {
        pureTone = GetComponent<AudioSource>();
    }

    private void Update()
    {
    }

    public void Calibrado()
    {
        pureTone.playOnAwake = true;
        pureTone.Play();
        Debug.Log("Emitiendo pitido...");
    }

    public void FinCalibrado()
    {
        pureTone.Stop();
    }
}
