using VContainer;
using VContainer.Unity;

public class RootLifetimeScope : LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        base.Configure(builder);

        // 子のLifetimeScopeに同じインスタンスを引き渡す
        builder.Register<OptionHolder>(Lifetime.Singleton);
        builder.Register<ScoreHolder>(Lifetime.Singleton);
    }
}