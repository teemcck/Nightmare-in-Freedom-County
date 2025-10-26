using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    [SerializeField] private int ThreeHeartScene = 0;
    [SerializeField] private int TwoHeartScene = 0;
    [SerializeField] private int OneHeartScene = 0;
    private PlayerHealth playerHealth;

    void Start()
    {
        playerHealth = FindFirstObjectByType<PlayerHealth>();
    }

    public void TriggerPlayerDeath()
    {
        playerHealth.CurrentPlayerHealth -= 1;
        if (playerHealth.CurrentPlayerHealth == 2)
        {
            SceneManager.LoadScene(ThreeHeartScene);
        }
        else if (playerHealth.CurrentPlayerHealth == 1)
        {
            SceneManager.LoadScene(TwoHeartScene);
        }
        else if (playerHealth.CurrentPlayerHealth <= 0)
        {
            SceneManager.LoadScene(OneHeartScene);
        }
    }
}
