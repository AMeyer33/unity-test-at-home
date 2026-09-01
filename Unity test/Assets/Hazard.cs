using UnityEngine;
using UnityEngine.SceneManagement;

public class Hazard : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object that touched the triangle has the "Player" tag
        if (other.CompareTag("Player"))
        {
            KillPlayer();
        }
    }

    private void KillPlayer()
    {
        Debug.Log("Player was killed by a hazard!");

        // Option A: Reload the current scene to restart the level
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        // Option B: If you prefer to destroy the player object instead, uncomment the line below:
        // Destroy(GameObject.FindWithTag("Player"));
    }
}
