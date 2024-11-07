using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverTextTrigger : MonoBehaviour
{

    public GameObject textBox;

    // Start is called before the first frame update
    void Start()
    {
        textBox.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the player’s hand is hovering
        if (other.CompareTag("Player"))
        {
            textBox.SetActive(true); // Show the text box
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            textBox.SetActive(false); // Hide the text box
        }
    }
}
