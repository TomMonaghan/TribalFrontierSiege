using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTechBuilding : MonoBehaviour
{
    public delegate void ButtonPush();
    public ButtonPush OnButtonPush;

    private void OnMouseDown()
    {
        OnButtonPush?.Invoke();
    }

}
