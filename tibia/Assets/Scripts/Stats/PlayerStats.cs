using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : Stats
{
    // Start is called before the first frame update
    void Start()
    {
		AddBasicAttributeHero(1);

	}

    // Update is called once per frame
    void Update()
    {
		if(Input.GetKeyDown(KeyCode.X)){
            NextLvl();
            Debug.Log(lvl);
        }
    }

    public override void NextLvl(){
        base.NextLvl();
        TalentTreeController.OnUpgrade(lvl);
    }

}
