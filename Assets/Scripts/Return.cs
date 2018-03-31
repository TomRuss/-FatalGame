using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Return : MonoBehaviour {

	//Returns us to main menu through operator selection.
	public void ReturnToMenu() {
		SceneManager.LoadScene("NewMainMenu");
	}

}
