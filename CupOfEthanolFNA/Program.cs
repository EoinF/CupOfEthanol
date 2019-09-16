namespace LackingPlatforms
{
    using System;

    internal static class Program
    {
        [STAThreadAttribute]
        private static void Main(string[] args)
        {
            //try
            //{
                using (MainMethod game = new MainMethod(args))
                {
                    game.Run();
                }
            //}
			//catch (Exception ex)
			//{
			//	ErrorReporter.LogException(new string[] {
			//		"Game crashed",
			//		"Message = " + ex.Message,
			//		"MethodName = " + ex.TargetSite.Name,
			//		ex.StackTrace,
			//	});
			//	System.Windows.Forms.MessageBox.Show(
			//		"Please share the content of error.txt located in the installation folder\n" +
			//		"You can get in touch by emailing flanagep" + "@t" + "cd" + ".ie\n\n" +
			//		"I will try my best to fix the issue asap", 
			//		"An unexpected error occured");
			//	throw ex;
			//}
		}
	}
}

