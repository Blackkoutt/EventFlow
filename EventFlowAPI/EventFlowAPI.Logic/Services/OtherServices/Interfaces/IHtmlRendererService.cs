using Microsoft.AspNetCore.Components;

namespace EventFlowAPI.Logic.Services.OtherServices.Interfaces
{
    public interface IHtmlRendererService
    {
        Task<string> RenderHtmlToStringAsync<TView>(Dictionary<string, object?> paramsDictionary) where TView : IComponent;
    }
}
