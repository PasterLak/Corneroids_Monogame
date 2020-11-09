using System;
namespace Corneroids
{
    public class CommandBase
    {
        public string commandId { get; }
        public string commandDiscription { get; }
        public string commandFormat { get; }

        public CommandBase(string id, string discription, string format)
        {
            commandId = id;
            commandDiscription = discription;
            commandFormat = format;
        }
    }
}
