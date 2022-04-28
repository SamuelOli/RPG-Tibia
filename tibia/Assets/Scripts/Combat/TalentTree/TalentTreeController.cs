using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalentTreeController : MonoBehaviour {
    public static int point = 0;
    public static int talentTreeLvl = 0;

    public List<TalentTree> talents;

    static PlayerStats player;

    // Start is called before the first frame update
    void Start () {

    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown ("space")) {
            UpTalentTree (talents[0]);
        }
        if (Input.GetKeyDown (KeyCode.C)) {
            ResetTalentTree ();
        }

    }

    public static void OnUpgrade (int newLvl) {
        float points = newLvl / 10;
        point += Mathf.FloorToInt (points) + 1;
    }

    public static void UpTalentTree (TalentTree talent) {
        if (player == null) {
            player = PlayerManager.GetPlayer ().GetComponent<PlayerStats> ();
        }
        Debug.Log ("Pontos: " + point + "\n" + talent.GetLvl ());
        if (point >= talent.GetPrice ()) {
            talentTreeLvl += talent.GetPrice ();
            point -= talent.GetPrice ();
            player.AddAttribute (talent.attribute);
            talent.Up ();
        }
    }

    public void ResetTalentTree () {

        if (player == null) {
            player = PlayerManager.GetPlayer ().GetComponent<PlayerStats> ();
        }
        for (int i = 0; i < talents.Count; i++) {
            int currentLvl = talents[i].GetLvl ();
            for (int j = 0; j < currentLvl; j++) {
                point += talents[i].GetLastPrice ();
                player.RemoveAttribute (talents[i].GetLastAttribute ());
                talents[i].RemoveUpgrade ();
            }
        }
    }
}