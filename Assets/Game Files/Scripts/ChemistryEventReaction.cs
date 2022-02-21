using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChemistryEventReaction : MonoBehaviour
{
	public ChemistryObject monitoredObject;
	public ChemistryState GoalState;
	public GameObject objToDestroy;

	private void Update()
	{
		if (monitoredObject.ChemistryState == GoalState)
			Destroy(objToDestroy);
	}
}

