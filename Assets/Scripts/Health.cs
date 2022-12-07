using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Trinitrotoluene;
using UnityEngine.SceneManagement;
public class Health : MonoBehaviour
{
    public enum DeathAction { Destroy, ReloadScene, Regen };
    [System.NonSerialized] public float counter;
    [System.NonSerialized] public float maxCounter;
    [System.NonSerialized] public float timeSinceDamage;
    [System.NonSerialized] public bool isFlashing;
    [System.NonSerialized] public int HP;
    [System.NonSerialized] public float healthFillAmount;
    [System.NonSerialized] public float healthEffectAmount;
    [Header("Settings")]
    public bool useFillAmount;
    public bool fadeOutBar = true;
    public bool useHealthEffect = true;

    [Header("Fill Amount")]
    public Image healthFillImage;
    public Image healthEffectImage;

    [Header("No Fill Amount")]
    public float healthBarWidth;
    public RectTransform healthEffect;
    public RectTransform healthFill;

    [Header("Both")]
    public CanvasGroup healthBarGroup;
    public TextMeshProUGUI healthCounterText;
    public Gradient healthBarColour;
    public int maxHP;
    public DeathAction deathAction;
    public Image healthFillRenderer;

    [Header("Setting Exclusive")]
    public float healthEffectTime;
    // public MeshRenderer myMesh;






    private void Start()
    {
        if (healthFill != null)
        {
            healthBarWidth = healthFill.sizeDelta.x;
        }
        timeSinceDamage = 0;

        HP = maxHP;
        healthFillAmount = (healthFill != null) ? healthFill.offsetMax.x * -1 : healthFillImage.fillAmount;

        if (useHealthEffect)
        {
            maxCounter = healthEffectTime;
            healthEffectAmount = healthEffect ? healthEffect.offsetMax.x * -1 : healthEffectImage.fillAmount;
        }

        if (fadeOutBar)
            healthBarGroup.alpha = HP < maxHP ? 1 : 0;
        else
            healthBarGroup.alpha = 1;
    }
    private void Update()
    {
        timeSinceDamage += Time.deltaTime;

        if (healthCounterText != null)
            healthCounterText.text = HP + "/" + maxHP;

        if (fadeOutBar)
            healthBarGroup.alpha += HP < maxHP && timeSinceDamage < 10 ? Functions.f.MoveTowards(healthBarGroup.alpha, 1, 0.1f) : Functions.f.MoveTowards(healthBarGroup.alpha, 0, 0.1f);

        if (useFillAmount)
        {
            healthFillImage.fillAmount += Functions.f.MoveTowards(healthFillImage.fillAmount, (float)HP / (float)maxHP, 0.1f);
            healthFillRenderer.color = healthBarColour.Evaluate(healthFillImage.fillAmount);

        }
        else
        {
            healthFillAmount += Functions.f.MoveTowards(healthFillAmount, (float)HP / (float)maxHP, 0.1f);
            healthFill.SetRight(Mathf.Round((1 - healthFillAmount) * healthBarWidth * 100) / 100);
            healthFillRenderer.color = healthBarColour.Evaluate(healthFillAmount);

        }

        if (useHealthEffect)
        {

            if (counter <= 0)
            {
                if (useFillAmount)
                {
                    healthEffectImage.fillAmount += Functions.f.MoveTowards(healthEffectImage.fillAmount, (float)HP / (float)maxHP, 0.1f);
                }
                else
                {
                    healthEffectAmount += Functions.f.MoveTowards(healthEffectAmount, (float)HP / (float)maxHP, 0.1f);
                    healthEffect.SetRight((1 - healthEffectAmount) * healthBarWidth);
                }
            }
            if (useFillAmount)
            {
                if (healthEffectImage.fillAmount - ((float)HP / (float)maxHP) < 0.04f)
                {
                    healthEffectImage.fillAmount = (float)HP / (float)maxHP;
                    maxCounter = healthEffectTime;
                }
            }
            else
            {
                if (healthEffectAmount - ((float)HP / (float)maxHP) < 0.04f)
                {
                    healthEffectAmount = (float)HP / (float)maxHP;
                    healthEffect.SetRight((1 - healthEffectAmount) * healthBarWidth);
                    maxCounter = healthEffectTime;
                }
            }
            counter -= Time.deltaTime;
        }
    }
    public void TakeDamage(int Damage)
    {
        timeSinceDamage = 0;
        Damage = Mathf.Clamp(Damage, 1, HP);

        print(transform.name + " took " + Damage + " Damage.");
        if (Damage == 0)
        {
            return;
        }
        HP -= Damage;


        Popup.Spawn(Refs.i.popup, transform.position + (Vector3.up * 2), Damage.ToString());

        if (HP <= 0)
        {
            switch (deathAction)
            {
                case DeathAction.Destroy:
                    {
                        GameObject particleClone = Instantiate(Refs.i.deathParticles, transform.position, Quaternion.Euler(90, 0, 0));
                        particleClone.GetComponent<ParticleSystem>().Play();
                    }
                    Destroy(gameObject);
                    break;
                case DeathAction.ReloadScene:
                    {
                        GameObject particleClone = Instantiate(Refs.i.deathParticles, transform.position, Quaternion.Euler(90, 0, 0));
                        particleClone.GetComponent<ParticleSystem>().Play();
                    }
                    Functions.LoadScene(SceneManager.GetActiveScene().buildIndex);
                    break;
                case DeathAction.Regen:
                    HP = maxHP;
                    Popup.Spawn(Refs.i.popup, transform.position, "Regenerated!");
                    break;
                default:
                    throw new System.NotImplementedException();
            }

        }

        // if (!isFlashing)
        //     StartCoroutine(Flash(0.1f));

        if (useHealthEffect)
        {
            counter = maxCounter;
            maxCounter *= 0.95f;
        }
    }
    // public IEnumerator Flash(float duration)
    // {
}
