using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public GameObject[] tutorialObjects; // Objects to fade in
    public float fadeDuration = 2.0f; // Duration of the fade effect
    private bool tutorialStarted = false;

    void Start()
    {
        // Initially hide the tutorial objects
        foreach (GameObject obj in tutorialObjects)
        {
            SetObjectAlpha(obj, 0);
        }
    }

    void Update()
    {
        // Check for the button press (e.g., A button on the Oculus Touch controller)
        if (!tutorialStarted && OVRInput.GetDown(OVRInput.Button.One))
        {
            tutorialStarted = true;
            StartTutorial();
        }

        // Check for mouse left click (for testing purposes)
        if (!tutorialStarted && Input.GetMouseButtonDown(0)) // 0 is the left mouse button
        {
            tutorialStarted = true;
            StartTutorial();
        }
    }

    public void StartTutorial()
    {
        StartCoroutine(FadeInTutorialObjects());
    }

    private IEnumerator FadeInTutorialObjects()
    {
        float elapsed = 0;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsed / fadeDuration);

            foreach (GameObject obj in tutorialObjects)
            {
                SetObjectAlpha(obj, alpha);
            }

            yield return null;
        }

        // Ensure final alpha is 1
        foreach (GameObject obj in tutorialObjects)
        {
            SetObjectAlpha(obj, 1);
        }
    }

    private void SetObjectAlpha(GameObject obj, float alpha)
    {
        Renderer[] renderers = obj.GetComponentsInChildren<Renderer>();

        foreach (Renderer renderer in renderers)
        {
            foreach (Material material in renderer.materials)
            {
                if (material.HasProperty("_Color"))
                {
                    Color color = material.color;
                    color.a = alpha;
                    material.color = color;
                }
            }
        }
    }
}