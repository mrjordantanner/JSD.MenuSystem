using UnityEngine;


namespace JSD.MenuSystem
{
    /// <summary>
    /// Updates the menu's data and refreshes the view.
    /// </summary>
    public interface IMenuPresenter<T> : IMenuPresenterBase
    {

        void UpdateData(T data);

        T CurrentData { get; }

        bool IsVisible { get; }
    }
}