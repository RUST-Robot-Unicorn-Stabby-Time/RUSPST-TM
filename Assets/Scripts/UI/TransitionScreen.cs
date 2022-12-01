using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TransitionScreen : MonoBehaviour
{
    [SerializeField] float speed;

    Material materaial;

    private void Awake()
    {
        materaial = GetComponent<Image>().material;
    }

    void Start ()
    {
        StartCoroutine(TransitionRoutineIn());
    }

    public void TransitionOut(System.Action callback) => StartCoroutine(TransitionRoutineOut(callback));

    public IEnumerator TransitionRoutineIn()
    {
        materaial.SetFloat("_Percent", 1.0f);
        
        float percent = -speed * 2.0f;
        while (percent < 1.0f)
        {
            Time.timeScale = percent < 0.0f ? 0.0f : 1.0f;

            materaial.SetFloat("_Percent", 1.0f - percent);
            percent += Time.unscaledDeltaTime * speed;
            yield return null;
        }

        materaial.SetFloat("_Percent", 0.0f);
    }

    public IEnumerator TransitionRoutineOut(System.Action callback)
    {
        float percent = 0.0f;
        while (percent < 1.0f)
        {
            materaial.SetFloat("_Percent", percent);
            percent += Time.unscaledDeltaTime * speed;
            yield return null;
        }

        materaial.SetFloat("_Percent", 1.0f);
        callback?.Invoke();
    }
}
