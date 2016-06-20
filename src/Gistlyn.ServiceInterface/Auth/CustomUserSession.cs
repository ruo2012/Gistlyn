﻿using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using ServiceStack;
using ServiceStack.Auth;
using ServiceStack.Logging;

namespace Gistlyn.ServiceInterface.Auth
{
    /// <summary>
    /// Create your own strong-typed Custom AuthUserSession where you can add additional AuthUserSession 
    /// fields required for your application. The base class is automatically populated with 
    /// User Data as and when they authenticate with your application. 
    /// </summary>
    public class CustomUserSession : AuthUserSession
    {
        private static ILog log = LogManager.GetLogger(typeof(CustomUserSession));

        public object lockObj = new object();

        public Dictionary<string, ScriptRunnerInfo> Scripts = new Dictionary<string, ScriptRunnerInfo>();

        public CustomUserSession() {}

        public override void OnAuthenticated(IServiceBase authService, IAuthSession session, IAuthTokens tokens, Dictionary<string, string> authInfo)
        {
            log.Debug("OnAuthenticated");
            base.OnAuthenticated(authService, session, tokens, authInfo);
            log.Debug("Custom.OnAuthenticated");

            //get user from db
            //IDataStore store = authService.TryResolve<IDataStore>();

            //authService.SaveSession(this, TimeSpan.FromDays(7 * 2));

            //Resolve the DbFactory from the IOC and persist the user info
            //authService.TryResolve<IDbConnectionFactory>().Exec(dbCmd => dbCmd.Save(user));
        }

        public override bool IsAuthorized(string provider)
        {
            return true;
        }
    }

}

