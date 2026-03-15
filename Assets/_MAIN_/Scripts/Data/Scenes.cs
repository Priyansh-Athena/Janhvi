public enum SceneType
{
    City,
    CrimeScene,
    ForensicsLab
}

public static class Scenes
{
    public static string Get(SceneType scene)
    {
        switch (scene)
        {
            case SceneType.City:
                return "City";

            case SceneType.CrimeScene:
                return "CrimeScene";

            case SceneType.ForensicsLab:
                return "Lab";

            default:
                return "";
        }
    }
}