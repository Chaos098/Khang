using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BoxItem : MonoBehaviour
{
    Animator anim;
    int random;
    Vector2 pos;
    public float seconds = 7f;




    void Start()
    {
        anim = GetComponent<Animator>();
        random = Random.Range(0,100);  
    }



    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            anim.SetBool("isTouching", true);
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            anim.SetBool("isTaken", true);
            anim.SetBool("isTouching", false);
            switch (random % 2 == 0)
            {
                case true:
                    collision.SendMessageUpwards("AddKits");
                    break;

                case false:
                    random = Random.Range(0, 100);

                    switch (random % 2 == 0)
                    {
                        case true:
                            collision.SendMessageUpwards("AddBullet", "ShotgunBullet");
                            break;
                        case false:
                            collision.SendMessageUpwards("AddBullet", "RiffleBullet");
                            break;

                    }

                    break;
            }
            StartCoroutine(Respawn());
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        anim.SetBool("isTouching", false);
    }

    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(1f);
        gameObject.transform.position = Vector2.zero;
        yield return new WaitForSeconds(seconds);
        //Instantiate(gameObject, gameObject.transform.position, gameObject.transform.rotation);
        gameObject.transform.position = pos;

    }
}
