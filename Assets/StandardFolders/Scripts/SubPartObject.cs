using System.Collections;
using UnityEngine;

public class SubPartObject : MonoBehaviour
{
    public Rigidbody[] subPartsRigidbody;
    public bool exploded = false;

    public Vector3 direction;
    public Vector3 lastPsotion;

    private float particleForce = 15;

    void Start()
    {
        //subPartsRigidbody = GetComponentsInChildren<Rigidbody>();
    }

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
                    Debug.Log("BBBBBBBBB");
                    SetExplosion(_force, _position, _rangeExplosion);
                }
            }
        }
    }

    public void SetExplosion(float _force, Transform _position, float _rangeExplosion)
    {
        if (exploded == false)
        {
            exploded = true;

            StartCoroutine(DelayExplosion(_force, _position, _rangeExplosion));
        }
    }

    IEnumerator DelayExplosion(float _force, Transform _position, float _rangeExplosion)
    {
        yield return new WaitForSeconds(0.01f);

        for (int i = 0; i < subPartsRigidbody.Length; i++)
        {
            subPartsRigidbody[i].AddExplosionForce(_force, _position.position, _rangeExplosion, 0, ForceMode.Impulse);
        }

        StartCoroutine(CreateParticle());
    }

    IEnumerator CreateParticle()
    {
        yield return new WaitForSeconds(0.01f);

        GameObject partible = Instantiate(ObjectsHelper.Instance.woodParticles, transform.position, transform.rotation, transform);

        yield return new WaitForSeconds(Random.Range(0.05f, 0.2f));

        partible.AddComponent<Rigidbody>().AddForceAtPosition(direction * particleForce, transform.position, ForceMode.Impulse);
    }

    public void Update()
    {
        direction = lastPsotion - transform.position;
        lastPsotion = transform.position;
    }
}
