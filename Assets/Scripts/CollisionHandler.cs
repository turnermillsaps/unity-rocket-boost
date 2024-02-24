using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float invokeDelay = 1.0f;
    [SerializeField] AudioClip success;
    [SerializeField] AudioClip failure;

    AudioSource audioSource;

    bool isTransitioning = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (isTransitioning) { return; }

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
        isTransitioning = true;
        audioSource.Stop();
        DisableMovement();
        audioSource.PlayOneShot(success);
        Invoke("LoadNextLevel", invokeDelay);
    }

    void StartCrashSequence()
    {
        isTransitioning = true;
        // TODO add particle effect upon crash
        audioSource.Stop();
        DisableMovement();
        audioSource.PlayOneShot(failure);
        Invoke("ReloadLevel", invokeDelay);
    }

    void DisableMovement()
    {
        GetComponent<Movement>().enabled = false;
    }
}
