/*
 * Име: Марти Георгиев Костадинов
 * Факултетен номер: F113364
 * Клас: MainForm
 * Описание: Галерия с фиксирани изображения и избор за редакция.
 */

namespace MiniGallery;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

public class MainForm : Form
{
    private List<ImageItem> gallery = new();
    private int selectedIndex = -1;

    private FlowLayoutPanel galleryPanel = new();
    private Button addBtn = new();
    private Button removeBtn = new();
    private Button editBtn = new();

    private const int ItemWidth = 380;
    private const int ItemHeight = 260;

    public MainForm()
    {
        Text = "Mini Gallery";
        Width = 900;
        Height = 650;
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;

        InitializeMenu();
        InitializeUI();
    }

    private void InitializeMenu()
    {
        MenuStrip menu = new();

        var file = new ToolStripMenuItem(LanguageManager.Get("File"));
        var exit = new ToolStripMenuItem(LanguageManager.Get("Exit"), null, (s, e) => Close());
        file.DropDownItems.Add(exit);

        var bg = new ToolStripMenuItem("BG", null, (s, e) =>
        {
            LanguageManager.SetLanguage("bg");
            UpdateLanguage();
        });

        var en = new ToolStripMenuItem("EN", null, (s, e) =>
        {
            LanguageManager.SetLanguage("en");
            UpdateLanguage();
        });


        menu.Items.Add(file);
        menu.Items.Add(bg);
        menu.Items.Add(en);

        MainMenuStrip = menu;
        Controls.Add(menu);
    }

    private void InitializeUI()
    {
        FlowLayoutPanel topPanel = new()
        {
            Dock = DockStyle.Top,
            Height = 60,
            Padding = new Padding(10)
        };

        addBtn.Text = LanguageManager.Get("Add");
        removeBtn.Text = LanguageManager.Get("Remove");
        editBtn.Text = LanguageManager.Get("Edit");

        addBtn.Size = new Size(130, 40);
        removeBtn.Size = new Size(130, 40);
        editBtn.Size = new Size(130, 40);

        addBtn.Click += AddImage;
        removeBtn.Click += RemoveImage;
        editBtn.Click += EditCaption;

        topPanel.Controls.Add(addBtn);
        topPanel.Controls.Add(removeBtn);
        topPanel.Controls.Add(editBtn);

        galleryPanel.Dock = DockStyle.Fill;
        galleryPanel.AutoScroll = true;
        galleryPanel.WrapContents = true;
        galleryPanel.Padding = new Padding(10);

        Controls.Add(galleryPanel);
        Controls.Add(topPanel);
    }

    private void RefreshGallery()
    {
        galleryPanel.Controls.Clear();

        for (int i = 0; i < gallery.Count; i++)
        {
            int index = i;

            Panel container = new Panel
            {
                Width = ItemWidth,
                Height = ItemHeight,
                Margin = new Padding(10),
                BorderStyle = index == selectedIndex
                    ? BorderStyle.Fixed3D
                    : BorderStyle.FixedSingle
            };

            PictureBox pb = new PictureBox
            {
                Dock = DockStyle.Fill,
                Image = Image.FromFile(gallery[i].ImagePath),
                SizeMode = PictureBoxSizeMode.Zoom,
                Cursor = Cursors.Hand
            };

            Label lbl = new Label
            {
                Dock = DockStyle.Bottom,
                Height = 30,
                Text = gallery[i].Caption,
                TextAlign = ContentAlignment.MiddleCenter
            };

            pb.Click += (s, e) =>
            {
                selectedIndex = index;
                RefreshGallery();
            };

            lbl.Click += (s, e) =>
            {
                selectedIndex = index;
                RefreshGallery();
            };

            container.Controls.Add(pb);
            container.Controls.Add(lbl);
            galleryPanel.Controls.Add(container);
        }
    }

    private void AddImage(object? s, EventArgs e)
    {
        OpenFileDialog ofd = new()
        {
            Filter = "Images|*.jpg;*.png;*.bmp"
        };

        if (ofd.ShowDialog() == DialogResult.OK)
        {
            gallery.Add(new ImageItem(ofd.FileName, "Нов надпис"));
            selectedIndex = gallery.Count - 1;
            RefreshGallery();
        }
    }

    private void RemoveImage(object? s, EventArgs e)
    {
        if (selectedIndex < 0 || gallery.Count == 0) return;

        gallery.RemoveAt(selectedIndex);
        selectedIndex = -1;
        RefreshGallery();
    }

    private void EditCaption(object? s, EventArgs e)
    {
        if (selectedIndex < 0) return;

        string text = Microsoft.VisualBasic.Interaction.InputBox(
            "Нов надпис:", "Редактиране", gallery[selectedIndex].Caption);

        if (!string.IsNullOrWhiteSpace(text))
        {
            gallery[selectedIndex].Caption = text;
            RefreshGallery();
        }
    }
    private void UpdateLanguage()
    {
        addBtn.Text = LanguageManager.Get("Add");
        removeBtn.Text = LanguageManager.Get("Remove");
        editBtn.Text = LanguageManager.Get("Edit");
        Text = "Mini Gallery";
    }

}
