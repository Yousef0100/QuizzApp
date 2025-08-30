using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exa_me
{
    [Flags]
    public enum MenuItemState
    {
        None        = 0,
        Default     = 1 << 0,       // 1
        Highlighted = 1 << 1,       // 2
        Selected    = 1 << 2,       // 4
        Correct     = 1 << 3,       // 8
        Wrong       = 1 << 4,       // 16
        Missing     = 1 << 5        // 32
    }

    internal class MenuItem
    {
        private string _title;
        private MenuItemState _state;

        private Menu _menu;
        public Action selectedAction { get; private set; } // called when the item is selected
        public Action<MenuItemState, MenuItemState> stateChangedAction { get; private set; } // called when the state of the item is changed (previous state, new state)

        public string Title {
            get {
                return _title;
            }
            private set {
                _title = value;
            }
        }
        public Menu Menu {
            get {
                return _menu;
            }
            private set {
                _menu = value;
            }
        }
        public MenuItemState State {
            get { 
                return _state;
            }
            set {
                if (value != _state) 
                    stateChangedAction?.Invoke(_state, value);
                _state = value;
            }
        }


        public MenuItem (Menu menu, string title) : this (menu, title, null)
        {

        }
        public MenuItem(Menu menu, string title, Action action)
        {
            Menu = menu;
            Title = title;

            selectedAction = action;
        }


        public void Execute ()
        {
            selectedAction?.Invoke();
        }


        public void AddState(MenuItemState state)
        {
            State |= state;
        }
        public void RemoveState(MenuItemState state)
        {
            State &= ~state;
        }


        public void AddStateChangedAction (Action<MenuItemState, MenuItemState> newAction)
        {
            stateChangedAction += newAction;
        }
        public void AddSelectedAction(Action newAction)
        {
            selectedAction += newAction;
        }

        public override string ToString()
        {
            return Title;
        }
    }
}
