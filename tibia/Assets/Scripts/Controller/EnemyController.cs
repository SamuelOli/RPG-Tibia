using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyController : MonoBehaviour
{

	EnemyStats enemyStats;

	private Rigidbody2D rb2D;



    void Start()
    {
		rb2D = GetComponent<Rigidbody2D>();
		enemyStats = GetComponent<EnemyStats>();

    }

    // Update is called once per frame
    void Update()
    {
		
    }


	public void Move(Transform target)
	{
		float distance = Vector2.Distance(target.position, transform.position);

		if (distance >= enemyStats.range.Value)
		{
			Vector2 direction = target.transform.position - transform.position;
			direction.Normalize();

			rb2D.velocity = new Vector2(direction.x * enemyStats.speed.Value * Time.deltaTime, direction.y * enemyStats.speed.Value * Time.deltaTime);
		}
		else
		{
			StopFollow();
		}
		if (GetComponent<EnemyCombat>().GetEnemy() == null)
		{
			GetComponent<EnemyCombat>().SelectEnemy(target.GetComponent<CharacterCombat>());
		}
	}



	public void StopFollow()
	{
		rb2D.velocity = new Vector2(0, 0);
	}



}
