using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public GameObject[] tutorialObjects; // Objects to fade in
    public float fadeDuration = 2.0f; // Duration of the fade effect
    private bool tutorialStarted = false;
    public GameObject titleObject; // The 3D title object
    public CanvasGroup titleCanvas; // The UI canvas for the title

    void Start()
    {
        // Initially set materials to transparent and hide objects
        foreach (GameObject obj in tutorialObjects)
        {
            SetObjectAlpha(obj, 0);
        }
    }

    void Update()
    {
        // Trigger fade-in for testing (e.g., mouse click or controller input)
        if (!tutorialStarted && Input.GetMouseButtonDown(0))
        {
            tutorialStarted = true;
            StartCoroutine(FadeInTutorialObjects());
        }
    }

    private IEnumerator FadeInTutorialObjects(){
    float elapsed = 0;

    while (elapsed < fadeDuration)
    {
        elapsed += Time.deltaTime;
        float alpha = Mathf.Clamp01(elapsed / fadeDuration);
        float fadeOutAlpha = 1 - alpha; // For fading out

        // Fade in tutorial objects
        foreach (GameObject obj in tutorialObjects)
        {
            SetObjectAlpha(obj, alpha);
        }

        // Fade out 3D title object
        if (titleObject)
        {
            SetObjectAlpha(titleObject, fadeOutAlpha);
        }

        // Fade out canvas
        if (titleCanvas)
        {
            titleCanvas.alpha = fadeOutAlpha;
        }

        yield return null;
    }

    // Ensure final states
    foreach (GameObject obj in tutorialObjects)
    {
        SetObjectAlpha(obj, 1);
    }

    if (titleObject)
    {
        SetObjectAlpha(titleObject, 0);
        titleObject.SetActive(false); // Optionally disable the title object
    }

    if (titleCanvas)
    {
        titleCanvas.alpha = 0;
        titleCanvas.gameObject.SetActive(false); // Optionally disable the canvas
    }
}



    private void SetObjectAlpha(GameObject obj, float alpha)
    {
        Renderer[] renderers = obj.GetComponentsInChildren<Renderer>();

        foreach (Renderer renderer in renderers)
        {
            foreach (Material material in renderer.materials)
            {
                // Switch to Transparent mode during fade-in
                if (alpha < 1f)
                {
                    SetMaterialModeTransparent(material);
                }

                // Adjust alpha
                if (material.HasProperty("_Color"))
                {
                    Color color = material.color;
                    color.a = alpha;
                    material.color = color;
                }

                // Switch back to Opaque mode once fully visible
                if (alpha >= 1f)
                {
                    SetMaterialModeOpaque(material);
                }
            }
        }
    }

    private void SetMaterialModeTransparent(Material material)
    {
        material.SetFloat("_Mode", 3); // Transparent
        material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        material.SetInt("_ZWrite", 0);
        material.DisableKeyword("_ALPHATEST_ON");
        material.EnableKeyword("_ALPHABLEND_ON");
        material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        material.renderQueue = 3000;
    }

    private void SetMaterialModeOpaque(Material material)
    {
        material.SetFloat("_Mode", 0); // Opaque
        material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
        material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
        material.SetInt("_ZWrite", 1);
        material.EnableKeyword("_ALPHATEST_ON");
        material.DisableKeyword("_ALPHABLEND_ON");
        material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        material.renderQueue = -1;
    }
}