using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroductionManager : MonoBehaviour
{
    private bool isPopupOn;
    // Start is called before the first frame update
    void Start()
    {
        isPopupOn = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPopup(GameObject popup, bool isActive, string message)
    {
        isPopupOn = isActive;
        Transform popupText = popup.GetComponent<Transform>().Find("Image").Find("Text");
        popupText.GetComponent<UnityEngine.UI.Text>().text = message;
        popup.SetActive(isActive);
    }

    public void DisablePopup(GameObject popup)
    {
        isPopupOn = false;
        popup.SetActive(false);
    }
}
