using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class spawner : MonoBehaviour {

	public Camera camera;
	public float maxDistance = 150.0f;

	public GameObject[] spawnees;
	public Image[] spawneeIcons;
	public GameObject currentSpawneeIconBorder;

	private int currentSpawneeId = 0;
	public float mass = 100f;

	// Use this for initialization
	void Start () {
		setCurrentSpaneeId(0);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Fire2"))
		{
			spawnBlock();
		}
		else if (Input.GetButtonDown("Fire1"))
		{
			destroyBlock();
		}

		//Tekertem-e fölfele a görgőt:
		if(Input.GetAxis("Mouse ScrollWheel") > 0)
		{
			increaseCurrentSpawneeId();
		}
		//Tekertem-e lefele a görgőt:
		else if (Input.GetAxis("Mouse ScrollWheel") < 0)
		{
			decreaseCurrentSpawneeId();
		}

		if (Input.GetKeyDown(KeyCode.Alpha1)) { setCurrentSpaneeId(0); }
		if (Input.GetKeyDown(KeyCode.Alpha2)) { setCurrentSpaneeId(1); }
		if (Input.GetKeyDown(KeyCode.Alpha3)) { setCurrentSpaneeId(2); }
		if (Input.GetKeyDown(KeyCode.Alpha4)) { setCurrentSpaneeId(3); }
		if (Input.GetKeyDown(KeyCode.Alpha5)) { setCurrentSpaneeId(4); }
		if (Input.GetKeyDown(KeyCode.Alpha6)) { setCurrentSpaneeId(5); }
		if (Input.GetKeyDown(KeyCode.Alpha7)) { setCurrentSpaneeId(6); }
		if (Input.GetKeyDown(KeyCode.Alpha8)) { setCurrentSpaneeId(7); }

		if (Input.GetKeyDown(KeyCode.H))
		{
			GameObject massBar = GameObject.Find("/Canvas/MassBarBackground/MassBar");
			massBar.GetComponent<Image>().fillAmount += 0.05f;
			mass = massBar.GetComponent<Image>().fillAmount * 200;
		}
		else if (Input.GetKeyDown(KeyCode.L))
		{
			GameObject massBar = GameObject.Find("/Canvas/MassBarBackground/MassBar");
			massBar.GetComponent<Image>().fillAmount -= 0.05f;
			mass = massBar.GetComponent<Image>().fillAmount * 200;
		}
	}

	void setCurrentSpaneeId(int spawneeId)
	{
		try { Destroy(spawneeIcons[currentSpawneeId].transform.GetChild(0).gameObject); } catch { }
		if (spawneeId >= spawnees.Length)
		{
			currentSpawneeId = 0;
		}
		else if(spawneeId < 0)
		{
			currentSpawneeId = spawnees.Length - 1;
		}
		else
		{
			currentSpawneeId = spawneeId;
		}
		Instantiate(currentSpawneeIconBorder, spawneeIcons[currentSpawneeId].transform, false);
		
	}
	void increaseCurrentSpawneeId()
	{
		setCurrentSpaneeId(currentSpawneeId + 1);
	}
	void decreaseCurrentSpawneeId()
	{
		setCurrentSpaneeId(currentSpawneeId - 1);
	}
	void spawnBlock()
	{
		RaycastHit hit;
		if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, maxDistance))
		{
			Vector3 spawnPoint = hit.point;
			spawnPoint.y += spawnees[currentSpawneeId].transform.lossyScale.y / 2;

			GameObject spawnee = Instantiate(spawnees[currentSpawneeId], spawnPoint, Quaternion.LookRotation(hit.normal));
			spawnee.GetComponent<Rigidbody>().mass = mass;
			spawnee.transform.SetParent(GameObject.Find("Blocks").transform);
		}
	}
	void destroyBlock()
	{
		RaycastHit hit;
		if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, maxDistance) && hit.transform.IsChildOf(GameObject.Find("Blocks").transform))
		{
			Destroy(hit.collider.gameObject);
		}
	}
}
