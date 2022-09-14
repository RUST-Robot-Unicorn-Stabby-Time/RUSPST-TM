using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//[CreateAssetMenu(menuName = "Scriptable Objects/Artifacts/Artifact Name")]
public abstract class Artifact : ScriptableObject
{
    // These will not be instanced per statboard, so they should be 'stateless'
    // As per request from the designers, this script will be extended to make various relics, this
    // does not necessarily mean 1 script per artifact, they can be shared. But for specific modifications this
    // can be extended.

    public string displayName;
    public Sprite icon;
    public Color baseColor;

    public abstract void Modify(GameObject ctx, string key, ref float val);
}

// Does artifact stuff