using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace WorldCup.App.Policies
{
    public static class AdminAuthorizationPolicy
    {
        public static string Name => "Admin";

        public static void Build(AuthorizationPolicyBuilder builder) =>
            builder.RequireClaim("groups", "f110a9b0-4f25-4c0a-84d6-1f2954eedd58");
    }
}
