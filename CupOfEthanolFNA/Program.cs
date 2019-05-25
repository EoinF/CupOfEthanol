namespace LackingPlatforms
{
    using System;

    internal static class Program
    {
        [STAThreadAttribute]
        private static void Main(string[] args)
        {
            //try
            {
                using (MainMethod game = new MainMethod(args))
                {
                    game.Run();
                }
            }
            //catch
            //{
            //    System.Windows.Forms.MessageBox.Show("An unhandled error has occured in this program.\nEoin","Unhandled Exception");
            //}
        }
    }
}

