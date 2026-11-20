
using UnityEngine;
using TMPro;

public class AnatomyInfoUI : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private GameObject infoPanel;
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private TMP_Text descriptionText;

    private void Start()
    {
        infoPanel.SetActive(false);
    }

    public void ShowInfo(string title, string description)
    {
        titleText.text = title;
        descriptionText.text = description;

        infoPanel.SetActive(true);
    }

    public void HideInfo()
    {
        infoPanel.SetActive(false);
    }

    public void ShowDemoInfo()
    {
        Debug.Log("ShowDemoInfo called");
        ShowInfo(
            "Sacrum",
            "The sacrum is a triangular bone located at the base of the spine. It forms the posterior part of the pelvis."
        );
    }
}
