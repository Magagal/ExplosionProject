using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeObject : MonoBehaviour
{
    public GameObject fracturecObject;

    public PartObject[] parts;
    public bool exploded = false;

    private void Awake()
    {
        Events.OnExplodeProjectile += CheckObject;
    }

    private void OnDestroy()
    {
        Events.OnExplodeProjectile -= CheckObject;
    }

    public void CheckObject(Collider[] _colliders, float _force, Transform _position, float _rangeExplosion)
    {
        if (exploded == false)
        {
            for (int i = 0; i < _colliders.Length; i++)
            {
                if (Object.ReferenceEquals(_colliders[i].gameObject, gameObject) == true)
                {
                    SetFracturedObject(true, _force, _position, _rangeExplosion);
                }
            }
        }
    }

    public void SetFracturedObject(bool _exploded = false, float _force = 0, Transform _position = null, float _rangeExplosion = 0)
    {
        gameObject.SetActive(false);

        PartObject partObject = Instantiate(fracturecObject, transform.position, transform.rotation, transform.parent).GetComponent<PartObject>();

        exploded = true;

        if (_exploded == true)
        {            
            Collider[] colliders = Physics.OverlapSphere(transform.position, _rangeExplosion);
            Events.OnExplodeProjectile?.Invoke(colliders, _force, _position, _rangeExplosion);
        }

        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == (int)GameLayers.Projectile)
        {
            SetFracturedObject();
        }
    }
}
