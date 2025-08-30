using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace Exa_me
{
    public struct MenuSettings
    {
        public ConsoleColor headerForegroundColor;
        public ConsoleColor headerBackgroundColor;

        public ConsoleColor titleForegroundColor;
        public ConsoleColor titleBackgroundColor;

        public ConsoleColor selectedItemForegroundColor;
        public ConsoleColor selectedItemBackgroundColor;

        public ConsoleColor defaultItemForegroundColor;
        public ConsoleColor defaultItemBackgroundColor;

        public ConsoleColor highlightedItemForegroundColor;
        public ConsoleColor highlightedItemBackgroundColor;

        public ConsoleColor deactivatedItemForegroundColor;
        public ConsoleColor deactivatedItemBackgroundColor;

        public ConsoleColor correctItemForegroundColor;
        public ConsoleColor correctItemBackgroundColor;

        public ConsoleColor wrongItemForegroundColor;
        public ConsoleColor wrongItemBackgroundColor;

        public ConsoleColor missingItemForegroundColor;
        public ConsoleColor missingItemBackgroundColor;
    }

    internal class Menu
    {
        private static int ID = 0;

        protected MenuSettings menuSettings;
        protected readonly List<MenuItem> items;

        protected bool showNumbering;
        protected bool deactivated;
        protected bool allowSelection;
        protected int maxItemLen;
        protected int currPtr;
        private  int _id;


        public string Header { get; set; }
        public string Title { get; set; }
        public int Id
        {
            get
            {
                return _id;
            }
            private set
            {
                _id = value;
            }
        }


        public Menu() : this("Empty Header", "Empty Title")
        {
            
        }
        public Menu(string header, string title, bool showNumbering = true, bool deactivated = true, bool allowSelection = true)
        {
            this.Header = header;
            this.Title = title;

            this.showNumbering = showNumbering;
            this.deactivated = deactivated;
            this.allowSelection = allowSelection;

            this.maxItemLen = 20;
            this.currPtr = 0;
            Id = ID++;

            menuSettings = MenuThemes.myExamTheme;

            items = new List<MenuItem>();

            NavigateTo(currPtr);
        }


        public void AddMenuItem(MenuItem item)
        {
            if (item.Menu != this)
                item = new MenuItem(this, item.Title, item.selectedAction);

            maxItemLen = Math.Max(maxItemLen, item.Title.Length);

            RegisterMenuItem(item);
            items.Add(item);

            if (items.Count == 1){
                currPtr = 0;
                if (!deactivated) 
                    NavigateTo(currPtr);
            }
        }
        public void AddMenuItem(string title)
        {
            MenuItem item = new MenuItem(this, title);

            AddMenuItem(item);
        }
        public void AddMenuItem(string title, Action action) 
        {
            MenuItem item = new MenuItem(this, title, action);

            AddMenuItem(item);
        }
        private void RegisterMenuItem (MenuItem item)
        {
            item.AddStateChangedAction(OnMenuItemStateChanged);
            item.AddSelectedAction(OnMenuItemSelected);
        }
        public List<MenuItem> GetMenuItems()
        {
            return items;
        }



        public void Deactivate()
        {
            deactivated = true;
            
            foreach(MenuItem item in items)
                item.State &= ~MenuItemState.Highlighted;
        }
        public void Activate()
        {
            deactivated = false;
            NavigateTo(currPtr);
        }
        public void AllowSelection(bool on)
        {
            allowSelection = on;
        }
        public virtual void Display()
        {
            // header
            SetConsoleColor(menuSettings.headerBackgroundColor, menuSettings.headerForegroundColor);

            string[] headerLines = Header.Split('\n');
            foreach (string line in headerLines)
            {
                int carterXPos = CenterCarter(line.Length);
                string s = new String(' ', carterXPos) + line;
                s += new String(' ', Console.WindowWidth - s.Length);
                Console.WriteLine(s);
            }

            Console.ResetColor();

            // title
            SetConsoleColor(menuSettings.titleBackgroundColor, menuSettings.titleForegroundColor);
            
            string[] titleLines = Title.Split('\n');
            foreach (string line in titleLines)
            {
                if (line.Length == 0)
                    continue;

                string s = line.Trim();
                s += new String(' ', Console.WindowWidth - s.Length);
                Console.WriteLine(s);
            }

            Console.ResetColor();

            // menuItems
            for (int i = 0; i < items.Count; ++i)
            {
                MenuItem item = items[i];

                // determining color schema
                switch (item.State)
                {
                    //case var x when deactivated:
                    //    SetConsoleColor(menuSettings.deactivatedItemBackgroundColor, menuSettings.deactivatedItemForegroundColor);
                    //    break;

                    case var x when x.HasFlag(MenuItemState.Selected) && x.HasFlag(MenuItemState.Highlighted):
                        SetConsoleColor(menuSettings.highlightedItemBackgroundColor, menuSettings.selectedItemForegroundColor);
                        break;

                    case var x when x.HasFlag(MenuItemState.Highlighted):
                        SetConsoleColor(menuSettings.highlightedItemBackgroundColor, menuSettings.highlightedItemForegroundColor);
                        break;

                    case var x when x.HasFlag(MenuItemState.Selected):
                        SetConsoleColor(menuSettings.selectedItemBackgroundColor, menuSettings.selectedItemForegroundColor);
                        break;

                    default:
                        SetConsoleColor(menuSettings.defaultItemBackgroundColor, menuSettings.defaultItemForegroundColor);
                        break;
                }

                // printing the item
                int prefix = i + 1;
                string itemStr = item.ToString();
                //string s = $"{(showNumbering ? $"[ ]  {prefix}. " : "")}{itemStr}";
                string s = $"{(showNumbering ? $"[{ (item.State.HasFlag(MenuItemState.Selected) ? "✔ " : "  ") }]   " : "")}{itemStr}";
                s += new String(' ', 80 - s.Length);
                Console.WriteLine(s);

                Console.ResetColor();
            }
        }


        private void OnMenuItemStateChanged (MenuItemState prevState, MenuItemState newState)
        {

        }
        private void OnMenuItemSelected ()
        {

        }

        public virtual void Select(int itemIdx)
        {
            if (itemIdx < 0 || itemIdx >= items.Count)
                return;

            if (!allowSelection) { 
                items[itemIdx].State &= ~MenuItemState.Selected;
                items[itemIdx].Execute();
                return;
            }

            if (items[itemIdx].State.HasFlag(MenuItemState.Selected))
                items[itemIdx].State &= ~MenuItemState.Selected;
            else
                items[itemIdx].State |= MenuItemState.Selected;

            items[itemIdx].Execute();
        }
        public virtual void Select()
        {
            Select(currPtr);
        }
        public void ClearSelection()
        {
            foreach (var item in items)
                item.RemoveState(MenuItemState.Selected);
        }


        public bool NavigateDown()
        {
            // currently on the last item
            if (currPtr == items.Count - 1) 
                return false;

            items[currPtr].State &= ~MenuItemState.Highlighted;
            currPtr++;
            items[currPtr].State |= MenuItemState.Highlighted;

            return true;
        }
        public bool NavigateUp()
        {
            // currently on the first item
            if (currPtr == 0)
                return false;

            items[currPtr].State &= ~MenuItemState.Highlighted;
            currPtr--;
            items[currPtr].State |= MenuItemState.Highlighted;

            return true;
        }
        public bool NavigateTo(int itemIdx)
        {
            if (itemIdx < 0 || itemIdx >= items.Count)
                return false;

            items[currPtr].State &= ~MenuItemState.Highlighted;
            currPtr = itemIdx;
            items[currPtr].State |= MenuItemState.Highlighted;
            
            return true;
        }


        public static void SetConsoleColor(ConsoleColor background, ConsoleColor foreground)
        {
            Console.BackgroundColor = background;
            Console.ForegroundColor = foreground;
        }
        public static void FlipConsoleColor()
        {
            SetConsoleColor(Console.ForegroundColor, Console.BackgroundColor);
        }
        public static int CenterCarter(int lineWidth)
        {
            int width = Console.WindowWidth;
            int xPos = (int)(0.5f * (width - lineWidth));

            return xPos;
        }

        public static Menu CreateMenu(string header, Question question)
        {
            Menu menu = new Menu(header, question.Title);

            int optionsCount = question.GetOptionsCount();
            for (int i = 0; i < optionsCount; ++i)
            {
                Option option = question.GetOption(i);

                menu.AddMenuItem(option.ToString());
            }

            return menu;
        }
    }
}
