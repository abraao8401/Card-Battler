using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AvisoZoom : MonoBehaviour
{
    public static AvisoZoom instance;

    public GameObject zoomHintObject;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        zoomHintObject.SetActive(false);
    }

    public void ShowHint()
    {
        zoomHintObject.SetActive(true);
    }

    public void HideHint()
    {
        zoomHintObject.SetActive(false);
    }
}