using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Finish":
                LoadNextLevel();
                break;
            case "Friendly":
                Debug.Log("This thing is friendly");
                break;
            case "Fuel":
                Debug.Log("Picked up fuel.");
                break;
            default:
                ReloadLevel();
                break;
        }
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    void LoadNextLevel()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings) 
        {
            nextSceneIndex = 0; // Reset to first scene if we reach the end
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
}
