using System.Windows;
using System.Media;


namespace Lesson10
{
    internal class Post
    {
		public static void PostMessage(string msg)
        {
            MessageBox.Show(msg);
        }

        public static void PostErrorMessage(string message)
        {
		 MessageBox.Show(message,
         "Ошибка",
         MessageBoxButton.OK,
         MessageBoxImage.Error);

         SystemSounds.Exclamation.Play();

        }
	}
}
