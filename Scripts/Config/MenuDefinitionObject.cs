using UnityEngine;

namespace JSD.MenuSystem
{
    [CreateAssetMenu(fileName = "NewMenuDefinition", menuName = "Menu Definition", order = 1)]
    public class MenuDefinitionObject : ScriptableObject
    {
        public string GetMenuId() => name;

        public GameObject MenuViewPrefab;

    }
}