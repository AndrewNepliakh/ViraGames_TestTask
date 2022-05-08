namespace Controllers
{
    public interface IRotatable : ICube
    {
        void Rotate();
        void SetAmountRotations(int obj);
        void SetSpeedRotation(int obj);
        void SetRadiusRotation(float obj);
        void SetDirectionsRotation(bool obj);
    }
}