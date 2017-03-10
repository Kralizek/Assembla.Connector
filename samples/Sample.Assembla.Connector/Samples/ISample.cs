using System.Threading.Tasks;
using Kralizek.Assembla;
using Kralizek.Assembla.Connector;

namespace Sample.Assembla.Connector.Samples
{
    public interface ISample
    {
        Task Execute(IAssemblaClient client);
    }
}