using System;
using System.Collections.Generic;
using System.IO;

namespace GuessNumber
{
    public interface IOutput
    {
        void WriteLine(string text);
    }

    public class ConsoleOutput : IOutput
    {
        public void WriteLine(string text)
        {
            Console.WriteLine(text);
        }
    }

    public class FileOutput : IOutput
    {
        private readonly string _path;

        public FileOutput(string path)
        {
            _path = path;
        }

        public void WriteLine(string text)
        {
            File.AppendAllText(_path, text + Environment.NewLine);
        }
    }

    public class CompositeOutput : IOutput
    {
        private readonly List<IOutput> _outputs;

        public CompositeOutput(params IOutput[] outputs)
        {
            _outputs = new List<IOutput>(outputs);
        }

        public void WriteLine(string text)
        {
            foreach (IOutput output in _outputs)
            {
                output.WriteLine(text);
            }
        }
    }
}