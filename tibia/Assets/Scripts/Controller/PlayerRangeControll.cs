using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRangeControll : MonoBehaviour
{
	float rangeControll = 4;

	CharacterCombat playerCombat;

	// Start is called before the first frame update
	void Start()
	{
		playerCombat = GetComponentInParent<CharacterCombat>();

		transform.localScale = new Vector2(rangeControll, rangeControll);
	}

	// Update is called once per frame
	void Update()
	{
		
	}

	void OnTriggerStay2D(Collider2D col)
	{
		if (playerCombat.GetEnemy() == null)
		{
			if (!col.gameObject.tag.Equals("Player") && !col.isTrigger)
			{
				if (col.GetComponent<CharacterCombat>() != null)
				{
					CharacterCombat colCombat = col.GetComponent<CharacterCombat>();
					playerCombat.SelectEnemy(colCombat);
				}
			}
		}
	}

	void OnTriggerExit2D(Collider2D col)
	{
		if (col.gameObject.GetInstanceID() == playerCombat.enemyCombat.GetInstanceID())
		{
			playerCombat.SelectEnemy(null);
			//playerCombat.enemyCombatSelect == null;
		}
	}
}
