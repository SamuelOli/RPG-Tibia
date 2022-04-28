using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    
    public GameObject publicPlayer;
    static GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = publicPlayer;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public static GameObject GetPlayer(){
        return player;
    }
}
