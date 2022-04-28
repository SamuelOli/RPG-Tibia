using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillsManager : MonoBehaviour
{
	public List<Skill> skillsList = new List<Skill>();

	CharacterCombat myCombat;
	Stats myStats;


    // Start is called before the first frame update
    void Start()
    {
		myStats = GetComponent<Stats>();
		myCombat = GetComponent<CharacterCombat>();
		AddSkillsList(myStats.hero.heroSkills);
    }

    // Update is called once per frame
    void Update()
    {
		if (Input.GetKeyDown(KeyCode.A))
		{
			AddSkillsList(skillsList);
		}

		if (skillsList != null)
		{
			for (int i = 0; i<skillsList.Count; i++)
			{
				skillsList[i].Cooldown();
			}
		}

    }



	public void AddSkillsList(List<Skill> list)
	{
		List<Skill> newList = new List<Skill>();

		int i = 0;
		int j = 0;
		for (i = 0; i < skillsList.Count; i++)
		{
			newList.Add(skillsList[i]);
		}
		for (j = 0; j < list.Count; j++)
		{
			newList.Add(Instantiate(list[j]));
		}

		skillsList = newList;
	}
	public void AddSkill(Skill newSkill)
	{
		List<Skill> newList = new List<Skill>();
		newList.Add(newSkill);
		AddSkillsList(newList);
	}

	public void DeleteSkill(Skill targetSkill)
	{
		skillsList.Remove(targetSkill);
	}
}
