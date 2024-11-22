using VContainer;
using VContainer.Unity;

public class RootLifetimeScope : LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        base.Configure(builder);

        // �q��LifetimeScope�ɓ����C���X�^���X�������n��
        builder.Register<OptionHolder>(Lifetime.Singleton);
        builder.Register<ScoreHolder>(Lifetime.Singleton);
    }
}