using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoadingScreen : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    [SerializeField]
   private List<string> _tips;

    [SerializeField]
    private List<Sprite> _tipImages;

    private TextMeshProUGUI _tipText;
    private Image _tipImage;

    private int index;

    private void Start()
    {
        _tipText = transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        _tipImage = transform.GetChild(1).GetComponent<Image>();
         index = Random.Range(0, _tips.Count);
        _tipText.text = _tips[index];
        _tipImage.sprite = _tipImages[index];
    }
}
