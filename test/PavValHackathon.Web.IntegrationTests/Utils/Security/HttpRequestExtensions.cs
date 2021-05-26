using System;
using GodelTech.StoryLine.Rest.Actions;

namespace PavValHackathon.Web.IntegrationTests.Utils.Security
{
    public static class HttpRequestExtensions
    {
        public static IHttpRequest OAuth2BearerToken(this IHttpRequest builder, int userSub)
        {
            if (builder is null) throw new ArgumentNullException(nameof(builder));

            builder.Header("tmp-user-sub", userSub.ToString());

            return builder;
        }
    }
}