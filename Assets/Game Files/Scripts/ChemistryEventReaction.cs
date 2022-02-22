using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChemistryEventReaction : MonoBehaviour
{
	public ChemistryObject monitoredObject;
	public ChemistryState GoalState;
	public GameObject objToDestroy;
	public bool endGame = false;

	private void Update()
	{
		if (monitoredObject.ChemistryState == GoalState)
		{
			if (endGame)
				StartCoroutine(EndGame());
			else
				Destroy(objToDestroy);
		}
	}

	IEnumerator EndGame()
    {
		monitoredObject.GetComponent<MeshRenderer>().enabled = false;
		yield return new WaitForSeconds(5);
		Application.Quit();
	}
}

