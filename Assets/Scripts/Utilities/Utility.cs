using DG.Tweening;
using UnityEngine;

public class Utility
{
    public static T FindComponent<T>(GameObject gameObject, string name) where T : Component
    {
        var components = gameObject.GetComponentsInChildren<T>(true);
        foreach (var component in components)
        {
            if (component.name.Equals(name))
            {
                return component;
            }
        }

        Debug.LogWarning($"Failed to FindComponent<{typeof(T).Name}>({gameObject.name}, {name})");
        return null;
    }

    public static Sequence RecyclableSequence()
    {
        return DOTween.Sequence().Pause().SetAutoKill(false);
    }
}