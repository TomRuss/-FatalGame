using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class PlayerUITwo : MonoBehaviour {

	[Header("ATH")]
	public Text ammoCounter;
	public Text totalAmmoCounter;
	public Text HealthCounter;
    public Image healthBar;
    public Text weaponNameText;
    public Image automaticImage;
    public Image semi_automaticImage;
    public Image non_automaticImage;
	[Header("ATH SWAY")]
	public GameObject athParent;
    [Header("CROSSHAIR")]
    public GameObject crossHair;
	public RectTransform[] rectTransform;
    public float fireEffectTime = 15f;
	[Header("weapon pickup")]
	public Text switchWeaponName;
	public GameObject parentSwitch;

	public static PlayerUITwo instance;

	float walkSize;
	public float currentSize;

	void Awake() {
		instance = this;

		walkSize = rectTransform [0].localPosition.y;
	}

    float shootEffect = 0f;
	void CalculateCrossHairPosition() {
	       
        if (fireFxState == fireEffectsState.PLUS && shootEffect < 38f) shootEffect += 30 * Time.deltaTime * fireEffectTime;
        if (fireFxState == fireEffectsState.BACK) shootEffect -= 10 * Time.deltaTime * fireEffectTime;
        if (shootEffect < 0)
        {
            fireFxState = fireEffectsState.NONE;
            fireFxState = 0;
        }
        currentSize += shootEffect;
	}

    enum fireEffectsState
    { PLUS, BACK, NONE  }
    fireEffectsState fireFxState = fireEffectsState.NONE;
    public void ApplyCrossHairShootEffect()
    {
        fireFxState = fireEffectsState.PLUS;
        CancelInvoke();
        Invoke("BackToIdle", 1f/fireEffectTime);
    }
    void BackToIdle()
    { fireFxState = fireEffectsState.BACK; }
}
