using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitDoor : MonoBehaviour
{
    [SerializeField] private int ExitSceneIndex;

    void OnTriggerEnter2D(Collider2D collision)
    {
        SceneManager.LoadScene(ExitSceneIndex);
    }
}