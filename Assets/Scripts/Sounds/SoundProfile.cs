using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Audio/Sound Profile")]
public class SoundProfile : ScriptableObject
{
    public AudioClip clip;
    public Vector2 volumeRange = Vector2.one;
    public Vector2 pitchRange = Vector2.one;

    public void Play ()
    {
        Play(Vector3.zero);
    }

    public void Play (Transform position)
    {
        Play(position.position);
    }

    private void Play(Vector3 position)
    {
        if (!clip)
        {
            Debug.LogWarning($"{name} is missing audio clip", this);
            return;
        }

        AudioSource source = new GameObject("Temp Audio Source").AddComponent<AudioSource>();

        source.transform.position = position;

        source.clip = clip;
        source.volume = Random.Range(volumeRange.x, volumeRange.y);
        source.pitch = Random.Range(pitchRange.x, pitchRange.y);

        source.Play();

        if (Application.isPlaying)
        {
            Destroy(source.gameObject, clip.length + 1.0f);
        }
        else
        {
            var theReasonILoveUnity = source.gameObject.AddComponent<LiterallyDoesNothing>();
            theReasonILoveUnity.StartCoroutine(DestroyImmediateWithDelay(source.gameObject));
        }
    }

    public IEnumerator DestroyImmediateWithDelay (GameObject gameObject)
    {
        yield return new WaitForSeconds(clip.length + 1.0f);
        DestroyImmediate(gameObject);
    }
}
