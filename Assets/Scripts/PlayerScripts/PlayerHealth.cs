using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private int MaxPlayerHealth = 3;
    public int CurrentPlayerHealth;

    private static PlayerHealth instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        ResetPlayerHealth();
    }
    
    private void OnSceneLoaded(Scene scene)
    {
        if (scene.name == "MainMenu" || scene.buildIndex == 0)
        {
            ResetPlayerHealth();
        }
    }

    public void ResetPlayerHealth()
    {
        CurrentPlayerHealth = MaxPlayerHealth;
    }
}
