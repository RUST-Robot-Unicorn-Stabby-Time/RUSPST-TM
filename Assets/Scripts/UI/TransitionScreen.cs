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

    IEnumerator Start ()
    {
        float percent = 0.0f;
        while (percent < 1.0f)
        {
            materaial.SetFloat("_Percent", 1.0f - percent);
            percent += Time.deltaTime * speed;
            yield return null;
        }

        materaial.SetFloat("_Percent", 0.0f);
    }

    public void Transition(System.Action callback) => StartCoroutine(TransitionRoutine(callback));
    public IEnumerator TransitionRoutine (System.Action callback)
    {
        float percent = 0.0f;
        while (percent < 1.0f)
        {
            materaial.SetFloat("_Percent", percent);
            percent += Time.deltaTime * speed;
            yield return null;
        }

        materaial.SetFloat("_Percent", 1.0f);
        callback?.Invoke();
    }
}
