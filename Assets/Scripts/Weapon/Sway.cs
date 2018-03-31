using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class Sway : MonoBehaviour
{
	[Range(0f, 5000f)]
	public float lerpUI;
	public GameObject uiParent;
	//public GameObject minimapParent;
    public float amount = 0.055f;
    public float maxAmount = 0.09f;
    public float smooth = 3;
    Vector3 def;
    Vector2 defAth;
    Vector3 euler;

    //start
    void Start()
    {
        def = transform.localPosition;
        euler = transform.localEulerAngles;
    }

    // smooth mouse look
    float _smooth;
    void Update()
    {
        _smooth = smooth;

        float factorX = -Input.GetAxis("Mouse X") * amount;
        float factorY = -Input.GetAxis("Mouse Y") * amount;

        if (factorX > maxAmount)
            factorX = maxAmount;
        if (factorX < -maxAmount)
            factorX = -maxAmount;
        if (factorY > maxAmount)
            factorY = maxAmount;
        if (factorY < -maxAmount)
            factorY = -maxAmount;

        Vector3 final = new Vector3(def.x + factorX, def.y + factorY, def.z);
        transform.localPosition = Vector3.Lerp(transform.localPosition, final, Time.deltaTime * _smooth);

		Vector3 lerpUI = new Vector2(defAth.x + factorX * 300f, defAth.y + factorY * 300f);
		uiParent.GetComponent<RectTransform> ().anchoredPosition = Vector2.Lerp(uiParent.GetComponent<RectTransform>().anchoredPosition, lerpUI, Time.deltaTime * _smooth);
		//minimapParent.GetComponent<RectTransform> ().anchoredPosition = Vector2.Lerp(minimapParent.GetComponent<RectTransform>().anchoredPosition, lerpUI, Time.deltaTime * _smooth);
    }

}
