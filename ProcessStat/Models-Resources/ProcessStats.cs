
namespace Models_Resources
{
    public class ProcessStats
    {
        public int ProcessId { get; set; }
        public string ProcessName { get; set; }
        public long WorkingSet { get; set; }
        public long WorkingSetPrivate { get; set; }
        public long PrivateBytes { get; set; }
        public long VirtualBytes { get; set; }
    }
}
