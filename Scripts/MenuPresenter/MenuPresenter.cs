using System.Collections;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Rendering;


namespace JSD.MenuSystem
{
    public class MenuPresenter<T> : IMenuPresenter<T>
    {
        private readonly IMenuData<T> _model;
        private readonly IMenuView<T> _view;
        private bool _isVisible;

        // TODO Could make this a list to create/destroy 'stacking' menus or menu 'group',
        // or a Dictionary to create a lookup table by menuId
        // But would a 'group' of menus have the same MenuPresenter, or each menu have its own?
        public GameObject CurrentMenuView { get; set; }

        public bool IsVisible => _isVisible;
        public T CurrentData => _model.GetData();

        public MenuPresenter(IMenuData<T> model, IMenuView<T> view, GameObject instantiatedMenuView)
        {
            _model = model;
            _view = view;
            CurrentMenuView = instantiatedMenuView;

            _model.OnDataChanged += OnDataChanged;
            _view.OnUserInput += OnUserInput;
        }

        public void Show(IMenuTransition transition = null, float transitionTime = 0f)
        {
            _isVisible = true;
            Refresh();

            //// Show with transition
            //if (transition != null && transitionTime > 0)
            //{
            //    var rectTransform = CurrentMenuView.GetComponent<RectTransform>();
            //    yield return transition.PlayShowTransition(rectTransform, transitionTime).WaitForCompletion();
            //}
        }

        public void Hide()
        {
            _isVisible = false;
        }

        public void Dispose()
        {
            _model.OnDataChanged -= OnDataChanged;
            _view.OnUserInput -= OnUserInput;
        }

        public void UpdateData(T data)
        {
            _model.UpdateData(data);
        }

        public void Refresh()
        {
            if (_isVisible)
            {
                Debug.Log($"Refereshed menu {CurrentMenuView.name}");
                _view.Display(_model.GetData());
            }
        }

        private void OnDataChanged(T data)
        {
            if (_isVisible)
            {
                _view.Display(data);
            }
        }

        private void OnUserInput(T data)
        {
            UpdateData(data);
        }
    } }