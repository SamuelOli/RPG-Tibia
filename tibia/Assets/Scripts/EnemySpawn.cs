using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
	public GameObject enemy;
	public float spawnCooldown;

	GameObject currentEnemy = null;
	float currentSpawnCooldown = 0;

	public Canvas canvasGame; 
	// Start is called before the first frame update
	void Start()
	{
		
	}

	// Update is called once per frame
	void Update()
	{
		if (currentEnemy == null)
		{
			if (currentSpawnCooldown > 0)
			{
				currentSpawnCooldown -= Time.deltaTime;
			}

			if (currentSpawnCooldown <= 0)
			{
				GameObject newEnemy = Instantiate(enemy);
				newEnemy.SetActive(true);
				newEnemy.transform.SetParent(this.transform);

				newEnemy.transform.position = transform.position;
				currentEnemy = newEnemy;

				currentEnemy.name = currentEnemy.name.Replace("(Clone)", "");
				currentEnemy.name += " lvl " + currentEnemy.GetComponent<Stats>().lvl;

				currentSpawnCooldown = spawnCooldown;
			}
		}
		else if (currentEnemy.GetComponent<CharacterCombat>() != null)
		{
			CharacterCombat enemyCombat = currentEnemy.GetComponent<CharacterCombat>();
			if (enemyCombat.maxHP <= 0)
			{
				enemyCombat.canvasGame = canvasGame;
				enemyCombat.currentHP = enemyCombat.maxHP = enemyCombat.myStats.hp.Value;
				enemyCombat.currentMana = enemyCombat.maxMana = enemyCombat.myStats.mana.Value;
			}
		}
	}

	public void EnemyDied()
	{
		currentEnemy = null;
	}
}
