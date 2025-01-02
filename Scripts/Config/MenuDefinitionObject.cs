using JSD.Events;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace JSD.MenuSystem
{
    [CreateAssetMenu(fileName = "NewMenuDefinition", menuName = "Menu Definition", order = 1)]
    public class MenuDefinitionObject : ScriptableObject
    {
        /// <summary>
        /// List of DataProviders that the Menu's DataAggregator will gather data from
        /// </summary>
        //[Header("Data Providers")]
        //public List<string> DataProviderIds = new();

        /// <summary>
        /// List of Events that the Menu's EventHub will register and unregister
        /// </summary>
        //[Header("Events")]
        //public List<IEvent> Events = new();

        [Header("Prefab")]
        public GameObject MenuViewPrefab;

        public string GetMenuId() => name;

    }
}
