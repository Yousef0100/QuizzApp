using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exa_me
{
    internal static class MenuThemes
    {
        public static MenuSettings darkTheme = new MenuSettings
        {
            headerForegroundColor = ConsoleColor.White,
            headerBackgroundColor = ConsoleColor.Black,

            titleForegroundColor = ConsoleColor.Cyan,
            titleBackgroundColor = ConsoleColor.Black,

            selectedItemForegroundColor = ConsoleColor.Black,
            selectedItemBackgroundColor = ConsoleColor.Cyan,

            defaultItemForegroundColor = ConsoleColor.Gray,
            defaultItemBackgroundColor = ConsoleColor.Black,

            highlightedItemForegroundColor = ConsoleColor.Yellow,
            highlightedItemBackgroundColor = ConsoleColor.Black,

            deactivatedItemForegroundColor = ConsoleColor.DarkGray,
            deactivatedItemBackgroundColor = ConsoleColor.Black,
        };

        public static MenuSettings blueTheme = new MenuSettings
        {
            headerForegroundColor = ConsoleColor.White,
            headerBackgroundColor = ConsoleColor.DarkBlue,

            titleForegroundColor = ConsoleColor.Yellow,
            titleBackgroundColor = ConsoleColor.DarkBlue,

            selectedItemForegroundColor = ConsoleColor.Black,
            selectedItemBackgroundColor = ConsoleColor.Yellow,

            defaultItemForegroundColor = ConsoleColor.White,
            defaultItemBackgroundColor = ConsoleColor.DarkBlue,

            highlightedItemForegroundColor = ConsoleColor.Cyan,
            highlightedItemBackgroundColor = ConsoleColor.DarkBlue,

            deactivatedItemForegroundColor = ConsoleColor.Gray,
            deactivatedItemBackgroundColor = ConsoleColor.DarkBlue,
        };

        public static MenuSettings lightTheme = new MenuSettings
        {
            headerForegroundColor = ConsoleColor.DarkBlue,
            headerBackgroundColor = ConsoleColor.White,

            titleForegroundColor = ConsoleColor.Black,
            titleBackgroundColor = ConsoleColor.White,

            selectedItemForegroundColor = ConsoleColor.White,
            selectedItemBackgroundColor = ConsoleColor.DarkBlue,

            defaultItemForegroundColor = ConsoleColor.Black,
            defaultItemBackgroundColor = ConsoleColor.White,

            highlightedItemForegroundColor = ConsoleColor.DarkRed,
            highlightedItemBackgroundColor = ConsoleColor.White,

            deactivatedItemForegroundColor = ConsoleColor.Gray,
            deactivatedItemBackgroundColor = ConsoleColor.White
        };

        public static MenuSettings retroGreen = new MenuSettings
        {
            headerForegroundColor = ConsoleColor.Green,
            headerBackgroundColor = ConsoleColor.Black,

            titleForegroundColor = ConsoleColor.DarkGreen,
            titleBackgroundColor = ConsoleColor.Black,

            selectedItemForegroundColor = ConsoleColor.Black,
            selectedItemBackgroundColor = ConsoleColor.Green,

            defaultItemForegroundColor = ConsoleColor.Green,
            defaultItemBackgroundColor = ConsoleColor.Black,

            highlightedItemForegroundColor = ConsoleColor.Yellow,
            highlightedItemBackgroundColor = ConsoleColor.Black,

            deactivatedItemForegroundColor = ConsoleColor.DarkGray,
            deactivatedItemBackgroundColor = ConsoleColor.Black
        };

        public static MenuSettings highContrast = new MenuSettings
        {
            headerForegroundColor = ConsoleColor.White,
            headerBackgroundColor = ConsoleColor.Red,

            titleForegroundColor = ConsoleColor.Yellow,
            titleBackgroundColor = ConsoleColor.Red,

            selectedItemForegroundColor = ConsoleColor.White,
            selectedItemBackgroundColor = ConsoleColor.DarkRed,

            defaultItemForegroundColor = ConsoleColor.White,
            defaultItemBackgroundColor = ConsoleColor.Black,

            highlightedItemForegroundColor = ConsoleColor.Black,
            highlightedItemBackgroundColor = ConsoleColor.Yellow,

            deactivatedItemForegroundColor = ConsoleColor.DarkGray,
            deactivatedItemBackgroundColor = ConsoleColor.Black
        };

        public static MenuSettings modernPurple = new MenuSettings
        {
            headerForegroundColor = ConsoleColor.Magenta,
            headerBackgroundColor = ConsoleColor.Black,

            titleForegroundColor = ConsoleColor.White,
            titleBackgroundColor = ConsoleColor.DarkMagenta,

            selectedItemForegroundColor = ConsoleColor.White,
            selectedItemBackgroundColor = ConsoleColor.Magenta,

            defaultItemForegroundColor = ConsoleColor.Gray,
            defaultItemBackgroundColor = ConsoleColor.Black,

            highlightedItemForegroundColor = ConsoleColor.Yellow,
            highlightedItemBackgroundColor = ConsoleColor.DarkMagenta,

            deactivatedItemForegroundColor = ConsoleColor.DarkGray,
            deactivatedItemBackgroundColor = ConsoleColor.Black
        };

        public static MenuSettings myTheme = new MenuSettings
        {
            headerBackgroundColor = ConsoleColor.Yellow,
            headerForegroundColor = ConsoleColor.Red,

            titleBackgroundColor = ConsoleColor.Black,
            titleForegroundColor = ConsoleColor.Yellow,

            selectedItemBackgroundColor = ConsoleColor.Black,
            selectedItemForegroundColor = ConsoleColor.Green,

            defaultItemBackgroundColor = ConsoleColor.Black,
            defaultItemForegroundColor = ConsoleColor.White,

            highlightedItemBackgroundColor = ConsoleColor.White,
            highlightedItemForegroundColor = ConsoleColor.Black,

            deactivatedItemBackgroundColor = ConsoleColor.Black,
            deactivatedItemForegroundColor = ConsoleColor.Gray
        };
    }
}
