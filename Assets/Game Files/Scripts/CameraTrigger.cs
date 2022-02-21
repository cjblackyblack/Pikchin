using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrigger : MonoBehaviour
{
	public GameObject cameraObj;

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.GetComponent<pikchin>())
			cameraObj.SetActive(true);
	}
}
