namespace LackingPlatforms
{
    using System;

    internal static class Program
    {
        [STAThreadAttribute]
        private static void Main(string[] args)
        {
			try
			{
				using (MainMethod game = new MainMethod(args))
				{
					game.Run();
				}
			}
			catch (Exception ex)
			{
				ErrorReporter.LogException(new string[] {
					"Game crashed",
					"Message = " + ex.Message,
					"MethodName = " + ex.TargetSite.Name,
					ex.StackTrace,
				});
				if (ex.Message.Contains("GL error GL_INVALID_ENUM in MatrixMode"))
				{
					System.Windows.Forms.MessageBox.Show(
						"There was an error with OpenGL graphics API\n" +
						"Here are some potential solutions:\n\n" +
						"1. Disable steam overlay\n" +
						"2. Update your graphics drivers\n" +
						"3. Close steam and launch the game executable manually\n\n" +
						"If none of these work, perhaps some other program is interfering with the game's graphics that you need to disable while the game is running",
						"OpenGL error");
				} else
				{
					System.Windows.Forms.MessageBox.Show(
						"Please share the content of error.txt located in the installation folder\n" +
						"You can get in touch by emailing flanagep" + "@t" + "cd" + ".ie\n\n" +
						"I will try my best to fix the issue asap",
						"An unexpected error occured");
				}
				throw ex;
			}
		}
	}
}

