using JSD.Events;
using JSD.MenuSystem;
using System;
using System.Collections.Generic;

public class PauseMenuController : MenuController<PauseMenuData>
{
    public PauseMenuController(string menuId, IEnumerable<IMenuDataProvider<PauseMenuData>> dataProviders)
        : base(menuId, dataProviders) { }

    public override void ShowMenu()
    {
        UnityEngine.Debug.Log($"PauseMenuController: ShowMenu()");

        var menuData = DataAggregator.AggregateData();
        MenuManager.Instance.CreateMenu(MenuId, menuData, 0.5f);
    }

    public override void CloseMenu()
    {
        UnityEngine.Debug.Log($"PauseMenuController: CloseMenu()");
        MenuManager.Instance.DestroyMenu(MenuId, 0.5f);
    }

    protected override void RegisterEvents()
    {
        UnityEngine.Debug.Log($"PauseMenuController: RegisterEvents()");

        EventHandlers = new Dictionary<Type, Delegate>
            {
                { typeof(PauseEvent), new Action<PauseEvent>(HandlePause) },
                { typeof(UnpauseEvent), new Action<UnpauseEvent>(HandleUnpause) },
                { typeof(TogglePauseEvent), new Action<TogglePauseEvent>(HandleTogglePause) },
                { typeof(EndRunEvent), new Action<EndRunEvent>(HandleEndRun) }
            };

        EventHub.RegisterEvents(EventHandlers);
    }

    // Event Handlers
    //private void HandlePause(PauseEvent e) => Mock_GameManager.Instance.Pause();
    //private void HandleUnpause(UnpauseEvent e) => Mock_GameManager.Instance.Unpause();
    private void HandleTogglePause(TogglePauseEvent e) => Mock_GameManager.Instance.TogglePause();
    private void HandleEndRun(EndRunEvent e) => Mock_GameManager.Instance.Quit();

    void HandlePause(PauseEvent e)
    {
        UnityEngine.Debug.Log($"PauseMenuController: HandlePause event handler");
        //Mock_GameManager.Instance.Pause();
        ShowMenu();
    }

    void HandleUnpause(UnpauseEvent e)
    {
        UnityEngine.Debug.Log($"PauseMenuController: HandleUnpause event handler");
        //Mock_GameManager.Instance.Unpause();
        CloseMenu();
    }
}
