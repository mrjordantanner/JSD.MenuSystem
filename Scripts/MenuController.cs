using JSD.Events;
using JSD.MenuSystem;
using System;
using System.Collections.Generic;
using System.Diagnostics;


namespace JSD.MenuSystem
{
    public abstract class MenuController<TMenuData> where TMenuData : class, new()
    {
        public string MenuId { get; }
        protected Dictionary<Type, Delegate> EventHandlers { get; set; }
        protected MenuDefinitionObject MenuDefinition { get; private set; }
        protected DataAggregator<TMenuData> DataAggregator { get; private set; }

        protected MenuController(string menuId, IEnumerable<IMenuDataProvider<TMenuData>> dataProviders)
        {
            MenuId = menuId ?? throw new ArgumentNullException(nameof(menuId));
            MenuDefinition = MenuManager.Instance.GetMenuDefinition(MenuId)
                             ?? throw new Exception($"MenuDefinition not found for MenuId {MenuId}");

            DataAggregator = new DataAggregator<TMenuData>(dataProviders ?? throw new ArgumentNullException(nameof(dataProviders)));

            Init();
        }

        public virtual void Init()
        {
            UnityEngine.Debug.Log($"MenuController: Base Class Init()");
            RegisterEvents();
        }

        protected abstract void RegisterEvents();

        public abstract void ShowMenu();
        public abstract void CloseMenu();
    }
}