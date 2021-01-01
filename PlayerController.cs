using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public int walkSpeed = 8;
	public int runSpeed = 14;
	private float currentSpeed;
	public int cameraSpeed = 2;
	private Camera mainCam;
    public GameObject menuScreen;
    public bool paused;
    Levels levels;
    public int jumpForce = 250;
	
	void Start() {
		currentSpeed = walkSpeed;
		Cursor.lockState = CursorLockMode.Locked;
		mainCam = transform.Find("Camera").GetComponent<Camera>();
        levels = GetComponent<Levels>();
	}
	
	void Update () {
        Vector3 player = gameObject.transform.position;
        //did player die
        if(player.y <= 0) {
            player = levels.respawnPoint.position;
            StartCoroutine(levels.RestartLevel());
        }
        //makes player move
        if(Input.GetKey(KeyCode.LeftShift)) {
			currentSpeed = runSpeed;
		}else{
			currentSpeed = walkSpeed;
		}
        if (Input.GetKey(KeyCode.W))
        {
            player += transform.forward * currentSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            player -= transform.forward * currentSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            player += transform.right * currentSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            player -= transform.right * currentSpeed * Time.deltaTime;
        }

        gameObject.transform.position = player;

        if (Input.GetKeyDown(KeyCode.Space))//jump
        {
            if (Physics.Raycast(gameObject.transform.position, -gameObject.transform.up, 1.2f))
            {
                gameObject.GetComponent<Rigidbody>().AddForce(transform.up * jumpForce);
            }
        }
        if(Input.GetKeyDown(KeyCode.Escape)) {
            //this doesnt activate when paused is true, only when ESCAPE is pressed. all the logic is handled AFTERWARD
            ActivateMenu();
        }
        mainCam.transform.Rotate(new Vector3(-Input.GetAxis("Mouse Y") * cameraSpeed, 0, 0));
        transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X") * cameraSpeed, 0));
    }

    void ActivateMenu() {
        paused = !paused;
        if(paused) {
            menuScreen.SetActive(true);
            Time.timeScale = 0;
        } else {
            menuScreen.SetActive(false);
            Time.timeScale = 1;
        }
    }
}