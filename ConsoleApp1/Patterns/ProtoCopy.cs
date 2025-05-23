namespace ConsoleApp1.Patterns
{
    public class ProtoCopy : ICloneable
    {
        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
