using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuordScript : MonoBehaviour {

    public static SuordScript instance;
    public Collider2D blade_edge;
    public SpriteRenderer visibility;
    SamuraiScript player;
    Collider2D bodyCollider;

	// Use this for initialization
	void Awake () {
        instance = this;
        visibility = GetComponent<SpriteRenderer>();
            visibility.color = new Color(1f, 1f, 1f, 0f);

        blade_edge = GetComponent<Collider2D>();
            blade_edge.enabled = false;

        bodyCollider = gameObject.GetComponent<Collider2D>();
	}

    private void Start()
    {
        player = SamuraiScript.instance;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Foe")
        {
            if (player.heavyWeaponSelected) {
                collision.gameObject.GetComponent<DamageableScript>().GetDamaged(player.heavyDamage, bodyCollider);
            }
            else
            {
                collision.gameObject.GetComponent<DamageableScript>().GetDamaged(player.lightDamage, bodyCollider);
            }
        }
    }
}
