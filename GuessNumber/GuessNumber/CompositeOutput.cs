using System.Collections.Generic;

namespace GuessNumber
{
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
