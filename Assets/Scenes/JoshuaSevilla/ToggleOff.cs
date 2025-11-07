using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleOff : MonoBehaviour
{
    [SerializeField] private GameObject target;

    public void DisableVisibility()
    {
        target.SetActive(false);
    }
}
