using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField]
    GameObject dustShoot;
    GameObject dustShootEffect;

    Animator gunEffect;
    Fire damage;


    int totalWeapon;
    int[] amountBullets;
    public int currentIndexWeapon;


    public GameObject[] guns;
    public GameObject currentGun, bullet, weaponHolder;


    public Hashtable bulletInventory = new Hashtable();

    public Shooting shootingBar;
    public Reload Reload_Bar;

    public TextMeshProUGUI numberOfBullets;
    public TextMeshProUGUI numberOfBulletsInventory;

    void Start()
    {

        damage = bullet.GetComponent<Fire>();
        totalWeapon = weaponHolder.transform.childCount;
        guns = new GameObject[totalWeapon];
        amountBullets = new int[totalWeapon];

        for (int i = 2; i < totalWeapon; i++)
        {
            guns[i] = weaponHolder.transform.GetChild(i).gameObject;
            switch (guns[i].name)
            {
                case "Pistol":
                    amountBullets[i] = 12;
                    break;
                case "Riffle":
                    amountBullets[i] = 24;
                    break;
                case "Shotgun":
                    amountBullets[i] = 6;
                    break;
            }

            guns[i].SetActive(false);
        }

        guns[2].SetActive(true);
        currentGun = guns[2];
        currentIndexWeapon = 2;
        numberOfBullets.text = amountBullets[currentIndexWeapon].ToString();
        numberOfBulletsInventory.text = "MAX";

    }




    // Update is called once per frame
    void Update()
    {

        // Flip player when run rotate
        FlipWeapon();


        // Add weapon into gunholder
        PickUp();


        //Take curren weapon animator
        gunEffect = currentGun.GetComponent<Animator>();

        // Change weapon
        SwapWeapon();


        // Shooting action
        if (Input.GetMouseButtonDown(0))
        {
            int index = 0;

            // Take amount of bullet of current gun
            for (int i = 2; i < totalWeapon; i++)
            {
                if (guns[i].name.Equals(currentGun.name))
                {
                    index = i;
                }
            }



            // If shoot bullet decrease
            if (amountBullets[index] == 0)
            {
                amountBullets[index] = 0;


                shootingBar.updateBar(0);
            }
            else
            {
                StartCoroutine(Reload(0.5f));
                amountBullets[index] -= 1;
                numberOfBullets.text = amountBullets[currentIndexWeapon].ToString();
                shooting();
                gunEffect.SetBool("isShooting", true);

                //Take the Transform (GameObject) of "currentgun" of DustShoot
                dustShootEffect = Instantiate(dustShoot, currentGun.transform.GetChild(2).transform.position, currentGun.transform.GetChild(2).transform.rotation);
            }


            // Run animation of shooting
            
        }
        else
        {

            gunEffect.SetBool("isShooting", false);

            Destroy(dustShootEffect, 1f);
        }


        Reload();


    }


    /* -------Idea------- */
    /* Check the number of child gameObject of player -> if it's more than total weapons in the present 
     * -> enlarge the inventory of weapon and number of bullets for each weapon 
     * 
     */

    void PickUp()
    {
        if (weaponHolder.transform.childCount > totalWeapon)
        {
            totalWeapon = weaponHolder.transform.childCount;

            // Create new weapon inventory
            guns = new GameObject[totalWeapon];

            for (int i = 2; i < totalWeapon; i++)
            {
                guns[i] = weaponHolder.transform.GetChild(i).gameObject;
                guns[i].SetActive(false);
            }

            // Create temporary bullet inventory
            int[] tmpAmountBullets = new int[totalWeapon];


            for (int i = 2; i < totalWeapon - 1; i++)
            {
                tmpAmountBullets[i] = amountBullets[i];
            }

            // Check the additional weapon and add the number of bullet belong to itself
            switch (guns[totalWeapon - 1].name)
            {
                case "Shotgun":
                    tmpAmountBullets[totalWeapon - 1] = 6;
                    break;
                case "Riffle":
                    tmpAmountBullets[totalWeapon - 1] = 24;
                    break;
            }


            // Transform the temporary bullet inventory to main bullet inventory
            amountBullets = new int[totalWeapon];

            for (int i = 2; i < totalWeapon; i++)
            {
                amountBullets[i] = tmpAmountBullets[i];
            }


            guns[currentIndexWeapon].SetActive(true);
            
            currentGun = guns[currentIndexWeapon];
            numberOfBullets.text = amountBullets[currentIndexWeapon].ToString();


            
        }
    }

    
    /* -----Idea----- */
    /*
     Check if the bullet inventory has the specific bullet yet
        -> If not -> create new slot
        -> If yes -> add more
     */
    public void AddBullet(string bulletName)
    {
        // Inventory for shotgun bullet
        if (!bulletInventory.ContainsKey("ShotgunBullet"))
        {
            if (bulletName.Contains("ShotgunBullet"))
            {
                bulletInventory.Add("ShotgunBullet", 2);
                numberOfBulletsInventory.text = bulletInventory["ShotgunBullet"].ToString();
                
            }
        }
        else
        {
            if (bulletName.Contains("ShotgunBullet"))
            {
                int tmp = (int)bulletInventory["ShotgunBullet"] + 2;
                bulletInventory["ShotgunBullet"] = tmp;
                numberOfBulletsInventory.text = bulletInventory["ShotgunBullet"].ToString();
            }
        }


        // Inventory for riffle bullet
        if (!bulletInventory.ContainsKey("RiffleBullet"))
        {
            if (bulletName.Contains("RiffleBullet"))
            {
                bulletInventory.Add("RiffleBullet", 12);
                numberOfBulletsInventory.text = bulletInventory["RiffleBullet"].ToString();
                Debug.Log((int)bulletInventory["RiffleBullet"]);
            }
            Debug.Log((int)bulletInventory["RiffleBullet"]);
        }
        else
        {
            if (bulletName.Contains("RiffleBullet"))
            {
                int tmp = (int)bulletInventory["RiffleBullet"] + 12;
                bulletInventory["RiffleBullet"] = tmp;
                numberOfBulletsInventory.text = bulletInventory["RiffleBullet"].ToString();
            }
        }


    }

    /* -----Idea----- */
    /*
     Check the amount of that bullet 
        -> If full -> exit 
        -> If not -> check bullet inventory -> find bullet -> reload
     */
    void Reload()
    {
        int index = 0, redundant = 0;
        if (Input.GetKeyDown(KeyCode.R) && currentGun.name == "Pistol")
        {
            for (int i = 2; i < totalWeapon; i++)
            {
                if (guns[i].name.Equals(currentGun.name))
                {
                    if (amountBullets[i] < 12)
                    {
                        StartCoroutine(WaitForReload(1f));
                        amountBullets[i] = 12;
                    }
                }
            }
        }


        if (Input.GetKeyDown(KeyCode.R) && currentGun.name == "Shotgun")
        {
            for (int i = 2; i < totalWeapon; i++)
            {
                if (guns[i].name.Equals(currentGun.name))
                {
                    index = i;
                }
            }

            if (amountBullets[index] < 6)
            {
                foreach (string nameBullet in bulletInventory.Keys)
                {
                    if (nameBullet.Contains(currentGun.name) && (int)bulletInventory[nameBullet] > 0)
                    {
                        StartCoroutine(WaitForReload(2f));

                        if((int)bulletInventory[nameBullet] >= 2)
                        {
                            amountBullets[index] += 2;
                            bulletInventory[nameBullet] = (int)bulletInventory[nameBullet] - 2;
                        }
                        else
                            amountBullets[index] += (int)bulletInventory[nameBullet];

                        if (amountBullets[index] > 6)
                        {
                            redundant = amountBullets[index] - 6;
                            amountBullets[index] = 6;
                        }
                        else { redundant = 0; }
                        int tmp = (int)bulletInventory[nameBullet] + redundant;
                        bulletInventory[nameBullet] = tmp;

                    }
                    if (bulletInventory[nameBullet] == null || bulletInventory == null)
                        Debug.Log("Out of bullet");

                }
            }
            else if (amountBullets[index] >= 6)
            {
                Debug.Log("Bullet is full !" + amountBullets[index]);
            }
        }

        if (Input.GetKeyDown(KeyCode.R) && currentGun.name == "Riffle")
        {
            for (int i = 2; i < totalWeapon; i++)
            {
                if (guns[i].name.Equals(currentGun.name))
                {
                    index = i;
                }
            }

            if (amountBullets[index] < 24)
            {
                foreach (string nameBullet in bulletInventory.Keys)
                {
                    if (nameBullet.Contains(currentGun.name) && (int)bulletInventory[nameBullet] > 0)
                    {
                        StartCoroutine(WaitForReload(2f));

                        if ((int)bulletInventory[nameBullet] >= 12)
                        {
                            amountBullets[index] += 12;
                            bulletInventory[nameBullet] = (int)bulletInventory[nameBullet] - 12;
                        }
                        else
                            amountBullets[index] += (int)bulletInventory[nameBullet];

                        if (amountBullets[index] > 24)
                        {
                            redundant = amountBullets[index] - 24;
                            amountBullets[index] = 24;
                        }
                        else { redundant = 0; }
                        int tmp = (int)bulletInventory[nameBullet] + redundant;
                        bulletInventory[nameBullet] = tmp;

                    }
                    if (bulletInventory[nameBullet] == null || bulletInventory == null)
                        Debug.Log("Out of bullet");

                }
            }
            else if (amountBullets[index] >= 24)
            {
                Debug.Log("Bullet is full !" + amountBullets[index]);
            }
        }
    }

    IEnumerator WaitForReload(float seconds)
    {
        Reload_Bar.updateBar(seconds);
        yield return new WaitForSeconds(seconds);
        numberOfBullets.text = amountBullets[currentIndexWeapon].ToString();

    }





    void SwapWeapon()
    {

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (currentIndexWeapon < totalWeapon - 1)
            {
                guns[currentIndexWeapon].SetActive(false);
                currentIndexWeapon++;
                guns[currentIndexWeapon].SetActive(true);
                currentGun = guns[currentIndexWeapon];
                numberOfBullets.text = amountBullets[currentIndexWeapon].ToString();
            }
            else
            {
                guns[currentIndexWeapon].SetActive(false);
                currentIndexWeapon = 2;
                guns[currentIndexWeapon].SetActive(true);
                currentGun = guns[currentIndexWeapon];
                numberOfBullets.text = amountBullets[currentIndexWeapon].ToString();
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (currentIndexWeapon > 2)
            {
                guns[currentIndexWeapon].SetActive(false);
                currentIndexWeapon--;
                guns[currentIndexWeapon].SetActive(true);
                currentGun = guns[currentIndexWeapon];
                numberOfBullets.text = amountBullets[currentIndexWeapon].ToString();
            }
            else
            {
                guns[currentIndexWeapon].SetActive(false);
                currentIndexWeapon = totalWeapon - 1;
                guns[currentIndexWeapon].SetActive(true);
                currentGun = guns[currentIndexWeapon];
                numberOfBullets.text = amountBullets[currentIndexWeapon].ToString();
            }
        }

        if (currentGun.name.Equals("Pistol"))
        {
            numberOfBulletsInventory.text = "Max";
            damage.damage = 20;
        }
        else if (currentGun.name.Equals("Shotgun"))
        {
            if (bulletInventory.ContainsKey("ShotgunBullet"))
                numberOfBulletsInventory.text = bulletInventory["ShotgunBullet"].ToString();
            else

                numberOfBulletsInventory.text = "0";
            damage.damage = 40;
        }
        else if (currentGun.name.Equals("Riffle"))
        {
            if (bulletInventory.ContainsKey("RiffleBullet"))
                numberOfBulletsInventory.text = bulletInventory["RiffleBullet"].ToString();
            else

                numberOfBulletsInventory.text = "0";
            damage.damage = 25;
        }
    }


    void FlipWeapon()
    {
        Vector3 gunpos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (gunpos.x < transform.position.x)
        {
            transform.eulerAngles = new Vector3(transform.rotation.x, 180f, transform.rotation.z);
        }
        else
        {
            transform.eulerAngles = new Vector3(transform.rotation.x, 0f, transform.rotation.z);
        }
    }



    void shooting()
    {
        GameObject shoot = Instantiate(bullet, currentGun.transform.GetChild(0).transform.position, currentGun.transform.GetChild(0).transform.rotation);
        Destroy(shoot, 3f);
    }

    IEnumerator Reload(float seconds)
    {
        shootingBar.updateBar(0);

        yield return new WaitForSeconds(seconds);

        shootingBar.updateBar(1);
    }
}
