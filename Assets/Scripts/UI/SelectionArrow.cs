using UnityEngine;
using UnityEngine.UI;

public class SelectionArrow : MonoBehaviour
{
    [SerializeField] private RectTransform[] options;
    [SerializeField] private AudioClip changeSound;
    [SerializeField] private AudioClip interactSound;
    private RectTransform rect;
    private int currentPosition;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    private void Update()
    {
        // Change the position of the selection arrow
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            ChangePosition(-1);
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            ChangePosition(1);

        // Interact with the current option
        if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.E))
            Interact();
    }

    private void ChangePosition(int _change)
    {
        currentPosition += _change;

        if (_change != 0)
            SoundManager.instance.PlaySound(changeSound);

        // Ensure the selection wraps around if reaching the end
        if (currentPosition < 0)
            currentPosition = options.Length - 1;
        else if (currentPosition > options.Length - 1)
            currentPosition = 0;

        // Print debug information
        Debug.Log("Arrow Position: " + rect.localPosition + ", Option Position: " + options[currentPosition].localPosition);

        // Set the local position of the arrow to match the selected option
        rect.localPosition = new Vector3(rect.localPosition.x, options[currentPosition].localPosition.y, 0);
    }

    private void Interact()
    {
        // Play interaction sound
        SoundManager.instance.PlaySound(interactSound);

        // Access the button component on each option and call its function
        options[currentPosition].GetComponent<Button>().onClick.Invoke();
    }
}
