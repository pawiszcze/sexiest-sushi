using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuordScript : MonoBehaviour {

    public static SuordScript instance;
    public Collider2D blade_edge;
    public SpriteRenderer visibility;
    Samurai_Script player;

	// Use this for initialization
	void Awake () {
        instance = this;
        visibility = GetComponent<SpriteRenderer>();
            visibility.color = new Color(1f, 1f, 1f, 0f);

        blade_edge = GetComponent<Collider2D>();
            blade_edge.enabled = false;
	}

    private void Start()
    {
        player = Samurai_Script.instance;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Foe")
        {
            if (player.heavyWeaponSelected) {
                collision.gameObject.GetComponent<FoeScript>().GetDamaged(player.heavyDamage, gameObject.GetComponent<Collider2D>());
            }
            else
            {
                collision.gameObject.GetComponent<FoeScript>().GetDamaged(player.lightDamage, gameObject.GetComponent<Collider2D>());
            }
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
