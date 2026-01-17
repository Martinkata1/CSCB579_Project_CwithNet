/*
 * Име: Марти Георгиев Костадинов
 * Факултетен номер: F113364
 * Клас: ImageItem
 * Описание: Представя едно изображение в галерията.
 */

namespace MiniGallery;

public class ImageItem
{
    public string ImagePath { get; set; }
    public string Caption { get; set; }

    public ImageItem(string path, string caption)
    {
        ImagePath = path;
        Caption = caption;
    }
}
