namespace Controllers
{
    public interface IMovingScene : IScene
    {
        IMovable Cube { get; }
    }
}