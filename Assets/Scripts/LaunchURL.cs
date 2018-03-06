using UnityEngine;
using System.Collections;

public class LaunchURL : MonoBehaviour {

	public string URL; 
	public string URL2;
	public string URL3;

	public void urlLinkOrWeb() 
	{
		Application.OpenURL (URL);
	}

	public void urlLinkOrWeb2() 
	{
		Application.OpenURL (URL2);
	}

	public void urlLinkOrWeb3() {
		Application.OpenURL (URL3);
	}
}
