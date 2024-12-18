using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject guide;
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void QuitGame()
    {
        Debug.Log("hell");
        Application.Quit();
        //EditorApplication.isPlaying = false;
    }

    public void Guide()
    {
        guide.SetActive(true);
    }

    public void Exit()
    {
        guide.SetActive(false);
    }
}
