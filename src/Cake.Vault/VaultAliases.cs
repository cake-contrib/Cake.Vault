using System;
using System.IO;
using System.Threading.Tasks;
using Cake.Core;
using Cake.Core.Annotations;
using Cake.Core.Diagnostics;
using Newtonsoft.Json;
using VaultSharp;
using VaultSharp.V1.AuthMethods.Token;

namespace Cake.Vault
{
	/// <summary>
	///     Get vault secrets.
	/// </summary>
	[CakeAliasCategory("Configuration")]
	[CakeNamespaceImport("Vault")]
	public static class VaultAliases
	{
		/// <summary>
		/// Get vault secrets
		/// </summary>
		/// <example>
		/// <code>
		/// Task("Vailt")
		///   .Does(async () => {
		///     await GetSecretsAsync();
		/// });
		/// </code>
		/// </example>
		/// <param name="context">The context.</param>
		/// <param name="url">Vault url</param>
		/// <param name="token">Vault token</param>
		/// <param name="path">Vault key pasth</param>
		/// <param name="output">Output path</param>
		[CakeMethodAlias]
		public static async Task GetSecretsAsync(this ICakeContext context,
			string url,
			string token,
			string path,
			string output
			)
		{
			if (context == null) throw new ArgumentNullException(nameof(context));

			try
			{
				var client=  new VaultClient(new VaultClientSettings(url, new TokenAuthMethodInfo(token)));
				var secrets = await client.V1.Secrets.KeyValue.V2.ReadSecretAsync(path);
				var data = JsonConvert.SerializeObject(secrets.Data.Data);
				File.WriteAllText(output, data);
			}
			catch (Exception e)
			{
				context.Log.Error(e.Message);
				context.Log.Error(e.StackTrace);
				throw;
			}
		}
	}
}
