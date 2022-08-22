using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneManager : MonoBehaviour
{
    public GameObject SettingPanel;

    public void StartBtn() {
        SceneManager.LoadScene(1);
    }

    public void OpenSettingBtn()
    {
        SettingPanel.SetActive(true);
    }

    public void ExitBtn()
    {
        Application.Quit();
    }
}
