using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class WeaponTwo : MonoBehaviour {

    public bool isShotgun;
	public Camera fpsCam;

    [Header("Animations")]

	public Animation am;
	public AnimationClip undrawA;
	public AnimationClip fireA;
	public AnimationClip reloadA;
	public AnimationClip reloadB;
    [Space(5)]

    [Header("Weapon Settings")]

	public int totalAmmo = 120;
	public int clipSize = 30;
	public int ammo;
	public int cooldown = 11;
    int _cooldown = 0;
    public float bulletForce = 200f;
    public float range = 1000f;
	public float hitForceForEnviroment;

    public WeaponTwo.fireMode mode;

	public bool canReload = false;
    [Space(5)]

    [Header ("Damage")]

	public float damage = 10f;

    public int headDamage = 70;
    public int maxHeadDamage = 90;

    public int bodyDamage = 5;
    public int maxBodyDamage = 9;

    public int legDamage = 3;
    public int maxLegDamage = 5;

	public GameObject thisWep;
    [Space(5)]

    [Header("Audio")]

	public AudioSource audFire;
    public AudioSource smallVerb;
    public AudioClip fireSound;
	public AudioClip reloadSound;
	public AudioClip emptySound;
    [Space (5)]

	[Header("Recoil")]
	public Transform recoilCam;

	public float recoilSpeed = 45f;
	public float recoilBackSpeed = 45f;
	public float maxRecoil = 5f;

	public float recoilBack = -20f;
	public float recoilBackPosSpeed = 10f;

    public float recoilPower = 30f;

    float currentAngle = 0f;
    [Space(5)]

    [Header("Fire")]

	public Camera cam;
	public ParticleSystem muzzleFlash;
    public Transform muzzleTrans;
	public GameObject impactEffect;
    GameObject mz;
    [Space(5)]

    [Header("Crosshair")]

	public float crossHairSize;

    [Space(5)]

    [Header("Aiming")]

    public Vector3[] aimPoints;

    public static WeaponTwo instance;

	public enum fireMode
	{
		AUTOMATIC,
		SEMI_AUTOMATIC
	}

	void Awake () 
	{
		if (recoilCam == null && transform.parent != null)
			recoilCam = transform.parent;
		instance = this;
	}

	void Start()
	{
		if (thisWep.activeSelf) 
		{
			UIManager.instance.UpdateAmmo (ammo);
			UIManager.instance.UpdateTotalAmmo (totalAmmo);
		}

		ammo = clipSize;
		InvokeRepeating ("UpdateCooldown", 0.01f, 0.01f);
	}

	void UpdateCooldown()
	{
		if (_cooldown > 0)
			_cooldown--;
	}

	void reload()
	{
		if (ammo == clipSize || totalAmmo <= 0)
			return;

        StartCoroutine(updateReload());
        am.CrossFade (reloadA.name);
		smallVerb.clip = reloadSound;
		smallVerb.Play ();

        if (totalAmmo >= (clipSize - ammo))
		{
			totalAmmo -= (clipSize - ammo);
			ammo += (clipSize - ammo);
		}
		else
		{
			ammo += totalAmmo;
			totalAmmo = 0;
		}

		canReload = false;
	}

    IEnumerator updateReload ()
    {
        yield return new WaitForSeconds(reloadA.length);
        UIManager.instance.UpdateAmmo(ammo);
        UIManager.instance.UpdateTotalAmmo(totalAmmo);

    }

    void fire()
    {
        if (ammo <= 0 || Input.GetKey(KeyCode.LeftShift) || am.IsPlaying(reloadA.name))
        {
            return;
        }

        if (!isShotgun)
        {
            if (ammo <= 0)
				smallVerb.clip = emptySound;
			smallVerb.Play ();
			ParticleSystem muzzle = Instantiate(muzzleFlash, muzzleTrans.position, muzzleTrans.rotation);
            Destroy(muzzle, 2);
            Destroy(mz, 2);
            currentAngle = Mathf.LerpAngle(currentAngle, maxRecoil, recoilSpeed * Time.deltaTime);
            _cooldown = cooldown;

            ammo -= 1;
            UIManager.instance.UpdateAmmo(ammo);

            cam.transform.Rotate(Vector3.forward, Random.Range(20, 30) * Time.deltaTime);

            am.Stop();
            am.Play(fireA.name);

            muzzleFlash.Play();

            RaycastHit hit;
			if (Physics.Raycast (fpsCam.transform.position, fpsCam.transform.forward, out hit, range)) {
				Debug.Log (hit.transform.name);

				Target target = hit.transform.GetComponent<Target> ();
				if (target != null) {
					target.TakeDamage (damage);
				}

				if (hit.rigidbody != null) {
					hit.rigidbody.AddForce (-hit.normal * bulletForce);
				}

				GameObject impactParticles = Instantiate (impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
				Destroy (impactParticles, 2f);
			}
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			if (Physics.Raycast (ray, out hit)) {
				if (hit.rigidbody != null)
					hit.rigidbody.AddForce (ray.direction * hitForceForEnviroment);
				}
			}

        }

	public void playSound () 
	{
        audFire.PlayOneShot (fireSound);
	}

    IEnumerator PlayEmptyDown () 
	{
		yield return new WaitForSeconds (0.01f);
		if (ammo == 0 || totalAmmo == 0) 
		{
            
		}
	}

	void Update()
	{
		if (Input.GetMouseButton (0) && ammo > 0 && !am.IsPlaying (reloadB.name)) 
		{
			//cam.transform.localPosition = new Vector3 (-0.008f, 1.03f, recoilBack * Time.deltaTime);
		} else 
		{
			//cam.transform.localPosition = new Vector3 (-0.008f, 1.03f, 0.195f * Time.deltaTime);
			currentAngle = Mathf.LerpAngle (currentAngle, 0f, recoilBackSpeed * Time.deltaTime);
		}
		if (ammo <= 0) 
		{
			currentAngle = Mathf.LerpAngle (currentAngle, 0f, recoilBackSpeed * Time.deltaTime);
		}

        if (Input.GetMouseButtonDown(1))
            Aiming.instance.Aim(aimPoints);

		if(mode == WeaponTwo.fireMode.AUTOMATIC && Input.GetMouseButton (0) && _cooldown <= 0)
		{
			audFire.clip = fireSound;
			audFire.Play ();
			fire ();
		}
		else if(mode == WeaponTwo.fireMode.SEMI_AUTOMATIC && Input.GetMouseButtonDown (0) && _cooldown <= 0)
		{
			audFire.clip = fireSound;
			audFire.Play ();
			fire ();

		}


		if(!am.IsPlaying (fireA.name) && ((undrawA != null && !am.IsPlaying (undrawA.name)) || undrawA == null) && !am.IsPlaying (reloadA.name) && Input.GetKeyDown (KeyCode.R))
		{
			canReload = true;
			reload ();
		}
		recoilCam.transform.localRotation = Quaternion.AngleAxis (currentAngle, Vector3.left);

		if (ammo == clipSize || totalAmmo <= 0)
			canReload = false;
    }
}
