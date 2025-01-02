using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace JSD.MenuSystem
{
    /// <summary>
    /// Aggregate data from all registered IMenuDataProviders to create the MenuData model.
    /// </summary>S
    public class DataAggregator<T> where T : new()
    {
        private readonly IEnumerable<IMenuDataProvider<T>> _dataProviders;

        public DataAggregator(IEnumerable<IMenuDataProvider<T>> providers)
        {
            _dataProviders = new List<IMenuDataProvider<T>>(providers);
            Debug.Log($"Created DataAggregator with {providers.Count()} DataProviders");

            //foreach (var provider in _dataProviders)
            //{
            //    Debug.Log(provider);
            //}
        }

        public T AggregateData()
        {
            var menuData = new T();

            foreach (var provider in _dataProviders)
            {
                if (provider == null)
                {
                    Debug.LogError("Provider was null");
                    return menuData;
                }

                provider.PopulateData(menuData);
            }

            return menuData;
        }
    }
}