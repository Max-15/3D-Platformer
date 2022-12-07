using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Trinitrotoluene;
public class Popup : MonoBehaviour
{
    [Header("Customization")]
    [SerializeField] float angleMin;
    [SerializeField] float angleMax;
    [SerializeField] float fontSizeMultiplier = 1;
    [SerializeField] AnimationCurve fontSizeCurve;
    [SerializeField] Gradient color;

    [Header("Physics")]
    public float startVelocity;
    [SerializeField] float gravity;

    [Header("Properties")]
    [SerializeField] float lifetime;
    float velocity;

    TextMeshPro textMesh;
    float timer;
    GameObject player;

    public static Popup Spawn(GameObject thingToSpawn, Vector3 position, string text)
    {
        GameObject damagePopupInstance = Instantiate(thingToSpawn);
        damagePopupInstance.transform.position = position;

        Popup damagePopup = damagePopupInstance.transform.GetChild(0).GetComponent<Popup>();
        damagePopup.Setup(text);

        return damagePopup;
    }

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
        textMesh = transform.GetComponent<TextMeshPro>();
    }
    public void Setup(string text)
    {
        transform.rotation = Quaternion.Euler(0, 0, Math.f.Random(angleMin, angleMax));
        timer = 0;
        textMesh.fontSize = fontSizeCurve.Evaluate(0) * fontSizeMultiplier;
        textMesh.text = text;
        velocity = startVelocity;

        int damage;
        if (int.TryParse(text, out damage))
            textMesh.sortingOrder = damage;
    }
    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= lifetime)
            Destroy(transform.parent.gameObject);

        textMesh.fontSize = fontSizeCurve.Evaluate(GetFraction()) * fontSizeMultiplier;
        textMesh.color = color.Evaluate(GetFraction());

        velocity -= (gravity * Time.deltaTime);

        transform.position += new Vector3(0, velocity * Time.deltaTime, 0);

        transform.rotation = Quaternion.LookRotation(-(player.transform.position - transform.position));
    }
    public float GetFraction()
    {
        return timer / lifetime;
    }
}
