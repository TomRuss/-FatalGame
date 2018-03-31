using UnityEngine;
using System.Collections;

public class Leaning : MonoBehaviour {

	public Transform weapon;

    public bool isLeaning;

    public bool canLean;

    public float smoothTime;
	public float camZClamp = 7f;
	public float weaponZClamp = 7.5f;

	public UIManager player;
    public bool isWepaon;

	void Update() {
		
		if(canLean && Input.GetKey(KeyCode.Q)) {
            isLeaning = true;
            transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(new Vector3(0f, 0f, camZClamp)), Time.deltaTime * smoothTime);
            //if(!player.weaponController.isAiming)
                //weapon.localRotation = Quaternion.Slerp(weapon.localRotation, Quaternion.Euler(new Vector3(0f, 0f, weaponZClamp)), Time.deltaTime * 8f);
            //else weapon.localRotation = Quaternion.Slerp(weapon.localRotation, Quaternion.Euler(new Vector3(0f, 0f, 0f)), Time.deltaTime * 8f);
		} else if(canLean && Input.GetKey(KeyCode.E)) {
            isLeaning = true;
            transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(new Vector3(0f, 0f, -camZClamp)), Time.deltaTime * smoothTime);
            //if(!player.weaponController.isAiming)
                //weapon.localRotation = Quaternion.Slerp(weapon.localRotation, Quaternion.Euler(new Vector3(0f, 0f, -weaponZClamp)), Time.deltaTime * 8f);
            //else weapon.localRotation = Quaternion.Slerp(weapon.localRotation, Quaternion.Euler(new Vector3(0f, 0f, 0f)), Time.deltaTime * 8f);
        } else {
            isLeaning = false;
            transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(new Vector3(0f, 0f, 0f)), Time.deltaTime * smoothTime);
            //weapon.localRotation = Quaternion.Slerp(weapon.localRotation, Quaternion.Euler(new Vector3(0f, 0f, 0f)), Time.deltaTime * 8f);
        }
	}
}
