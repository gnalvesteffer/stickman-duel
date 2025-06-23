using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Xorberax.Duel.Core.Aseprite;
using Xorberax.Duel.DuelGame;

// Setup DI. Register IAsepriteAnimationManager.
var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.AddSingleton<IAsepriteAnimationManager, AsepriteAnimationManager>();
        services.AddSingleton<DuelGame>();
    })
    .Build();

var game = host.Services.GetRequiredService<DuelGame>();
game.Start();