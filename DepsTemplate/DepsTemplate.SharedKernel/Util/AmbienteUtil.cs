using dotenv.net;
using System;

namespace DepsTemplate.SharedKernel.Util
{
    public static class AmbienteUtil
    {
        public static string GetValue(string variableName)
        {
            DotEnv.Load(options: new DotEnvOptions(envFilePaths: new[] { "C:\\Users\\migue\\Desktop\\DepsTemplate\\DepsTemplate\\DepsTemplate.Web\\settings.env" }));
            string value = Environment.GetEnvironmentVariable(variableName);
            return value;
        }
    }
}
