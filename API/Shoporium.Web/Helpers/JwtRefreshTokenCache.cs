using Shoporium.Business.Logins;

namespace Shoporium.Web.Helpers
{
    public class JwtRefreshTokenCache : BackgroundService
    {
        private Timer? _timer;

        public JwtRefreshTokenCache(IServiceProvider services)
        {
            Services = services;
        }

        private IServiceProvider Services { get; }

        protected override Task ExecuteAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromMinutes(5));
            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            using var scope = Services.CreateScope();
            var loginFacade = scope.ServiceProvider
                    .GetRequiredService<ILoginFacade>();

            loginFacade.RemoveExpiredRefreshTokens(DateTime.UtcNow);
        }

        public override Task StopAsync(CancellationToken stoppingToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public override void Dispose() => _timer?.Dispose();
    }
}
