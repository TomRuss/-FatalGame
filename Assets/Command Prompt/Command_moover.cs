using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Command_moover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public GameObject command;

    bool isOver = false;

    private void Update()
    {
        if (!command.activeSelf || !Input.GetMouseButton(0)) return;
        command.transform.position += new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0f) * 19f;
    }

    public void OnPointerEnter(PointerEventData eventData)
    { isOver = true;  }
    public void OnPointerExit(PointerEventData eventData)
    { isOver = false; }
}