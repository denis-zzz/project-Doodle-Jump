using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonOnClick : MonoBehaviour
{
    public void PlayOnClick(){
        SceneManager.LoadScene("Main");
    }

    public void CancelOnClick(){
        Application.Quit();
    }

    public void MenuOnClick(){
        SceneManager.LoadScene("Menu");
    }
}
