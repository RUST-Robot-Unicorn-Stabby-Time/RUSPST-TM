using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extenstion
{
    public static T GetOrAddComponent<T> (this GameObject gameObject) where T : Component
    {
        if (gameObject.TryGetComponent(out T t))
        {
            return t;
        }
        else return gameObject.AddComponent<T>();
    }

    public static bool TryGetComponentInParent <T> (this Component ctx, out T component) where T : Component
    {
        component = ctx.GetComponentInParent<T>();
        return component;
    }
}
