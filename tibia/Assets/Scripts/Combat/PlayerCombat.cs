using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCombat : CharacterCombat
{
	public GameObject imgSelected;

	public static GameObject pubPlayer;

    // Start is called before the first frame update
	public override void Start()
    {
		pubPlayer = this.gameObject;
		base.Start();
    }

    // Update is called once per frame
	public override void Update()
    {
		base.Update();

		if (Input.GetMouseButtonDown(0))
		{
			Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

			RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
			if (hit.collider != null)
			{
				if (hit.transform.GetComponent<CharacterCombat>() != null)
				{
					if (hit.transform.GetComponent<CharacterCombat>() != GetComponent<CharacterCombat>())
					{
						SelectEnemy(hit.transform.GetComponent<CharacterCombat>());
					}
				}
			}
		}



		if (enemyCombat != null)
		{
			imgSelected.SetActive(true);
			imgSelected.transform.position = enemyCombat.transform.position;
		}
		else
		{
			imgSelected.SetActive(false);
		}

		//Teste
		if (Input.GetKeyDown(KeyCode.F))
		{
			if (enemyCombat != null)
			{
				mySkills.skillsList[0].CastSkill(this.gameObject, enemyCombat.gameObject, canvasGame);
			}
		}

		if (Input.GetKeyDown(KeyCode.G))
		{
			if (enemyCombat != null)
			{
				mySkills.skillsList[1].CastSkill(this.gameObject, enemyCombat.gameObject, canvasGame);
			}
		}
		if (Input.GetKeyDown(KeyCode.D))
		{
			if (enemyCombat != null)
			{
				mySkills.skillsList[2].CastSkill(this.gameObject, enemyCombat.gameObject, canvasGame);
			}

		}
    }

	public override void Die(GameObject enemy)
	{
		base.Die(enemy);
		SceneManager.LoadScene(0);
	}
}
