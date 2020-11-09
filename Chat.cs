using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Corneroids
{
    public sealed class Chat 
    {

        private const byte MaxMessages = 10;
        private static List<string> messageLines = new List<string>();
        private static List<Color> messageColors = new List<Color>();

        public static bool Active { get; set; }

        private static List<CommandBase> commands = new List<CommandBase>()
        {
            new Command("/hi", "say hi", "", null),
            new Command("/pos", "Get player position", "", () =>
            {
                SendMessage(Engine.camera.Position.ToString(), Color.Yellow);
            }),
            new Command<int>("/money", "Add money", "/money <count>", (x) =>
            {
                SendMessage("Money added: " + x.ToString(), Color.Green);
            }),
            new Command<int, int>("/get", "Add item", "/get <id> <count>", (x,y) =>
            {
                SendMessage($"Item added: ( id {x} / count {y} )", Color.Green);
            }),
            new Command<int, int>("/teleport", "", "", (x,y) =>
            {
                SendMessage($"Item added: ( id {x} / count {y} )", Color.Green);
            })

        };

        public static void OnStart()
        {
            SendMessage("/hi");

            SendMessage("/paods");

            SendMessage("/get 5 18");

            
        }

        public static void SendMessage(string text, Color color, string author = "")
        {
            if (Active == false) return;

            if (text[0] == '/')
            {
                ReadCommand(text);
                return;
            }

            MessageReading(text,color,author);

        }
        public static void SendMessage(string text)
        {
            if (Active == false) return;

            if (text[0] == '/')
            {
                ReadCommand(text);
                return;
            }

            MessageReading(text, Color.White, "");

        }
        public static void SendMessage(string text, string author )
        {
            if (Active == false) return;

            if (text[0] == '/')
            {
                ReadCommand(text);
                return;
            }

            MessageReading(text, Color.White, author);

        }

        private static void MessageReading(string text, Color color, string author = "")
        {
            if (messageLines.Count >= MaxMessages)
            {
                messageLines.RemoveAt(0);
                messageColors.RemoveAt(0);

            }


            if (author != string.Empty)
            {
                text = $"[{author}]: {text}";
            }

            text = $"> {text}";

            messageLines.Add(text);
            messageColors.Add(color);
        }

        private static void ReadCommand(string commandText)
        {
            foreach (CommandBase command in commands)
            {

                if (commandText.Contains(command.commandId))
                {
                    if (command as Command != null)
                    {
                        (command as Command).Invoke();

                        return;
                    }
                    else if (command as Command<int> != null)
                    {
                        string[] prop = commandText.Split(' ');

                        if(int.TryParse(prop[1], out int result))
                        (command as Command<int>).Invoke(result);
                        else
                        {
                            SendMessage("Invalid syntax!", Color.Red);
                        }

                        return;
                    }
                    else if (command as Command<int,int> != null)
                    {
                        string[] prop = commandText.Split(' ');

                        if (int.TryParse(prop[1], out int result) && int.TryParse(prop[2], out int result2))
                            (command as Command<int,int>).Invoke(result, result2);
                        else
                        {
                            SendMessage("Invalid syntax!", Color.Red);
                        }

                        return;
                    }
                }
                
            }

            SendMessage("Invalid command!", Color.Red);

        }

        public static void DrawMessages()
        {
            if (Active == false) return;

            if (messageLines.Count == 0) return;

            byte count = (byte)messageLines.Count;
           

            for (int i = 0; i < count; i++)
            {
                
                Engine.spriteBatch.DrawString
                    (
                    Engine.font,
                    $"{messageLines[count - 1 - i]}",
                    new Vector2(5, 270 - i * 20),
                    messageColors[count - 1 - i]
                    );
            }
        }
    }
}
