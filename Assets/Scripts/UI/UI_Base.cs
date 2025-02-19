using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;
using VInspector;

public enum State
{
    Destroyed,
    Birth,
    Stand,
    Death
}

public abstract class UI_Base : MonoBehaviour
{
    #region Inspector
    [ShowInInspector] protected State CurrentState { get { return currentState; } }
    #endregion

    public RectTransform Get(int index) => children[index];
    public T Get<T>(int index) where T : Component => Get(index).GetComponent<T>();

    private readonly SequenceHandler sequenceHandler = new();
    private readonly List<RectTransform> children = new();

    protected CanvasGroup canvasGroup;

    private State currentState;

    private void Awake() => Initialize();
    private void OnDestroy() => Deinitialize();

    protected virtual void Initialize()
    {
        sequenceHandler.Initialize();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    protected virtual void Deinitialize()
    {
        sequenceHandler.Deinitialize();
    }

    public virtual void Birth()
    {
        currentState = State.Birth;
        sequenceHandler.Birth.Restart();
        canvasGroup.interactable = false;
    }

    public virtual void Stand()
    {
        currentState = State.Stand;
        sequenceHandler.Stand.Restart();
        canvasGroup.interactable = true;
    }

    public virtual void Death()
    {
        currentState = State.Death;

        sequenceHandler.Stand.Pause();
        sequenceHandler.Death.Restart();
        canvasGroup.interactable = false;
    }

    public virtual void Destroy()
    {
        currentState = State.Destroyed;
        Destroy(gameObject);
    }

    protected void BindChildren(Type enumType)
    {
        var names = Enum.GetNames(enumType);
        for (int i = 0; i < names.Length; i++)
        {
            var child = gameObject.FindComponent<RectTransform>(names[i]);
            children.Add(child);
        }
    }

    protected void BindSequence(State type, params Func<Sequence>[] sequences)
    {
        sequenceHandler.Bind(type, sequences);
    }
}