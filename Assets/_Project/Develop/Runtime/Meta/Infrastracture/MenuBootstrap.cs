using Assets._Project.Develop.Runtime.UI.Menu;
using VContainer;
using VContainer.Unity;

namespace Assets._Project.Develop.Runtime.Meta.Infrastracture
{
    public class MenuBootstrap : IStartable
    {
        [Inject]
        private readonly MenuScreenPresenter _menuScreenPresenter;

        public void Start()
        {
        }
    }
}