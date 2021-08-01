namespace Student.MyJWT
{
    //We are using this to expose any setting on application.setting.json
    //we want to inject the section using DI (dependency injection) so we can access it in our classes
    public class AppSettings
    {
        public string Secret { get; set; }
    }
}