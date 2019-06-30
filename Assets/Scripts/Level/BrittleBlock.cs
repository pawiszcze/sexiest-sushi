using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BrittleBlock : MonoBehaviour
{
    Samurai player;
    SpriteRenderer thisSpriter;
    Collider2D thisCollider;
    bool isAlreadyFallingApart;

    void Start()
    {
        player = Samurai.instance;
        thisCollider = gameObject.GetComponent<Collider2D>();
        thisSpriter = GetComponent<SpriteRenderer>();
        isAlreadyFallingApart = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == player.gameObject)
        {
            if (!isAlreadyFallingApart)
            {
                isAlreadyFallingApart = true;
                StartCoroutine(FallApart());
            }
        }
    }

    private IEnumerator FallApart()
    {
        float timer = 0;
        while (timer < 1f)
        {
            thisSpriter.color = new Color(1f, 1f, 1f, 1 - timer);
            timer += Time.deltaTime;
            yield return null;
        }
        thisCollider.enabled = false;

        yield return new WaitForSeconds(5);

        thisSpriter.color = new Color(1f, 1f, 1f, 1f);
        thisCollider.enabled = true;
        isAlreadyFallingApart = false;
        yield return null;
    }
}
