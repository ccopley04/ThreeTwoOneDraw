using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleMenu : MonoBehaviour
{
    private bool isToggled = false;
    [SerializeField] private GameObject targetButton;

    public void ChangeVisibility()
    {
        targetButton.SetActive(!isToggled);
        isToggled = !isToggled;
    }
}
