using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleMenu : MonoBehaviour
{
    [SerializeField] private GameObject targetButton;
    public void ChangeVisibility()
    {
        if (targetButton != null)
        {
            bool isActive = targetButton.activeSelf;
            targetButton.SetActive(!isActive);
        }
        //isToggled = !isToggled;
    }
}
