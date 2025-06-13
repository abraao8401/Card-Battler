using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GerenciarDeck : MonoBehaviour
{

    public GameObject canvasPrincipal;
    public GameObject canvasCoralix, canvasThalacora, canvasDrenia, canvasMaghydra, canvasCinergeist, canvasScorikarn, canvasFloramina, canvasVenevore, canvasThornoak, canvasAurarch, canvasSolvigil, canvasOrelion, canvasOcukrak, canvasMentisect, canvasNoegnos, canvasTenebrall, canvasNargazhul, canvasDraumora;

    void Start()
    {

    }

    void Update()
    {

    }

    public void Coralix()
    {
        canvasPrincipal.SetActive(false);
        canvasCoralix.SetActive(true);
    }

    public void Thalacora()
    {
        canvasPrincipal.SetActive(false);
        canvasThalacora.SetActive(true);
    }

    public void Drenia()
    {
        canvasPrincipal.SetActive(false);
        canvasDrenia.SetActive(true);
    }
    
    public void Maghydra()
    {
        canvasPrincipal.SetActive(false);
        canvasMaghydra.SetActive(true);
    }

    public void Cinergeist()
    {
        canvasPrincipal.SetActive(false);
        canvasCinergeist.SetActive(true);
    }

    public void Scorikarn()
    {
        canvasPrincipal.SetActive(false);
        canvasScorikarn.SetActive(true);
    }

    public void Floramina()
    {
        canvasPrincipal.SetActive(false);
        canvasFloramina.SetActive(true);
    }

    public void Venevore()
    {
        canvasPrincipal.SetActive(false);
        canvasVenevore.SetActive(true);
    }

    public void Thornoak()
    {
        canvasPrincipal.SetActive(false);
        canvasThornoak.SetActive(true);
    }

    public void Aurarch()
    {
        canvasPrincipal.SetActive(false);
        canvasAurarch.SetActive(true);
    }

    public void Solvigil()
    {
        canvasPrincipal.SetActive(false);
        canvasSolvigil.SetActive(true);
    }

    public void Orelion()
    {
        canvasPrincipal.SetActive(false);
        canvasOrelion.SetActive(true);
    }

    public void Ocukrak()
    {
        canvasPrincipal.SetActive(false);
        canvasOcukrak.SetActive(true);
    }

    public void Mentisect()
    {
        canvasPrincipal.SetActive(false);
        canvasMentisect.SetActive(true);
    }

    public void Noegnos()
    {
        canvasPrincipal.SetActive(false);
        canvasNoegnos.SetActive(true);
    }

    public void Tenebrall()
    {
        canvasPrincipal.SetActive(false);
        canvasTenebrall.SetActive(true);
    }

    public void Nargazhul()
    {
        canvasPrincipal.SetActive(false);
        canvasNargazhul.SetActive(true);
    }

    public void Draumora()
    {
        canvasPrincipal.SetActive(false);
        canvasDraumora.SetActive(true);
    }

    public void VoltarLista()
    {
        gameObject.transform.root.gameObject.SetActive(false);
        canvasPrincipal.SetActive(true);
    }
}
