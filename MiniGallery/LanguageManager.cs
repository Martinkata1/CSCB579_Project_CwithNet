/*
 * Име: Марти Георгиев Костадинов
 * Факултетен номер: F113364
 * Клас: LanguageManager
 * Описание: Управлява текстовете за различни езици.
 */

namespace MiniGallery;

using System.Collections.Generic;

public static class LanguageManager
{
    private static string current = "bg";

    private static Dictionary<string, Dictionary<string, string>> texts =
        new()
        {
            ["bg"] = new()
            {
                ["Add"] = "Добави",
                ["Remove"] = "Премахни",
                ["Edit"] = "Редактирай",
                ["File"] = "Файл",
                ["Exit"] = "Изход"
            },
            ["en"] = new()
            {
                ["Add"] = "Add",
                ["Remove"] = "Remove",
                ["Edit"] = "Edit",
                ["File"] = "File",
                ["Exit"] = "Exit"
            }
        };

    public static void SetLanguage(string lang)
    {
        current = lang;
    }

    public static string Get(string key)
    {
        return texts[current][key];
    }
}
