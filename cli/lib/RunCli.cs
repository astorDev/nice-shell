using Hesive;

namespace NiceShell;

public static class RunCliExtensions
{
    public static async Task<int> RunCliAsync<T1>(this App app, Func<T1, Task> func) where T1 : class
    {
        try
        {
            var service1 = app.Services.GetRequiredService<T1>();
            await func(service1);
            return 0;
        }
        catch (Exception ex)
        {
            app.Logger.LogError(ex, "An error occurred while running the CLI.");
            return 1;
        }
    }

    public static async Task<int> RunCliAsync<T1, T2>(this App app, Func<T1, T2, Task> func) 
        where T1 : class 
        where T2 : class
    {
        try
        {
            var service1 = app.Services.GetRequiredService<T1>();
            var service2 = app.Services.GetRequiredService<T2>();
            await func(service1, service2);
            return 0;
        }
        catch (Exception ex)
        {
            app.Logger.LogError(ex, "An error occurred while running the CLI.");
            return 1;
        }
    }

    public static async Task<int> RunCliAsync<T1, T2, T3>(this App app, Func<T1, T2, T3, Task> func) 
        where T1 : class 
        where T2 : class
        where T3 : class
    {
        try
        {
            var service1 = app.Services.GetRequiredService<T1>();
            var service2 = app.Services.GetRequiredService<T2>();
            var service3 = app.Services.GetRequiredService<T3>();
            await func(service1, service2, service3);
            return 0;
        }
        catch (Exception ex)
        {
            app.Logger.LogError(ex, "An error occurred while running the CLI.");
            return 1;
        }
    }

    public static async Task<int> RunCliAsync<T1>(this App app, Func<T1, Task<int>> func) where T1 : class
    {
        try
        {
            var service1 = app.Services.GetRequiredService<T1>();
            return await func(service1);
        }
        catch (Exception ex)
        {
            app.Logger.LogError(ex, "An error occurred while running the CLI.");
            return 1;
        }
    }

    public static async Task<int> RunCliAsync<T1, T2>(this App app, Func<T1, T2, Task<int>> func) 
        where T1 : class 
        where T2 : class
    {
        try
        {
            var service1 = app.Services.GetRequiredService<T1>();
            var service2 = app.Services.GetRequiredService<T2>();
            return await func(service1, service2);
        }
        catch (Exception ex)
        {
            app.Logger.LogError(ex, "An error occurred while running the CLI.");
            return 1;
        }
    }

    public static async Task<int> RunCliAsync<T1, T2, T3>(this App app, Func<T1, T2, T3, Task<int>> func) 
        where T1 : class 
        where T2 : class
        where T3 : class
    {
        try
        {
            var service1 = app.Services.GetRequiredService<T1>();
            var service2 = app.Services.GetRequiredService<T2>();
            var service3 = app.Services.GetRequiredService<T3>();
            return await func(service1, service2, service3);
        }
        catch (Exception ex)
        {
            app.Logger.LogError(ex, "An error occurred while running the CLI.");
            return 1;
        }
    }
}