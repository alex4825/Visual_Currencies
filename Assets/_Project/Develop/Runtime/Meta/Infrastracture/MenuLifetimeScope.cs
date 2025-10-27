using Assets._Project.Develop.Runtime.Meta.Features.Wallet;
using Assets._Project.Develop.Runtime.Meta.Infrastracture;
using Assets._Project.Develop.Runtime.UI.Core;
using Assets._Project.Develop.Runtime.UI.Menu;
using Assets._Project.Develop.Runtime.Utilities.ConfigsManagement;
using VContainer;
using VContainer.Unity;

public class MenuLifetimeScope : LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        base.Configure(builder);

        builder.RegisterEntryPoint<MenuBootstrap>();

        builder.Register<WalletService>(Lifetime.Singleton);

        builder.RegisterComponentInHierarchy<MenuScreenView>();
        builder.Register<MenuScreenPresenter>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();

        builder.Register<ViewsFactory>(Lifetime.Singleton);
        builder.Register<PresentersFactory>(Lifetime.Singleton);

        builder.Register<ConfigsProviderService>(Lifetime.Singleton);

        builder.Register<CurrencyRandomizer>(Lifetime.Singleton);
    }
}
