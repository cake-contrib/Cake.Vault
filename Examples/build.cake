#addin nuget:?package=Cake.Vault&version=0.0.1-alpha&prerelease&loaddependencies=true

var target = Argument("target", "Default");

Task("Default")
    .Does(async () => {
        await GetSecretsAsync(
            "https://localhost:8200",
            "token",
            "secret1",
            "config.json"
        );
    });

RunTarget(target);