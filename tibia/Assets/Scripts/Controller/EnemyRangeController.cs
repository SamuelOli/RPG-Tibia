using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangeController : MonoBehaviour
{
	float rangeControll = 3;
	// Start is called before the first frame update
	void Start()
	{

		transform.localScale = new Vector2(rangeControll, rangeControll);
	}

	// Update is called once per frame
	void Update()
	{

	}

	void OnTriggerStay2D(Collider2D col)
	{
		if (col.gameObject.tag.Equals("Player") && !col.isTrigger)
		{
			GetComponentInParent<EnemyController>().Move(col.transform);
			transform.localScale = new Vector3(rangeControll, rangeControll, 0) + new Vector3(1, 1, 0);
		}
		//Debug.Log("Collision Detected");
	}

	void OnTriggerExit2D(Collider2D col)
	{
		if (col.gameObject.GetComponent<CharacterCombat>() != null)
		{
			if (col.gameObject.GetComponent<CharacterCombat>() == GetComponentInParent<CharacterCombat>().enemyCombat && !col.isTrigger)
			{
				GetComponentInParent<EnemyController>().StopFollow();
				GetComponentInParent<EnemyCombat>().SelectEnemy(null);
				transform.localScale = new Vector3(rangeControll, rangeControll, 0) - new Vector3(1, 1, 0);
			}
		}
	}
}
