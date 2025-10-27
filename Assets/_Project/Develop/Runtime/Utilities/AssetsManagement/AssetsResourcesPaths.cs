using System.Collections.Generic;

namespace Assets._Project.Develop.Runtime.Utilities.AssetsManagement
{
    public class AssetsResourcesPaths
    {
        private static Dictionary<string, string> _viewIDToResourcesPath = new Dictionary<string, string>()
        {
            {AssetsIDs.CurrencyView, "UI/CurrencyView" },
            {AssetsIDs.CurrencyIconsConfig, "Configs/CurrencyIconsConfig" },
        };

        public static bool TryGetBy(string id, out string path)
        {
            if (_viewIDToResourcesPath.ContainsKey(id))
            {
                path = _viewIDToResourcesPath[id];
                return true;
            }

            path = null;
            return false;
        }
    }
}
