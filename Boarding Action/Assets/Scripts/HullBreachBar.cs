using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HullBreachBar : MonoBehaviour {

    public GameManager gameM;
    public CombatScene combatS;
    public float HullBreach;
    float startingHull;
    bool hullDestroyed;


	// Use this for initialization
	void Start () {
        HullBreach = gameM.GetHullBreach();
        startingHull = HullBreach;
        hullDestroyed = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void DamageHull(float damage)
    {
        if (!hullDestroyed)
        {
            HullBreach -= damage;
            if (HullBreach <= 0)
            {
                HullBreach = 0;
                HullIsBreached();
            }

            this.gameObject.transform.localScale = new Vector3(HullBreach / startingHull, 1, 1);
        }

    }

    void HullIsBreached()
    {
        for (int i = 0; i < combatS.chars.Count; i++)
        {
            combatS.chars[i].GetComponent<Character>().takeDamage(50);
        }

        for (int i = 0; i < combatS.enems.Count; i++)
        {
            combatS.enems[i].GetComponent<Character>().takeDamage(50);
        }
        hullDestroyed = true;
    }
}
