using NLayerApp.Core.Interfaces;

namespace NLayerApp.Core.Configurations
{
    public class Client : IConfiguration
    {
        public string Id { get; set; }
        public string Secret { get; set; }
        public List<string> Audiences { get; set; }

    }
}
