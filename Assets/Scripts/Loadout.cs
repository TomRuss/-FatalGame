using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loadout : MonoBehaviour {

	public GameObject camera;
	public GameObject fpsUI;
	public GameObject aI;

	public GameObject player;
	public GameObject assualt;
	public GameObject medic;
	public GameObject sniper;
	public GameObject support;

	public GameObject medicText;
	public GameObject sniperText;
	public GameObject assualtText;

	public GameObject assualtWeaponUI;
	public GameObject classesUI;
	public GameObject medicWeaponUI;
	public GameObject sniperWeaponUI;

	public void Assualt() {
		fpsUI.SetActive (false);
		player.SetActive (false);
		medic.SetActive (true);
		sniper.SetActive (true);
		support.SetActive (true);
		aI.SetActive (false);
		assualtText.SetActive (true);
		assualtWeaponUI.SetActive (true);
		medicText.SetActive (false);
		medicWeaponUI.SetActive (false);
		sniperText.SetActive (false);
		sniperWeaponUI.SetActive (false);
	}

	public void Medic() {
		fpsUI.SetActive (false);
		player.SetActive (false);
		medic.SetActive (true);
		sniper.SetActive (true);
		support.SetActive (true);
		aI.SetActive (false);
		assualtText.SetActive (false);
		assualtWeaponUI.SetActive (false);
		medicWeaponUI.SetActive (true);
		medicText.SetActive (true);
		sniperText.SetActive (false);
		sniperWeaponUI.SetActive (false);
	}

	public void Sniper() {
		fpsUI.SetActive (false);
		player.SetActive (false);
		medic.SetActive (true);
		sniper.SetActive (true);
		support.SetActive (true);
		aI.SetActive (false);
		assualtText.SetActive (false);
		assualtWeaponUI.SetActive (false);
		medicWeaponUI.SetActive (false);
		medicText.SetActive (false);
		assualt.SetActive (true);
		sniperText.SetActive (true);
		sniperWeaponUI.SetActive (true);
	}

	public void ConfirmLoadout() {
		support.SetActive (false);
		sniper.SetActive (false);
		medic.SetActive (false);
		assualt.SetActive (false);
		aI.SetActive (true);
		camera.SetActive (false);
		player.SetActive (true);
		fpsUI.SetActive (true);
		assualtText.SetActive (false);
		assualtWeaponUI.SetActive (false);
		classesUI.SetActive (false);
		medicText.SetActive (false);
		medicWeaponUI.SetActive (false);
		sniperText.SetActive (false);
		sniperWeaponUI.SetActive (false);
	}
}
