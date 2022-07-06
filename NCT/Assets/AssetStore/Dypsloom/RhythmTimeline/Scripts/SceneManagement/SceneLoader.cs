using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] protected int m_SceneBuildIndex;

    [ContextMenu("LoadScene")]
    public void LoadScene()
    {
        LoadScene(m_SceneBuildIndex);
    }

    private void LoadScene(int buildIndex)
    {
        SceneManager.LoadScene(buildIndex);
    }
}
