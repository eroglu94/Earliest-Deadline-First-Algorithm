using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Operating_Project
{
    class ScheduleGraphic
    {
        //Global Variables
        int itemCounter = 0;


        public void DrawRectangle(SecondaryWindow window, Brush color, Point coordinate, Scheduling.Timeline process)
        {
            var grid = new Grid();

            Rectangle rec = new Rectangle()
            {
                Width = 30,
                Height = 30,
                Fill = color,
                StrokeThickness = 0
            };

            TextBlock tb = new TextBlock()
            {
                Text = process.Process.Name + itemCounter,
                FontSize = 16,
                Height = double.NaN,
                Width = double.NaN,
                Opacity = 50,
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center

            };
            //Thickness margin = tb.Margin;
            //margin.Left = 5;
            //margin.Right = 5;
            //margin.Top = 3;
            //margin.Bottom = 5;
            //tb.Margin = margin;



            grid.Children.Add(rec);
            grid.Children.Add(tb);
            window._Canvas.Children.Add(grid);

            //window._Canvas.Children.Add(rec);
            Canvas.SetLeft(grid, coordinate.X);
            Canvas.SetTop(grid, coordinate.Y);


            //itemCounter++;
        }

        public void DrawScheduleGraphic(SecondaryWindow SW, Scheduling.Timeline[] Timeline, int processCount)
        {
            int count = -5;
            Brush Color;
            foreach (var item in Timeline)
            {
                if (item.Process.isDeadlineFailed)
                {
                    Color = Brushes.White;
                }
                else
                {
                    Color = item.Process.Color;
                }
                

                var name = item.Process.Name;
                if (name == "A")
                {
                    DrawRectangle(SW, Color, new Point(count + 5, 30), item);
                }
                if (name == "B")
                {
                    DrawRectangle(SW, Color, new Point(count + 5, 60), item);
                }
                if (name == "C")
                {
                    DrawRectangle(SW, Color, new Point(count + 5, 90), item);
                }
                if (name == "D")
                {
                    DrawRectangle(SW, Color, new Point(count + 5, 120), item);
                }
                if (name == "E")
                {
                    DrawRectangle(SW, Color, new Point(count + 5, 150), item);
                }
                if (name == "F")
                {
                    DrawRectangle(SW, Color, new Point(count + 5, 180), item);
                }
                if (name == "G")
                {
                    DrawRectangle(SW, Color, new Point(count + 5, 210), item);
                }
                if (name == "H")
                {
                    DrawRectangle(SW, Color, new Point(count + 5, 240), item);
                }
                if (name == "I")
                {
                    DrawRectangle(SW, Color, new Point(count + 5, 270), item);
                }
                if (name == "J")
                {
                    DrawRectangle(SW, Color, new Point(count + 5, 300), item);
                }

                if (item.Process.Name != "Finished")
                {
                    count += 31;
                    itemCounter++;
                }

            }

            SW.Width = 0;
            SW.Height = 0;

            for (int i = 0; i < processCount; i++)
            {
                SW.Height = SW.ActualHeight + 30;
            }
            SW.Width = count + processCount + 3;



        }

    }
}
