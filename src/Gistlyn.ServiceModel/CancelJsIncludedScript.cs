﻿using System;
using Gistlyn.ServiceModel.Types;
using ServiceStack;

namespace Gistlyn.ServiceModel
{
    public class CancelJsIncludedScript : IReturn<CancelJsIncludedScriptResponse>
    {
        public string GistHash { get; set; }
    }

    public class CancelJsIncludedScriptResponse
    {
        public ScriptExecutionResult Result { get; set; }
    }
}
