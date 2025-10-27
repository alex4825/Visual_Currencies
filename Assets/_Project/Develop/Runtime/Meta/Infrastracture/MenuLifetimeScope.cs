using Assets._Project.Develop.Runtime.Meta.Features.Wallet;
using Assets._Project.Develop.Runtime.Meta.Infrastracture;
using Assets._Project.Develop.Runtime.UI.Menu;
using VContainer;
using VContainer.Unity;

public class MenuLifetimeScope : LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        base.Configure(builder);

        builder.RegisterEntryPoint<MenuBootstrap>();

        builder.Register<WalletService>(Lifetime.Singleton);
    }
}
