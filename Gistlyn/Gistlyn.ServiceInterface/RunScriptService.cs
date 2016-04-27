﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack;
using Gistlyn.ServiceModel;
using Gistlyn.SnippetEngine;
using Gistlyn.Common.Objects;
using ServiceStack.Text;
using System.IO;
using XmlSerializer = System.Xml.Serialization.XmlSerializer;
using Gistlyn.Common.Interfaces;

namespace Gistlyn.ServiceInterface
{
    public class RunScriptService : Service
    {
        public WebHostConfig Config { get; set; }

        public IDataContext DataContext { get; set; }

        public object Any(RunScript request)
        {
            ScriptRunner runner = new ScriptRunner();
            ScriptExecutionResult result = new ScriptExecutionResult();

            try 
            {
                result = runner.Execute (request.Code).Result;
            } catch (Exception e) 
            {
                result.Exception = e;
            }

            return new RunScriptResponse {
                Result = result
            };
        }

        public object Any(RunMultipleScripts request)
        {
            ScriptRunner runner = new ScriptRunner();
            ScriptExecutionResult result = new ScriptExecutionResult();

            request.References = request.References ?? new List<AssemblyReference>();

            if (!String.IsNullOrEmpty(request.Packages))
            {
                PackageCollection packages = null;

                XmlSerializer serializer = new XmlSerializer(typeof(PackageCollection));

                byte[] arr = request.Packages.ToAsciiBytes();

                using (MemoryStream ms = new MemoryStream(arr))
                {
                    packages = (PackageCollection)serializer.Deserialize(ms);
                }

                foreach (NugetPackageInfo package in packages.Packages)
                {
                    //istall it
                    request.References.AddRange(NugetHelper.RestorePackage(DataContext, Config.NugetPackagesDirectory, package.Id, package.Ver));
                }
            }

            //distinct by name
            request.References = request.References.GroupBy(a => a.Name).Select(g => g.First()).ToList();
            List<AssemblyReference> addedReferences = request.References
                                                             .Select(r => new AssemblyReference().PopulateWith(r))
                                                             .ToList();

            foreach (AssemblyReference reference in request.References)
            {
                if (!Path.IsPathRooted(reference.Path))
                {
                    var rootPath = System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath;
                    reference.Path = Path.Combine(rootPath, Config.NugetPackagesDirectory, reference.Path);
                }
            }

            try
            {
                result = runner.Execute(request.MainCode, request.Scripts, request.References.Select(r=>r.Path).ToList()).Result;
            }
            catch (Exception e)
            {
                result.Exception = e;
            }

            return new RunMultipleScriptResponse
            {
                Result = result,
                References = addedReferences
            };
        }

    }
}