using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ability : MonoBehaviour {

	public GameObject[] buttons;
    public HullBreachBar hullBreach;

    //Attacks
    //// base damage
    public void Attack(Character c)
    {
        float temp = GameObject.Find("GameManager").GetComponent<GameManager>().activeCharacter.GetComponent<Character>().damage;
        c.takeDamage(temp*0.5f);

    }
    //// double damage
	public void AttackPlus(Character c, float x)
    {
        float temp =GameObject.Find("GameManager").GetComponent<GameManager>().activeCharacter.GetComponent<Character>().damage;
        c.takeDamage(temp* x);
        hullBreach.DamageHull(20);
    }

    //Buffs
    public void ArmorUp(Character c)
    {
        c.armor += c.startingArmor * 0.25f;
    }
    public void SpeedUp(Character c)
    {
        c.speed += Mathf.FloorToInt(c.startingSpeed * 0.25f);
    }
    public void AttackUp(Character c)
    {
        c.damage += c.startingDamage * 0.25f;
    }
	public void Heal(Character c)
	{
		c.health += c.startingHealth * 0.25f;
	}

    //Get enemy target
    public void GetEnemy()
    {
        //gets the active character from gamemanager
        GameObject active = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().activeCharacter;
        active.GetComponent<Character>().abilitySelected = true;
        active.GetComponent<Character>().enemySelect = true;
    }

    //Get character Target
    public void GetCharacter()
    {
        //gets the active character from gamemanager
        GameObject active = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().activeCharacter;
        active.GetComponent<Character>().abilitySelected = true;
        active.GetComponent<Character>().enemySelect = false;
    }

    //populate ablility buttons
    public void PopButtons(Character active)
    {
        string[] abilities = active.abilities;
        for (int i = 0; i < abilities.Length; i++)
        {
			buttons [i].SetActive (true);
            switch (abilities[i])
            {
                case "attack":
                    buttons[i].GetComponentInChildren<Text>().text = "Basic Attack";
                    buttons[i].GetComponent<Button>().onClick.AddListener(GetEnemy);
                    active.chosenAbility = "attack";
                    break;
                case "dragonStrike":
                    buttons[i].GetComponentInChildren<Text>().text = "Dragon Strike";
                    buttons[i].GetComponent<Button>().onClick.AddListener(GetEnemy);
                    active.chosenAbility = "dragon";
                    break;
			case "armor+":
				buttons[i].GetComponentInChildren<Text>().text = "Armor Up";
				buttons[i].GetComponent<Button>().onClick.AddListener(GetCharacter);
				active.chosenAbility = "armor";
				break;
			case "heal":
				buttons[i].GetComponentInChildren<Text>().text = "Heal";
				buttons[i].GetComponent<Button>().onClick.AddListener(GetCharacter);
				active.chosenAbility = "heal";
				break;
			case "headshot":
				buttons [i].GetComponentInChildren<Text> ().text = "Headshot";
				buttons[i].GetComponent<Button>().onClick.AddListener(GetEnemy);
				active.chosenAbility = "headshot";
				break;
			case "sludge":
				buttons[i].GetComponentInChildren<Text>().text = "Sludge Cannon";
				buttons[i].GetComponent<Button>().onClick.AddListener(GetEnemy);
				active.chosenAbility = "sludge";
				break;
			case "hammer":
				buttons[i].GetComponentInChildren<Text>().text = "Hammer Smash";
				buttons[i].GetComponent<Button>().onClick.AddListener(GetEnemy);
				active.chosenAbility = "hammer";
				break;
			case "attackUp":
				buttons[i].GetComponentInChildren<Text>().text = "Attack Up";
				buttons[i].GetComponent<Button>().onClick.AddListener(GetEnemy);
				active.chosenAbility = "attack+";
				break;
            }
        }
    }

    public void DoAbility(Character c)
    {
        string ability = GameObject.Find("GameManager").GetComponent<GameManager>().activeCharacter.GetComponent<Character>().chosenAbility;
		GameObject.Find ("GameManager").GetComponent<GameManager> ().activeCharacter.GetComponent<Character> ().attacking = true;
        switch (ability)
        {
            case "attack":
                Attack(c);
                break;
		    case "dragon":
			    AttackPlus(c, 1f);
			    break;
		    case "armor":
			    ArmorUp(c);
			    break;
		    case "heal":
			    Heal(c);
			    break;
		    case "headshot":
			    AttackPlus(c ,2f);
			    break;
		    case "sludge":
			    AttackPlus(c, 1f);
			    break;
		    case "hammer":
			    AttackPlus(c, 1f);
			    break;
		    case "attack+":
			    AttackUp (c);
			    break;
        }
    }

	public void DisableButtons()
	{
		for (int i = 0; i < 3; i++) {
			buttons [i].SetActive (false);
		}
	}
}
