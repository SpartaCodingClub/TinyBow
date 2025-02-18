using UnityEngine;

public class Define
{
    // Animation Parameters
    public static readonly int IsAttack = Animator.StringToHash(nameof(IsAttack));
    public static readonly int IsMove = Animator.StringToHash(nameof(IsMove));

    // Animation States
    public static readonly int Idle = Animator.StringToHash(nameof(Idle));

    // Colors
    public static readonly Color Sea = new(0.2784314f, 0.6705883f, 0.6627451f);
    public static readonly Color Sky = new(0.8666667f, 0.7764707f, 0.6313726f);

    // Texts
    public static readonly string MainRenderer = nameof(MainRenderer);
    public static readonly string MainTex = "_MainTex";
    public static readonly string Player = nameof(Player);
}