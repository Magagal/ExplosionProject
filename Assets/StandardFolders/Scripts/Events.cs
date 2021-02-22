
using UnityEngine;

public static class Events
{
    public static ExplodedEvent OnExplodeProjectile;
    public static BoolEvent OnShakeCamera;
}

public delegate void SimpleEvent();
public delegate void IntEvent(int i);
public delegate void FloatEvent(float f);
public delegate void DoubleEvent(double d);
public delegate void BoolEvent(bool b);
public delegate void StringEvent(string s);

public delegate void StringStringEvent(string s1, string s2);
public delegate void StringIntEvent(string s, int i);
public delegate void StringStringIntEvent(string s1, string s2, int i);

public delegate void ExplodedEvent(Collider[] _colliders, float _force, Transform _position, float _rangeExplosion);
//public delegate void GamePopupEvent(GamePopUp gamePopUp, bool _enable);

public delegate void GenericEvent<T>(T t);
