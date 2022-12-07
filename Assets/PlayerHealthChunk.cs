using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Trinitrotoluene;
using UnityEngine.UI;
public class PlayerHealthChunk : MonoBehaviour
{
    CanvasGroup canvasGroup;
    Image image;
    public bool isDisabled;

    private void Start()
    {
        image = GetComponent<Image>();
        canvasGroup = GetComponent<CanvasGroup>();
    }
    public void Disable()
    {
        canvasGroup.LeanAlpha(0, 1f);
        StartCoroutine(Fall(1f));
        isDisabled = true;
    }
    public IEnumerator Fall(float time)
    {
        Vector2 oldPosition = transform.localPosition;
        Vector2 velocity = Math.v2.Random(new Vector2(150, 250), new Vector2(100, 200));
        float rotationalVelocity = Math.f.Random(-100, 100);
        float timer = 0;
        while (timer < time)
        {
            transform.localPosition += (Vector3)(velocity * Time.deltaTime);
            transform.Rotate(0, 0, rotationalVelocity * Time.deltaTime);

            velocity.y -= 400 * Time.deltaTime;
            yield return null;
            timer += Time.deltaTime;
        }
        transform.localPosition = oldPosition;
    }
    public IEnumerator FlashRed(float time = 0.25f)
    {
        float timer;
        Vector4 colorTo;

        while (!isDisabled)
        {
            colorTo = new Vector4(1, 1f, 1f, 1);
            timer = 0;
            while (timer < time)
            {
                image.color += (Color)Functions.v4.MoveTowards(image.color, colorTo, (time / 4f));
                yield return null;
                timer += Time.deltaTime;
            }
            image.color = colorTo;

            colorTo = new Vector4(1, 0.1f, 0.1f, 1);
            timer = 0;
            while (timer < time)
            {
                image.color += (Color)Functions.v4.MoveTowards(image.color, colorTo, (time / 4f));
                yield return null;
                timer += Time.deltaTime;
            }
            image.color = colorTo;
        }

    }
}