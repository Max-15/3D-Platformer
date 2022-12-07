using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Refs : MonoBehaviour
{
    static Refs _i;
    public static Refs i
    {
        get
        {
            if (_i == null)
                _i = (Instantiate(Resources.Load("Refs")) as GameObject).GetComponent<Refs>();
            return _i;
        }
    }
    [Header("Popup")]
    public GameObject popup;
    [Header("Particles")]
    public GameObject deathParticles;
}
