using System;
using UnityEngine;

public class DeathSticker : MonoBehaviour
{
    public GameObject deathSticker;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MyGameManager.Instance?.DeathEvent.AddListener(PlaceDeathSticker);

        deathSticker.gameObject.SetActive(false);
    }

    private void PlaceDeathSticker(Transform arg0)
    {
        deathSticker.transform.position = arg0.position;
        deathSticker.gameObject.SetActive(true);
    }

}
