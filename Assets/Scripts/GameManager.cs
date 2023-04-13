using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager instance => _instance;
    public bool motionSicknessOn = false;
    void Awake()
    {
        if (_instance == null) _instance = this;
        else Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public void LoadSceneWithString(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void ToggleMotionSicknessOption()
    {
        motionSicknessOn = !motionSicknessOn;
    }
}
