using UnityEngine;
using System.Collections;

public class WeaponAnimtions : MonoBehaviour {

	public Animation am;
	public AnimationClip idle;
	public AnimationClip walk;
	public AnimationClip run;
	public AnimationClip undraw;
    public AnimationClip idleAim;
    public AnimationClip walkAim;

    public static WeaponAnimtions instance;

    void Awake ()
    {
        instance = this;
    }


	void Update()
	{
		if (Input.GetKey (KeyCode.W) || Input.GetKey (KeyCode.A) || Input.GetKey (KeyCode.S) || Input.GetKey (KeyCode.D) && !WeaponSwitching.instance.am.IsPlaying(undraw.name)) 
		{
            if (WeaponTwo.instance.am.IsPlaying(WeaponTwo.instance.reloadA.name))
                return;
			if (Input.GetKey (KeyCode.LeftShift))
			{
				am.CrossFade (run.name);
			} else
				am.CrossFade (walk.name);
		}
        else if (Aiming.instance.aiming == true)
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
                am.CrossFade (walkAim.name);
            else
                am.CrossFade(idleAim.name);
        }
        else 
		{
			am.CrossFade (idle.name);
		}

       
	}

}
