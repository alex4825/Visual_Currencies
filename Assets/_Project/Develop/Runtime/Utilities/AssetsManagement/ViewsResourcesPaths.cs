using Assets._Project.Develop.Runtime.UI.Core;
using System.Collections.Generic;

namespace Assets._Project.Develop.Runtime.Utilities.AssetsManagement
{
    public class ViewsResourcesPaths
    {
        private static Dictionary<string, string> _viewIDToResourcesPath = new Dictionary<string, string>()
        {
            {ViewIDs.CurrencyView, "UI/CurrencyView" },
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
