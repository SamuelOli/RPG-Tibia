using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillController : MonoBehaviour
{
	CharacterCombat myCombat;

	float damage;
	TypeDamage typeDamage;

	Rigidbody2D rb2D;

	Transform skillTarget;
	float skillSpeed;

	Vector2 startPosition;
	float distance = 500;
	Vector2 direction = new Vector2(0, 0);
	float time = 2;

    // Start is called before the first frame update
    void Start()
    {
		
    }

	// Update is called once per frame
	void Update()
	{
		time -= Time.deltaTime;
		if (skillSpeed > 0 && skillTarget != null)
		{
			direction = skillTarget.transform.position - transform.position;
			direction.Normalize();
			rb2D.velocity = new Vector2(direction.x * skillSpeed * Time.deltaTime, direction.y * skillSpeed * Time.deltaTime);
		}

		if (Vector2.Distance(transform.position, startPosition) >= distance || (skillTarget == null && time <= 0))
		{
			Destroy(gameObject);
		}
	}

	public void StartSkillController(float skillDamage, TypeDamage skillTypeDamage, Skill skill, GameObject caster, GameObject target, float speed)
	{
		rb2D = GetComponent<Rigidbody2D>();

		damage = skillDamage;
		typeDamage = skillTypeDamage;

		distance = skill.maxDistance;
		startPosition = transform.position;

		skillSpeed = speed;
		skillTarget = target.transform;

		myCombat = caster.GetComponent<CharacterCombat>();
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.GetComponent<CharacterCombat>() != myCombat && !col.isTrigger)
		{
			if (col.GetComponent<CharacterCombat>() != null && col.GetComponent<CharacterCombat>() == skillTarget.gameObject.GetComponent<CharacterCombat>())
			{
				CharacterCombat enemyCombat = col.gameObject.GetComponent<CharacterCombat>();
                Destroy(this.gameObject);
				enemyCombat.TakeDamage(typeDamage, myCombat, damage);

			}
			else if (col.CompareTag("Obstacle")){
				Destroy(this.gameObject);
			}
		}
	}
}
