using System.Collections;
using UnityEngine;

public class TutorialSwitch : MonoBehaviour
{
    public static bool TurorialOn = true;
    [SerializeField]
    private GameObject _tutotrialPanel;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (TurorialOn)
        {
            if (collision.gameObject.tag == "Ball")
            {
                StartCoroutine(EnableTutorial());
            }
            else
            {
                return;
            }
        }
    }
    IEnumerator EnableTutorial()
    {
        if (GameMenu.Instance != null)
        {
            GameMenu.Instance.GamePanelClose();
        }
        GetComponent<Collider2D>().enabled = false;
        Instantiate(_tutotrialPanel, _tutotrialPanel.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.35f);
    }


}
