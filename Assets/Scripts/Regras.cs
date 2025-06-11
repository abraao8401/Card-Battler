using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Regras : MonoBehaviour
{
    public string tutorialScene;
    public void VoltaTutorial()
    {
        SceneManager.LoadScene(tutorialScene);
    }
}
