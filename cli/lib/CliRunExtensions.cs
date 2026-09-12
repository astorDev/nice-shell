using Microsoft.Extensions.DependencyInjection;
namespace NiceShell;

public static class CliParamsRunExtensions
{
    extension (Cli cli)
    {
        #region One Service
        public int Run<T1>(Action<T1> action) where T1 : notnull => cli.Run(() =>
        {
            var service1 = cli.Services.GetRequiredService<T1>();
            action(service1);
        });

        public int Run<T1>(Func<T1, int> action) where T1 : notnull => cli.Run(() =>
        {
            var service1 = cli.Services.GetRequiredService<T1>();
            return action(service1);
        });

        public Task<int> RunAsync<T1>(Func<T1, Task<int>> action) where T1 : notnull => cli.RunAsync(async () =>
        {
            var service1 = cli.Services.GetRequiredService<T1>();
            return await action(service1);
        });

        public Task<int> RunAsync<T1>(Func<T1, Task> action) where T1 : notnull => cli.RunAsync(async () =>
        {
            var service1 = cli.Services.GetRequiredService<T1>();
            await action(service1);
        });
        #endregion

        #region Two Services
        public int Run<T1, T2>(Action<T1, T2> action) where T1 : notnull where T2 : notnull => cli.Run(() =>
        {
            var service1 = cli.Services.GetRequiredService<T1>();
            var service2 = cli.Services.GetRequiredService<T2>();
            action(service1, service2);
        });

        public int Run<T1, T2>(Func<T1, T2, int> action) where T1 : notnull where T2 : notnull => cli.Run(() =>
        {
            var service1 = cli.Services.GetRequiredService<T1>();
            var service2 = cli.Services.GetRequiredService<T2>();
            return action(service1, service2);
        });

        public Task<int> RunAsync<T1, T2>(Func<T1, T2, Task> action) where T1 : notnull where T2 : notnull => cli.RunAsync(async () =>
        {
            var service1 = cli.Services.GetRequiredService<T1>();
            var service2 = cli.Services.GetRequiredService<T2>();
            await action(service1, service2);
        });

        public Task<int> RunAsync<T1, T2>(Func<T1, T2, Task<int>> action) where T1 : notnull where T2 : notnull => cli.RunAsync(async () =>
        {
            var service1 = cli.Services.GetRequiredService<T1>();
            var service2 = cli.Services.GetRequiredService<T2>();
            return await action(service1, service2);
        });
        #endregion

        #region Three Services
        public int Run<T1, T2, T3>(Action<T1, T2, T3> action) where T1 : notnull where T2 : notnull where T3 : notnull => cli.Run(() =>
        {
            var service1 = cli.Services.GetRequiredService<T1>();
            var service2 = cli.Services.GetRequiredService<T2>();
            var service3 = cli.Services.GetRequiredService<T3>();
            action(service1, service2, service3);
        });

        public int Run<T1, T2, T3>(Func<T1, T2, T3, int> action) where T1 : notnull where T2 : notnull where T3 : notnull => cli.Run(() =>
        {
            var service1 = cli.Services.GetRequiredService<T1>();
            var service2 = cli.Services.GetRequiredService<T2>();
            var service3 = cli.Services.GetRequiredService<T3>();
            return action(service1, service2, service3);
        });

        public Task<int> RunAsync<T1, T2, T3>(Func<T1, T2, T3, Task> action) where T1 : notnull where T2 : notnull where T3 : notnull => cli.RunAsync(async () =>
        {
            var service1 = cli.Services.GetRequiredService<T1>();
            var service2 = cli.Services.GetRequiredService<T2>();
            var service3 = cli.Services.GetRequiredService<T3>();
            await action(service1, service2, service3);
        });

        public Task<int> RunAsync<T1, T2, T3>(Func<T1, T2, T3, Task<int>> action) where T1 : notnull where T2 : notnull where T3 : notnull => cli.RunAsync(async () =>
        {
            var service1 = cli.Services.GetRequiredService<T1>();
            var service2 = cli.Services.GetRequiredService<T2>();
            var service3 = cli.Services.GetRequiredService<T3>();
            return await action(service1, service2, service3);
        });
        #endregion

        #region Four Services
        public int Run<T1, T2, T3, T4>(Action<T1, T2, T3, T4> action) where T1 : notnull where T2 : notnull where T3 : notnull where T4 : notnull => cli.Run(() =>
        {
            var service1 = cli.Services.GetRequiredService<T1>();
            var service2 = cli.Services.GetRequiredService<T2>();
            var service3 = cli.Services.GetRequiredService<T3>();
            var service4 = cli.Services.GetRequiredService<T4>();
            action(service1, service2, service3, service4);
        });

        public int Run<T1, T2, T3, T4>(Func<T1, T2, T3, T4, int> action) where T1 : notnull where T2 : notnull where T3 : notnull where T4 : notnull => cli.Run(() =>
        {
            var service1 = cli.Services.GetRequiredService<T1>();
            var service2 = cli.Services.GetRequiredService<T2>();
            var service3 = cli.Services.GetRequiredService<T3>();
            var service4 = cli.Services.GetRequiredService<T4>();
            return action(service1, service2, service3, service4);
        });

        public Task<int> RunAsync<T1, T2, T3, T4>(Func<T1, T2, T3, T4, Task> action) where T1 : notnull where T2 : notnull where T3 : notnull where T4 : notnull => cli.RunAsync(async () =>
        {
            var service1 = cli.Services.GetRequiredService<T1>();
            var service2 = cli.Services.GetRequiredService<T2>();
            var service3 = cli.Services.GetRequiredService<T3>();
            var service4 = cli.Services.GetRequiredService<T4>();
            await action(service1, service2, service3, service4);
        });

        public Task<int> RunAsync<T1, T2, T3, T4>(Func<T1, T2, T3, T4, Task<int>> action) where T1 : notnull where T2 : notnull where T3 : notnull where T4 : notnull => cli.RunAsync(async () =>
        {
            var service1 = cli.Services.GetRequiredService<T1>();
            var service2 = cli.Services.GetRequiredService<T2>();
            var service3 = cli.Services.GetRequiredService<T3>();
            var service4 = cli.Services.GetRequiredService<T4>();
            return await action(service1, service2, service3, service4);
        });
        #endregion
    }
}