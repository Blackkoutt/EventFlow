using EventFlowAPI.Logic.Services.OtherServices.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Logging;

namespace EventFlowAPI.Logic.Services.OtherServices.Services
{
    public class HtmlRendererService(
        IServiceProvider serviceProvider,
        ILoggerFactory loggerFactory) : IHtmlRendererService
    {
        private readonly IServiceProvider _serviceProvider = serviceProvider;
        private readonly ILoggerFactory _loggerFactory = loggerFactory;

        public async Task<string> RenderHtmlToStringAsync<TView>(Dictionary<string, object?> paramsDictionary) where TView : IComponent
        {
            await using var htmlRenderer = new HtmlRenderer(_serviceProvider, _loggerFactory);
            var html = await htmlRenderer.Dispatcher.InvokeAsync(async () =>
            {
                var parameters = ParameterView.FromDictionary(paramsDictionary);
                var output = await htmlRenderer.RenderComponentAsync<TView>(parameters);
                return output.ToHtmlString();
            });
            return html;
        }
    }
}
