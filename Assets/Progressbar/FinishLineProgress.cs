using UnityEngine;
using UnityEngine.UI;

public class FinishLineProgress : MonoBehaviour
{
    public Transform player; // Reference to the player
    public Transform finishLine; // Reference to the finish line
    public Slider progressBar; // UI Slider for progress

    private float startZ; // Initial Z position of the player
    private float finishZ; // Z position of the finish line

    private void Start()
    {
        if (player == null || finishLine == null || progressBar == null)
        {
            Debug.LogError("Assign Player, Finish Line, and Progress Bar in Inspector!");
            return;
        }

        startZ = player.position.z;
        finishZ = finishLine.position.z;
    }

    private void Update()
    {
        float progress = Mathf.InverseLerp(startZ, finishZ, player.position.z);
        progressBar.value = progress;
    }
}
