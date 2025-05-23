namespace ConsoleApp1.Patterns
{
    public class ExternalParty
    {
        public IEnumerable<int> GetData()
        {
            return Enumerable.Range(1, 10);
        }
    }
    public interface IDataManager
    {
        List<string> GetData();
    }
    public class AdapterDP : IDataManager
    {
        private readonly ExternalParty _externalParty;
        public AdapterDP()
        {
            _externalParty = new ExternalParty();
        }
        public List<string> GetData()
        {
            var xx = _externalParty.GetData();
            return xx.Select(x => string.Concat($"Item ", x, ":")).ToList();
        }
    }
    public class DemoAdapter
    {
        public void Run()
        {
            var adapter = new AdapterDP();
            IList data = adapter.GetData();
            foreach (var item in data)
            {
                Console.WriteLine(item);
            }
        }
    }
}
