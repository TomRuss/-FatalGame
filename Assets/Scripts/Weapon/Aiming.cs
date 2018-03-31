using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Aiming : MonoBehaviour {

    public float time = 2;
    Vector3 point = Vector3.zero;
    Quaternion rotation;

    public static Aiming instance;
    public bool aiming;
	public bool canAim;
    public float alpha = 1f;
	public Camera cam;

	public float zoomNormal = 60;
	public float zoomAmount = 45;
	public float zoomSpeed = 5;

    void Awake()
    {
        instance = this;
        rotation = Quaternion.Euler(Vector3.zero);
    }

    void Update () {
		if (Input.GetKey (KeyCode.LeftShift) || WeaponTwo.instance.am.IsPlaying (WeaponTwo.instance.reloadA.name)) 
		{
			canAim = false;
            aiming = false;
            return;
		} else 
		{
			canAim = true;
		}

		if (Input.GetMouseButton (1) && canAim == true) 
		{
			cam.fieldOfView = Mathf.Lerp (cam.fieldOfView, zoomAmount, Time.deltaTime * zoomSpeed);
		} else 
		{
			cam.fieldOfView = Mathf.Lerp (cam.fieldOfView, zoomNormal, Time.deltaTime * zoomSpeed);
		}

		if(Input.GetMouseButtonUp(1))
        {
            point = Vector3.zero;
            rotation = Quaternion.Euler(Vector3.zero);
            aiming = false;
        }

        transform.localPosition = Vector3.Lerp(transform.localPosition, point, Time.deltaTime * time);
        transform.localRotation = Quaternion.Lerp(transform.localRotation, rotation, Time.deltaTime * time);
        
        if(point != Vector3.zero && alpha > 0f)
            alpha -= 1f * Time.deltaTime * time;
        else if(alpha < 1f)
            alpha += 1f * Time.deltaTime * time;

        foreach (Transform _cross in UIManager.instance.crosshairs)
        {
            _cross.GetComponent<Image>().color = new Color(1f, 1f, 1f, alpha);
        }
    }

    public void Aim(Vector3[] aimPoint)
    {
        point = aimPoint[0];
        rotation = Quaternion.Euler(transform.localEulerAngles + aimPoint[1]);
        aiming = true;
    }
}
