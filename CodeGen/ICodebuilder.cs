using System.Threading.Tasks;

namespace CodeGen
{
    public interface ICodebuilder
    {


        Task<bool> BuildAsync();
    }
}