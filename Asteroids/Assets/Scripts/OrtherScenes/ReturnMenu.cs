using UnityEngine;
using System.Collections;

public class ReturnMenu : MonoBehaviour {

	public void ReturnToMenu () {
		Application.LoadLevel("MenuScene");
	}
}
