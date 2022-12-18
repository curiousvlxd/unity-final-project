namespace DefaultNamespace
{
    public class Utils
    {
        public static string MakeTimeString(float time)
        {
            int t = (int)time;
            int h = t / 3600;
            int m = (t % 3600) / 60;
            int s = t % 60;
            int d = (int)((time - t) * 10);
            
            return $"{m:00}:{s:00}.{d}";
        }
    }
}