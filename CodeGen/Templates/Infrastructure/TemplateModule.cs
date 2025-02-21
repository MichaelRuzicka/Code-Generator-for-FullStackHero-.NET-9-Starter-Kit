using Carter;
using [Root_Namespace].Framework.Core.Persistence;
using [Root_Namespace].Framework.Infrastructure.Persistence;
using [Root_Namespace].[Module_Namespace].[Module].Domain;
using [Root_Namespace].[Module_Namespace].[Module].Infrastructure.Endpoints.v1;
using [Root_Namespace].[Module_Namespace].[Module].Infrastructure.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace [Root_Namespace].[Module_Namespace].[Module].Infrastructure;
public static class [Module]Module
{
    public class Endpoints : CarterModule
    {
        public Endpoints() : base("[Module]") { } //[Module] must be lowercase

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        var productGroup = app.MapGroup("products").WithTags("products");
        productGroup.MapProductCreationEndpoint();
        productGroup.MapGetProductEndpoint();
        productGroup.MapGetProductListEndpoint();
        productGroup.MapProductUpdateEndpoint();
        productGroup.MapProductDeleteEndpoint();

        var brandGroup = app.MapGroup("brands").WithTags("brands");
        brandGroup.MapBrandCreationEndpoint();
        brandGroup.MapGetBrandEndpoint();
        brandGroup.MapGetBrandListEndpoint();
        brandGroup.MapBrandUpdateEndpoint();
        brandGroup.MapBrandDeleteEndpoint();

        //var artikelGroup = app.MapGroup("artikels").WithTags("artikels");
        //artikelGroup.MapArtikelCreationEndpoint();
        //artikelGroup.MapGetArtikelEndpoint();
        //artikelGroup.MapGetArtikelListEndpoint();
        //artikelGroup.MapArtikelUpdateEndpoint();
        //artikelGroup.MapArtikelDeleteEndpoint();

        //[Routes]
    }
 
    public static WebApplicationBuilder RegisterCatalogServices(this WebApplicationBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);
        builder.Services.BindDbContext<CatalogDbContext>();
        builder.Services.AddScoped<IDbInitializer, CatalogDbInitializer>();
        builder.Services.AddKeyedScoped<IRepository<Product>, CatalogRepository<Product>>("catalog:products");
        builder.Services.AddKeyedScoped<IReadRepository<Product>, CatalogRepository<Product>>("catalog:products");

        builder.Services.AddKeyedScoped<IRepository<Brand>, CatalogRepository<Brand>>("catalog:brands");
        builder.Services.AddKeyedScoped<IReadRepository<Brand>, CatalogRepository<Brand>>("catalog:brands");

        //[Services]

        return builder;
    }
    public static WebApplication UseCatalogModule(this WebApplication app)
    {
        return app;
    }
}
