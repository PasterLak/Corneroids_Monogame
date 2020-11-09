using System;

namespace Corneroids
{

    public class Command : CommandBase
    {
        private Action command;

        public Command(string id, string discription, string format, Action command) : base(id, discription, format)
        {
            this.command = command;
        }

        public void Invoke()
        {
            if(command != null)
            command.Invoke();
        }
    }

    public class Command<T1> : CommandBase
    {
        private Action<T1> command;

        public Command(string id, string discription, string format, Action<T1> command) : base(id, discription, format)
        {
            this.command = command;
        }

        public void Invoke(T1 value)
        {
            if (command != null)
                command.Invoke(value);
        }
    }


    public class Command<T1, T2> : CommandBase
    {
        private Action<T1, T2> command;

        public Command(string id, string discription, string format, Action<T1, T2> command) : base(id, discription, format)
        {
            this.command = command;
        }

        public void Invoke(T1 value, T2 value2)
        {
            if (command != null)
                command.Invoke(value, value2);
        }
    }



    public class Command<T1, T2, T3> : CommandBase
    {
        private Action<T1, T2, T3> command;

        public Command(string id, string discription, string format, Action<T1, T2, T3> command) : base(id, discription, format)
        {
            this.command = command;
        }

        public void Invoke(T1 value1, T2 value2, T3 value3)
        {
            if (command != null)
                command.Invoke(value1, value2, value3);
        }
    }


}
