using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace JSD.MenuSystem
{
    /// <summary>
    /// Aggregate data from various sources to create the MenuData object to be passed to the MenuView
    /// </summary>
    public class MenuDataMapper<T> where T : new()
    {
        private readonly List<IMenuDataProvider<T>> _dataProviders;

        public MenuDataMapper(IEnumerable<IMenuDataProvider<T>> providers)
        {
            _dataProviders = new List<IMenuDataProvider<T>>(providers);
        }

        public T MapData()
        {
            var menuData = new T();

            foreach (var provider in _dataProviders)
            {
                provider.PopulateData(menuData);
            }

            return menuData;
        }
    }
}