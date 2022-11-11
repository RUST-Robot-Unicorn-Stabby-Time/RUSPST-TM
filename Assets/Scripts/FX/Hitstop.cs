using UnityEngine;

[System.Serializable]
public class Hitstop
{
    [SerializeField] float duration = 0.1f;

    public void Play ()
    {
        HitstopController.Instance.Play(duration);
    }
}
