using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyCombat : CharacterCombat
{
	public GameObject prefBagLoot;
	public LootTable enemyLoot;

	// Start is called before the first frame update
	public override void Start()
	{
		base.Start();
	}

	// Update is called once per frame
	public override void Update()
	{
		base.Update();

		if (enemyCombat != null )
		{
			for (int i = 0; i < mySkills.skillsList.Count; i++)
			{
				if (mySkills.skillsList[i].GetCurrentCoolDown() <= 0)
				{
					mySkills.skillsList[i].CastSkill(this.gameObject, enemyCombat.gameObject, canvasGame);
				}
			}
		}
	}

	public override void Die(GameObject enemy)
	{
		base.Die(enemy);

		int random = Random.Range(0, 120);
		int quantity = 0;


		if (random <= 20)
		{
			quantity = 0;
		}
		else  if (random <= 60)
		{
			quantity = 1;
		}
		else if (random <= 90)
		{
			quantity = 2;
		}
		else if (random <= 110)
		{
			quantity = 3;
		}
		else 
		{
			quantity = 4;
		}

		List<Item> loot = enemyLoot.GenerateItemsFromLootTable(4);

		if (loot.Count > 0)
		{
			prefBagLoot.SetActive(true);
			prefBagLoot.transform.SetParent(canvasbackgroungGame.transform);
			prefBagLoot.transform.position = transform.position;
			prefBagLoot.GetComponent<OpenBagLoot>().items = loot;
			//prefItem.GetComponent<Image>().sprite = loot[i].itemIcon;
			//prefItem.GetComponent<ItemDropped>().item = loot[i];
		}
		EnemySpawn spawn = GetComponentInParent<EnemySpawn>();
		spawn.EnemyDied();

		Destroy(gameObject);
	}
}
