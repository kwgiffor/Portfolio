using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {
    //Hidden because no need to change
    [HideInInspector]
    public float startingHealth;
    [HideInInspector]
    public float startingArmor;
    [HideInInspector]
    public int startingSpeed;
    [HideInInspector]
    public float startingDamage;
    [HideInInspector]
    public int state = 0;
    [HideInInspector]
    public bool abilitySelected = false;
    [HideInInspector]
    public bool enemySelect = true;
	[HideInInspector]
	public bool clickable = true;
	public bool active = false;

    public float health;
    public float armor;
    public float damage;
    public int speed;
    public string[] abilities;
    public bool enemy;
	public bool attacking = false;
	public GameObject healthBar;
	public GameObject armorBar;
	public GameObject arrow;
	Animator animator;

    [HideInInspector]
    public string chosenAbility;

    void Start () {
        startingHealth = health;
        startingArmor = armor;
        startingDamage = damage;
        startingSpeed = speed;
		animator = GetComponent<Animator>();
    }

	void Update () {
		animator.SetBool ("Attacking", attacking);

		healthBar.transform.localScale = new Vector3 (health/startingHealth, 1, 1);
        if(armor/startingArmor >= 0)
        {
            armorBar.transform.localScale = new Vector3(armor / startingArmor, 1, 1);
        }
        else
        {
            armorBar.transform.localScale = new Vector3(0, 1, 1);
        }
        

		arrow.SetActive (active);
	}

    //take damage
    public void takeDamage(float d)
    {
        if (this.armor > d)
            this.armor -= d;
        else
        {
            d -= this.armor;
            this.armor = 0;
            this.health -= d;
        }

        if (this.health <= 0)
        {
            Death();
        }
            
    }

    //character is dead
    private void Death()
    {
        Debug.Log("I HAVE DIED");
        GameObject.Find("SceneManager").GetComponent<CombatScene>().allCharacters.Remove(gameObject);
        if (enemy)
        {
            GameObject.Find("SceneManager").GetComponent<CombatScene>().enems.Remove(gameObject);
        }
        else
        {
            GameObject.Find("SceneManager").GetComponent<CombatScene>().chars.Remove(gameObject);
        }
        GameObject.Find("SceneManager").GetComponent<CombatScene>().ChangeSpeedOrder();
        gameObject.SetActive(false);
    }

	void OnMouseDown(){
		if (this.clickable) {
			GameObject.Find ("SceneManager").GetComponent<CombatScene> ().target = this;
		}
	}

	public void SetAttacking(){
		attacking = false;
	}
}
