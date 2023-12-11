using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VolumeText : MonoBehaviour
{
    [SerializeField] private string volumeName;
    [SerializeField] private string textIntro;
    private TextMeshProUGUI txt;

    private void Awake()
    {
        // Get the TextMeshProUGUI component attached to this GameObject
        txt = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        // Update the displayed volume text
        UpdateVolume();
    }

    private void UpdateVolume()
    {
        // Retrieve the volume value from PlayerPrefs and convert it to a percentage
        float volumeValue = PlayerPrefs.GetFloat(volumeName) * 100;

        // Set the text to display the volume information
        txt.text = textIntro + volumeValue.ToString();
    }
}
