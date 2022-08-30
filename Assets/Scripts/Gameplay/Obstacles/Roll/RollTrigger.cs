using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollTrigger : MonoBehaviour
{
    public Roll rollScript;
	[HideInInspector] [SerializeField] public RollMaterials rm = new RollMaterials();

	private void OnTriggerStay(Collider other)
	{
		if (other.tag == "Showel")
		{
			rollScript.UpdateSpiralTwisting(other.transform.gameObject);
		}
	}
}
