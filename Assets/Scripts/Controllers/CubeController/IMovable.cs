namespace Controllers
{
    public interface IMovable : ICube
    {
        void Move();
        void SetSpeed(int obj);
    }
}