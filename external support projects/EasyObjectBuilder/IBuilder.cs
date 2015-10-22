namespace EasyObjectBuilder
{
    public interface IBuilder
    {
        object Build();
    }

    public interface IBuilder<out T>
    {
        T Build();
    }
}