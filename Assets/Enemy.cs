using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    GameObject player;
    private void Start() {
        player = GameObject.FindWithTag("Player");
    }
    private void Update() {
        transform.rotation = Quaternion.LookRotation(-(player.transform.position - transform.position));
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
    }
}
