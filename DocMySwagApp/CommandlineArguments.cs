namespace DocMySwagApp
{
    internal class CommandlineArguments : ICommandlineArguments
    {
        public string InputFileName { get; set; }
        public string OutputFileName { get; set; }
        public string FileType { get; set; }
    }
}
