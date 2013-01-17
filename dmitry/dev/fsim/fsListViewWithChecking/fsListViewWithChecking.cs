using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ListViewWithChecking
{
    /// <summary>
    /// This class implements simple and fast operations (adding, removing, checking)
    /// on {ListViews with checking} having different ListViewitem's names.
    /// Implements drag-and-drop functionality, smart items checking, and suit assigning actions to 
    /// adding, removing, checking, and dragging items between different instances of fsListViewWithChecking
    /// (with the use of fsListViewWithCheckingSupervisor class)
    /// </summary>
    public partial class fsListViewWithChecking : UserControl
    {
        private bool isfsListViewWithChecking(object someObject)
        {
            if (someObject is ListView)
            {
                if (((ListView)someObject).Parent is fsListViewWithChecking)
                    return true;
                else
                    return false;
            }
            return false;
        }

        private List<string> m_names;

        private List<bool> m_checks;

        private int m_count;

        /// <summary>
        /// The list of names of a fsListViewWithChecking instance items.
        /// </summary>
        public List<string> ItemsNames { get { return m_names; } }

        /// <summary>
        /// The list of check values of a fsListViewWithChecking instance items.
        /// </summary>
        public List<bool> ItemsChecks { get { return m_checks; } }

        /// <summary>
        /// The count of items of a fsListViewWithChecking instance.
        /// </summary>
        public int Count { get { return m_count; } }

        #region Private doing with ListViewItems in m_listView

        private void AddItem()
        {
            m_listView.Items.Add("");
        }

        private void AddItemRange(int count)
        {
            if (count > 0)
                for (int i = 0; i < count; i++)
                {
                    AddItem();
                }
        }

        private void RemoveItem()
        {
            if (m_count > 0)
                m_listView.Items.RemoveAt(0);
            else
                throw new Exception("An attempt to remove an item from the empty m_listView");
        }

        private void RemoveItemRange(int count)
        {
            try
            {
                if (count > 0)
                    for (int i = 0; i < count; i++)
                    {
                        RemoveItem();
                    }
            }
            catch (Exception)
            {
                throw new Exception("An attempt to remove an exceeding number of items from m_listView");
            }
        }

        #endregion

        #region Private doing with names in m_names

        private void InsertName(int index, string name)
        {
            try
            {
                m_names.Insert(index, name);
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new Exception("An attempt to insert the name at an inappropriate index");
            }
        }

        private void AddName(string name)
        {
            m_names.Add(name);
        }

        private void AddNameRange(IEnumerable<string> collection)
        {
            m_names.AddRange(collection);
        }

        private void RemoveNameAt(int index)
        {
            try
            {
                m_names.RemoveAt(index); ;
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new Exception("An attempt to remove the name at an inappropriate index");
            }
        }

        private bool RemoveName(string name)
        {
            return m_names.Remove(name);
        }

        #endregion

        #region Private doing with check values in m_checks

        private void InsertCheck(int index, bool check)
        {
            m_checks.Insert(index, check);
        }

        private void AddCheck(bool check)
        {
            m_checks.Add(check);
        }

        private void AddCheckRange(IEnumerable<bool> collection)
        {
            m_checks.AddRange(collection);
        }

        private void RemoveCheckAt(int index)
        {
            try
            {
                m_checks.RemoveAt(index); ;
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new Exception("An attempt to remove the check at an inappropriate index");
            }
        }

        #endregion

        #region Assigning

        /// <summary>
        /// Assigns names to a created fsListViewWithChecking instance.
        /// </summary>
        private void AssignNames()
        {
            for (int i = 0; i < m_count; i++)
            {
                m_listView.Items[i].Text = m_names[i];
            }
        }

        /// <summary>
        /// Assigns checks to a created fsListViewWithChecking instance.
        /// </summary>
        /// <returns>A list of states ("index", new check value at "index", etc.) where a check value was changed at "index". </returns>
        private List<fsCheckItemInfo> AssignChecks()
        {
            var result = new List<fsCheckItemInfo>();
            for (int i = 0; i < m_count; i++)
            {
                bool checkValue = m_listView.Items[i].Checked;
                if (checkValue != m_checks[i])
                {
                    m_listView.Items[i].Checked = m_checks[i];
                    result.Add(new fsCheckItemInfo(i, m_checks[i]));
                }
            }
            return result;
        }

        /// <summary> 
        /// Assigns names and checks to a created fsListViewWithChecking instance.
        /// </summary>
        protected virtual void Assign()
        {
            AssignNames();
            AssignChecks();
        }

        #endregion

        #region Constructor

        #region Constructor events

        /// <summary>
        /// An event which is raised when a number of items is added to a fsListViewWithChecking instance
        /// </summary>
        public event EventHandler<EventArgs> MultipleAddListViewItemsEvent;

        /// <summary>
        /// Publishing MultipleAddListViewItemsEvent
        /// </summary>
        /// <param name="e">An event argument which is to be converted to an appropriate type of a class which inherits EventArgs </param>
        protected virtual void OnMultipleAddListViewItemsEvent(EventArgs e)
        {
            // Make a temporary copy of the event to avoid possibility of
            // a race condition if the last subscriber unsubscribes
            // immediately after the null check and before the event is raised.
            EventHandler<EventArgs> handler = MultipleAddListViewItemsEvent;

            // Event will be null if there are no subscribers
            if (handler != null)
                handler(this, (fsListViewWithCheckingEventArgs)e);
        }

        /// <summary>
        /// An event which is raised when a number of items is added to a fsListViewWithChecking instance
        /// </summary>
        public event EventHandler<EventArgs> RechargeListViewEvent;

        /// <summary>
        /// Publishing RechargeListViewEvent
        /// </summary>
        /// <param name="e">An event argument which is to be converted to an appropriate type of a class which inherits EventArgs </param>
        protected virtual void OnRechargeListViewEvent(EventArgs e)
        {
            // Make a temporary copy of the event to avoid possibility of
            // a race condition if the last subscriber unsubscribes
            // immediately after the null check and before the event is raised.
            EventHandler<EventArgs> handler = RechargeListViewEvent;

            // Event will be null if there are no subscribers
            if (handler != null)
                handler(this, (fsListViewWithCheckingEventArgs)e);
        }

        #endregion

        /// <summary> 
        /// Creating an empty fsListViewWithChecking instance
        /// </summary>
        public fsListViewWithChecking()
        {
            InitializeComponent();
            m_names = new List<string>();
            m_checks = new List<bool>();
            m_count = 0;
            IsSpecificCheckDoing = false;
            AllowedDrag = false;
            AllowedDrop = false;
        }

        /// <summary> 
        /// Performs adding specific items and their check values to a fsListViewWithChecking instance.
        /// Items are ordered by the natural order of "items". An item "item[i]"
        /// has the check value "checks[i]". If the count of either "items" or  "checks" is more than the
        /// count of another collection than the first one is truncated to be of an equal size.
        /// If any two item names in "items" coincide an exception rises.
        /// If there are common strings in existing and added collections of names an exception rises.
        /// </summary>
        /// <param name="items">Collection of specific items for ListViewItems</param>
        /// <param name="checks">Collection of check values for ListViewItems</param>
        /// <param name="isOnLoad">The flag that shows whether adding is performing while initial 
        /// fsListViewWithChecking instance is being filled</param>
        public virtual void AddToListViewWithChecking(IEnumerable<object> items, IEnumerable<bool> checks, bool isOnLoad)
        {
            IsSpecificCheckDoing = true;

            int itemsCount = items.Count();
            for (int i = 0; i < itemsCount - 1; i++)
            {
                string elementAt_i = (string)items.ElementAt(i);
                for (int j = i + 1; j < itemsCount; j++)
                {
                    if (elementAt_i.Equals((string)items.ElementAt(j)))
                        throw new Exception("There are coinciding strings in added names");
                }
            }
            if (m_names.Intersect((IEnumerable<string>)items).Count() > 0)
                throw new Exception("There are common strings in existing and added names");

            int checksCount = checks.Count();
            int count = Math.Min(itemsCount, checksCount);
            AddItemRange(count);
            m_count += count;
            if (itemsCount == checksCount)
            {
                AddNameRange((IEnumerable<string>)items);
                AddCheckRange(checks);
            }
            else
            {
                if (checksCount < itemsCount)
                {
                    for (int i = 0; i < checksCount; i++)
                    {
                        AddName((string)items.ElementAt(i));
                    }
                    AddCheckRange(checks);
                }
                else
                {
                    AddNameRange((IEnumerable<string>)items);
                    for (int i = 0; i < itemsCount; i++)
                    {
                        AddCheck(checks.ElementAt(i));
                    }
                }
            }
            Assign();

            IsSpecificCheckDoing = false;

            if (!isOnLoad)
            {
                var resultAdd = new List<KeyValuePair<string, fsCheckItemInfo>>();
                for (int i = 0; i < items.Count(); i++)
                {
                    resultAdd.Add(new KeyValuePair<string, fsCheckItemInfo>
                                      ((string)items.ElementAt(i),
                                       new fsCheckItemInfo(m_count + i, checks.ElementAt(i))
                                      )
                                 );
                }
                OnMultipleAddListViewItemsEvent(new fsListViewWithCheckingEventArgs(resultAdd));
            }
        }

        /// <summary>
        /// Creates an array of bool values "value" with the length equaled to the length of "names".
        /// </summary>
        /// <param name="names">A collection of strings</param>
        /// <returns>An array of true values</returns>
        private bool[] ChecksFromNames(IEnumerable<string> names, bool value)
        {
            int namesCount = names.Count();
            var checks = new bool[namesCount];
            for (int i = 0; i < namesCount; i++)
            {
                checks[i] = value;
            }
            return checks;
        }

        /// <summary> 
        /// Performs adding specific items to a fsListViewWithChecking instance.
        /// Items are ordered by the natural order of "items". All the added items have
        /// check value "value".
        /// </summary>
        /// <param name="items">A collection of specific items for ListViewItems</param>
        /// <param name="value">true or false</param>
        /// <param name="isOnLoad">The flag that shows whether adding is performing while initial 
        /// fsListViewWithChecking instance is being filled</param>
        public virtual void AddToListViewWithChecking(IEnumerable<object> items, bool value, bool isOnLoad)
        {
            AddToListViewWithChecking(items, ChecksFromNames((IEnumerable<string>)items, value), isOnLoad);
        }

        /// <summary>
        /// Performs recharging of a fsListViewWithChecking instance. New specific items with their new
        /// check values are loaded. By this an appropriate amount of ListViewItems are either added or removed
        /// (what defined by the difference between the old and new amounts of items). An item "item[i]" will
        /// have the check value "checks[i]". If the count of either "items" or  "checks" is more than the
        /// count of another collection than the first one is truncated to be of an equal size.
        /// If any two item names in "items" coincide an exception rises.
        /// </summary>
        /// <param name="items">Items to be recharged</param>
        /// <param name="checks">Check values of items to be recharged</param>
        public virtual void RechargeListViewWithChecking(IEnumerable<object> items, IEnumerable<bool> checks)
        {
            IsSpecificCheckDoing = true;

            int itemsCount = items.Count();
            for (int i = 0; i < itemsCount - 1; i++)
            {
                string elementAt_i = (string)items.ElementAt(i);
                for (int j = i + 1; j < itemsCount; j++)
                {
                    if (elementAt_i.Equals(items.ElementAt(j)))
                        throw new Exception("There are coinciding strings in recharged names");
                }
            }

            int previousCount = m_count;
            List<string> previousNames = m_names;
            List<bool> previousChecks = m_checks;

            int checksCount = checks.Count();
            int currentCount = Math.Min(itemsCount, checksCount);

            if (itemsCount == checksCount)
            {
                m_names = ((IEnumerable<string>)items).ToList();
                m_checks = checks.ToList();
            }
            else
                if (itemsCount < checksCount)
                {
                    m_names = ((IEnumerable<string>)items).ToList();
                    m_checks = checks.Take(itemsCount).ToList();
                }
                else
                {
                    m_names = ((IEnumerable<string>)items).Take(checksCount).ToList();
                    m_checks = checks.ToList();
                }

            if (previousCount < currentCount)
                AddItemRange(currentCount - previousCount);
            else
                if (previousCount > currentCount)
                    RemoveItemRange(previousCount - currentCount);
            m_count = currentCount;
            Assign();

            IsSpecificCheckDoing = false;

            // We form the list of items (with their previous and current states) having common names in previous and current charge.
            var resultCommon = new List<KeyValuePair<string, fsCheckItemInfo[]>>();
            IEnumerable<string> intersection = previousNames.Intersect(m_names);
            foreach (var name in intersection)
            {
                int previousIndex = previousNames.IndexOf(name);
                bool previousCheck = previousChecks[previousIndex];
                int currentIndex = m_names.IndexOf(name);
                bool currentCheck = m_checks[currentIndex];
                resultCommon.Add(new KeyValuePair<string, fsCheckItemInfo[]>
                                     (name,
                                      new fsCheckItemInfo[]
                                          {  
                                              new fsCheckItemInfo(previousIndex, previousCheck),
                                              new fsCheckItemInfo(currentIndex, currentCheck)
                                          }
                                     )
                                );
            }

            // We form the list of old items with their states that are to be removed by recharging operation
            var resultRemove = new List<KeyValuePair<string, fsCheckItemInfo>>();
            IEnumerable<string> previousExceptCurrent = previousNames.Except(m_names);
            foreach (var name in previousExceptCurrent)
            {
                int previousIndex = previousNames.IndexOf(name);
                bool previousCheck = previousChecks[previousIndex];
                resultRemove.Add(new KeyValuePair<string, fsCheckItemInfo>
                                     (name,
                                      new fsCheckItemInfo(previousIndex, previousCheck)
                                     )
                                );
            }

            // We form the list of new items with their states that are to be added by recharging operation
            var resultAdd = new List<KeyValuePair<string, fsCheckItemInfo>>();
            IEnumerable<string> currentExceptPrevious = m_names.Except(previousNames);
            foreach (var name in currentExceptPrevious)
            {
                int currentIndex = m_names.IndexOf(name);
                bool currentCheck = m_checks[currentIndex];
                resultAdd.Add(new KeyValuePair<string, fsCheckItemInfo>
                                  (name,
                                   new fsCheckItemInfo(currentIndex, currentCheck)
                                  )
                             );
            }

            OnRechargeListViewEvent(new fsListViewWithCheckingEventArgs(resultRemove, resultCommon, resultAdd));
        }

        /// <summary>
        /// Performs recharging of a fsListViewWithChecking instance. 
        /// Items are ordered by the natural order of "items". All the added items have
        /// check value "value".
        /// </summary>
        /// <param name="items">Items to be recharged</param>
        /// <param name="value">true or false</param>
        public virtual void RechargeListViewWithChecking(IEnumerable<object> items, bool value)
        {
            RechargeListViewWithChecking(items, ChecksFromNames((IEnumerable<string>)items, value));
        }

        #endregion

        #region Doing with fsListViewWithChecking

        #region Events for constructing items

        /// <summary>
        /// An event which is raised when an item is added to a fsListViewWithChecking instance
        /// </summary>
        public event EventHandler<EventArgs> AddListViewItemEvent;

        /// <summary>
        /// Publishing AddListViewItemEvent
        /// </summary>
        /// <param name="e">An event argument which is to be converted to an appropriate type of a class which inherits EventArgs </param>
        protected virtual void OnAddListViewItemEvent(EventArgs e)
        {
            // Make a temporary copy of the event to avoid possibility of
            // a race condition if the last subscriber unsubscribes
            // immediately after the null check and before the event is raised.
            EventHandler<EventArgs> handler = AddListViewItemEvent;

            // Event will be null if there are no subscribers
            if (handler != null)
                handler(this, (fsListViewWithCheckingEventArgs)e);
        }

        /// <summary>
        /// An event which is raised when an item is removed from a fsListViewWithChecking instance
        /// </summary>
        public event EventHandler<EventArgs> RemoveListViewItemEvent;

        /// <summary>
        /// Publishing RemoveListViewItemEvent
        /// </summary>
        /// <param name="e">An event argument which is to be converted to an appropriate type of a class which inherits EventArgs </param>
        protected virtual void OnRemoveListViewItemEvent(EventArgs e)
        {
            // Make a temporary copy of the event to avoid possibility of
            // a race condition if the last subscriber unsubscribes
            // immediately after the null check and before the event is raised.
            EventHandler<EventArgs> handler = RemoveListViewItemEvent;

            // Event will be null if there are no subscribers
            if (handler != null)
                handler(this, (fsListViewWithCheckingEventArgs)e);
        }

        #endregion

        #region Constructing items

        /// <summary> 
        /// This flag displays whether the process of specific checking ListViewItems operations
        /// is on during which the firing of the event ListViewItem_Checked is to be suppressed.
        /// </summary>
        public bool IsSpecificCheckDoing { get; private set; }

        /// <summary>
        /// True if a fsListViewWithChecking instance contains an item with the name "name"; otherwise, false.
        /// </summary>
        /// <param name="name">Any string</param>
        public bool ContainsName(string name)
        {
            return m_names.Contains(name);
        }

        /// <summary> 
        /// Inserts an item "item" into a fsListViewWithChecking instance at index "index" 
        /// with a check value "isChecked".
        /// </summary>
        /// <param name="index">Index to insert at</param>
        /// <param name="item">Item to insert</param>
        /// <param name="isChecked">Check value to set</param>
        public virtual void Insert(int index, object item, bool isChecked)
        {
            if (ContainsName((string)item))
            {
                throw new Exception("The name of an item to be inserted already exists");
            }
            IsSpecificCheckDoing = true;
            try
            {
                InsertName(index, (string)item);
            }
            catch (Exception)
            {
                IsSpecificCheckDoing = false;
                throw new Exception("An attempt to insert an item at an inapproptiate index");
            }
            InsertCheck(index, isChecked);
            AddItem();
            m_count++;
            Assign();
            OnAddListViewItemEvent(new fsListViewWithCheckingEventArgs((string)item, index, isChecked));
            IsSpecificCheckDoing = false;
        }

        /// <summary> 
        /// Adds an item "item" with a check value "isChecked" to a fsListViewWithChecking instance.
        /// </summary>
        /// <param name="item">Item to add</param>
        /// <param name="isChecked">Check value to set</param>
        public virtual void Add(object item, bool isChecked)
        {
            if (ContainsName((string)item))
            {
                throw new Exception("The name of an item to be added already exists");
            }
            IsSpecificCheckDoing = true;
            m_listView.Items.Add((string)item);
            m_listView.Items[m_count++].Checked = isChecked;
            AddName((string)item);
            AddCheck(isChecked);
            OnAddListViewItemEvent(new fsListViewWithCheckingEventArgs((string)item, m_count - 1, isChecked));
            IsSpecificCheckDoing = false;
        }

        /// <summary> 
        /// Removes an item from a fsListViewWithChecking instance at index "index".
        /// If "index" is negative or more than the items count then an exception rises.
        /// </summary>
        /// <param name="index">Index to remove at</param>
        public virtual void RemoveAt(int index)
        {
            IsSpecificCheckDoing = true;
            string item;
            bool checkValue;
            try
            {
                item = m_names[index];
                checkValue = m_checks[index];
                RemoveNameAt(index);
            }
            catch (Exception)
            {
                IsSpecificCheckDoing = false;
                throw new Exception("An attempt to remove the item at an inapproptiate index");
            }
            RemoveItem();
            RemoveCheckAt(index);
            m_count--;
            Assign();
            OnRemoveListViewItemEvent(new fsListViewWithCheckingEventArgs(item, index, checkValue));
            IsSpecificCheckDoing = false;
        }

        /// <summary>
        /// Removes an item with the name "name" from a fsListViewWithChecking instance.
        /// </summary>
        /// <param name="name">The name of an item</param>
        /// <returns>True, if an item was successfully removed; otherwise, false</returns>
        public bool RemoveByName(string name)
        {
            int index = m_names.IndexOf(name);
            if (index == -1)
                return false;
            RemoveAt(index);
            return true;
        }


        /// <summary> 
        /// Removes an item "item" from a fsListViewWithChecking instance.
        /// </summary>
        /// <param name="item">Item to remove </param>
        /// <returns>True, if an item was successfully removed; otherwise, false</returns>
        public virtual bool Remove(object item)
        {
            return RemoveByName((string)item);
        }

        /// <summary> 
        /// Drags an item on a fsListViewWithChecking instance from the place 
        /// with the index "index1" and drops it on the place with the index "index2"
        /// </summary>
        /// <param name="index1">Index to drag from </param>
        /// <param name="index2">Index to drop on </param>
        public virtual void DragFromTo(int index1, int index2)
        {
            if (index1 == index2)
                return;
            string name1 = m_names[index1];
            bool check1 = m_checks[index1];
            m_names.RemoveAt(index1);
            m_checks.RemoveAt(index1);
            if (index1 < index2)
            {
                m_names.Insert(index2 - 1, name1);
                m_checks.Insert(index2 - 1, check1);
            }
            else
            {
                m_names.Insert(index2, name1);
                m_checks.Insert(index2, check1);
            }
            Assign();
        }

        #endregion

        #region Checking items

        #region {Checking items} events

        /// <summary>
        /// An event which is raised when an item of a fsListViewWithChecking instance is checked
        /// </summary>
        public event EventHandler<EventArgs> CheckListViewItemEvent;

        /// <summary>
        /// Publishing CheckListViewItemEvent
        /// </summary>
        /// <param name="e">An event argument which is to be converted to an appropriate type of a class which inherits EventArgs </param>
        protected virtual void OnCheckListViewItemEvent(EventArgs e)
        {
            // Make a temporary copy of the event to avoid possibility of
            // a race condition if the last subscriber unsubscribes
            // immediately after the null check and before the event is raised.
            EventHandler<EventArgs> handler = CheckListViewItemEvent;

            // Event will be null if there are no subscribers
            if (handler != null)
                handler(this, (fsListViewWithCheckingEventArgs)e);
        }

        /// <summary>
        /// An event which is raised when a number of items of a fsListViewWithChecking instance is checked
        /// </summary>
        public event EventHandler<EventArgs> MultipleCheckListViewItemsEvent;

        /// <summary>
        /// Publishing CheckListViewItemEvent
        /// </summary>
        /// <param name="e">An event argument which is to be converted to an appropriate type of a class which inherits EventArgs </param>
        protected virtual void OnMultipleCheckListViewItemsEvent(EventArgs e)
        {
            // Make a temporary copy of the event to avoid possibility of
            // a race condition if the last subscriber unsubscribes
            // immediately after the null check and before the event is raised.
            EventHandler<EventArgs> handler = MultipleCheckListViewItemsEvent;

            // Event will be null if there are no subscribers
            if (handler != null)
                handler(this, (fsListViewWithCheckingEventArgs)e);
        }

        private void ListView_MouseMove(object sender, MouseEventArgs e)
        {
            if (isfsListViewWithChecking(sender))
            {
                Point targetPoint = new Point(e.X, e.Y); // Mouse pointer coordinates in (ListView)sender)
                int InsertionMarkIndex = ((ListView)sender).InsertionMark.NearestIndex(targetPoint);
                // If the mouse pointer reaches the area of an item; this area is determined 
                // by a possibility to insert Insertion Mark 
                if (InsertionMarkIndex > -1)
                {
                    Rectangle itemBounds = ((ListView)sender).GetItemRect(InsertionMarkIndex);
                    // If mouse pointer lies in the item rectangle in (ListView)sender)
                    if (itemBounds.X <= targetPoint.X && targetPoint.X <= itemBounds.Right &&
                        itemBounds.Top <= targetPoint.Y && targetPoint.Y <= itemBounds.Bottom)
                    {
                        // We force (ListView)sender to be focused for immediate selection of an item
                        // in whose rectangle the mouse pointer lies .  
                        ((ListView)sender).Focus();
                        ((ListView)sender).Items[InsertionMarkIndex].Selected = true;
                    }
                }
            }
        }

        private void ListViewItem_Checked(object sender, ItemCheckedEventArgs e)
        {
            if (e.Item.Selected)
            {
                bool checkValue = e.Item.Checked;
                m_checks[e.Item.Index] = checkValue;
                OnCheckListViewItemEvent(new fsListViewWithCheckingEventArgs(e.Item));
            }
        }

        #endregion

        #region Doing with checking

        private void SetCheckAll(bool isChecked)
        {
            for (int i = 0; i < m_count; i++)
            {
                m_checks[i] = isChecked;
            }
        }

        private void SetCheck(IEnumerable<int> indices, bool isChecked)
        {
            foreach (var index in indices)
            {
                if (0 <= index && index < m_count)
                    m_checks[index] = isChecked;
            }
        }

        /// <summary> 
        /// Returns the list of all items in fsListViewWithChecking
        /// with the check value "isChecked"
        /// </summary>
        /// <param name="isChecked">check value </param>
        protected virtual List<object> GetItemsWithCheckValue(bool isChecked)
        {
            List<object> checkValueItems = new List<object>();
            for (int i = 0; i < m_count; i++)
            {
                if (m_checks[i] == isChecked)
                    checkValueItems.Add(m_names[i]);
            }
            return checkValueItems;
        }

        private List<int> GetIndicesWithCheckValue(bool isChecked)
        {
            List<int> checkValueIndices = new List<int>();
            for (int i = 0; i < m_count; i++)
            {
                if (m_listView.Items[i].Checked == isChecked)
                    checkValueIndices.Add(i);
            }
            return checkValueIndices;
        }

        private void MakeChecking()
        {
            IsSpecificCheckDoing = true;
            OnMultipleCheckListViewItemsEvent(new fsListViewWithCheckingEventArgs(AssignChecks()));
            IsSpecificCheckDoing = false;
        }

        /// <summary> 
        /// Checks all items in  fsListViewWithChecking
        /// </summary>
        public void CheckAll()
        {
            SetCheckAll(true);
            MakeChecking();
        }

        /// <summary> 
        /// Unchecks all items in  fsListViewWithChecking
        /// </summary>
        public void UncheckAll()
        {
            SetCheckAll(false);
            MakeChecking();
        }

        /// <summary> 
        /// Checks all items in fsListViewWithChecking with appropriate indices
        /// from the collection "indices"
        /// </summary>
        /// <param name="indices">Collection of indices to check at</param>
        public void Check(IEnumerable<int> indices)
        {
            SetCheck(indices, true);
            MakeChecking();
        }

        /// <summary> 
        /// Unchecks all items in fsListViewWithChecking with appropriate indices
        /// from the collection "indices"
        /// </summary>
        /// <param name="indices">Collection of indices to check at</param>
        public void Uncheck(IEnumerable<int> indices)
        {
            SetCheck(indices, false);
            MakeChecking();
        }

        /// <summary> 
        /// Returns the list of all checked items in fsListViewWithChecking
        /// </summary>
        public virtual List<object> GetCheckedItems()
        {
            return GetItemsWithCheckValue(true);
        }

        /// <summary> 
        /// Returns the list of all unchecked items in fsListViewWithChecking
        /// </summary>
        public virtual List<object> GetUncheckedItems()
        {
            return GetItemsWithCheckValue(false);
        }

        /// <summary> 
        /// Returns the list of indices of all checked items in fsListViewWithChecking
        /// </summary>
        public List<int> GetCheckedIndices()
        {
            return GetIndicesWithCheckValue(true);
        }

        /// <summary> 
        /// Returns the list of indices of all unchecked items in fsListViewWithChecking
        /// </summary>
        public List<int> GetUncheckedIndices()
        {
            return GetIndicesWithCheckValue(false);
        }

        #endregion

        #endregion

        #region Drag-and-drop functionality

        #region {Setting as the source ListView} event

        /// <summary>
        /// An event which is raised when an item has been started to be dragged on a fsListViewWithChecking instance;
        /// setting that instance as the source ListView
        /// </summary>
        public event EventHandler<EventArgs> SetAsSourceListViewEvent;

        /// <summary>
        /// Publishing SetAsSourceListViewEvent
        /// </summary>
        /// <param name="e">An event argument which is to be converted to an appropriate type of a class which inherits EventArgs </param>
        protected virtual void OnSetAsSourceListViewEvent(EventArgs e)
        {
            // Make a temporary copy of the event to avoid possibility of
            // a race condition if the last subscriber unsubscribes
            // immediately after the null check and before the event is raised.
            EventHandler<EventArgs> handler = SetAsSourceListViewEvent;

            // Event will be null if there are no subscribers
            if (handler != null)
                handler(this, (fsListViewWithCheckingEventArgs)e);
        }

        #endregion

        #region {Setting as the target ListView} event

        /// <summary>
        /// An event which is raised when an item has been dropped on a fsListViewWithChecking instance;
        /// setting that instance as the target ListView
        /// </summary>
        public event EventHandler<EventArgs> SetAsTargetListViewEvent;

        /// <summary>
        /// Publishing SetAsTargetListViewEvent
        /// </summary>
        /// <param name="e">An event argument which is to be converted to an appropriate type of a class which inherits EventArgs </param>
        protected virtual void OnSetAsTargetListViewEvent(EventArgs e)
        {
            // Make a temporary copy of the event to avoid possibility of
            // a race condition if the last subscriber unsubscribes
            // immediately after the null check and before the event is raised.
            EventHandler<EventArgs> handler = SetAsTargetListViewEvent;

            // Event will be null if there are no subscribers
            if (handler != null)
                handler(this, (fsListViewWithCheckingEventArgs)e);
        }

        #endregion

        #region GiveFeedback action

        /// The functionality in this region is created to provide impossibility
        /// to drop dragged item from a fsListViewWithChecking instance 
        /// on a control that is not a fsListViewWithChecking instance 

        // See http://stackoverflow.com/questions/586479/is-there-a-quick-way-to-get-the-control-thats-under-the-mouse
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern IntPtr WindowFromPoint(Point pnt);

        private Control previousControl = null;

        private bool isPreviousAllowDrop = false;

        private void ListView_GiveFeedback(object sender, GiveFeedbackEventArgs e)
        {
            IntPtr hWnd = WindowFromPoint(MousePosition);
            if (hWnd != IntPtr.Zero)
            {
                Control control = Control.FromHandle(hWnd);
                if (!control.Equals(previousControl))
                {
                    if (previousControl != null)
                    {
                        previousControl.AllowDrop = isPreviousAllowDrop;
                    }
                    previousControl = control;
                    isPreviousAllowDrop = control.AllowDrop;
                    if (!isfsListViewWithChecking(control))
                        control.AllowDrop = false;
                }
            }
        }

        #endregion

        #region ItemDrag action

        private string draggedItemName;

        private bool draggedItemCheckValue;

        /// <summary>
        /// True if this fsListViewWithChecking instance is allowed to drag on; otherwise, false.
        /// </summary>
        public bool AllowedDrag { get; set; }

        // Starts the drag-and-drop operation when an item is dragged.
        private void ListView_ItemDrag(object sender, ItemDragEventArgs e)
        {
            if (AllowedDrag)
            {
                draggedItemName = ((ListViewItem)e.Item).Text;
                draggedItemCheckValue = ((ListViewItem)e.Item).Checked;
                OnSetAsSourceListViewEvent(new fsListViewWithCheckingEventArgs((ListViewItem)e.Item));
                ((ListView)sender).DoDragDrop(e.Item, DragDropEffects.Move);
            }
            else
            {
                OnSetAsSourceListViewEvent(new fsListViewWithCheckingEventArgs());
                ((ListView)sender).DoDragDrop(e.Item, DragDropEffects.None);
            }
        }

        #endregion

        #region DragEnter action

        /// <summary>
        /// True if this fsListViewWithChecking instance is allowed to drop on; otherwise, false.
        /// </summary>
        public bool AllowedDrop { get; set; }

        // Sets the target drop effect.
        private void ListView_DragEnter(object sender, DragEventArgs e)
        {
            OnSetAsTargetListViewEvent(new fsListViewWithCheckingEventArgs());
            if (AllowedDrop)
                e.Effect = e.AllowedEffect;
        }

        #endregion

        #region DragOver action

        /// <summary>
        /// The color of the insertion mark for this fsListViewWithChecking instance.
        /// </summary>
        public Color InsertionMarkColor { get; set; }

        // Moves the insertion mark as the item is dragged.
        private void ListView_DragOver(object sender, DragEventArgs e)
        {
            if (AllowedDrop)
            {
                ((ListView)sender).InsertionMark.Color = InsertionMarkColor;

                if (((ListView)sender).Items.Count == 0)
                {
                    ((ListView)sender).InsertionMark.Index = 0;
                    ((ListView)sender).InsertionMark.AppearsAfterItem = false;
                    return;
                }

                // Retrieve the client coordinates of the mouse pointer.
                Point targetPoint = ((ListView)sender).PointToClient(new Point(e.X, e.Y));

                // Retrieve the index of the item line closest to the mouse pointer.
                int InsertionMarkIndex = ((ListView)sender).InsertionMark.NearestIndex(new Point(0, targetPoint.Y));

                // Confirm that the mouse pointer is not over the dragged item.
                if (InsertionMarkIndex > -1)
                {
                    // Determine whether the mouse pointer is to the top or
                    // the bottom of the midpoint of the closest item and set
                    // the InsertionMark.AppearsAfterItem property accordingly.
                    Rectangle itemBounds = ((ListView)sender).GetItemRect(InsertionMarkIndex);
                    if (targetPoint.Y > itemBounds.Bottom + (itemBounds.Height / 2))
                        ((ListView)sender).InsertionMark.AppearsAfterItem = true;
                    else
                        ((ListView)sender).InsertionMark.AppearsAfterItem = false;
                }

                // Set the location of the insertion mark. If the mouse is
                // over the dragged item, the targetIndex value is -1 and
                // the insertion mark disappears.
                ((ListView)sender).InsertionMark.Index = InsertionMarkIndex;
            }
        }

        #endregion

        #region DragLeave action

        /// <summary>
        /// Assigning the value -1 to the index of the insertion mark for this fsListViewWithChecking instance.
        /// </summary>
        public void RemoveInsertionMark()
        {
            m_listView.InsertionMark.Index = -1;
        }

        // Removes the insertion mark when the mouse leaves the control.
        private void ListView_DragLeave(object sender, EventArgs e)
        {
            ((fsListViewWithChecking)((ListView)sender).Parent).RemoveInsertionMark();
        }

        #endregion

        #region DragDrop action

        private int targetIndex;

        // Moves the item to the location of the insertion mark.
        private void ListView_DragDrop(object sender, DragEventArgs e)
        {
            targetIndex = ((ListView)sender).InsertionMark.Index;

            // If the insertion mark is not visible, exit the method.
            if (targetIndex == -1)
                return;

            // If the insertion mark is to the bottom of the item with
            // the corresponding index, increment the target index.
            if (((ListView)sender).InsertionMark.AppearsAfterItem)
                targetIndex++;

            OnSetAsTargetListViewEvent(new fsListViewWithCheckingEventArgs(draggedItemName, targetIndex, draggedItemCheckValue));
        }

        #endregion

        #endregion

        #endregion
    }
}
