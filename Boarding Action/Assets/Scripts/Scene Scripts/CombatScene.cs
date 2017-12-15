using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatScene : MonoBehaviour {

    public Transform[] charPos;
    public Transform[] enemyPos;
    public GameObject[] enemies = new GameObject[5];


    GameManager gm;
	public Character target;
    public List<GameObject> allCharacters;
    public List<GameObject> speedOrder;
	public List<GameObject> chars;
	public List<GameObject> enems;
    public int next;
    public bool getEnemy;
    public bool getCharacter;
    public bool end = false;

	bool start = false;

    // Use this for initialization
    void Start()
    {
        //find GameManager
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
		GameObject temp;
        //place Characters
        if (gm.characters.Length > 0)
        {
            for (int i = 0; i < gm.characters.Length; i++)
            {
				temp = Instantiate (gm.characters [i], charPos [i].position, charPos [i].rotation);
				allCharacters.Add(temp);
				chars.Add (temp);
            }
        }


        //place Enemies
		for (int i = 0; i < enemies.Length; i++) {
			temp = Instantiate (enemies [i], enemyPos [i].position, enemyPos [i].rotation);
			allCharacters.Add (temp);
			enems.Add (temp);
		}
        ChangeSpeedOrder();
        next = 0;
	}

    //Called every frame
    void Update()
    {
        if (next > speedOrder.Count-1)
        {
            next = 0;
        }
        Character nextChar = speedOrder[next].GetComponent<Character>();

        //checks the character state
        if (!end)
        {
            switch (nextChar.state)
            {
			case 0:
				nextChar.state++;
				nextChar.active = true;
                    break;
                case 1: //setup
                    gm.activeCharacter = nextChar.gameObject;
					nextChar.active = true;
                    //if its an enemies turn
                    if (nextChar.enemy)
                    {
                        float i = Random.Range(0, chars.Count+1);
                        target = chars[Mathf.FloorToInt(i)].GetComponent<Character>();
                        nextChar.chosenAbility = "attack";
                        GameObject.Find("EventSystem").GetComponent<Ability>().DoAbility(target);
                        nextChar.state = 5;
                    }
                    else // if characters turn
                    {
                        GameObject.Find("EventSystem").GetComponent<Ability>().PopButtons(nextChar);
                        nextChar.state++;
                    }
                    break;
                case 2: //button input
                    if (nextChar.abilitySelected)
                    {
                        //if ability has been selected
                        GameObject.Find("EventSystem").GetComponent<Ability>().DisableButtons();
                        if (nextChar.enemySelect)
                        {//if enemy is target
                            nextChar.state = 4;
                            nextChar.abilitySelected = false;
                            nextChar.enemySelect = false;
                        }
                        else
                        {//if character is target
                            nextChar.state = 3;
                        }
                    }
                    break;
                case 3: //get character target
                    if (!start)
                    {
                        SetEnemyInactive();
                        start = true;
                    }
                    if (target != null)
					{
						resetChars ();
                        Debug.Log(target);
                        nextChar.state = 5;
                        GameObject.Find("EventSystem").GetComponent<Ability>().DoAbility(target);
                    }
                    break;
                case 4: //get enemy target
                    if (!start)
                    {
                        SetCharsInactive();
                        start = true;
                    }
                    if (target != null)
                    {
						resetChars ();
                        Debug.Log(target);
                        nextChar.state = 5;
                        GameObject.Find("EventSystem").GetComponent<Ability>().DoAbility(target);
                    }
                    break;
			case 5: //reset everything
				if (!nextChar.attacking) {
					nextChar.state = 0;
					nextChar.active = false;
					start = false;
					target = null;
					next++;
				}
					break;
				
            }
        }
        else
        {
            Debug.Log("you win");
        }

        if (enems.Count == 0) { end = true; }
    }

    //set/change speed order
    public void ChangeSpeedOrder()
    {
        speedOrder = new List<GameObject>();
        foreach (GameObject element in allCharacters)
        {
            insert(element, 0);
        }
    }

    //insert gameobject into speedOrder
    private void insert(GameObject x, int e)
    {
        if (speedOrder.Count == 0 || speedOrder.Count == e)
        {
            speedOrder.Add(x);
        }
        else if (x.GetComponent<Character>().speed > speedOrder[e].GetComponent<Character>().speed)
        {
            speedOrder.Insert(e, x);
        }
        else
        {
            insert(x, e + 1);
        }
    }

    private void SetEnemyInactive()
    {
		for (int i = 0; i < enems.Count; i++) 
		{
            Color tempcolor;
            Color newColor;
            tempcolor = enemies [i].GetComponent<SpriteRenderer> ().color;
			newColor = new Color (tempcolor.r, tempcolor.b,  0.5f);
			enems [i].GetComponent<SpriteRenderer> ().color = newColor;
			enems [i].GetComponent<Character>().clickable = false;
		}
    }

    private void SetCharsInactive()
    {
		for (int i = 0; i < chars.Count; i++) 
		{
            Color tempcolor;
            Color newColor;
            tempcolor = chars [i].GetComponent<SpriteRenderer> ().color;
			newColor = new Color (tempcolor.r, tempcolor.g, tempcolor.b,  0.5f );
			chars [i].GetComponent<SpriteRenderer> ().color = newColor;
			chars [i].GetComponent<Character>().clickable = false;
		}
    }

	private void resetChars()
	{
		for (int i = 0; i < allCharacters.Count; i++) 
		{
		Color temp;
			temp = allCharacters [i].GetComponent<SpriteRenderer> ().color;
			allCharacters [i].GetComponent<SpriteRenderer> ().color = new Color (temp.r, temp.g, temp.b, 255);
			allCharacters [i].GetComponent<Character>().clickable = true;
		}
	}
}