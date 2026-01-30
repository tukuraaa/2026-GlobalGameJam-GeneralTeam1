using R3;

public class EarthUnit:Singleton<EarthUnit>
{
    public ReactiveProperty<int> LifePoint = new ReactiveProperty<int>(3);
}