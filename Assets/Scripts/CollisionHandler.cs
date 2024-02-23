using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float invokeDelay = 1.0f;
    void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Finish":
                StartLoadLevelSequence();
                break;
            case "Friendly":
                Debug.Log("This thing is friendly");
                break;
            default:
                StartCrashSequence();
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

    void StartLoadLevelSequence()
    {
        DisableMovement();
        Invoke("LoadNextLevel", invokeDelay);
    }

    void StartCrashSequence()
    {
        // TODO add SFX upon crash
        // TODO add particle effect upon crash
        DisableMovement();
        Invoke("ReloadLevel", invokeDelay);
    }

    void DisableMovement()
    {
        GetComponent<Movement>().enabled = false;
    }
}
