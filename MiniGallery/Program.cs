/*
 * Име: Марти Георгиев Костадинов
 * Факултетен номер: F113364
 * Клас: Program
 * Описание: Стартова точка на Windows Forms приложението.
 */

namespace MiniGallery;

using System;
using System.Windows.Forms;

static class Program
{
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();
        Application.Run(new MainForm());
    }
}
