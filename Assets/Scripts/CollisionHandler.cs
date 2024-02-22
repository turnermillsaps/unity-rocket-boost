using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Finish":
                Debug.Log("Wow, you did it!");
                break;
            case "Friendly":
                Debug.Log("This thing is friendly");
                break;
            case "Fuel":
                Debug.Log("Picked up fuel.");
                break;
            default:
                Debug.Log("Sorry, you died.");
                break;
        }
    }
}
