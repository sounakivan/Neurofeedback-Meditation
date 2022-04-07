/// <summary>
/// Contain configuration of a specific App.
/// </summary>
static class AppConfig
{
    public static string AppUrl              = "wss://localhost:6868";
    public static string AppName             = "UnityApp";
    
    /// <summary>
    /// Name of directory where contain tmp data and logs file.
    /// </summary>
    public static string TmpAppDataDir       = "UnityApp";
    public static string ClientId            = "5YwDXevkIhLc9hxQhe7lwhgidBXgfR1arCaeHm6k";
    public static string ClientSecret        = "TsqQNK2i6jaE7omnsgl6sD895MPHw88jN2oZSbgLTw6eJvRKUtUXblxlCXWLkExCxXTo8xfFKDWcC0YTBpGWGmvnmsQdrdMQU6EReajbob5qQ3LWEzlZjpUY5k5BNLaJ";
    public static string AppVersion          = "3.2.0 Dev";
    
    /// <summary>
    /// License Id is used for App
    /// In most cases, you don't need to specify the license id. Cortex will find the appropriate license based on the client id
    /// </summary>
    public static string AppLicenseId        = "";
}