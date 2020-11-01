using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    public float bulletSpeed;
    public GameObject bulletSpawnerLeft;
    public GameObject bulletSpawnerRight;
    public List<GameObject> bulletPrefabs;
    public ParticleSystem ps;
    PlayerMovement p_playerMovement;

    private Animator anim;
   

    // Start is called before the first frame update
    void Start()
    {
        p_playerMovement = GetComponent<PlayerMovement>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            GameObject bulletPrefab = bulletPrefabs[(int)Random.Range(0,bulletPrefabs.Count - .01f)];

            if (p_playerMovement.playerSide == Direction.Right)
            {
                GameObject bullet = Instantiate(bulletPrefab, bulletSpawnerRight.transform);
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                rb.velocity = new Vector2(bulletSpeed, rb.velocity.y);
                Instantiate(ps, bulletSpawnerRight.transform);
            }
            else
            if (p_playerMovement.playerSide == Direction.Left)
            {
                GameObject bullet = Instantiate(bulletPrefab, bulletSpawnerLeft.transform);
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                rb.velocity = new Vector2(-bulletSpeed, rb.velocity.y);
                Instantiate(ps, bulletSpawnerLeft.transform);
            }

            anim.SetTrigger("Shoot");
        }
    }
}
