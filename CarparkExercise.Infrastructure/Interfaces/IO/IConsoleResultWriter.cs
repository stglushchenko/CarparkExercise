namespace CarparkExercise.Infrastructure.Interfaces.IO
{
    public interface IConsoleResultWriter
    {
        void Write(string payRateName, decimal totalPrice);
    }
}
