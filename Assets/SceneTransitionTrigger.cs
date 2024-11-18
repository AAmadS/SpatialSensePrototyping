using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Required for scene management

public class SceneTransitionTrigger : MonoBehaviour
{
    [Tooltip("Name of the scene to load.")]
    public string sceneName;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger is the player
        Debug.Log($"Triggered by: {other.name}");
        if (other.CompareTag("Player")) // Ensure your player has the tag "Player"
        {
            // Load the specified scene
            LoadScene();
        }
    }

    private void LoadScene()
    {
        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogWarning("Scene name not specified!");
        }
    }
}
