using DG.Tweening;
using UnityEngine;

namespace JSD.MenuSystem
{
    public interface IMenuTransition
    {
        Tween PlayShowTransition(RectTransform menuTransform, float duration);
        Tween PlayHideTransition(RectTransform menuTransform, float duration);
    }
}