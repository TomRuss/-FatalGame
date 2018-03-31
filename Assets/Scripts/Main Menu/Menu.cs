using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

	public void QuitGame() {
		Application.Quit ();
		Debug.Log ("Quitting Fatal...");
	}


}
