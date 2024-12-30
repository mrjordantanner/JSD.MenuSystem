using JSD.MenuSystem;
using UnityEngine;
using System.Collections;


namespace JSD.MenuSystem
{
    /// <summary>
    /// Updates the menu's data and refreshes the view. Base Class that is non-typed.
    /// </summary>
    public interface IMenuPresenterBase
    {
        void Show(IMenuTransition transition = null, float transitionTime = 0f);
        void Hide();
        void Dispose();
        GameObject CurrentMenuView { get; set; }

    }
}