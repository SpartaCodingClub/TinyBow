using UnityEngine;

public class UIManager
{
    public T Show<T>() where T : UI_Base
    {
        string path = $"UI/{typeof(T).Name}";
        GameObject original = Resources.Load<GameObject>(path);
        if (original == null)
        {
            Debug.LogWarning($"Failed to Load<{typeof(T).Name}>()");
            return null;
        }

        GameObject gameObject = Object.Instantiate(original);
        T @base = gameObject.GetComponent<T>();
        @base.Birth();

        return @base;
    }
}