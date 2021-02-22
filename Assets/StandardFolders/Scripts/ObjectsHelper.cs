using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsHelper : Singleton<ObjectsHelper>
{
    public GameObject prefabProjectile;

    public GameObject[] prefabObjectsToExplode;

    public GameObject woodParticles;

    public Transform conteinerProjectiles;
    public Transform conteinerObjectsToExplode;

    public Transform startProjectile;
    public Transform explodeObjectPostion;

    public GameObject shootParticle;
}
