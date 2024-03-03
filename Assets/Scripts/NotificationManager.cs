using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationManager : MonoBehaviour
{
    public static NotificationManager Instance;
    public GameObject NotificationPanel;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void ShowNotification(string message)
    {
        NotificationPanel.SetActive(true);
        NotificationPanel.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = message;
        StartCoroutine(HideNotification());
    }

    IEnumerator HideNotification()
    {
        yield return new WaitForSeconds(3);
        NotificationPanel.SetActive(false);
    }

}
