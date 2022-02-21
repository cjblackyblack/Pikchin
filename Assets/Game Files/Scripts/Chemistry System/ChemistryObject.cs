using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ChemistryMaterial { Default, Wood, Metal, Water, Pikchin }
public enum ChemistryState { None, Fire, Elec, Water}

public class ChemistryObject : MonoBehaviour
{
	public bool Source;
	public ChemistryMaterial ChemistryMaterial;
	public ChemistryState ChemistryState;

	public GameObject FireVFX;
	public GameObject ShockVFX;
	public GameObject WetVFX;

	public float BurnTime;
	public float ShockTime;

	float burnTimer;
	float shockTimer;

	private void Update()
	{
		ChemistryUpdate();
	}
	private void ChemistryUpdate()
	{
		switch (ChemistryState) 
		{
			case ChemistryState.Fire:
				{
					if (!Source)
					{
						burnTimer -= Time.deltaTime;
						if (burnTimer <= 0)
							ClearChemistry();
					}
					break;
				}
			case ChemistryState.Elec:
				{
					if (!Source)
					{
						shockTimer -= Time.deltaTime;
						if (shockTimer <= 0)
							ClearChemistry();
					}
					break;
				}
			case ChemistryState.Water:
				{
					break;
				}
		}
	}


	//called by collision enter
	public void OnChemistryTransmit(ChemistryObject receiverObject)
	{
		receiverObject.OnChemistryReceive(this);
	}

	public void OnChemistryReceive(ChemistryObject transmitter)
	{
		switch (ChemistryMaterial) 
		{
			case ChemistryMaterial.Wood:
				{
					switch (transmitter.ChemistryState)
					{
						case ChemistryState.Fire:
							{
								Burn();
								break;
							}
						case ChemistryState.Elec:
							{
								if (ChemistryState == ChemistryState.Water)
								{ ChemistryState = ChemistryState.Elec; Burn(); }
								break;
							}
						case ChemistryState.Water:
							{
								Douse();
								break;
							}
					}
					break;
				}
			case ChemistryMaterial.Metal:
				{
					switch (transmitter.ChemistryState)
					{
						case ChemistryState.Fire:
							{
								break;
							}
						case ChemistryState.Elec:
							{
								Shock();
								break;
							}
						case ChemistryState.Water:
							{
								break;
							}
					}
					break;
				}
			case ChemistryMaterial.Water:
				{
					switch (transmitter.ChemistryState)
					{
						case ChemistryState.Fire:
							{
								//OnChemistryTransmit(transmitter);
								break;
							}
						case ChemistryState.Elec:
							{
								Shock();
								break;
							}
						case ChemistryState.Water:
							{
								//if(ChemistryState == ChemistryState.Elec)
									//OnChemistryTransmit(transmitter);
								break;
							}
					}
					break;
				}
			case ChemistryMaterial.Pikchin:
				{
					switch (transmitter.ChemistryState)
					{
						case ChemistryState.Fire:
							{
								Burn();
								break;
							}
						case ChemistryState.Elec:
							{
								Shock();
								break;
							}
						case ChemistryState.Water:
							{
								Douse();
								break;
							}
					}
					break;
				}
		}
	}

	public void ClearChemistry()
	{
		if (!Source)
		{
			if (ChemistryMaterial == ChemistryMaterial.Water)
				ChemistryState = ChemistryState.Water;
			else
				ChemistryState = ChemistryState.None;
		}
	}

	public void Burn()
	{
		if (!Source)
		{
			if (ChemistryState == ChemistryState.Water)
				ClearChemistry();
			else
			{
				burnTimer = BurnTime;
				ChemistryState = ChemistryState.Fire;
			}
		}
	}

	public void Shock()
	{
		if (!Source)
		{
			shockTimer = ShockTime;
			ChemistryState = ChemistryState.Elec;
		}
	}

	public void Douse()
	{

		if (!Source)
		{
			if (ChemistryState == ChemistryState.Fire)
				ClearChemistry();
			else
				ChemistryState = ChemistryState.Water;
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.GetComponent<ChemistryObject>())
		{
			OnChemistryTransmit(other.gameObject.GetComponent<ChemistryObject>());
		}
	}

	private void OnTriggerStay(Collider other)
	{
		if (other.gameObject.GetComponent<ChemistryObject>())
		{
			OnChemistryTransmit(other.gameObject.GetComponent<ChemistryObject>());
		}
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.GetComponent<ChemistryObject>())
		{
			OnChemistryTransmit(collision.gameObject.GetComponent<ChemistryObject>());
		}
	}

	private void OnCollisionStay(Collision collision)
	{
		if (collision.gameObject.GetComponent<ChemistryObject>())
		{
			OnChemistryTransmit(collision.gameObject.GetComponent<ChemistryObject>());
		}
	}
}