using UnityEngine;
using TMPro;
using System.Collections;

public class GameStartScript : MonoBehaviour
{
    public Canvas getReadyCanvas; // Assign the Canvas in the Inspector
    public TextMeshProUGUI textField; // Assign the Text Mesh Pro UGUI Text field in the Inspector
    public AudioSource audioSource; // Assign the AudioSource in the Inspector
    public AudioClip song; // Assign the song in the Inspector

    // This script assumes the Canvas and AudioSource are enabled to start with,
    // and the AudioSource isn't playing anything

    void Start()
    {
        StartCoroutine(StartRoutine());
    }

    IEnumerator StartRoutine()
    {
        // Show "Get ready..." for 3 seconds
        textField.text = "Get ready...";
        yield return new WaitForSeconds(3f);

        // Show "Go!" for 0.5 seconds
        textField.text = "Go!";
        yield return new WaitForSeconds(0.5f);

        // Deactivate the canvas
        getReadyCanvas.gameObject.SetActive(false);

        // Start the music
        audioSource.clip = song;
        audioSource.Play();
    }
}
