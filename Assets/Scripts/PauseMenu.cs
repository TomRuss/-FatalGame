using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

	// THIS IS NOT THE FINAL PAUSE MENU, THIS IS FOR TESTING PURPOSES FOR RIGHT NOW, UNTIL WE GET DONE DESIGNING THE PAUSE MENU.

	void Update() {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			Cursor.visible = true;
			SceneManager.LoadScene(0);
		}

	}

}
