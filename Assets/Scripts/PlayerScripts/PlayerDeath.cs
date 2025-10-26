using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    [SerializeField] private int ThreeHeartScene = 0;
    [SerializeField] private int TwoHeartScene = 0;
    [SerializeField] private int OneHeartScene = 0;
    [SerializeField] private PlayerHealth playerHealth;

    public void TriggerPlayerDeath()
    {
        playerHealth.CurrentPlayerHealth -= 1;
        if (playerHealth.CurrentPlayerHealth == 2)
        {
            SceneManager.SetActiveScene(SceneManager.GetSceneAt(ThreeHeartScene));
        }
        else if (playerHealth.CurrentPlayerHealth == 1)
        {
            SceneManager.SetActiveScene(SceneManager.GetSceneAt(TwoHeartScene));
        }
        else if (playerHealth.CurrentPlayerHealth == 0)
        {
            SceneManager.SetActiveScene(SceneManager.GetSceneAt(OneHeartScene));
        }
    }
}
