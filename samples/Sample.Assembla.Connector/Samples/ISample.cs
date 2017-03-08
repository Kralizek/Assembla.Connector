using System.Threading.Tasks;
using Assembla;

namespace Sample.Assembla.Connector.Samples
{
    public interface ISample
    {
        Task Execute(IAssemblaClient client);
    }
}