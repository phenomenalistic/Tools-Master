using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterTrigger : MonoBehaviour
{
	public WaterObstacles waterObstacles;
	private void OnTriggerStay(Collider other)
	{
		if (other.tag == "Boards")
		{
			waterObstacles.ShowCompletedBoards(other.transform.gameObject);
		}
	}
}
