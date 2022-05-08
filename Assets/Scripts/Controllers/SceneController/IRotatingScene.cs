namespace Controllers
{
    public interface IRotatingScene : IScene
    {
        IRotatable Cube { get; }
    }
}