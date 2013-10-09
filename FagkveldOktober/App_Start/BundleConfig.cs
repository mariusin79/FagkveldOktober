﻿using System.Web.Optimization;

namespace FagkveldOktober
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/Content/scripts").Include(
                "~/Scripts/lib/jquery-{version}.js",
                "~/Scripts/lib/bootstrap.js",
                "~/Scripts/lib/underscore.js",
                "~/Scripts/lib/angular.js",
                "~/Scripts/lib/angular-resource.js",
                "~/Scripts/lib/angular-route.js",
                "~/Scripts/app.js"
                ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/bootstrap/bootstrap.css",
                "~/Content/site.css"
                ));
        }
    }
}