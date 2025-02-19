using DG.Tweening;
using UnityEngine.UI;

public class UI_MinigameIntro : UI_Base
{
    #region Birth
    private Sequence Frame_Birth()
    {
        var child = Get((int)Children.Frame);

        return Utility.RecyclableSequence()
            .Append(child.DOScaleY(1.0f, 0.5f).From(0.0f).SetEase(Ease.OutBack).OnComplete(Stand).SetDelay(0.2f));
    }

    private Sequence Content_Birth()
    {
        var title = Get<Graphic>((int)Children.Title);
        var info = Get<Graphic>((int)Children.Info);

        return Utility.RecyclableSequence()
            .Append(title.DOFade(1.0f, 0.5f).From(0.0f).SetDelay(0.2f))
            .Join(info.DOFade(1.0f, 0.5f).From(0.0f));
    }
    #endregion
    #region Death
    private Sequence Frame_Death()
    {
        var child = Get((int)Children.Frame);

        return Utility.RecyclableSequence()
            .Append(child.DOScaleY(0.0f, 0.5f).SetEase(Ease.InBack).OnComplete(Destroy));
    }

    private Sequence Content_Death()
    {
        var title = Get<Graphic>((int)Children.Title);
        var info = Get<Graphic>((int)Children.Info);

        return Utility.RecyclableSequence()
            .Append(title.DOFade(0.0f, 0.5f))
            .Join(info.DOFade(0.0f, 0.5f));
    }
    #endregion

    private enum Children
    {
        Frame,
        Title,
        Info
    }

    protected override void Initialize()
    {
        base.Initialize();
        BindChildren(typeof(Children));

        BindSequence(State.Birth, Frame_Birth, Content_Birth);
        BindSequence(State.Death, Frame_Death, Content_Death);
    }

    public override void Death()
    {
        base.Death();
        Managers.Game.MiniGame_Start();
    }
}