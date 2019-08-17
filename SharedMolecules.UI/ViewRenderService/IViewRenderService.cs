

using System.Threading.Tasks;

namespace SharedMolecules.UI.ViewRenderService
{
    public interface IViewRenderService
    {
        Task<string> RenderToStringAsync(string viewName, object model);
    }
}
