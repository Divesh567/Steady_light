using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamond : MonoBehaviour
{

    [SerializeField]
    private int _diamondNo;

    public int diamondNo
    {

        get { return _diamondNo; }
        set
        {
            _diamondNo = value;
        }
    }

    private DiamondController _diamondController;

    private void Start()
    {
        _diamondController = transform.parent.gameObject.GetComponent<DiamondController>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            _diamondController.CollectedDiamondNumber(_diamondNo);
            SoundManager.Instance.PlayDiamondCollected();
            gameObject.SetActive(false);
        }
    }

    public int GetDiamondNo()
    {
        return _diamondNo;
    }

   
}
