using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CustomButton : MonoBehaviour
{
    public Button button;
    public Image image;
    public TextMeshProUGUI textMesh;


    public AudioSource audioSource;
    public AudioController audioController;
    public AudioClip audioClip;

    public virtual void Start()
    {
        button.onClick.AddListener(() => audioController.PlayAudio(audioSource, audioClip));
    }
}
