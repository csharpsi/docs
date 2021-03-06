﻿using System.Web.Mvc;
using System.Web.Routing;
using Raven.Documentation.Parser.Data;
using Raven.Documentation.Web.Core.ViewModels;
using Raven.Documentation.Web.Helpers;
using Raven.Documentation.Web.Models;
using Raven.Documentation.Web.Routing;

namespace Raven.Documentation.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            MapArticlesRoutes(routes);
            MapAttachmentsRoutes(routes);

            routes.MapRouteLowerCase(
                "Docs",
                "article/{version}/{language}/{*key}",
                new
                {
                    controller = MVC.Docs.Name,
                    action = MVC.Docs.ActionNames.ArticlePage
                },
                new
                {
                    version = "1.0|2.0|2.5|3.0|3.5|4.0",
                    language = "csharp|java|http|python"
                },
                new[] { "Raven.Documentation.Web.Controllers" });

            routes.MapRouteLowerCase(
                "Routes",
                "{action}/{version}/{language}",
                new
                {
                    controller = MVC.Docs.Name
                },
                new
                {
                    version = "1.0|2.0|2.5|3.0|3.5|4.0",
                    language = "csharp|java|http|python"
                },
                new[] { "Raven.Documentation.Web.Controllers" });

            routes.MapRouteLowerCase(
                "Default",
                "{action}/{version}/{language}",
                new
                {
                    controller = MVC.Docs.Name,
                    action = MVC.Docs.ActionNames.Index,
                    version = "4.0",
                    language = Language.Csharp
                },
                new
                {
                    version = "1.0|2.0|2.5|3.0|3.5|4.0",
                    language = "csharp|java|http|python"
                },
                new[] { "Raven.Documentation.Web.Controllers" });

            routes.MapRoute("ArticleSearch", "docs/search/{version}/{language}", new { controller = MVC.Docs.Name, action = MVC.Docs.ActionNames.Search });
            routes.MapRoute("ArticleSearchSuggest", "docs/suggest/{version}/{language}/{*searchTerm}", new { controller = MVC.Docs.Name, action = MVC.Docs.ActionNames.Suggest });

            routes.MapRoute("ArticleByRoutePage", "docs/article-page/{version}/{language}/{*key}", new { controller = MVC.Docs.Name, action = MVC.Docs.ActionNames.ArticlePage });
            routes.MapRoute("DocsLegacyV1", "docs/1.0/{*key}", new { controller = MVC.Docs.Name, action = MVC.Docs.ActionNames.OldArticlePage, language = Language.Csharp, version = "1.0" });
            routes.MapRoute("DocsLegacyV2", "docs/2.0/{*key}", new { controller = MVC.Docs.Name, action = MVC.Docs.ActionNames.OldArticlePage, language = Language.Csharp, version = "2.0" });
            routes.MapRoute("DocsLegacyV25", "docs/2.5/{*key}", new { controller = MVC.Docs.Name, action = MVC.Docs.ActionNames.OldArticlePage, language = Language.Csharp, version = "2.5" });


            routes.MapRoute(
                "DocsDefault",
                "docs/{action}/{version}/{language}",
                new
                {
                    controller = MVC.Docs.Name,
                    action = MVC.Docs.ActionNames.Index,
                    version = DocsVersion.Default,
                    language = DocumentationLanguage.Default
                },
                new
                {
                    version = "1.0|2.0|2.5|3.0|3.5|4.0",
                    language = "csharp|java|http"
                },
                new[] { "Raven.Documentation.Web.Controllers" });

            routes.MapRoute("DocsLegacy", "docs/{*key}", new { controller = MVC.Docs.Name, action = MVC.Docs.ActionNames.OldArticlePage, language = Language.Csharp, version = "2.5" });
        }

        private static void MapAttachmentsRoutes(RouteCollection routes)
        {
            routes.MapRouteLowerCase(
                "Attachments",
                "attachments/{type}",
                new
                {
                    controller = MVC.Attachments.Name,
                    action = MVC.Attachments.ActionNames.Get
                },
                new
                {
                    type = $"{ArticleType.Documentation.GetDescription()}|{ArticleType.Articles.GetDescription()}"
                },
                new[] { "Raven.Documentation.Web.Controllers" });
        }

        private static void MapArticlesRoutes(RouteCollection routes)
        {
            routes.MapRouteLowerCase(
                "ArticlesGenerate",
                "articles/generate",
                new
                {
                    controller = MVC.Articles.Name,
                    action = MVC.Articles.ActionNames.Generate
                },
                null,
                new[] { "Raven.Documentation.Web.Controllers" });

            routes.MapRouteLowerCase(
                "Articles",
                "articles/{*key}",
                new
                {
                    controller = MVC.Articles.Name,
                    action = MVC.Articles.ActionNames.Articles
                },
                null,
                new[] { "Raven.Documentation.Web.Controllers" });
        }
    }
}
