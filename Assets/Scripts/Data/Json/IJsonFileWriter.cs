public interface IJsonFileWriter : IGameService
{
    void SerialiseData<T>(T data);
}