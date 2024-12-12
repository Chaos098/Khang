using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletItem : MonoBehaviour
{
    Animator anim;
    Vector2 pos;
    public float seconds = 10f;

    void Start()
    {
        anim = GetComponent<Animator>();
        pos = gameObject.transform.position;
    }



    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            anim.SetBool("isTouching", true);
        }

        if (collision.CompareTag("Player") && Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("Item picked");
            collision.SendMessageUpwards("AddBullet", gameObject.name);
            StartCoroutine(Respawn());

        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        anim.SetBool("isTouching", false);
    }

    IEnumerator Respawn()
    {
        gameObject.transform.position = Vector2.zero;
        yield return new WaitForSeconds(seconds);
        //Instantiate(gameObject, gameObject.transform.position, gameObject.transform.rotation);
        gameObject.transform.position = pos;

    }
}
