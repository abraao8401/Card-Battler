using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
    public void Menu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void RegrasBasicas()
    {
        SceneManager.LoadScene("Regras Basicas");
        AudioManager.instance.PlaySFX(0);
    }

    public void Batalhas()
    {
        SceneManager.LoadScene("Batalhas");
        AudioManager.instance.PlaySFX(0);
    }
    
    public void Elementos()
    {
        SceneManager.LoadScene("Elementos");
        AudioManager.instance.PlaySFX(0);
    }
}
