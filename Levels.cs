using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Levels : MonoBehaviour {

	//public int currentLevel;
	public Transform respawnPoint;
	private float respawnTimer = 1.4f;
	public Transform player;
	public GameObject deathScreen;
	public GameObject winScreen;
	public GameObject level2;
	public GameObject level3;
	public GameObject level4;
	public GameObject level5;
	public GameObject level6;
	private int maxLevels = 6;
	public int currentLevel;
	private int lastLevel;

	void Start() {
		//unless main menu, lock AND HIDE da fuckin mouse dumbass!!!!!!
		if(SceneManager.GetActiveScene().buildIndex == 0) {
			Cursor.lockState = CursorLockMode.None; Cursor.visible = true;
		} else {
			Cursor.visible = false;
		}
        //rest
		Debug.Log("Active Scene name is: " + SceneManager.GetActiveScene().name + "\nActive Scene index: " + SceneManager.GetActiveScene().buildIndex);
		currentLevel = PlayerPrefs.GetInt("lastCompletedLevel");
		lastLevel = currentLevel-1;
		Debug.Log(currentLevel + " is the ID of 'currentLevel'");
		Debug.Log(lastLevel + " is the ID of your last completed level");
		switch(PlayerPrefs.GetInt("lastCompletedLevel")) {
			case 2:
				level2.SetActive(true);
				break;
			case 3:
				level2.SetActive(true);
				level3.SetActive(true);
				break;
			case 4:
				level2.SetActive(true);
				level3.SetActive(true);
				level4.SetActive(true);
				break;
			case 5:
				level2.SetActive(true);
				level3.SetActive(true);
				level4.SetActive(true);
				level5.SetActive(true);
				break;
			case 6:
				level2.SetActive(true);
				level3.SetActive(true);
				level4.SetActive(true);
				level5.SetActive(true);
				level6.SetActive(true);
				break;
		}
	}
	
	public void Level1Button() {
		SceneManager.LoadScene(1);
	}
	public void Level2Button() {
		SceneManager.LoadScene(2);
	}
	public void Level3Button() {
		SceneManager.LoadScene(3);
	}
	public void Level4Button() {
		SceneManager.LoadScene(4);
	}
	public void Level5Button() {
		SceneManager.LoadScene(5);
	}
	public void Level6Button() {
		SceneManager.LoadScene(6);
	}
	public void Reset() {
		PlayerPrefs.SetInt("lastCompletedLevel", 0);
		//how do i load the first level then
		currentLevel = 1;
		level2.SetActive(false);
		level3.SetActive(false);
		level4.SetActive(false);
		level5.SetActive(false);
		level6.SetActive(false);
	}

	void OnTriggerEnter(Collider col) {
		//check what the player landed on
		if(col.transform.tag == "death") {
			StartCoroutine(RestartLevel());
		}
		if(col.transform.tag == "finishLine") {
			StartCoroutine(WinLevel());
		}
	}

	public IEnumerator WinLevel() {
		Debug.Log("win");
		int nextLevel = SceneManager.GetActiveScene().buildIndex + 1;
		PlayerPrefs.SetInt("lastCompletedLevel", nextLevel);
		Debug.Log(PlayerPrefs.GetInt("lastCompletedLevel" + " is the ID of the new lastCompletedLevel"));
		//make player imobile to further alert them that they completed the game
		PlayerController controller = player.GetComponent<PlayerController>();
		Rigidbody rb = player.GetComponent<Rigidbody>();
		controller.enabled = false;
		rb.isKinematic = true;
		winScreen.SetActive(true);
		yield return new WaitForSeconds(respawnTimer);
		//last thing is loading the scene!! dont fuck up zang!!
		if(PlayerPrefs.GetInt("lastCompletedLevel") < maxLevels+1) {
			SceneManager.LoadScene(nextLevel);
		}else{
			Debug.Log("You completed the game!!!!");
			PlayerPrefs.SetInt("lastCompletedLevel", maxLevels);
			SceneManager.LoadScene(0);
		}
	}
	public IEnumerator RestartLevel() {
		Debug.Log("Player died!");
		//disable the controller and remove gravity-ish so the player doesnt slide if they had too much momentum!
		PlayerController controller = player.GetComponent<PlayerController>();
		Rigidbody rb = player.GetComponent<Rigidbody>();
		controller.enabled = false;
		rb.isKinematic = true;
		deathScreen.SetActive(true);
		//reset position and then force them to wait
		player.position = respawnPoint.position;
		yield return new WaitForSeconds(respawnTimer);
		//reenable shit
		rb.isKinematic = false;
		controller.enabled = true;
		deathScreen.SetActive(false);
	}
}
