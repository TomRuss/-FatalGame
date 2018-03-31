using UnityEngine;
using System.Collections;

public class WeaponSwitching : MonoBehaviour {

	public Animation am;
	public AnimationClip undraw;

	public GameObject[] weapons;
	public int index = 0;
	public int currentWeapon;

    public static WeaponSwitching instance;

	void Start()
	{ 
		StartCoroutine(switchW (0f, 0));
        instance = this;
	}

	void Update()
	{
		if (am.IsPlaying (undraw.name))
            return;

        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            switchWeapon(index + 1);
        }

        if (am.IsPlaying(undraw.name) || index == 0)
            return;

        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            switchWeapon(index - 1);
        }

    }

	void switchWeapon(int _index)
	{

		if (_index > weapons.Length-1)
			_index = 0;

		WeaponTwo _w = weapons [index].GetComponent <WeaponTwo> ();
		//AnimationClip _undraw = _w.undrawA;
		//if(_undraw == null)
		//{
           // WeaponAnimtions.instance.am.Stop();
			//am.CrossFade (undraw.name);
			//_undraw = undraw;
		//}
		//else
			//_w.am.CrossFade (_undraw.name);

		index = _index;
		StartCoroutine (switchW (0, index));
	}

	IEnumerator switchW(float seconds, int _index)
	{
		yield return new WaitForSeconds (seconds);
		foreach(GameObject o in weapons)
		{
			o.SetActive (false);
		}
		weapons [index].SetActive (true);
		currentWeapon = index;
	}
}
