using DG.Tweening;
using TMPro;
using UnityEngine.UI;

public class UI_MinigameScore : UI_Base
{
    #region Birth
    private Sequence Frame_Birth()
    {
        var child = Get((int)Children.Frame);

        return Utility.RecyclableSequence()
            .Append(child.DOScaleX(1.0f, 0.5f).From(0.0f).SetEase(Ease.OutBack).OnComplete(Stand).SetDelay(0.5f));
    }

    private Sequence Content_Birth()
    {
        var title = Get<Graphic>((int)Children.Text_Title);
        var score = Get<Graphic>((int)Children.Text_Score);

        return Utility.RecyclableSequence()
            .Append(title.DOFade(1.0f, 0.5f).From(0.0f).SetDelay(0.2f))
            .Join(score.DOFade(1.0f, 0.5f).From(0.0f));
    }

    private Sequence Frame_Best_Birth()
    {
        var child = Get((int)Children.Frame_Best);

        return Utility.RecyclableSequence()
            .Append(child.DOScaleX(1.0f, 0.5f).From(0.0f).SetEase(Ease.OutBack).SetDelay(0.5f));
    }

    private Sequence Content_Best_Birth()
    {
        var title = Get<Graphic>((int)Children.Text_BestTitle);
        var score = Get<Graphic>((int)Children.Text_BestScore);

        return Utility.RecyclableSequence()
            .Append(title.DOFade(1.0f, 0.5f).From(0.0f).SetDelay(0.2f))
            .Join(score.DOFade(1.0f, 0.5f).From(0.0f));
    }
    #endregion
    #region Death
    private Sequence Frame_Death()
    {
        var child = Get((int)Children.Frame);

        return Utility.RecyclableSequence()
            .Append(child.DOScaleX(0f, 0.2f).OnComplete(Destroy));
    }

    private Sequence Content_Death()
    {
        var title = Get<Graphic>((int)Children.Text_Title);
        var score = Get<Graphic>((int)Children.Text_Score);

        return Utility.RecyclableSequence()
            .Append(title.DOFade(0f, 0.2f))
            .Join(score.DOFade(0f, 0.2f));
    }

    private Sequence Frame_Best_Death()
    {
        var child = Get((int)Children.Frame_Best);

        return Utility.RecyclableSequence()
            .Append(child.DOScaleX(0f, 0.2f));
    }

    private Sequence Content_Best_Death()
    {
        var title = Get<Graphic>((int)Children.Text_BestTitle);
        var score = Get<Graphic>((int)Children.Text_BestScore);

        return Utility.RecyclableSequence()
            .Append(title.DOFade(0f, 0.2f))
            .Join(score.DOFade(0f, 0.2f));
    }
    #endregion

    private enum Children
    {
        Frame,
        Text_Title,
        Text_Score,

        Frame_Best,
        Text_BestTitle,
        Text_BestScore
    }

    protected override void Initialize()
    {
        base.Initialize();
        BindChildren(typeof(Children));

        BindSequence(State.Birth, Frame_Birth, Content_Birth);
        BindSequence(State.Birth, Frame_Best_Birth, Content_Best_Birth);
        BindSequence(State.Death, Frame_Death, Content_Death, Frame_Best_Death, Content_Best_Death);
    }

    public void UpdateUI(int score, int bestScore)
    {
        Get<TextMeshProUGUI>((int)Children.Text_Score).text = score.ToString();
        Get<TextMeshProUGUI>((int)Children.Text_BestScore).text = bestScore.ToString();
    }
}