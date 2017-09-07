namespace DocMySwagApp
{
    public interface ICommandlineArguments
    {
        string FileType { get; }
        string InputFileName { get; }
        string OutputFileName { get;}
    }
}