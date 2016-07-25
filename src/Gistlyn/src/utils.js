System.register(['react-redux'], function(exports_1, context_1) {
    "use strict";
    var __moduleName = context_1 && context_1.id;
    var react_redux_1;
    var Config, StateKey, GistCacheKey, GistTemplates, FileNames;
    function reduxify(mapStateToProps, mapDispatchToProps, mergeProps, options) {
        return function (target) { return (react_redux_1.connect(mapStateToProps, mapDispatchToProps, mergeProps, options)(target)); };
    }
    exports_1("reduxify", reduxify);
    function getSortedFileNames(files) {
        var fileNames = Object.keys(files);
        fileNames.sort(function (a, b) {
            if (a.toLowerCase() === "main.cs")
                return -1;
            if (b.toLowerCase() === "main.cs")
                return 1;
            if (!a.endsWith(".cs") && b.endsWith(".cs"))
                return 1;
            if (a === b)
                return 0;
            return a < b ? -1 : 0;
        });
        return fileNames;
    }
    exports_1("getSortedFileNames", getSortedFileNames);
    function addPackages(packagesConfig, pkgs) {
        var xml = "";
        pkgs.forEach(function (pkg) {
            if (!pkg.id || packagesConfig.indexOf("\"" + pkg.id + "\"") >= 0)
                return;
            var attrs = Object.keys(pkg).map(function (k) { return (k + "=\"" + pkg[k] + "\""); });
            xml += "  <package " + attrs.join(" ") + " />\n";
        });
        return xml
            ? packagesConfig.replace("</packages>", "") + xml + "</packages>"
            : packagesConfig;
    }
    exports_1("addPackages", addPackages);
    function addClientPackages(packagesConfig) {
        return addPackages(packagesConfig, [
            { id: "ServiceStack.Client", version: Config.LatestVersion, targetFramework: "net45" },
            { id: "ServiceStack.Text", version: Config.LatestVersion, targetFramework: "net45" },
            { id: "ServiceStack.Interfaces", version: Config.LatestVersion, targetFramework: "net45" },
        ]);
    }
    exports_1("addClientPackages", addClientPackages);
    return {
        setters:[
            function (react_redux_1_1) {
                react_redux_1 = react_redux_1_1;
            }],
        execute: function() {
            exports_1("Config", Config = {
                LatestVersion: "4.0.60",
            });
            exports_1("StateKey", StateKey = "/v1/state");
            exports_1("GistCacheKey", GistCacheKey = function (gist) { return ("/v1/gists/" + gist); });
            exports_1("GistTemplates", GistTemplates = {
                NewGist: "4fab2fa13aade23c81cabe83314c3cd0",
                NewPrivateGist: "7eaa8f65869fa6682913e3517bec0f7e",
                AddServiceStackReferenceGist: "eefea9cece5419f5d5dc24492d01c07c",
                HomeCollection: "2cc6b5db6afd3ccb0d0149e55fdb3a6a",
                Gists: ["4fab2fa13aade23c81cabe83314c3cd0", "7eaa8f65869fa6682913e3517bec0f7e",
                    "eefea9cece5419f5d5dc24492d01c07c", "2cc6b5db6afd3ccb0d0149e55fdb3a6a"]
            });
            exports_1("FileNames", FileNames = {
                GistMain: "main.cs",
                GistPackages: "packages.config",
                CollectionIndex: "index.md"
            });
            ;
        }
    }
});
//# sourceMappingURL=utils.js.map