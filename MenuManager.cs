using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    public void LoadLevel(int level)
    {
        SceneManager.LoadScene("Game Scene", LoadSceneMode.Single);
        Debug.Log("level: " + level);
        GameManager.instance.levelNum = level;
    }

    public void Menu()
    {
        SceneManager.LoadScene("Menu Scene", LoadSceneMode.Single);
    }


}
