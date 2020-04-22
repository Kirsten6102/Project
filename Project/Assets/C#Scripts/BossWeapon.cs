using UnityEngine;

public class BossWeapon : MonoBehaviour {

    public Transform pointToFireFrom;
    public GameObject bulletPrefab;

	
	void Update ()
    {
        if (Input.GetButtonDown("Horizontal"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, pointToFireFrom.position, pointToFireFrom.rotation);
    }
    
}
