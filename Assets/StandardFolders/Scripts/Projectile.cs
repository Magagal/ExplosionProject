using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Rigidbody projectileRigidbody;
    public GameObject projectileFracted;

    public bool explosive = false;
    public float forceProjectile = 150;
    public float forceExplosion = 7;
    public bool checkColision = false;
    public float rangeExplosion = 3;
    

    // Start is called before the first frame update
    void Start()
    {
        projectileRigidbody = GetComponent<Rigidbody>();
        projectileRigidbody.AddForce(transform.forward * forceProjectile,  ForceMode.Impulse);

        checkColision = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (checkColision == true && explosive == true)
        {
            checkColision = false;

            StartCoroutine(DelayToExplode());
        }
    }

    IEnumerator DelayToExplode()
    {
        yield return new WaitForSeconds(0.02f);

        gameObject.SetActive(false);

        ProjectileFracted projectile = Instantiate(projectileFracted, transform.position, transform.rotation, transform.parent).GetComponent<ProjectileFracted>();

        Collider[] colliders = Physics.OverlapSphere(transform.position, rangeExplosion);

        for (int i = 0; i < projectile.partsProjectile.Length; i++)
        {
            projectile.partsProjectile[i].attachedRigidbody?.AddExplosionForce(forceExplosion, transform.position, rangeExplosion, 0, ForceMode.Impulse);
        }

        Events.OnExplodeProjectile?.Invoke(colliders, forceExplosion, transform, rangeExplosion);

        //Events.OnShakeCamera?.Invoke(true);
    }

    public void SetProjectile(float _forceProjectile, float _forceExplosion, bool _explosive)
    {
        explosive = _explosive;
        forceProjectile = _forceProjectile;
        forceExplosion = _forceExplosion;
    }
}
