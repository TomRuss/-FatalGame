using UnityEngine;
using System.Collections;

public class Inspect : MonoBehaviour {

	[Header("INSPECT")]
	public bool canInspect;
	public bool inspectingWeapon;
	public float inspectTime;

	[Space(5)]

	[Header("ANIMATIONS")]
	public Animation am;
	public AnimationClip inspect;
	public AnimationClip go;
	public AnimationClip hold;
	public AnimationClip back;
	public AnimationClip no;
	public AnimationClip ok;

	[Space(5)]

	[Header("HAND SIGNALS")]
	public bool canDoHandSignals;
	public bool doingHandSignals;
	public float handSignalTime;
	
	void Update() {
		if(Input.GetKeyDown(KeyCode.F) && canInspect && !inspectingWeapon) StartCoroutine(InspectWeapon());
		if (!canInspect && inspectingWeapon) {
			inspectingWeapon = false;
		}

		if(Input.GetKeyDown(KeyCode.F1) && canDoHandSignals && !doingHandSignals) StartCoroutine(GoAnimation());
		if (!canDoHandSignals && doingHandSignals) {
			doingHandSignals = false;
		}

		if (Input.GetKeyDown (KeyCode.F2) && canDoHandSignals && !doingHandSignals) StartCoroutine (HoldAnimation ());
		if (!canDoHandSignals && doingHandSignals) {
			doingHandSignals = false;
		}

		if (Input.GetKeyDown (KeyCode.F3) && canDoHandSignals && !doingHandSignals) StartCoroutine (BackAnimation ());
		if (!canDoHandSignals && doingHandSignals) {
			doingHandSignals = false;
		}

		if (Input.GetKeyDown (KeyCode.F4) && canDoHandSignals && !doingHandSignals) StartCoroutine (NoAnimation ());
		if (!canDoHandSignals && doingHandSignals) {
			doingHandSignals = false;
		}

		if(Input.GetKeyDown (KeyCode.F5) && canDoHandSignals && !doingHandSignals) StartCoroutine (OkAnimation());
		if (!canDoHandSignals && doingHandSignals) {
			doingHandSignals = false;
		}
	}

	IEnumerator OkAnimation() {
		doingHandSignals = true;
		am.Play (ok.name);
		yield return new WaitForSeconds (handSignalTime);
		doingHandSignals = false;
		am.Stop (ok.name);
	}

	IEnumerator NoAnimation() {
		doingHandSignals = true;
		am.Play (no.name);
		yield return new WaitForSeconds (handSignalTime);
		doingHandSignals = false;
		am.Stop (no.name);
	}

	IEnumerator BackAnimation() {
		doingHandSignals = true;
		am.Play (back.name);
		yield return new WaitForSeconds (handSignalTime);
		doingHandSignals = false;
		am.Stop (back.name);
	}

	IEnumerator HoldAnimation() {
		doingHandSignals = true;
		am.Play (hold.name);
		yield return new WaitForSeconds (handSignalTime);
		doingHandSignals = false;
		am.Stop (hold.name);
	}

	IEnumerator GoAnimation() {
		doingHandSignals = true;
		am.Play (go.name);
		yield return new WaitForSeconds (handSignalTime);
		doingHandSignals = false;
		am.Stop (go.name);
	}
	
	IEnumerator InspectWeapon() {
		inspectingWeapon = true;
		am.Play (inspect.name);
		yield return new WaitForSeconds(inspectTime);
		inspectingWeapon = false;
		am.Stop (inspect.name);
	}
}