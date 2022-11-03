using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Audio/Sound Profile")]
public class SoundProfile : ScriptableObject
{
    public AudioClip[] clips;
    public Vector2 volumeRange = Vector2.one;
    public Vector2 pitchRange = Vector2.one;
    public float delay;

    int lastIndex;

    public void Play()
    {
        Play(Vector3.zero);
    }

    public void Play(Transform position)
    {
        Play(position.position);
    }

    private void Play(Vector3 position)
    {
        if (clips != null ? clips.Length == 0 : true)
        {
            Debug.LogWarning($"{name} is missing audio clip", this);
            return;
        }

        AudioSource source = new GameObject("Temp Audio Source").AddComponent<AudioSource>();

        source.transform.position = position;


        source.clip = clips[Random.Range(0, clips.Length)];
        source.volume = Random.Range(volumeRange.x, volumeRange.y);
        source.pitch = Random.Range(pitchRange.x, pitchRange.y);

        source.PlayDelayed(delay);

        if (Application.isPlaying)
        {
            Destroy(source.gameObject, source.clip.length + 1.0f);
        }
        else
        {
            var theReasonILoveUnity = source.gameObject.AddComponent<LiterallyDoesNothing>();
            theReasonILoveUnity.StartCoroutine(DestroyImmediateWithDelay(source.gameObject, source.clip.length + 1.0f));
        }
    }

    public IEnumerator DestroyImmediateWithDelay(GameObject gameObject, float delay)
    {
        yield return new WaitForSeconds(delay);
        DestroyImmediate(gameObject);
    }
}
