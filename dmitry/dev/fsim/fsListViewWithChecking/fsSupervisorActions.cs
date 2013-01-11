using System;
using System.Collections.Generic;

namespace ListViewWithChecking
{
    public delegate void OnAddAction(fsListViewWithChecking list, int index, object item, bool checkValue);

    public delegate void OnRemoveAction(fsListViewWithChecking list, int index, object item, bool checkValue);

    public delegate void OnCheckAction(fsListViewWithChecking list, int index, bool checkValue);

    public delegate void OnMultipleCheckAction(fsListViewWithChecking list, Dictionary<int, bool> indicesAndChecks);

    public delegate void OnDragDropAction(fsListViewWithChecking sourceList, fsListViewWithChecking targetList,
                                          int sourceIndex, int targetIndex,
                                          object item, bool checkValue);
}