using UnityEngine;
using System.Collections;

public class W : MonoBehaviour
{
	[Header("Weapon Setup")]
	public Vector3 aimPos;
	public float fireRate = 0.1f;
	public int magSize = 30;
	public bool isShotgun;
	private int currentAmmo;
	private Vector3 startPos;
	private bool aiming;

	private bool active = true;

	[Header("Weapon Animatios")]
	public string draw = "Draw";
    public string hide = "Hide";
    public string inspection = "inspection";

    public string fire = "Fire";
	public string fireAim = "FireAim";
	public string fireLast = "FireLast";

	public string reload = "Reload";
	public string completeReload = "CompleteReload";

	public string startReload = "ReloadStart";
	public string insert = "Insert";
	public string endReload = "ReloadEnd";

	private float nextShotTime = 0;
	private float nextReloadTime = 0;

	private void Start ()
	{
		currentAmmo = magSize;
		startPos = transform.position;
	}

	public float GetHideTime ()
	{
		return GetComponent<Animation>()[hide].length;
	}

	public void Deselect ()
	{
		aiming = false;
		StartCoroutine(DeselectAnim());
	}

	private IEnumerator DeselectAnim ()
	{
		Hide();
		active = false;
		yield return new WaitForSeconds(GetHideTime());
	}

	public void Select ()
	{
		StartCoroutine(SelectAnim());
	}

	private IEnumerator SelectAnim()
	{
		Draw();
		yield return new WaitForSeconds(GetComponent<Animation>()[draw].length);
		active = true;
	}

	// Update is called once per frame
	private void Update ()
	{
		if (active)
		{
			if (aiming)
			{
				transform.position = Vector3.Lerp(transform.position, aimPos, Time.deltaTime * 10);
			}
			else
			{
				transform.position = Vector3.Lerp(transform.position, startPos, Time.deltaTime * 10);
			}

			if (Input.GetKeyDown(KeyCode.R) && currentAmmo < magSize)
			{
				if (isShotgun)
				{
					aiming = false;
					StartCoroutine(ReloadShotgun());
				}
				else
				{
					if (currentAmmo > 0)
					{
						aiming = false;
						Reload();
					}
					else
					{
						aiming = false;
						CompleteReload();
					}
				}

			}

			if (Input.GetKey(KeyCode.Mouse0))
			{
				if (nextShotTime < Time.time && currentAmmo > 0 && nextReloadTime < Time.time)
				{
					if (aiming)
					{
						if (currentAmmo == 1 && fireLast != "")
						{
							FireLast();
						}
						else
							FireAim();
					}
					else
					{
						if (currentAmmo == 1 && fireLast != "")
						{
							FireLast();
						}
						else
							Fire();
					}
				}
			}

			if (Input.GetKeyDown(KeyCode.Mouse1))
			{
				if (!aiming)
				{
					aiming = true;
				}
				else
				{
					aiming = false;
				}
			}
		}
		else
		{
			aiming = false;
		}
    }

	public void Draw ()
	{
		GetComponent<Animation>().Stop();
		GetComponent<Animation>().Play(draw);
	}

	public void Hide()
	{
		GetComponent<Animation>().Stop();
		GetComponent<Animation>().Play(hide);
	}

	public void Fire()
	{
		currentAmmo--;
		nextShotTime = Time.time + fireRate;
		GetComponent<Animation>().Stop();
		GetComponent<Animation>().Play(fire);
	}

	public void FireLast()
	{
		currentAmmo--;
		nextShotTime = Time.time + fireRate;
		GetComponent<Animation>().Stop();
		GetComponent<Animation>().Play(fireLast);
	}

	public void FireAim()
	{
		currentAmmo--;
		nextShotTime = Time.time + fireRate;
		GetComponent<Animation>().Stop();
		GetComponent<Animation>().Play(fireAim);
	}

	public IEnumerator ReloadShotgun()
	{
		GetComponent<Animation>().Stop();
		nextReloadTime = Time.time + GetComponent<Animation>()[startReload].length + ((magSize - currentAmmo) * GetComponent<Animation>()[insert].length) 
			+ GetComponent<Animation>()[endReload].length;

		GetComponent<Animation>().Play(startReload);
		yield return new WaitForSeconds(GetComponent<Animation>()[startReload].length);
		while (currentAmmo < magSize)
		{
			GetComponent<Animation>().Stop();
			currentAmmo++;
			GetComponent<Animation>().Play(insert);
			yield return new WaitForSeconds(GetComponent<Animation>()[insert].length);
		}

		GetComponent<Animation>().Stop();
		GetComponent<Animation>().Play(endReload);
	}

	public void Reload()
	{
		nextReloadTime = Time.time + GetComponent<Animation>()[reload].length;
		currentAmmo = magSize;
		GetComponent<Animation>().Stop();
		GetComponent<Animation>().Play(reload);
	}

	public void CompleteReload()
	{
		nextReloadTime = Time.time + GetComponent<Animation>()[completeReload].length;
		currentAmmo = magSize;
		GetComponent<Animation>().Stop();
		GetComponent<Animation>().Play(completeReload);
	}

	private void OnGUI ()
	{
		float offset = Screen.height / 7.2f;
		GUI.Box(new Rect(Screen.width - offset * 2, Screen.height - offset / 2, offset, offset / 3.5f), currentAmmo + " / 999");
	}
}
