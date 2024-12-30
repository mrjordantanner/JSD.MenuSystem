using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using JSD.Utilities;
using System.Linq;
using DG.Tweening;
using static System.TimeZoneInfo;


namespace JSD.MenuSystem
{
    /// <summary>
    /// Singleton. Dynamically creates and destroys Menu Prefabs.
    /// </summary>
    public class MenuManager : Singleton<MenuManager>
    {
        public Canvas targetCanvas;
        [SerializeField] private Dictionary<string, IMenuPresenterBase> _activeMenus = new();
        private MenuDefinitionObject[] menuDefinitions;

        protected override void Awake()
        {
            base.Awake();

            menuDefinitions = Resources.LoadAll<MenuDefinitionObject>("MenuDefinitions");
            Debug.Log($"Loaded {menuDefinitions.Count()} MenuDefinitions from Resources");
        }

        public GameObject GetMenuPrefab(string menuId)
        {
            var menuDefinition = menuDefinitions.Where(d => d.GetMenuId() == menuId).FirstOrDefault();
            if (menuDefinition != null)
            {
                var Prefab = menuDefinition.MenuViewPrefab;
                if (Prefab != null)
                {
                    return Prefab;
                }
            }

            Debug.LogError($"Unable to find Menu Prefab matching MenuId {menuId}");
            return null;
        }

        public IEnumerator ShowMenu<T>(
            string menuId,
            IMenuData<T> data,
            IMenuTransition transition = null,
            float transitionTime = 0f)
        {
            // Check if menu already exists
            if (_activeMenus.TryGetValue(menuId, out var existingMenu))
            {
                //yield return 
                    existingMenu.Show(transition, transitionTime);
                yield break;
            }

            // Get and Instantiate MenuView Prefab
            var prefab = GetMenuPrefab(menuId);
            if (prefab == null) yield break;
            var instantiatedObject = Instantiate(prefab, targetCanvas.transform);

            // Ensure it implements IMenuView<T>
            if (instantiatedObject.TryGetComponent(out IMenuView<T> view))
            {
                var rectTransform = instantiatedObject.GetComponent<RectTransform>();
                var presenter = new MenuPresenter<T>(data, view, instantiatedObject);
                _activeMenus[menuId] = presenter;
                presenter.Show();

                // Show with transition
                if (transition != null && transitionTime > 0)
                {
                    yield return transition.PlayShowTransition(rectTransform, transitionTime).WaitForCompletion();
                }

                // Initialize the Close button if there is one
                var closeButton = instantiatedObject.GetComponentInChildren<MenuCloseButton>();
                if (closeButton != null)
                {
                    closeButton.Initialize(() =>
                    {
                        StartCoroutine(CloseMenu(menuId, transition, transitionTime));
                    });
                }
            }
            else
            {
                Debug.LogError($"The provided prefab does not contain a component that implements IMenuView<{typeof(T).Name}>.");
                Destroy(instantiatedObject);
            }
        }

        public IEnumerator CloseMenu(string menuId, IMenuTransition transition = null, float transitionTime = 0f)
        {
            if (_activeMenus.TryGetValue(menuId, out var presenter))
            {
                var MenuObject = presenter.CurrentMenuView;
                if (MenuObject != null)
                {
                    if (transition != null && transitionTime > 0)
                    {
                        yield return transition.PlayHideTransition(MenuObject.GetComponent<RectTransform>(), transitionTime).WaitForCompletion();
                    }

                    presenter.Dispose();
                    _activeMenus.Remove(menuId);
                    Destroy(MenuObject);
                }
            }
        }

        public IEnumerator HideMenu(string menuId, IMenuTransition transition = null, float transitionTime = 0f)
        {
            if (_activeMenus.TryGetValue(menuId, out var presenter))
            {
                var MenuObject = presenter.CurrentMenuView;
                if (MenuObject != null)
                {
                    if (transition != null && transitionTime > 0)
                    {
                        yield return transition.PlayHideTransition(MenuObject.GetComponent<RectTransform>(), transitionTime).WaitForCompletion();
                    }

                    presenter.Hide();
                }
            }
        }

        private void OnDestroy()
        {
            foreach (var menu in _activeMenus.Values)
            {
                menu.Dispose();
            }
            _activeMenus.Clear();
        }
    }
}