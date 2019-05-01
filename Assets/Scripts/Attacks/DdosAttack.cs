using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DdosAttack : GenericAttack {

    //Start is called before the first frame update
    void Start()
    {
        StartDdosAttack();
        //if 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartDdosAttack() {

        WebAttack.AttackComponent(); 
        if (durability <= 0) {
            repeatAttack = true; 
        } else {
            repeatAttack = false; 
        }

        for(int i = 0; i = 20; i++) {
            WebAttack webAttack = (new GameObject("WebAttack")).AddComponent<WebAttack>();
            webAttack.Run((Component) this.initialGameObject.GetComponent(typeof(Component)));
        }

        //ATTACK

    }
}
